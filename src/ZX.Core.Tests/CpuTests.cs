using ZX.Core.Cpu;

namespace ZX.Core.Tests;

[TestClass]
public class CPU_Ld_Tests : BaseCpuTests
{
    [TestMethod]
    public void Op_0x22_Ld()
    {
        var cpu = Test(new() { Program = { 0x22, 0x12, 0x34 }, Reg = new() { HL = 0xABCD } });

        var address = BitConverter.ToUInt16(new byte[] { 0x12, 0x34 });

        Assert.AreEqual(0xCD, cpu.Memory[address]);
        Assert.AreEqual(0xAB, cpu.Memory[address + 1]);
    }
}

[TestClass]
public class CPU_Rrc_Tests : BaseCpuTests
{
    [DataRow(unchecked((sbyte)0b_0000_0001), unchecked((sbyte)0b_1000_0000), true, false)]
    [DataRow(unchecked((sbyte)0b_0000_0001), unchecked((sbyte)0b_1000_0000), true, false)]
    [DataRow(unchecked((sbyte)0b_1000_0000), unchecked((sbyte)0b_0100_0000), false, false)]
    [DataRow(unchecked((sbyte)0b_1000_0000), unchecked((sbyte)0b_0100_0000), false, false)]
    [TestMethod]
    public void Rrc(sbyte a, sbyte expected, bool expectedC, bool expectedP)
    {
        var cpu = new CpuRuntime();

        var ua = unchecked((byte)a);

        cpu.Rrc(ref ua);

        a = unchecked((sbyte)ua);

        Assert.AreEqual(expected, a, "Result");
        Assert.AreEqual(expectedC, cpu.Flag.C, "C");
        Assert.AreEqual(expectedP, cpu.Flag.P, "P");
    }
}

[TestClass]
public class CPU_Add_Tests : BaseCpuTests
{
    [DataRow((sbyte)0b_0000_0001, (sbyte)0b_0000_0010, (sbyte)0b_0000_0011, false, false, false)]
    [DataRow((sbyte)-1, (sbyte)-2, (sbyte)-3, false, true, true)]
    [DataRow((sbyte)-127, (sbyte)-10, (sbyte)119, true, true, false)]
    [DataRow((sbyte)-127, (sbyte)-1, (sbyte)-128, false, true, true)]
    [DataRow((sbyte)-128, (sbyte)-1, (sbyte)127, true, true, false)]

    [TestMethod]
    public void Add(sbyte a, sbyte b, sbyte expected, bool expectedP, bool expectedC, bool expectedH)
    {
        var cpu = new CpuRuntime();

        var ua = unchecked((byte)a);
        var ub = unchecked((byte)b);

        cpu.Add(ref ua, ub);

        a = unchecked((sbyte)ua);

        Assert.AreEqual(expected, a, "Result");
        Assert.AreEqual(expectedP, cpu.Flag.P, "P");
        Assert.AreEqual(expectedC, cpu.Flag.C, "C");
        Assert.AreEqual(expectedH, cpu.Flag.H, "H");
    }

    [DataRow((short)0x44, (short)0x44, (short)0x88, false, false)]
    [DataRow((short)0x88, (short)0x88, (short)0x110, false, true)]
    [TestMethod]
    public void Add(short a, short b, sbyte expected, bool expectedC, bool expectedH)
    {
        var cpu = new CpuRuntime();

        var ua = unchecked((ushort)a);
        var ub = unchecked((ushort)b);

        cpu.Add(ref ua, ub);

        a = unchecked((short)ua);

        Assert.AreEqual(expected, a, "Result");
        Assert.AreEqual(expectedC, cpu.Flag.C, "C");
        Assert.AreEqual(expectedH, cpu.Flag.H, "H");
    }

    [DataRow((sbyte)0b_0000_0001, (sbyte)0b_0000_0010, true, (sbyte)0b_0000_0100, false, false, false)]
    [DataRow((sbyte)0b_0000_0001, (sbyte)0b_0000_0010, false, (sbyte)0b_0000_0011, false, false, false)]

    [DataRow((sbyte)-1, (sbyte)-2, true, (sbyte)-2, false, true, true)]
    [DataRow((sbyte)-1, (sbyte)-2, false, (sbyte)-3, false, true, true)]

    [DataRow((sbyte)-127, (sbyte)-10, true, (sbyte)120, true, true, false)]
    [DataRow((sbyte)-127, (sbyte)-10, false, (sbyte)119, true, true, false)]

    [DataRow((sbyte)-127, (sbyte)-1, true, (sbyte)-127, false, true, true)]
    [DataRow((sbyte)-127, (sbyte)-1, false, (sbyte)-128, false, true, true)]

    [DataRow((sbyte)-128, (sbyte)-1, true, (sbyte)-128, false, true, false)]
    [DataRow((sbyte)-128, (sbyte)-1, false, (sbyte)127, true, true, false)]

    [TestMethod]
    public void Adc(sbyte a, sbyte b, bool initialC, sbyte expected, bool expectedP, bool expectedC, bool expectedH)
    {
        var cpu = new CpuRuntime();

        cpu.Flag.C = initialC;

        var ua = unchecked((byte)a);
        var ub = unchecked((byte)b);

        cpu.Adc(ref ua, ub);

        a = unchecked((sbyte)ua);

        Assert.AreEqual(expected, a, "Result");
        Assert.AreEqual(expectedP, cpu.Flag.P, "P");
        Assert.AreEqual(expectedC, cpu.Flag.C, "C");
        Assert.AreEqual(expectedH, cpu.Flag.H, "H");
    }
}

[TestClass]
public class CPUTests : BaseCpuTests
{
    [TestMethod]
    public void Op_0x10_djnz_NoJump()
    {
        var cpu = Test(new() { Program = { 0x10, 0x5 }, Origin = 0x02, Reg = new() { B = 0x01 } });

        Assert.AreEqual(0x00, cpu.Reg.B);
        Assert.AreEqual(0x04, cpu.Reg.PC);
    }

    [TestMethod]
    public void Op_0x10_djnz_JumpForward()
    {
        var cpu = Test(new() { Program = { 0x10, 0x5 }, Origin = 0x02, Reg = new() { B = 0x02 } });

        Assert.AreEqual(0x01, cpu.Reg.B);
        Assert.AreEqual(0x09, cpu.Reg.PC);
    }

    [TestMethod]
    public void Op_0x10_djnz_JumpBackward()
    {
        var cpu = Test(new() { Program = { 0x10, 0xFD }, Origin = 0x02, Reg = new() { B = 0x02 } });

        Assert.AreEqual(0x01, cpu.Reg.B);
        Assert.AreEqual(0x01, cpu.Reg.PC);
    }

    [TestMethod]
    public void Op_0x18_jr_JumpForward()
    {
        var cpu = Test(new() { Program = { 0x10, 0x5 }, Origin = 0x02 });

        Assert.AreEqual(0x09, cpu.Reg.PC);
    }

    [TestMethod]
    public void Op_0x18_jr_JumpBackward()
    {
        var cpu = Test(new() { Program = { 0x10, 0xFD }, Origin = 0x02 });

        Assert.AreEqual(0x01, cpu.Reg.PC);
    }
}
