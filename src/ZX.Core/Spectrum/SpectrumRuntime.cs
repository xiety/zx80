using System.Diagnostics;

using ZX.Core.Cpu;
using ZX.Core.FileFormats;

namespace ZX.Core.Spectrum;

public class SpectrumRuntime(string biosFolder)
{
    public const ushort AddressProgram = 0x5CCB;
    public const ushort AddressVideoMemoryStart = 0x4000;
    public const ushort AddressVideoMemoryEnd = 0x5AFF;

    private readonly CpuRuntime cpu = new();

    public CpuRuntime Cpu => cpu;
    public byte[] Memory => cpu.Memory;
    public byte[] Output => cpu.Output;
    public Registers Reg => cpu.Reg;

    private const int TicksPerFrame = 69888;

    public event Action? OnStep;

    public event Action<ushort>? OnMemoryRead
    {
        add { cpu.OnMemoryRead += value; }
        remove { cpu.OnMemoryRead -= value; }
    }

    public event Action<ushort>? OnOpcodeRead
    {
        add { cpu.OnOpcodeRead += value; }
        remove { cpu.OnOpcodeRead -= value; }
    }

    public event Action? OnReset;

    public void InitializeBios()
    {
        cpu.Clear();
        cpu.Load(0x0000, File.ReadAllBytes(Path.Combine(biosFolder, "48.rom")));
        cpu.LoadRoutineCatalog(new RoutineCatalog(Path.Combine(biosFolder, "48.rom.description")));
        cpu.SetPC(0x0000);
    }

    public void LoadBin(string filename)
    {
        var bytes = File.ReadAllBytes(filename);
        var addressEnd = (ushort)(AddressProgram + bytes.Length - 1);

        cpu.Load(AddressProgram, bytes);

        OnReset?.Invoke();
    }

    public void Load(string filename)
    {
        var ext = Path.GetExtension(filename).ToLower();

        switch (ext)
        {
            case ".sna":
                LoadSna(filename);
                break;
            case ".tap":
                LoadTap(filename);
                break;
            default:
                throw new NotSupportedException($"Unknown extension: {ext}");
        }
    }

    private void LoadSna(string filename)
    {
        var (reg, data) = new SnaFileFormat().Load(filename);
        var start = (ushort)(65536 - data.Length);

        cpu.InitRegisters(reg);
        cpu.Load(start, data);
        cpu.Retn(); //restore PC from stack

        OnReset?.Invoke();
    }

    private void LoadTap(string filename)
    {
        var (address, data) = new TapFileFormat().Load(filename);

        //cpu.InitRegisters(new Registers() { AF = 0xFFFF, SP = 0xFFFF, PC = cpu.Reg.PC });
        cpu.Load(16384, data.AsSpan(16384));

        OnReset?.Invoke();
    }

    public void Reset()
    {
        InitializeBios();

        OnReset?.Invoke();
    }

    public void SaveSna(string filename)
    {
        var cpuCopy = cpu.Clone();

        //modify memory to save file
        cpuCopy.Push(cpu.Reg.PC);

        var format = new SnaFileFormat();
        format.Save(filename, cpu.Reg, cpu.Memory);
    }

    public void RunFrame()
    {
        var interruptStartTime = Stopwatch.GetTimestamp();

        cpu.Interrupt();

        var currentTick = 0;

        do
        {
            currentTick += cpu.RunStep();

            OnStep?.Invoke();
        }
        while (currentTick < TicksPerFrame);

        WaitRealTime(interruptStartTime);
    }

    private void WaitRealTime(long interruptStartTime)
    {
        var interruptDuration = (Stopwatch.Frequency / 100);
        var required = interruptStartTime + interruptDuration;

        do
        {
            Thread.Sleep(1);
        }
        while (Stopwatch.GetTimestamp() < required);
    }
}
