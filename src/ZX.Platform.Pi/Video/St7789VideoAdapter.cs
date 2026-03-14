namespace ZX.Platform.Pi;

public sealed class St7789VideoAdapter(byte[] memory, int hatWidth, int hatHeight)
    : VideoAdapterBase(memory), IDisposable
{
    private readonly St7789Display display = new((ushort)hatWidth, (ushort)hatHeight);
    private readonly int xOffset = (hatWidth - Width) / 2;
    private readonly int yOffset = (hatHeight - Height) / 2;

    public void Update()
    {
        Render();
        display.Flush();
    }

    protected override void WritePixel(int x, int y, uint color)
    {
        var dstX = x + xOffset;
        var dstY = y + yOffset;

        if ((uint)dstX < (uint)hatWidth && (uint)dstY < (uint)hatHeight)
        {
            var c = (ushort)((((color >> 16) & 0xF8) << 8) | (((color >> 8) & 0xFC) << 3) | ((color & 0xF8) >> 3));
            var dst = ((dstY * hatWidth) + dstX) * 2;
            var buf = display.Buffer;
            buf[dst] = (byte)(c >> 8);
            buf[dst + 1] = (byte)c;
        }
    }

    public void Dispose() => display.Dispose();
}
