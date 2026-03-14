using ZX.Core.Spectrum;
using ZX.Platform.Pi;

var spectrum = new SpectrumRuntime("");
spectrum.InitializeBios();

using var videoAdapter = new St7789VideoAdapter(spectrum.Memory, 240, 240);
using var inputAdapter = new GpioKempstonAdapter(spectrum.Output);
using var timer = new PeriodicTimer(TimeSpan.FromMilliseconds(20));

var running = true;
Console.CancelKeyPress += (s, e) =>
{
    e.Cancel = true;
    running = false;
};

while (running && await timer.WaitForNextTickAsync())
{
    inputAdapter.Update();
    spectrum.RunFrame();
    videoAdapter.Update();
}
