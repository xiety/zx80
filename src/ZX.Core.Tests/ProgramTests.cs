using ZX.Core.Spectrum;

namespace ZX.Core.Tests;

[TestClass]
public class ProgramTests
{
    private static readonly string folder = @"..\..\..\..\..\..\..\data\zx\";

    [TestMethod]
    public void TestBios()
    {
        var spectrum = new SpectrumRuntime(Path.Combine(folder, "bios"));

        spectrum.InitializeBios();

        var dump = spectrum.Memory.ToArray();

        var expected = File.ReadAllBytes(Path.Combine(folder, "dumps", "bios.dump"));

        //File.WriteAllBytes(Path.Combine(folder, "dumps", "bios.dump"), dump);

        AssertMemory(dump, expected);
    }

    [TestMethod]
    public void TestInitialize()
    {
        var spectrum = new SpectrumRuntime(Path.Combine(folder, "bios"));

        spectrum.InitializeBios();

        var dump = spectrum.Memory.ToArray();

        var expected = File.ReadAllBytes(Path.Combine(folder, "dumps", "initialize.dump"));

        //File.WriteAllBytes(Path.Combine(folder, "dumps", "initialize.dump"), dump);

        AssertMemory(dump, expected);
    }

    [TestMethod]
    public void Test5()
    {
        var spectrum = new SpectrumRuntime(Path.Combine(folder, "bios"));

        spectrum.InitializeBios();
        spectrum.LoadBin(Path.Combine(folder, "programs", "test5.bin"));

        var dump = spectrum.Memory.ToArray();

        var expected = File.ReadAllBytes(Path.Combine(folder, "dumps", "test5.dump"));

        //File.WriteAllBytes(Path.Combine(folder, "dumps", "test5.dump"), dump);

        AssertMemory(dump, expected);
    }

    private static void AssertMemory(byte[] dump, byte[] expected)
    {
        CollectionAssert.AreEqual(
                    expected[SpectrumRuntime.AddressVideoMemoryStart..(SpectrumRuntime.AddressVideoMemoryEnd + 1)],
                    dump[SpectrumRuntime.AddressVideoMemoryStart..(SpectrumRuntime.AddressVideoMemoryEnd + 1)],
                    "VideoMemory");

        CollectionAssert.AreEqual(expected, dump, "FullMemory");
    }
}
