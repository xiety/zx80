using ZX.Core.Math;

namespace ZX.Core.Tests;

[TestClass]
public class BitMathTests
{
    [TestMethod]
    [DataRow(byte.MinValue)]
    [DataRow(byte.MaxValue)]
    public void TestConvertByte(byte b)
    {
        var bits = Bits.FromByte(b);
        var back = bits.ToByte();

        Assert.AreEqual(b, back);
    }

    [TestMethod]
    [DataRow(sbyte.MinValue)]
    [DataRow(sbyte.MaxValue)]
    public void TestConvertSByte(sbyte b)
    {
        var bits = Bits.FromSByte(b);
        var back = bits.ToSByte();

        Assert.AreEqual(b, back);
    }

    //[TestMethod]
    //public void TestAddSByte()
    //{
    //    for (int ia = sbyte.MinValue; ia <= sbyte.MaxValue; ++ia)
    //    {
    //        for (int ib = sbyte.MinValue; ib <= sbyte.MaxValue; ++ib)
    //        {
    //            sbyte a = unchecked((sbyte)ia);
    //            sbyte b = unchecked((sbyte)ib);

    //            var bitsA = Bits.FromSByte(a);
    //            var bitsB = Bits.FromSByte(b);

    //            var (bitsR, carry, halfCarry) = Bits.Add(bitsA, bitsB);

    //            var r = bitsR.ToSByte();

    //            var calculatedResult = a + b;
    //            var expectedResult = (sbyte)calculatedResult;
    //            var (expectedCarry, expectedHalfCarry) = Bits.CalcAddCarry(a, b, 0);

    //            Assert.AreEqual(expectedResult, r);
    //            Assert.AreEqual(expectedCarry, carry, "Carry");
    //            Assert.AreEqual(expectedHalfCarry, halfCarry, "HalfCarry");
    //        }
    //    }
    //}

    //[TestMethod]
    //public void TestSubSByte()
    //{
    //    for (int ia = 0; ia <= sbyte.MaxValue; ++ia)
    //    {
    //        for (int ib = 0; ib <= sbyte.MaxValue; ++ib)
    //        {
    //            sbyte a = unchecked((sbyte)ia);
    //            sbyte b = unchecked((sbyte)ib);

    //            var bitsA = Bits.FromSByte(a);
    //            var bitsB = Bits.FromSByte(b);

    //            var (bitsR, borrow, halfBorrow) = Bits.Sub(bitsA, bitsB, false, 3);

    //            var r = bitsR.ToSByte();

    //            var calculatedResult = a - b;
    //            var expectedResult = (sbyte)calculatedResult;
    //            var expectedBits = Bits.FromSByte(expectedResult);
    //            var (expectedBorrow, expectedHalfBorrow) = Bits.CalcSubCarry(a, b, 0);

    //            Assert.AreEqual(expectedResult, r, $"{bitsA}-{bitsB}={bitsR} expected: {expectedBits} ({a}-{b}={r} expected: {expectedResult})");
    //            Assert.AreEqual(expectedBorrow, borrow, "Borrow");
    //            Assert.AreEqual(expectedHalfBorrow, halfBorrow, "HalfBorrow");
    //        }
    //    }
    //}
}
