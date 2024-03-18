using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ZX.Platform.Windows;

public class VideoAdapter(byte[] memory)
{
    private const int width = 256;
    private const int height = 192;

    private const ushort addressPixelsStart = 0x4000;

    private const ushort attributesOffset = (256 / 8) * 192;
    private const ushort addressVideoLength = attributesOffset + (32 * 24);
    private readonly byte[] videoMemory = new byte[addressVideoLength];
    private readonly byte[] videoMemoryLast = new byte[addressVideoLength];
    private readonly byte[] imageBytes = new byte[width * height * 3];

    public Task? Task { get; private set; }

    public Bitmap GenerateImage()
    {
        Render();
        return FromBytes(imageBytes);
    }

    public void Render()
    {
        Array.Copy(memory, addressPixelsStart, videoMemory, 0, addressVideoLength);

        var pixelIndex = 0;

        for (var y = 0; y < height; ++y)
        {
            ushort outputY = (ushort)(((y * 8) % 64) + (y / 8) % 8 + (y / 64) * 64);
            ushort attributeIndex = (ushort)(attributesOffset + (outputY / 8) * 32);

            int imageBytesIndex = (outputY * 3 * 256);

            for (var x = 0; x < width / 8; ++x)
            {
                var debug = false;// (pixelIndex + 0x4000) is 0x4001 or 0x4101 or 0x4201 or 0x4301 or 0x4401 or 0x4501 or 0x4601 or 0x4701;

                var pixelsRow = videoMemory[pixelIndex];
                var attribute = videoMemory[attributeIndex];

                bool bright = (attribute & 0b_0100_0000) == 0b_0100_0000;
                var background = ToColor(bright, (attribute & 0b_0011_1000) >> 3);
                var foreground = ToColor(bright, (attribute & 0b_0000_0111));

                for (var i = 7; i >= 0; --i)
                {
                    var pixel = (pixelsRow & (1 << i)) == (1 << i);

                    if (debug)
                        pixel = true;

                    imageBytes[imageBytesIndex] = (byte)(pixel ? foreground.B : background.B);
                    imageBytes[imageBytesIndex + 1] = (byte)(pixel ? foreground.G : background.G);
                    imageBytes[imageBytesIndex + 2] = (byte)(pixel ? foreground.R : background.R);

                    imageBytesIndex += 3;
                }

                pixelIndex++;
                attributeIndex++;
            }
        }

        Array.Copy(videoMemory, videoMemoryLast, addressVideoLength);
    }

    private static (byte R, byte G, byte B) ToColor(bool bright, int mode)
        => (bright, mode) switch
        {
            (false, 1) => (0x00, 0x00,0xD7),
            (true, 1) => (0x00, 0x00, 0xFF),
            (false, 2) => (0xD7, 0x00, 0x00),
            (true, 2) => (0xFF, 0x00, 0x00),
            (false, 3) => (0xD7, 0x00, 0xD7),
            (true, 3) => (0xFF, 0x00, 0xFF),
            (false, 4) => (0x00, 0xD7, 0x00),
            (true, 4) => (0x00, 0xFF, 0x00),
            (false, 5) => (0x00, 0xD7, 0xD7),
            (true, 5) => (0x00, 0xFF, 0xFF),
            (false, 6) => (0xD7, 0xD7, 0x00),
            (true, 6) => (0xFF, 0xFF, 0x00),
            (false, 7) => (0xD7, 0xD7, 0xD7),
            (true, 7) => (0xFF, 0xFF, 0xFF),
            _ => (0x00, 0x00, 0x00),
        };

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
}
