//This file is autogenerated, do not change it
#pragma warning disable CS0168 // Variable is declared but never used
#pragma warning disable CS0162 // Unreachable code detected
namespace ZX.Core.Cpu;

public partial class CpuRuntime
{
    private (int, string) InterpretFDCB()
    {
        bool isFast; //cannot be declared inside multiple switch cases
        var arg = ReadByteOpcode();
        var op = ReadByteOpcode();

        switch (op)
        {
            case 0x00:
                Rlc(GetByteMemory(reg.IY, arg), ref reg.B);
                return (23, "rlc (iy+d),b");
            case 0x01:
                Rlc(GetByteMemory(reg.IY, arg), ref reg.C);
                return (23, "rlc (iy+d),c");
            case 0x02:
                Rlc(GetByteMemory(reg.IY, arg), ref reg.D);
                return (23, "rlc (iy+d),d");
            case 0x03:
                Rlc(GetByteMemory(reg.IY, arg), ref reg.E);
                return (23, "rlc (iy+d),e");
            case 0x04:
                Rlc(GetByteMemory(reg.IY, arg), ref reg.H);
                return (23, "rlc (iy+d),h");
            case 0x05:
                Rlc(GetByteMemory(reg.IY, arg), ref reg.L);
                return (23, "rlc (iy+d),l");
            case 0x06:
                Rlc(ref GetByteMemoryRef(reg.IY, arg));
                return (23, "rlc (iy+d)");
            case 0x07:
                Rlc(GetByteMemory(reg.IY, arg), ref reg.A);
                return (23, "rlc (iy+d),a");
            case 0x08:
                Rrc(GetByteMemory(reg.IY, arg), ref reg.B);
                return (23, "rrc (iy+d),b");
            case 0x09:
                Rrc(GetByteMemory(reg.IY, arg), ref reg.C);
                return (23, "rrc (iy+d),c");
            case 0x0A:
                Rrc(GetByteMemory(reg.IY, arg), ref reg.D);
                return (23, "rrc (iy+d),d");
            case 0x0B:
                Rrc(GetByteMemory(reg.IY, arg), ref reg.E);
                return (23, "rrc (iy+d),e");
            case 0x0C:
                Rrc(GetByteMemory(reg.IY, arg), ref reg.H);
                return (23, "rrc (iy+d),h");
            case 0x0D:
                Rrc(GetByteMemory(reg.IY, arg), ref reg.L);
                return (23, "rrc (iy+d),l");
            case 0x0E:
                Rrc(ref GetByteMemoryRef(reg.IY, arg));
                return (23, "rrc (iy+d)");
            case 0x0F:
                Rrc(GetByteMemory(reg.IY, arg), ref reg.A);
                return (23, "rrc (iy+d),a");
            case 0x10:
                Rl(GetByteMemory(reg.IY, arg), ref reg.B);
                return (23, "rl (iy+d),b");
            case 0x11:
                Rl(GetByteMemory(reg.IY, arg), ref reg.C);
                return (23, "rl (iy+d),c");
            case 0x12:
                Rl(GetByteMemory(reg.IY, arg), ref reg.D);
                return (23, "rl (iy+d),d");
            case 0x13:
                Rl(GetByteMemory(reg.IY, arg), ref reg.E);
                return (23, "rl (iy+d),e");
            case 0x14:
                Rl(GetByteMemory(reg.IY, arg), ref reg.H);
                return (23, "rl (iy+d),h");
            case 0x15:
                Rl(GetByteMemory(reg.IY, arg), ref reg.L);
                return (23, "rl (iy+d),l");
            case 0x16:
                Rl(ref GetByteMemoryRef(reg.IY, arg));
                return (23, "rl (iy+d)");
            case 0x17:
                Rl(GetByteMemory(reg.IY, arg), ref reg.A);
                return (23, "rl (iy+d),a");
            case 0x18:
                Rr(GetByteMemory(reg.IY, arg), ref reg.B);
                return (23, "rr (iy+d),b");
            case 0x19:
                Rr(GetByteMemory(reg.IY, arg), ref reg.C);
                return (23, "rr (iy+d),c");
            case 0x1A:
                Rr(GetByteMemory(reg.IY, arg), ref reg.D);
                return (23, "rr (iy+d),d");
            case 0x1B:
                Rr(GetByteMemory(reg.IY, arg), ref reg.E);
                return (23, "rr (iy+d),e");
            case 0x1C:
                Rr(GetByteMemory(reg.IY, arg), ref reg.H);
                return (23, "rr (iy+d),h");
            case 0x1D:
                Rr(GetByteMemory(reg.IY, arg), ref reg.L);
                return (23, "rr (iy+d),l");
            case 0x1E:
                Rr(ref GetByteMemoryRef(reg.IY, arg));
                return (23, "rr (iy+d)");
            case 0x1F:
                Rr(GetByteMemory(reg.IY, arg), ref reg.A);
                return (23, "rr (iy+d),a");
            case 0x20:
                Sla(GetByteMemory(reg.IY, arg), ref reg.B);
                return (23, "sla (iy+d),b");
            case 0x21:
                Sla(GetByteMemory(reg.IY, arg), ref reg.C);
                return (23, "sla (iy+d),c");
            case 0x22:
                Sla(GetByteMemory(reg.IY, arg), ref reg.D);
                return (23, "sla (iy+d),d");
            case 0x23:
                Sla(GetByteMemory(reg.IY, arg), ref reg.E);
                return (23, "sla (iy+d),e");
            case 0x24:
                Sla(GetByteMemory(reg.IY, arg), ref reg.H);
                return (23, "sla (iy+d),h");
            case 0x25:
                Sla(GetByteMemory(reg.IY, arg), ref reg.L);
                return (23, "sla (iy+d),l");
            case 0x26:
                Sla(ref GetByteMemoryRef(reg.IY, arg));
                return (23, "sla (iy+d)");
            case 0x27:
                Sla(GetByteMemory(reg.IY, arg), ref reg.A);
                return (23, "sla (iy+d),a");
            case 0x28:
                Sra(GetByteMemory(reg.IY, arg), ref reg.B);
                return (23, "sra (iy+d),b");
            case 0x29:
                Sra(GetByteMemory(reg.IY, arg), ref reg.C);
                return (23, "sra (iy+d),c");
            case 0x2A:
                Sra(GetByteMemory(reg.IY, arg), ref reg.D);
                return (23, "sra (iy+d),d");
            case 0x2B:
                Sra(GetByteMemory(reg.IY, arg), ref reg.E);
                return (23, "sra (iy+d),e");
            case 0x2C:
                Sra(GetByteMemory(reg.IY, arg), ref reg.H);
                return (23, "sra (iy+d),h");
            case 0x2D:
                Sra(GetByteMemory(reg.IY, arg), ref reg.L);
                return (23, "sra (iy+d),l");
            case 0x2E:
                Sra(ref GetByteMemoryRef(reg.IY, arg));
                return (23, "sra (iy+d)");
            case 0x2F:
                Sra(GetByteMemory(reg.IY, arg), ref reg.A);
                return (23, "sra (iy+d),a");
            case 0x30:
                Sll(GetByteMemory(reg.IY, arg), ref reg.B);
                return (23, "sll (iy+d),b");
            case 0x31:
                Sll(GetByteMemory(reg.IY, arg), ref reg.C);
                return (23, "sll (iy+d),c");
            case 0x32:
                Sll(GetByteMemory(reg.IY, arg), ref reg.D);
                return (23, "sll (iy+d),d");
            case 0x33:
                Sll(GetByteMemory(reg.IY, arg), ref reg.E);
                return (23, "sll (iy+d),e");
            case 0x34:
                Sll(GetByteMemory(reg.IY, arg), ref reg.H);
                return (23, "sll (iy+d),h");
            case 0x35:
                Sll(GetByteMemory(reg.IY, arg), ref reg.L);
                return (23, "sll (iy+d),l");
            case 0x36:
                Sll(ref GetByteMemoryRef(reg.IY, arg));
                return (23, "sll (iy+d)");
            case 0x37:
                Sll(GetByteMemory(reg.IY, arg), ref reg.A);
                return (23, "sll (iy+d),a");
            case 0x38:
                Srl(GetByteMemory(reg.IY, arg), ref reg.B);
                return (23, "srl (iy+d),b");
            case 0x39:
                Srl(GetByteMemory(reg.IY, arg), ref reg.C);
                return (23, "srl (iy+d),c");
            case 0x3A:
                Srl(GetByteMemory(reg.IY, arg), ref reg.D);
                return (23, "srl (iy+d),d");
            case 0x3B:
                Srl(GetByteMemory(reg.IY, arg), ref reg.E);
                return (23, "srl (iy+d),e");
            case 0x3C:
                Srl(GetByteMemory(reg.IY, arg), ref reg.H);
                return (23, "srl (iy+d),h");
            case 0x3D:
                Srl(GetByteMemory(reg.IY, arg), ref reg.L);
                return (23, "srl (iy+d),l");
            case 0x3E:
                Srl(ref GetByteMemoryRef(reg.IY, arg));
                return (23, "srl (iy+d)");
            case 0x3F:
                Srl(GetByteMemory(reg.IY, arg), ref reg.A);
                return (23, "srl (iy+d),a");
            case 0x40:
                Bit(0, GetByteMemory(reg.IY, arg));
                return (20, "bit 0,(iy+d)");
            case 0x41:
                Bit(0, GetByteMemory(reg.IY, arg));
                return (20, "bit 0,(iy+d)");
            case 0x42:
                Bit(0, GetByteMemory(reg.IY, arg));
                return (20, "bit 0,(iy+d)");
            case 0x43:
                Bit(0, GetByteMemory(reg.IY, arg));
                return (20, "bit 0,(iy+d)");
            case 0x44:
                Bit(0, GetByteMemory(reg.IY, arg));
                return (20, "bit 0,(iy+d)");
            case 0x45:
                Bit(0, GetByteMemory(reg.IY, arg));
                return (20, "bit 0,(iy+d)");
            case 0x46:
                Bit(0, GetByteMemory(reg.IY, arg));
                return (20, "bit 0,(iy+d)");
            case 0x47:
                Bit(0, GetByteMemory(reg.IY, arg));
                return (20, "bit 0,(iy+d)");
            case 0x48:
                Bit(1, GetByteMemory(reg.IY, arg));
                return (20, "bit 1,(iy+d)");
            case 0x49:
                Bit(1, GetByteMemory(reg.IY, arg));
                return (20, "bit 1,(iy+d)");
            case 0x4A:
                Bit(1, GetByteMemory(reg.IY, arg));
                return (20, "bit 1,(iy+d)");
            case 0x4B:
                Bit(1, GetByteMemory(reg.IY, arg));
                return (20, "bit 1,(iy+d)");
            case 0x4C:
                Bit(1, GetByteMemory(reg.IY, arg));
                return (20, "bit 1,(iy+d)");
            case 0x4D:
                Bit(1, GetByteMemory(reg.IY, arg));
                return (20, "bit 1,(iy+d)");
            case 0x4E:
                Bit(1, GetByteMemory(reg.IY, arg));
                return (20, "bit 1,(iy+d)");
            case 0x4F:
                Bit(1, GetByteMemory(reg.IY, arg));
                return (20, "bit 1,(iy+d)");
            case 0x50:
                Bit(2, GetByteMemory(reg.IY, arg));
                return (20, "bit 2,(iy+d)");
            case 0x51:
                Bit(2, GetByteMemory(reg.IY, arg));
                return (20, "bit 2,(iy+d)");
            case 0x52:
                Bit(2, GetByteMemory(reg.IY, arg));
                return (20, "bit 2,(iy+d)");
            case 0x53:
                Bit(2, GetByteMemory(reg.IY, arg));
                return (20, "bit 2,(iy+d)");
            case 0x54:
                Bit(2, GetByteMemory(reg.IY, arg));
                return (20, "bit 2,(iy+d)");
            case 0x55:
                Bit(2, GetByteMemory(reg.IY, arg));
                return (20, "bit 2,(iy+d)");
            case 0x56:
                Bit(2, GetByteMemory(reg.IY, arg));
                return (20, "bit 2,(iy+d)");
            case 0x57:
                Bit(2, GetByteMemory(reg.IY, arg));
                return (20, "bit 2,(iy+d)");
            case 0x58:
                Bit(3, GetByteMemory(reg.IY, arg));
                return (20, "bit 3,(iy+d)");
            case 0x59:
                Bit(3, GetByteMemory(reg.IY, arg));
                return (20, "bit 3,(iy+d)");
            case 0x5A:
                Bit(3, GetByteMemory(reg.IY, arg));
                return (20, "bit 3,(iy+d)");
            case 0x5B:
                Bit(3, GetByteMemory(reg.IY, arg));
                return (20, "bit 3,(iy+d)");
            case 0x5C:
                Bit(3, GetByteMemory(reg.IY, arg));
                return (20, "bit 3,(iy+d)");
            case 0x5D:
                Bit(3, GetByteMemory(reg.IY, arg));
                return (20, "bit 3,(iy+d)");
            case 0x5E:
                Bit(3, GetByteMemory(reg.IY, arg));
                return (20, "bit 3,(iy+d)");
            case 0x5F:
                Bit(3, GetByteMemory(reg.IY, arg));
                return (20, "bit 3,(iy+d)");
            case 0x60:
                Bit(4, GetByteMemory(reg.IY, arg));
                return (20, "bit 4,(iy+d)");
            case 0x61:
                Bit(4, GetByteMemory(reg.IY, arg));
                return (20, "bit 4,(iy+d)");
            case 0x62:
                Bit(4, GetByteMemory(reg.IY, arg));
                return (20, "bit 4,(iy+d)");
            case 0x63:
                Bit(4, GetByteMemory(reg.IY, arg));
                return (20, "bit 4,(iy+d)");
            case 0x64:
                Bit(4, GetByteMemory(reg.IY, arg));
                return (20, "bit 4,(iy+d)");
            case 0x65:
                Bit(4, GetByteMemory(reg.IY, arg));
                return (20, "bit 4,(iy+d)");
            case 0x66:
                Bit(4, GetByteMemory(reg.IY, arg));
                return (20, "bit 4,(iy+d)");
            case 0x67:
                Bit(4, GetByteMemory(reg.IY, arg));
                return (20, "bit 4,(iy+d)");
            case 0x68:
                Bit(5, GetByteMemory(reg.IY, arg));
                return (20, "bit 5,(iy+d)");
            case 0x69:
                Bit(5, GetByteMemory(reg.IY, arg));
                return (20, "bit 5,(iy+d)");
            case 0x6A:
                Bit(5, GetByteMemory(reg.IY, arg));
                return (20, "bit 5,(iy+d)");
            case 0x6B:
                Bit(5, GetByteMemory(reg.IY, arg));
                return (20, "bit 5,(iy+d)");
            case 0x6C:
                Bit(5, GetByteMemory(reg.IY, arg));
                return (20, "bit 5,(iy+d)");
            case 0x6D:
                Bit(5, GetByteMemory(reg.IY, arg));
                return (20, "bit 5,(iy+d)");
            case 0x6E:
                Bit(5, GetByteMemory(reg.IY, arg));
                return (20, "bit 5,(iy+d)");
            case 0x6F:
                Bit(5, GetByteMemory(reg.IY, arg));
                return (20, "bit 5,(iy+d)");
            case 0x70:
                Bit(6, GetByteMemory(reg.IY, arg));
                return (20, "bit 6,(iy+d)");
            case 0x71:
                Bit(6, GetByteMemory(reg.IY, arg));
                return (20, "bit 6,(iy+d)");
            case 0x72:
                Bit(6, GetByteMemory(reg.IY, arg));
                return (20, "bit 6,(iy+d)");
            case 0x73:
                Bit(6, GetByteMemory(reg.IY, arg));
                return (20, "bit 6,(iy+d)");
            case 0x74:
                Bit(6, GetByteMemory(reg.IY, arg));
                return (20, "bit 6,(iy+d)");
            case 0x75:
                Bit(6, GetByteMemory(reg.IY, arg));
                return (20, "bit 6,(iy+d)");
            case 0x76:
                Bit(6, GetByteMemory(reg.IY, arg));
                return (20, "bit 6,(iy+d)");
            case 0x77:
                Bit(6, GetByteMemory(reg.IY, arg));
                return (20, "bit 6,(iy+d)");
            case 0x78:
                Bit(7, GetByteMemory(reg.IY, arg));
                return (20, "bit 7,(iy+d)");
            case 0x79:
                Bit(7, GetByteMemory(reg.IY, arg));
                return (20, "bit 7,(iy+d)");
            case 0x7A:
                Bit(7, GetByteMemory(reg.IY, arg));
                return (20, "bit 7,(iy+d)");
            case 0x7B:
                Bit(7, GetByteMemory(reg.IY, arg));
                return (20, "bit 7,(iy+d)");
            case 0x7C:
                Bit(7, GetByteMemory(reg.IY, arg));
                return (20, "bit 7,(iy+d)");
            case 0x7D:
                Bit(7, GetByteMemory(reg.IY, arg));
                return (20, "bit 7,(iy+d)");
            case 0x7E:
                Bit(7, GetByteMemory(reg.IY, arg));
                return (20, "bit 7,(iy+d)");
            case 0x7F:
                Bit(7, GetByteMemory(reg.IY, arg));
                return (20, "bit 7,(iy+d)");
            case 0x80:
                Res(0, GetByteMemory(reg.IY, arg), ref reg.B);
                return (23, "res 0,(iy+d),b");
            case 0x81:
                Res(0, GetByteMemory(reg.IY, arg), ref reg.C);
                return (23, "res 0,(iy+d),c");
            case 0x82:
                Res(0, GetByteMemory(reg.IY, arg), ref reg.D);
                return (23, "res 0,(iy+d),d");
            case 0x83:
                Res(0, GetByteMemory(reg.IY, arg), ref reg.E);
                return (23, "res 0,(iy+d),e");
            case 0x84:
                Res(0, GetByteMemory(reg.IY, arg), ref reg.H);
                return (23, "res 0,(iy+d),h");
            case 0x85:
                Res(0, GetByteMemory(reg.IY, arg), ref reg.L);
                return (23, "res 0,(iy+d),l");
            case 0x86:
                Res(0, ref GetByteMemoryRef(reg.IY, arg));
                return (23, "res 0,(iy+d)");
            case 0x87:
                Res(0, GetByteMemory(reg.IY, arg), ref reg.A);
                return (23, "res 0,(iy+d),a");
            case 0x88:
                Res(1, GetByteMemory(reg.IY, arg), ref reg.B);
                return (23, "res 1,(iy+d),b");
            case 0x89:
                Res(1, GetByteMemory(reg.IY, arg), ref reg.C);
                return (23, "res 1,(iy+d),c");
            case 0x8A:
                Res(1, GetByteMemory(reg.IY, arg), ref reg.D);
                return (23, "res 1,(iy+d),d");
            case 0x8B:
                Res(1, GetByteMemory(reg.IY, arg), ref reg.E);
                return (23, "res 1,(iy+d),e");
            case 0x8C:
                Res(1, GetByteMemory(reg.IY, arg), ref reg.H);
                return (23, "res 1,(iy+d),h");
            case 0x8D:
                Res(1, GetByteMemory(reg.IY, arg), ref reg.L);
                return (23, "res 1,(iy+d),l");
            case 0x8E:
                Res(1, ref GetByteMemoryRef(reg.IY, arg));
                return (23, "res 1,(iy+d)");
            case 0x8F:
                Res(1, GetByteMemory(reg.IY, arg), ref reg.A);
                return (23, "res 1,(iy+d),a");
            case 0x90:
                Res(2, GetByteMemory(reg.IY, arg), ref reg.B);
                return (23, "res 2,(iy+d),b");
            case 0x91:
                Res(2, GetByteMemory(reg.IY, arg), ref reg.C);
                return (23, "res 2,(iy+d),c");
            case 0x92:
                Res(2, GetByteMemory(reg.IY, arg), ref reg.D);
                return (23, "res 2,(iy+d),d");
            case 0x93:
                Res(2, GetByteMemory(reg.IY, arg), ref reg.E);
                return (23, "res 2,(iy+d),e");
            case 0x94:
                Res(2, GetByteMemory(reg.IY, arg), ref reg.H);
                return (23, "res 2,(iy+d),h");
            case 0x95:
                Res(2, GetByteMemory(reg.IY, arg), ref reg.L);
                return (23, "res 2,(iy+d),l");
            case 0x96:
                Res(2, ref GetByteMemoryRef(reg.IY, arg));
                return (23, "res 2,(iy+d)");
            case 0x97:
                Res(2, GetByteMemory(reg.IY, arg), ref reg.A);
                return (23, "res 2,(iy+d),a");
            case 0x98:
                Res(3, GetByteMemory(reg.IY, arg), ref reg.B);
                return (23, "res 3,(iy+d),b");
            case 0x99:
                Res(3, GetByteMemory(reg.IY, arg), ref reg.C);
                return (23, "res 3,(iy+d),c");
            case 0x9A:
                Res(3, GetByteMemory(reg.IY, arg), ref reg.D);
                return (23, "res 3,(iy+d),d");
            case 0x9B:
                Res(3, GetByteMemory(reg.IY, arg), ref reg.E);
                return (23, "res 3,(iy+d),e");
            case 0x9C:
                Res(3, GetByteMemory(reg.IY, arg), ref reg.H);
                return (23, "res 3,(iy+d),h");
            case 0x9D:
                Res(3, GetByteMemory(reg.IY, arg), ref reg.L);
                return (23, "res 3,(iy+d),l");
            case 0x9E:
                Res(3, ref GetByteMemoryRef(reg.IY, arg));
                return (23, "res 3,(iy+d)");
            case 0x9F:
                Res(3, GetByteMemory(reg.IY, arg), ref reg.A);
                return (23, "res 3,(iy+d),a");
            case 0xA0:
                Res(4, GetByteMemory(reg.IY, arg), ref reg.B);
                return (23, "res 4,(iy+d),b");
            case 0xA1:
                Res(4, GetByteMemory(reg.IY, arg), ref reg.C);
                return (23, "res 4,(iy+d),c");
            case 0xA2:
                Res(4, GetByteMemory(reg.IY, arg), ref reg.D);
                return (23, "res 4,(iy+d),d");
            case 0xA3:
                Res(4, GetByteMemory(reg.IY, arg), ref reg.E);
                return (23, "res 4,(iy+d),e");
            case 0xA4:
                Res(4, GetByteMemory(reg.IY, arg), ref reg.H);
                return (23, "res 4,(iy+d),h");
            case 0xA5:
                Res(4, GetByteMemory(reg.IY, arg), ref reg.L);
                return (23, "res 4,(iy+d),l");
            case 0xA6:
                Res(4, ref GetByteMemoryRef(reg.IY, arg));
                return (23, "res 4,(iy+d)");
            case 0xA7:
                Res(4, GetByteMemory(reg.IY, arg), ref reg.A);
                return (23, "res 4,(iy+d),a");
            case 0xA8:
                Res(5, GetByteMemory(reg.IY, arg), ref reg.B);
                return (23, "res 5,(iy+d),b");
            case 0xA9:
                Res(5, GetByteMemory(reg.IY, arg), ref reg.C);
                return (23, "res 5,(iy+d),c");
            case 0xAA:
                Res(5, GetByteMemory(reg.IY, arg), ref reg.D);
                return (23, "res 5,(iy+d),d");
            case 0xAB:
                Res(5, GetByteMemory(reg.IY, arg), ref reg.E);
                return (23, "res 5,(iy+d),e");
            case 0xAC:
                Res(5, GetByteMemory(reg.IY, arg), ref reg.H);
                return (23, "res 5,(iy+d),h");
            case 0xAD:
                Res(5, GetByteMemory(reg.IY, arg), ref reg.L);
                return (23, "res 5,(iy+d),l");
            case 0xAE:
                Res(5, ref GetByteMemoryRef(reg.IY, arg));
                return (23, "res 5,(iy+d)");
            case 0xAF:
                Res(5, GetByteMemory(reg.IY, arg), ref reg.A);
                return (23, "res 5,(iy+d),a");
            case 0xB0:
                Res(6, GetByteMemory(reg.IY, arg), ref reg.B);
                return (23, "res 6,(iy+d),b");
            case 0xB1:
                Res(6, GetByteMemory(reg.IY, arg), ref reg.C);
                return (23, "res 6,(iy+d),c");
            case 0xB2:
                Res(6, GetByteMemory(reg.IY, arg), ref reg.D);
                return (23, "res 6,(iy+d),d");
            case 0xB3:
                Res(6, GetByteMemory(reg.IY, arg), ref reg.E);
                return (23, "res 6,(iy+d),e");
            case 0xB4:
                Res(6, GetByteMemory(reg.IY, arg), ref reg.H);
                return (23, "res 6,(iy+d),h");
            case 0xB5:
                Res(6, GetByteMemory(reg.IY, arg), ref reg.L);
                return (23, "res 6,(iy+d),l");
            case 0xB6:
                Res(6, ref GetByteMemoryRef(reg.IY, arg));
                return (23, "res 6,(iy+d)");
            case 0xB7:
                Res(6, GetByteMemory(reg.IY, arg), ref reg.A);
                return (23, "res 6,(iy+d),a");
            case 0xB8:
                Res(7, GetByteMemory(reg.IY, arg), ref reg.B);
                return (23, "res 7,(iy+d),b");
            case 0xB9:
                Res(7, GetByteMemory(reg.IY, arg), ref reg.C);
                return (23, "res 7,(iy+d),c");
            case 0xBA:
                Res(7, GetByteMemory(reg.IY, arg), ref reg.D);
                return (23, "res 7,(iy+d),d");
            case 0xBB:
                Res(7, GetByteMemory(reg.IY, arg), ref reg.E);
                return (23, "res 7,(iy+d),e");
            case 0xBC:
                Res(7, GetByteMemory(reg.IY, arg), ref reg.H);
                return (23, "res 7,(iy+d),h");
            case 0xBD:
                Res(7, GetByteMemory(reg.IY, arg), ref reg.L);
                return (23, "res 7,(iy+d),l");
            case 0xBE:
                Res(7, ref GetByteMemoryRef(reg.IY, arg));
                return (23, "res 7,(iy+d)");
            case 0xBF:
                Res(7, GetByteMemory(reg.IY, arg), ref reg.A);
                return (23, "res 7,(iy+d),a");
            case 0xC0:
                Set(0, GetByteMemory(reg.IY, arg), ref reg.B);
                return (23, "set 0,(iy+d),b");
            case 0xC1:
                Set(0, GetByteMemory(reg.IY, arg), ref reg.C);
                return (23, "set 0,(iy+d),c");
            case 0xC2:
                Set(0, GetByteMemory(reg.IY, arg), ref reg.D);
                return (23, "set 0,(iy+d),d");
            case 0xC3:
                Set(0, GetByteMemory(reg.IY, arg), ref reg.E);
                return (23, "set 0,(iy+d),e");
            case 0xC4:
                Set(0, GetByteMemory(reg.IY, arg), ref reg.H);
                return (23, "set 0,(iy+d),h");
            case 0xC5:
                Set(0, GetByteMemory(reg.IY, arg), ref reg.L);
                return (23, "set 0,(iy+d),l");
            case 0xC6:
                Set(0, ref GetByteMemoryRef(reg.IY, arg));
                return (23, "set 0,(iy+d)");
            case 0xC7:
                Set(0, GetByteMemory(reg.IY, arg), ref reg.A);
                return (23, "set 0,(iy+d),a");
            case 0xC8:
                Set(1, GetByteMemory(reg.IY, arg), ref reg.B);
                return (23, "set 1,(iy+d),b");
            case 0xC9:
                Set(1, GetByteMemory(reg.IY, arg), ref reg.C);
                return (23, "set 1,(iy+d),c");
            case 0xCA:
                Set(1, GetByteMemory(reg.IY, arg), ref reg.D);
                return (23, "set 1,(iy+d),d");
            case 0xCB:
                Set(1, GetByteMemory(reg.IY, arg), ref reg.E);
                return (23, "set 1,(iy+d),e");
            case 0xCC:
                Set(1, GetByteMemory(reg.IY, arg), ref reg.H);
                return (23, "set 1,(iy+d),h");
            case 0xCD:
                Set(1, GetByteMemory(reg.IY, arg), ref reg.L);
                return (23, "set 1,(iy+d),l");
            case 0xCE:
                Set(1, ref GetByteMemoryRef(reg.IY, arg));
                return (23, "set 1,(iy+d)");
            case 0xCF:
                Set(1, GetByteMemory(reg.IY, arg), ref reg.A);
                return (23, "set 1,(iy+d),a");
            case 0xD0:
                Set(2, GetByteMemory(reg.IY, arg), ref reg.B);
                return (23, "set 2,(iy+d),b");
            case 0xD1:
                Set(2, GetByteMemory(reg.IY, arg), ref reg.C);
                return (23, "set 2,(iy+d),c");
            case 0xD2:
                Set(2, GetByteMemory(reg.IY, arg), ref reg.D);
                return (23, "set 2,(iy+d),d");
            case 0xD3:
                Set(2, GetByteMemory(reg.IY, arg), ref reg.E);
                return (23, "set 2,(iy+d),e");
            case 0xD4:
                Set(2, GetByteMemory(reg.IY, arg), ref reg.H);
                return (23, "set 2,(iy+d),h");
            case 0xD5:
                Set(2, GetByteMemory(reg.IY, arg), ref reg.L);
                return (23, "set 2,(iy+d),l");
            case 0xD6:
                Set(2, ref GetByteMemoryRef(reg.IY, arg));
                return (23, "set 2,(iy+d)");
            case 0xD7:
                Set(2, GetByteMemory(reg.IY, arg), ref reg.A);
                return (23, "set 2,(iy+d),a");
            case 0xD8:
                Set(3, GetByteMemory(reg.IY, arg), ref reg.B);
                return (23, "set 3,(iy+d),b");
            case 0xD9:
                Set(3, GetByteMemory(reg.IY, arg), ref reg.C);
                return (23, "set 3,(iy+d),c");
            case 0xDA:
                Set(3, GetByteMemory(reg.IY, arg), ref reg.D);
                return (23, "set 3,(iy+d),d");
            case 0xDB:
                Set(3, GetByteMemory(reg.IY, arg), ref reg.E);
                return (23, "set 3,(iy+d),e");
            case 0xDC:
                Set(3, GetByteMemory(reg.IY, arg), ref reg.H);
                return (23, "set 3,(iy+d),h");
            case 0xDD:
                Set(3, GetByteMemory(reg.IY, arg), ref reg.L);
                return (23, "set 3,(iy+d),l");
            case 0xDE:
                Set(3, ref GetByteMemoryRef(reg.IY, arg));
                return (23, "set 3,(iy+d)");
            case 0xDF:
                Set(3, GetByteMemory(reg.IY, arg), ref reg.A);
                return (23, "set 3,(iy+d),a");
            case 0xE0:
                Set(4, GetByteMemory(reg.IY, arg), ref reg.B);
                return (23, "set 4,(iy+d),b");
            case 0xE1:
                Set(4, GetByteMemory(reg.IY, arg), ref reg.C);
                return (23, "set 4,(iy+d),c");
            case 0xE2:
                Set(4, GetByteMemory(reg.IY, arg), ref reg.D);
                return (23, "set 4,(iy+d),d");
            case 0xE3:
                Set(4, GetByteMemory(reg.IY, arg), ref reg.E);
                return (23, "set 4,(iy+d),e");
            case 0xE4:
                Set(4, GetByteMemory(reg.IY, arg), ref reg.H);
                return (23, "set 4,(iy+d),h");
            case 0xE5:
                Set(4, GetByteMemory(reg.IY, arg), ref reg.L);
                return (23, "set 4,(iy+d),l");
            case 0xE6:
                Set(4, ref GetByteMemoryRef(reg.IY, arg));
                return (23, "set 4,(iy+d)");
            case 0xE7:
                Set(4, GetByteMemory(reg.IY, arg), ref reg.A);
                return (23, "set 4,(iy+d),a");
            case 0xE8:
                Set(5, GetByteMemory(reg.IY, arg), ref reg.B);
                return (23, "set 5,(iy+d),b");
            case 0xE9:
                Set(5, GetByteMemory(reg.IY, arg), ref reg.C);
                return (23, "set 5,(iy+d),c");
            case 0xEA:
                Set(5, GetByteMemory(reg.IY, arg), ref reg.D);
                return (23, "set 5,(iy+d),d");
            case 0xEB:
                Set(5, GetByteMemory(reg.IY, arg), ref reg.E);
                return (23, "set 5,(iy+d),e");
            case 0xEC:
                Set(5, GetByteMemory(reg.IY, arg), ref reg.H);
                return (23, "set 5,(iy+d),h");
            case 0xED:
                Set(5, GetByteMemory(reg.IY, arg), ref reg.L);
                return (23, "set 5,(iy+d),l");
            case 0xEE:
                Set(5, ref GetByteMemoryRef(reg.IY, arg));
                return (23, "set 5,(iy+d)");
            case 0xEF:
                Set(5, GetByteMemory(reg.IY, arg), ref reg.A);
                return (23, "set 5,(iy+d),a");
            case 0xF0:
                Set(6, GetByteMemory(reg.IY, arg), ref reg.B);
                return (23, "set 6,(iy+d),b");
            case 0xF1:
                Set(6, GetByteMemory(reg.IY, arg), ref reg.C);
                return (23, "set 6,(iy+d),c");
            case 0xF2:
                Set(6, GetByteMemory(reg.IY, arg), ref reg.D);
                return (23, "set 6,(iy+d),d");
            case 0xF3:
                Set(6, GetByteMemory(reg.IY, arg), ref reg.E);
                return (23, "set 6,(iy+d),e");
            case 0xF4:
                Set(6, GetByteMemory(reg.IY, arg), ref reg.H);
                return (23, "set 6,(iy+d),h");
            case 0xF5:
                Set(6, GetByteMemory(reg.IY, arg), ref reg.L);
                return (23, "set 6,(iy+d),l");
            case 0xF6:
                Set(6, ref GetByteMemoryRef(reg.IY, arg));
                return (23, "set 6,(iy+d)");
            case 0xF7:
                Set(6, GetByteMemory(reg.IY, arg), ref reg.A);
                return (23, "set 6,(iy+d),a");
            case 0xF8:
                Set(7, GetByteMemory(reg.IY, arg), ref reg.B);
                return (23, "set 7,(iy+d),b");
            case 0xF9:
                Set(7, GetByteMemory(reg.IY, arg), ref reg.C);
                return (23, "set 7,(iy+d),c");
            case 0xFA:
                Set(7, GetByteMemory(reg.IY, arg), ref reg.D);
                return (23, "set 7,(iy+d),d");
            case 0xFB:
                Set(7, GetByteMemory(reg.IY, arg), ref reg.E);
                return (23, "set 7,(iy+d),e");
            case 0xFC:
                Set(7, GetByteMemory(reg.IY, arg), ref reg.H);
                return (23, "set 7,(iy+d),h");
            case 0xFD:
                Set(7, GetByteMemory(reg.IY, arg), ref reg.L);
                return (23, "set 7,(iy+d),l");
            case 0xFE:
                Set(7, ref GetByteMemoryRef(reg.IY, arg));
                return (23, "set 7,(iy+d)");
            case 0xFF:
                Set(7, GetByteMemory(reg.IY, arg), ref reg.A);
                return (23, "set 7,(iy+d),a");
            default:
                //throw new Exception($"{op:X2}");
                Console.WriteLine($"{op:X2}");
                return (0, "UNKNOWN");
        }
    }
}