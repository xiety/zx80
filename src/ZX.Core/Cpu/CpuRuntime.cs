using System;

namespace ZX.Core.Cpu;

public partial class CpuRuntime
{
    const uint totalMemory = 65536;
    const uint totalOutput = 65536;

    const ushort InterruptAddress = 0x0038;

    readonly byte[] memory;
    readonly byte[] output = new byte[totalOutput];

    private readonly Stack<ushort> callStackDebug = new();

    private readonly Registers reg;
    private readonly Flags flag;

    private int currentDepthDebug;

    private bool isHalt = false;

    private RoutineCatalog? routineCatalog;

    public Registers Reg => reg;
    public Flags Flag => flag;
    public byte[] Memory => memory;
    public byte[] Output => output;

    public event Action<ushort>? OnMemoryRead;
    public event Action<ushort>? OnOpcodeRead;

    public CpuRuntime()
        : this(new(), new byte[totalMemory])
    {
    }

    private CpuRuntime(Registers reg, byte[] memory)
    {
        this.reg = reg;
        this.memory = memory;
        flag = new(reg);
        Init();
    }

    public CpuRuntime Clone()
        => new(reg, memory);

    private void Init()
    {
        flag.Z = true;
        flag.H = true;
        flag.P = true;
    }

    public int RunStep()
    {
        if (isHalt)
        {
            if (!reg.IFF1)
                throw new Exception("Halt with no interrupt");

            reg.PC--;
        }

        try
        {
            var pc = reg.PC;

            var (ticks, desc) = Interpret();

            //Console.WriteLine($"{pc:X4} {desc} -> HL: {reg.HL:X4} (HL): {GetByteMemory(reg.HL):X2} Z: {flag.Z}");

            return ticks;
        }
        catch (Exception ex)
        {
            LogError(ex.ToString());
            throw;
        }
    }

    public void Interrupt()
    {
        if (!reg.IFF1)
            return;

        isHalt = false;

        Di(); //disable

        if (reg.InterruptMode == 1)
        {
            CallCommon(InterruptAddress);

            //TODO: ticksUntilInterruption += 13;
        }
        else if (reg.InterruptMode == 2)
        {
            var offset = GetUShortMemory((ushort)(reg.I << 8));
            CallCommon(offset);

            //TODO: ticksUntilInterruption += 19;
        }
    }

    public void Clear()
        => Array.Fill(memory, (byte)0);

    public void Load(uint address, ReadOnlySpan<byte> program)
        => program.CopyTo(memory.AsSpan((int)address));

    public void LoadRoutineCatalog(RoutineCatalog routineCatalog)
        => this.routineCatalog = routineCatalog;

    public void InitRegisters(Registers reg)
        => this.reg.Initialize(reg);

    public void SetPC(ushort pc)
        => reg.PC = pc;

    public void CleanStack()
        => currentDepthDebug = 0;

    private void UpdateFlagUndocumented(byte r)
    {
        flag.F3 = (r & 0b_0000_1000) == 0b_0000_1000;
        flag.F5 = (r & 0b_0010_0000) == 0b_0010_0000;
    }

    private void UpdateFlagZ(byte r)
        => flag.Z = (r == 0);

    private void UpdateFlagZ(ushort rr)
        => flag.Z = (rr == 0);

    private void UpdateFlagS(byte r)
        => flag.S = (r & 0b_1000_0000) == 0b_1000_0000;

    private void UpdateFlagS(ushort rr)
        => flag.S = (rr & 0b_1000_0000_0000_0000) == 0b_1000_0000_0000_0000;

    private void UpdateFlagSZU(byte r)
    {
        UpdateFlagS(r);
        UpdateFlagZ(r);
        UpdateFlagUndocumented(r);
    }

    /*
    private bool UpdateFlagPWithParity(byte r)
    {
        ulong x1 = 0x0101010101010101;
        ulong x2 = 0x8040201008040201;
        return ((((r * x1) & x2) % (ulong)0x1FF) & 1) == 0;
    }
    */

    private void UpdateFlagPWithParity(byte r)
    {
        byte s = 1;
        int cnt = 0;

        for (var i = 0; i < 8; ++i)
        {
            if ((byte)(r & s) == s)
                cnt++;

            s <<= 1;
        }

        flag.P = (cnt % 2) == 0;
    }

    private void LogError(string text)
    {
        File.AppendAllText(@"zx_error.log", text + Environment.NewLine);
    }
}
