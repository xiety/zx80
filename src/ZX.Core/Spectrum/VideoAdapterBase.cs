namespace ZX.Platform;

public abstract class VideoAdapterBase(byte[] memory)
{
    public const int Width = 256;
    public const int Height = 192;

    private static readonly uint[] palette =
    [
        0x000000, 0x0000D7, 0xD70000, 0xD700D7, 0x00D700, 0x00D7D7, 0xD7D700, 0xD7D7D7,
        0x000000, 0x0000FF, 0xFF0000, 0xFF00FF, 0x00FF00, 0x00FFFF, 0xFFFF00, 0xFFFFFF
    ];

    protected void Render()
    {
        var pixelIdx = 0;
        for (var y = 0; y < Height; ++y)
        {
            var targetY = (ushort)(((y * 8) % 64) + ((y / 8) % 8) + ((y / 64) * 64));
            var attrBase = 0x5800 + ((targetY / 8) * 32);

            for (var x = 0; x < 32; ++x)
            {
                var pixels = memory[0x4000 + pixelIdx++];
                var attr = memory[attrBase + x];

                var bg = palette[(attr & 0x40) != 0 ? 8 + ((attr >> 3) & 7) : (attr >> 3) & 7];
                var fg = palette[(attr & 0x40) != 0 ? 8 + (attr & 7) : (attr & 7)];

                for (var i = 0; i < 8; ++i)
                {
                    var color = (pixels & (0x80 >> i)) != 0 ? fg : bg;
                    WritePixel((x * 8) + i, targetY, color);
                }
            }
        }
    }

    protected abstract void WritePixel(int x, int y, uint color);
}
