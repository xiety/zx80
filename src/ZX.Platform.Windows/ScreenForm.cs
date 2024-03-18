using System.Drawing.Imaging;

using ZX.Core.Spectrum;

namespace ZX.Platform.Windows
{
    public partial class ScreenForm : Form
    {
        private readonly string folder = @"..\..\..\..\..\..\..\data\zx\";
        private readonly string folderGames;

        private SpectrumRuntime spectrum = default!;
        private VideoAdapter video = default!;
        private DebugVisualizer debug = default!;
        private KeyboardAdapter keyboard = default!;

        private CancellationTokenSource tokenSource = default!;
        private BackgroundTask backgroundTask = default!;

        private bool started;

        private bool closing;

        public ScreenForm()
        {
            folderGames = Path.Combine(folder, "games");

            InitializeComponent();
        }

        private async void ScreenForm_Load(object sender, EventArgs e)
        {
            pictureScreen.Focus();

            tokenSource = new CancellationTokenSource();

            spectrum = new SpectrumRuntime(Path.Combine(folder, "bios"));
            video = new VideoAdapter(spectrum.Memory);
            debug = new DebugVisualizer(spectrum);
            keyboard = new KeyboardAdapter(spectrum.Output);

            spectrum.InitializeBios();

            backgroundTask = new BackgroundTask(BackgroundProcessCpu, tokenSource.Token);
            await backgroundTask.RunAsync(); //await to handle exceptions
        }

        private async Task BackgroundProcessCpu(CancellationToken cancel)
        {
            if (started)
            {
                spectrum.RunFrame();
                GenerateImages();
            }
        }

        private void GenerateImages()
        {
            var image = video.GenerateImage();
            var debugImage = debug.GenerateImage();

            if (closing)
                return;

            pictureScreen.Invoke(() =>
            {
                pictureScreen.Image = image;
                pictureDebug.Image = debugImage;
            });
        }

        private void ButtonLoad_Click(object sender, EventArgs e)
        {
            var filename = comboFiles.SelectedItem as string;

            if (filename is not null)
            {
                backgroundTask.Dispatch(() => spectrum.LoadSna(Path.Combine(folderGames, filename)));

                pictureScreen.Focus();

                started = true;
            }
        }

        private void ButtonReset_Click(object sender, EventArgs e)
        {
            backgroundTask.Dispatch(spectrum.Reset);

            pictureScreen.Focus();
        }

        private void ScreenForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            tokenSource.Cancel();

            closing = true;

            //Invoke pending operations before disposing form
            Application.DoEvents();

            //Possible deadlock?
            backgroundTask.Wait();
        }

        protected override bool ProcessKeyPreview(ref Message m)
        {
            //KeyDown event doesn't work with Enter

            const int WM_KEYDOWN = 0x100;
            const int WM_KEYUP = 0x101;

            switch (m.Msg)
            {
                case WM_KEYDOWN:
                    keyboard.SetKeyDown((Keys)m.WParam);
                    break;
                case WM_KEYUP:
                    keyboard.SetKeyUp((Keys)m.WParam);
                    break;
            }

            return false;
        }

        private void ButtonScreenshot_Click(object sender, EventArgs e)
        {
            var now = DateTime.Now;

            var datename = $@"{now:yyMMdd-HHmmss}-{spectrum.Reg.PC:X4}";
            var fullpath = Path.Combine(folder, "debug_images", datename);

            if (!Directory.Exists(fullpath))
                Directory.CreateDirectory(fullpath);

            pictureScreen.Image.Save(Path.Combine(fullpath, "data.png"), ImageFormat.Png);
            spectrum.SaveSna(Path.Combine(fullpath, "data.sna"));
            debug.SaveTrace(Path.Combine(fullpath, "data.map"));
        }

        private void PictureScreen_Click(object sender, EventArgs e)
            => pictureScreen.Select();

        private void ComboFiles_DropDown(object sender, EventArgs e)
        {
            var files = Directory.GetFiles(folderGames, "*.sna", SearchOption.AllDirectories);

            comboFiles.Items.Clear();

            foreach (var file in files)
            {
                comboFiles.Items.Add(Path.GetRelativePath(folderGames, file));
            }
        }
    }
}
