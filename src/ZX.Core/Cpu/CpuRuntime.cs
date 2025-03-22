using System;

namespace ZX.Core.Cpu;

public partial class CpuRuntime
{
    public const int FramesPerSecond = 50;
    public const int TStatesPerSecond = 3494400;
    public const int TStatesPerFrame = TStatesPerSecond / FramesPerSecond;

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

    private bool throwOnNotImplemented = false;

    private RoutineCatalog? routineCatalog;

    public Registers Reg => reg;
    public Flags Flag => flag;
    public byte[] Memory => memory;
    public byte[] Output => output;

    public long CurrentTick { get; private set; }

    public event Action<ushort>? OnMemoryRead;
    public event Action<ushort>? OnOpcodeRead;
    public event Action<byte, byte>? OnOutput;

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

    public void RunFrame()
    {
        CurrentTick = 0;

        do
        {
            var ticks = RunStep();
            CurrentTick += ticks;
        }
        while (CurrentTick < TStatesPerFrame);

        Interrupt();
    }

    // public for testing only
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

            //TODO: ticksUntilInterruption += 17;
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

    private void UpdateFlagsXY(byte r)
    {
        flag.X = (r & 0b_0000_1000) == 0b_0000_1000;
        flag.Y = (r & 0b_0010_0000) == 0b_0010_0000;
    }

    private void UpdateFlagsXY(ushort rr)
    {
        flag.X = (rr & 0b_0000_1000_0000_0000) == 0b_0000_1000_0000_0000;
        flag.Y = (rr & 0b_0010_0000_0000_0000) == 0b_0010_0000_0000_0000;
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
        UpdateFlagsXY(r);
    }

    private void UpdateFlagSZU(ushort rr)
    {
        UpdateFlagS(rr);
        UpdateFlagZ(rr);
        UpdateFlagsXY(rr);
    }

    private void UpdateFlagSZUP(byte r)
    {
        UpdateFlagSZU(r);
        UpdateFlagP(r);
    }

    private void UpdateFlagP(byte r)
    {
        var cnt = System.Numerics.BitOperations.PopCount(r);
        flag.P = (cnt % 2) == 0;
    }

    private void LogError(string text)
    {
        Console.WriteLine(text);
        File.AppendAllText(@"zx_error.log", text + Environment.NewLine);
    }
}
