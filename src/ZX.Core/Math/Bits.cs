using System.Diagnostics;

namespace ZX.Core.Math;

[DebuggerDisplay("{DebuggerDisplay,nq}")]
public class Bits(int length)
{
    private readonly bool[] data = new bool[length];

    public int Length => data.Length;

    private string DebuggerDisplay
        => String.Join("", data.Reverse().Select(a => a ? "1" : "0")) + $" ({ToSByte()} {ToSByte():x})";

    public bool this[int n]
    {
        get => data[n];
        set => data[n] = value;
    }

    public override string ToString()
        => String.Join("", data.Reverse().Select(a => a ? "1" : "0"));

    public byte ToByte()
    {
        byte b = 0;

        for (var i = 0; i < 8; ++i)
            b |= (byte)((this[i] ? 1 : 0) << i);

        return b;
    }

    public sbyte ToSByte()
        => unchecked((sbyte)ToByte());

    public ushort ToUShort()
    {
        var len = 16;
        ushort b = 0;

        for (var i = 0; i < len; ++i)
            b |= (ushort)((this[i] ? 1 : 0) << i);

        return b;
    }

    public static Bits FromByte(byte b)
    {
        var len = 8;
        var bits = new Bits(len);
        var c = b;

        for (var i = 0; i < len; ++i)
        {
            bits[i] = (c & 1) == 1;
            c >>= 1;
        }

        return bits;
    }

    public static Bits FromSByte(sbyte b)
        => FromByte((byte)b);

    public static Bits FromUShort(ushort b)
    {
        var len = 16;
        var bits = new Bits(len);
        var c = b;

        for (var i = 0; i < len; ++i)
        {
            bits[i] = (c & 1) == 1;
            c >>= 1;
        }

        return bits;
    }

    public static (Bits Result, bool Carry, bool HalfCarry) Add(Bits a, Bits b)
    {
        var result = new Bits(a.Length);
        var carry = false;
        var halfCarry = false;

        for (var i = 0; i < a.Length; ++i)
        {
            (carry, result[i]) = (carry, a[i], b[i]) switch
            {
                (false, false, false) => (false, false),
                (false, false, true) or (false, true, false) or (true, false, false) => (false, true),
                (false, true, true) or (true, false, true) or (true, true, false) => (true, false),
                (true, true, true) => (true, true),
            };

            if (i == 3)
                halfCarry = carry;
        }

        return (result, carry, halfCarry);
    }

    public static (ushort Result, bool Borrow, bool HalfBorrow) Sub(ushort a, ushort b, bool inputBorrow, int halfIndex)
    {
        var result = Sub(Bits.FromUShort(a), Bits.FromUShort(b), inputBorrow, halfIndex);
        return (result.Result.ToUShort(), result.Borrow, result.HalfBorrow);
    }

    public static (Bits Result, bool Borrow, bool HalfBorrow) Sub(Bits a, Bits b, bool inputBorrow, int halfIndex)
    {
        var result = new Bits(a.Length);
        var borrow = inputBorrow;
        var halfBorrow = false;

        for (var i = 0; i < a.Length; ++i)
        {
            (borrow, result[i]) = (borrow, a[i], b[i]) switch
            {
                (false, false, false) => (false, false),
                (false, false, true) => (true, true),
                (false, true, false) => (false, true),
                (false, true, true) => (false, false),
                (true, false, false) => (true, true),
                (true, false, true) => (true, false),
                (true, true, false) => (false, false),
                (true, true, true) => (true, true),
            };

            if (i == halfIndex)
                halfBorrow = borrow;
        }

        return (result, borrow, halfBorrow);
    }

    //public static (bool Carry, bool HalfCarry) CalcAddCarry(sbyte a, sbyte b, int carryBit)
    //{
    //    byte ua = unchecked((byte)a);
    //    byte ub = unchecked((byte)b);

    //    var carry = (ua + ub + carryBit) > 255;
    //    var halfCarry = ((ua % 16) + (ub % 16) + carryBit) > 15;

    //    return (carry, halfCarry);
    //}

    public static (bool Carry, bool HalfCarry) CalcAddCarry(byte a, byte b, byte carryIn)
    {
        int fullSum = a + b + carryIn;
        int lowNibbleSum = (a & 0x0F) + (b & 0x0F) + carryIn;
        return (fullSum > 0xFF, lowNibbleSum > 0x0F);
    }

    public static (bool Carry, bool HalfCarry) CalcAddCarry(ushort a, ushort b, byte carryIn)
    {
        var fullSum = a + b + carryIn;
        var lowNibbleSum = (a & 0xFF) + (b & 0xFF) + carryIn;
        return (fullSum > 0xFFFF, lowNibbleSum > 0xFF);
    }

    public static (bool Borrow, bool HalfBorrow) CalcSubCarry(byte a, byte b, byte carryIn)
    {
        var fullSub = a - b - carryIn;
        int lowNibbleSub = (a & 0x0F) - (b & 0x0F) - carryIn;
        return (fullSub < 0, lowNibbleSub < 0);
    }

    public static byte Low(ushort u)
        => (byte)(u & 0xFF);

    public static byte High(ushort u)
        => (byte)(u >> 8);
}
