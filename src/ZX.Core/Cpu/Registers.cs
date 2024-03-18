using System.Runtime.InteropServices;

namespace ZX.Core.Cpu;

[StructLayout(LayoutKind.Explicit)]
public class Registers
{
    [FieldOffset(0)]
    public ushort AF;
    [FieldOffset(1)]
    public byte A;      //Accumulator
    [FieldOffset(0)]
    public byte F;

    [FieldOffset(2)]
    public ushort BC;
    [FieldOffset(3)]
    public byte B;
    [FieldOffset(2)]
    public byte C;

    [FieldOffset(4)]
    public ushort DE;
    [FieldOffset(5)]
    public byte D;
    [FieldOffset(4)]
    public byte E;

    [FieldOffset(6)]
    public ushort HL;
    [FieldOffset(7)]
    public byte H;
    [FieldOffset(6)]
    public byte L;

    [FieldOffset(8)]
    public ushort AFs;

    [FieldOffset(10)]
    public ushort BCs;

    [FieldOffset(12)]
    public ushort DEs;

    [FieldOffset(14)]
    public ushort HLs;

    [FieldOffset(16)]
    public ushort IR;
    [FieldOffset(17)]
    public byte I;
    [FieldOffset(16)]
    public byte R; //DRAM refresh counter, 8 bits (msb does not count)

    [FieldOffset(18)]
    public ushort IX;
    [FieldOffset(19)]
    public byte IXH;
    [FieldOffset(18)]
    public byte IXL;

    [FieldOffset(20)]
    public ushort IY;
    [FieldOffset(21)]
    public byte IYH;
    [FieldOffset(20)]
    public byte IYL;

    [FieldOffset(22)]
    public ushort PC;   //Current operation

    [FieldOffset(24)]
    public ushort SP;   //Stack pointer

    [FieldOffset(26)]
    public byte InterruptMode;

    [FieldOffset(27)]
    public bool IFF1;

    [FieldOffset(28)]
    public bool IFF2;

    public void Initialize(Registers reg)
    {
        AF = reg.AF;
        BC = reg.BC;
        DE = reg.DE;
        HL = reg.HL;
        AFs = reg.AFs;
        BCs = reg.BCs;
        DEs = reg.DEs;
        HLs = reg.HLs;
        IR = reg.IR;
        IX = reg.IX;
        IY = reg.IY;
        PC = reg.PC;
        SP = reg.SP;

        InterruptMode = reg.InterruptMode;
        IFF1 = reg.IFF1;
        IFF2 = reg.IFF2;
    }
}
