using System.Globalization;

using CsvHelper;

using ZX.CodeGeneration;

var settings = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
{
    HasHeaderRecord = true,
    Delimiter = ";",
};

using var reader = new StreamReader(@"Data\operations.csv");
using var csv = new CsvReader(reader, settings);

var items = csv.GetRecords<Item>().ToArray();

var gen = new OpcodesGenerator(
    items,
    @"..\..\..\..\ZX.Core\Generated\CpuRuntime.Interpret{0}.cs"
);

string[] direct = ["", "CB", "DD", "ED", "FD"];
string[] reverse = ["DDCB", "FDCB"];

foreach (var name in direct)
    gen.Run(name);

foreach (var name in reverse)
    gen.Run(name, true);
