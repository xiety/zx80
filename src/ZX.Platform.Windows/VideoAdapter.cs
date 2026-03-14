namespace ZX.Platform.Windows;

public sealed class VideoAdapter(byte[] memory) : VideoAdapterBase(memory)
{
    private readonly byte[] buffer = new byte[Width * Height * 3];

    public byte[] GetBuffer()
    {
        Render();
        return buffer;
    }

    protected override void WritePixel(int x, int y, uint color)
    {
        var i = ((y * Width) + x) * 3;
        buffer[i] = (byte)color;
        buffer[i + 1] = (byte)(color >> 8);
        buffer[i + 2] = (byte)(color >> 16);
    }
}
