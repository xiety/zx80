using System.Drawing.Imaging;
using System.Runtime.InteropServices;

using ZX.Core.Cpu;
using ZX.Core.Spectrum;

namespace ZX.Platform.Windows;

public class DebugVisualizer
{
    private const int width = 256;
    private const int height = 256;
    private readonly byte[] imageBytes = new byte[width * height * 3];

    private readonly HashSet<ushort> opcodeSet = [];
    private readonly HashSet<ushort> readSet = [];

    private readonly SpectrumRuntime spectrum;
    private readonly byte[] memory;
    private readonly Registers reg;

    public DebugVisualizer(SpectrumRuntime spectrum)
    {
        this.spectrum = spectrum;

        memory = spectrum.Memory;
        reg = spectrum.Reg;

        spectrum.OnMemoryRead += HandleMemoryRead;
        spectrum.OnOpcodeRead += HandleOpcodeRead;
        spectrum.OnReset += HandleReset;
    }

    public Bitmap GenerateImage()
    {
        Render();
        return FromBytes(imageBytes);
    }

    private bool Render()
    {
        ushort memoryIndex = 0;
        var imageIndex = 0;

        for (var y = 0; y < 256; ++y)
        {
            for (var x = 0; x < 256; ++x)
            {
                var p = memory[memoryIndex];

                if (opcodeSet.Contains(memoryIndex))
                {
                    imageBytes[imageIndex] = 0;
                    imageBytes[imageIndex + 1] = p;
                    imageBytes[imageIndex + 2] = 0;
                }
                else if (readSet.Contains(memoryIndex))
                {
                    imageBytes[imageIndex] = p;
                    imageBytes[imageIndex + 1] = 0;
                    imageBytes[imageIndex + 2] = 0;
                }
                else
                {
                    imageBytes[imageIndex] = p;
                    imageBytes[imageIndex + 1] = p;
                    imageBytes[imageIndex + 2] = p;
                }

                memoryIndex++;
                imageIndex += 3;
            }
        }

        var pos = reg.PC * 3;

        imageBytes[pos] = 0;
        imageBytes[pos + 1] = 0;
        imageBytes[pos + 2] = 255;

        return true;
    }

    private Bitmap FromBytes(byte[] data)
    {
        var stride = (width % 4 == 0) ? width : width + 4 - width % 4;

        var formatOutput = PixelFormat.Format24bppRgb;
        var rect = new Rectangle(0, 0, width, height);
        var bmp = new Bitmap(stride, height, formatOutput);

        var bmpData = bmp.LockBits(rect, ImageLockMode.ReadOnly, formatOutput);
        Marshal.Copy(data, 0, bmpData.Scan0, data.Length);
        bmp.UnlockBits(bmpData);

        return bmp;
    }

    private void HandleMemoryRead(ushort address)
        => readSet.Add(address);

    private void HandleOpcodeRead(ushort address)
        => opcodeSet.Add(address);

    private void HandleReset()
    {
        opcodeSet.Clear();
        readSet.Clear();
    }

    public void SaveTrace(string filename)
    {
        var combined = Enumerable.Concat(opcodeSet.Select(a => (a, $"c {a}")), readSet.Select(a => (a, $"b {a}")))
            .OrderBy(a => a.Item1);

        File.WriteAllLines(filename, combined.Select(a => a.Item2));
    }
}
