namespace ZX.Core.Cpu;

public class Flags(Registers reg)
{
    /// <summary>
    /// Sign
    /// </summary>
    public bool S
    {
        get => (reg.F & 0b_1000_0000) == 0b_1000_0000;
        set => reg.F = value ? (byte)(reg.F | 0b_1000_0000) : (byte)(reg.F & ~0b_1000_0000);
    }

    /// <summary>
    /// Zero
    /// </summary>
    public bool Z
    {
        get => (reg.F & 0b_0100_0000) == 0b_0100_0000;
        set => reg.F = value ? (byte)(reg.F | 0b_0100_0000) : (byte)(reg.F & ~0b_0100_0000);
    }

    public bool Y //undocumented flag
    {
        get => (reg.F & 0b_0010_0000) == 0b_0010_0000;
        set => reg.F = value ? (byte)(reg.F | 0b_0010_0000) : (byte)(reg.F & ~0b_0010_0000);
    }

    /// <summary>
    /// Half curry
    /// </summary>
    public bool H
    {
        get => (reg.F & 0b_0001_0000) == 0b_0001_0000;
        set => reg.F = value ? (byte)(reg.F | 0b_0001_0000) : (byte)(reg.F & ~0b_0001_0000);
    }

    public bool X //undocumented flag
    {
        get => (reg.F & 0b_0000_1000) == 0b_0000_1000;
        set => reg.F = value ? (byte)(reg.F | 0b_0000_1000) : (byte)(reg.F & ~0b_0000_1000);
    }

    /// <summary>
    /// Parity or Overflow
    /// </summary>
    public bool P
    {
        get => (reg.F & 0b_0000_0100) == 0b_0000_0100;
        set => reg.F = value ? (byte)(reg.F | 0b_0000_0100) : (byte)(reg.F & ~0b_0000_0100);
    }

    /// <summary>
    /// Subtract
    /// </summary>
    public bool N
    {
        get => (reg.F & 0b_0000_0010) == 0b_0000_0010;
        set => reg.F = value ? (byte)(reg.F | 0b_0000_0010) : (byte)(reg.F & ~0b_0000_0010);
    }

    /// <summary>
    /// Carry
    /// </summary>
    public bool C
    {
        get => (reg.F & 0b_0000_0001) == 0b_0000_0001;
        set => reg.F = value ? (byte)(reg.F | 0b_0000_0001) : (byte)(reg.F & ~0b_0000_0001);
    }
}
