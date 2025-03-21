using ZX.Core.Cpu;

namespace ZX.Core.Tests;

public abstract class BaseCpuTests
{
    protected CpuRuntime Test(Param param)
    {
        var cpu = new CpuRuntime();

        cpu.Load(param.Origin, [.. param.Program]);

        if (param.Reg is Registers r)
            cpu.InitRegisters(r);

        cpu.SetPC(param.Origin);

        cpu.RunStep();

        return cpu;
    }
}

public class Param
{
    public List<byte> Program { get; } = [];

    public ushort Origin { get; set; } = 0;

    public Registers? Reg { get; set; }
}
