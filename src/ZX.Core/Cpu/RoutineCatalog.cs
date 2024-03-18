using System.Globalization;

namespace ZX.Core.Cpu;

public class RoutineCatalog
{
    private readonly List<Item> items;

    public RoutineCatalog(string filename)
    {
        var lines = File.ReadAllLines(filename);

        var numberStyles = NumberStyles.HexNumber | NumberStyles.AllowTrailingWhite;

        items = lines
            .Chunk(2)
            .Select(a => new Item(ushort.Parse(a[0], numberStyles), a[1]))
            .Reverse()
            .ToList();
    }

    public string? GetDescription(ushort address)
    {
        return items
            .Where(a => a.Address <= address)
            .Select(a => a.Description)
            .FirstOrDefault();
    }

    record Item(ushort Address, string Description);
}
