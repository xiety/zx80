using System.Runtime.InteropServices;

namespace ZX.Core.Cpu;

public partial class CpuRuntime
{
    private ushort LittleEndian(byte a, byte b)
        => (ushort)(a + 256 * b);

    private ushort ReadUShortOpcode()
    {
        ushort ret = LittleEndian(memory[reg.PC], memory[reg.PC + 1]);

        OnOpcodeRead?.Invoke(reg.PC);
        OnOpcodeRead?.Invoke((ushort)(reg.PC + 1));

        reg.PC += 2;

        return ret;
    }

    private byte ReadByteOpcode()
    {
        byte ret = memory[reg.PC];

        OnOpcodeRead?.Invoke(reg.PC);

        reg.PC += 1;

        return ret;
    }

    private ushort GetUShortMemory(ushort address)
        => GetUShortMemoryRef(address);

    private byte GetByteMemory(ushort address)
        => GetByteMemoryRef(address);

    private byte GetByteMemory(ushort address, byte offset)
        => GetByteMemoryRef(address, offset);

    private ref ushort GetUShortMemoryRef(ushort address)
    {
        if (address == 0xFFFF)
            throw new ArgumentOutOfRangeException(nameof(address));

        var span = memory.AsSpan(address, 2);
        ref ushort result = ref MemoryMarshal.AsRef<ushort>(span);

        OnMemoryRead?.Invoke(address);
        OnMemoryRead?.Invoke((ushort)(address + 1));

        return ref result;
    }

    private ref byte GetByteMemoryRef(ushort address)
    {
        ref byte result = ref memory[address];

        OnMemoryRead?.Invoke(address);

        return ref result;
    }

    private ref byte GetByteMemoryRef(ushort address, byte offset)
        => ref GetByteMemoryRef((ushort)(address + unchecked((sbyte)offset)));
}
