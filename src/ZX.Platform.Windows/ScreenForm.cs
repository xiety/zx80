using System.Diagnostics;
using System.Drawing.Imaging;

using ZX.Core.Cpu;
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
        private SoundAdapter sound = default!;

        long frameStartTime = Stopwatch.GetTimestamp();

        private CancellationTokenSource tokenSource = default!;
        private BackgroundTask backgroundTask = default!;

        private bool speedLimit = true;
        private bool started;
        private bool closing;

        public ScreenForm()
        {
            this.DoubleBuffered = true;

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
            sound = new SoundAdapter(spectrum);

            spectrum.OnOutput += sound.HandleOutput;

            spectrum.Reset();

            await RunBackgroundTask();
        }

        private async Task RunBackgroundTask()
        {
            try
            {
                backgroundTask = new BackgroundTask(BackgroundProcessCpu, tokenSource.Token);
                await backgroundTask.RunAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        private async Task BackgroundProcessCpu(CancellationToken cancel)
        {
            if (started)
            {
                spectrum.RunFrame();
                sound.RunFrame();
            }

            GenerateImages();

            if (speedLimit)
                WaitRealTime();
        }

        private void WaitRealTime()
        {
            var frameDuration = (long)(Stopwatch.Frequency / (double)CpuRuntime.FramesPerSecond);
            var required = frameStartTime + frameDuration;

            do
            {
                Thread.Yield();
                frameStartTime = Stopwatch.GetTimestamp();
            }
            while (frameStartTime < required);
        }

        private void GenerateImages()
        {
            var image = video.GenerateImage();
            var debugImage = debug.GenerateImage();

            if (closing)
                return;

            pictureScreen.Invoke(() =>
            {
                textBoxFps.Text = $"{spectrum.Fps}";
                pictureScreen.Image = image;
                pictureDebug.Image = debugImage;
            });
        }

        private void ButtonLoad_Click(object sender, EventArgs e)
        {
            var filename = comboFiles.SelectedItem as string;

            if (filename is not null)
            {
                backgroundTask.Dispatch(() => spectrum.Load(Path.Combine(folderGames, filename)));
                pictureScreen.Focus();

                started = true;
            }
        }

        private void ButtonReset_Click(object sender, EventArgs e)
        {
            backgroundTask.Dispatch(spectrum.Reset);

            pictureScreen.Focus();

            started = true;
        }

        private void ScreenForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            tokenSource.Cancel();

            closing = true;

            //Invoke pending operations before disposing form
            Application.DoEvents();

            backgroundTask?.Wait();
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

            pictureScreen.Image?.Save(Path.Combine(fullpath, "data.png"), ImageFormat.Png);
            pictureDebug.Image?.Save(Path.Combine(fullpath, "debug.png"), ImageFormat.Png);
            spectrum.SaveSna(Path.Combine(fullpath, "data.sna"));
            debug.SaveTrace(Path.Combine(fullpath, "data.map"));
        }

        private void PictureScreen_Click(object sender, EventArgs e)
            => pictureScreen.Select();

        private void ComboFiles_DropDown(object sender, EventArgs e)
        {
            var files = Directory.GetFiles(folderGames, "*.*", SearchOption.AllDirectories);

            comboFiles.Items.Clear();

            foreach (var file in files)
            {
                comboFiles.Items.Add(Path.GetRelativePath(folderGames, file));
            }
        }
    }
}
