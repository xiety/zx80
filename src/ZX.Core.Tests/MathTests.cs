namespace ZX.Core.Tests;

[TestClass]
public class MathTests
{
    [TestMethod]
    public void InvertBits1()
    {
        byte a = 0b_0101_1010;
        byte actual = (byte)(~a);
        byte expected = 0b_1010_0101;

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ShiftLeftSByte1()
    {
        sbyte a = unchecked((sbyte)0b_1001_0001);
        sbyte actual = (sbyte)(a << 1);
        sbyte expected = unchecked((sbyte)0b_0010_0010);

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ShiftLeftSByte2()
    {
        sbyte a = unchecked((sbyte)0b_0101_0001);
        sbyte actual = (sbyte)(a << 1);
        sbyte expected = unchecked((sbyte)0b_1010_0010);

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void AddBytesOverflowUp1()
    {
        byte a = 255;
        byte b = 1;

        byte c = (byte)(a + b);

        Assert.AreEqual(0, c);
    }

    [TestMethod]
    public void AddBytesOverflowUp2()
    {
        byte a = 255;
        byte b = 2;

        byte c = (byte)(a + b);

        Assert.AreEqual(1, c);
    }

    [TestMethod]
    public void CastInt256()
    {
        int a = 256;
        byte b = (byte)a;

        Assert.AreEqual(0, b);
    }

    [TestMethod]
    public void CastInt257()
    {
        int a = 257;
        byte b = (byte)a;

        Assert.AreEqual(1, b);
    }

    [TestMethod]
    public void CastInt513()
    {
        int a = 513;
        byte b = (byte)a;

        Assert.AreEqual(1, b);
    }
}
