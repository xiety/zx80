//This file is autogenerated, do not change it
#pragma warning disable CS0168 // Variable is declared but never used
#pragma warning disable CS0162 // Unreachable code detected
namespace ZX.Core.Cpu;

public partial class CpuRuntime
{
    private (int, string) InterpretCB()
    {
        bool isFast; //cannot be declared inside multiple switch cases
        var op = ReadByteOpcode();

        switch (op)
        {
            case 0x00:
                Rlc(ref reg.B);
                return (8, "rlc b");
            case 0x01:
                Rlc(ref reg.C);
                return (8, "rlc c");
            case 0x02:
                Rlc(ref reg.D);
                return (8, "rlc d");
            case 0x03:
                Rlc(ref reg.E);
                return (8, "rlc e");
            case 0x04:
                Rlc(ref reg.H);
                return (8, "rlc h");
            case 0x05:
                Rlc(ref reg.L);
                return (8, "rlc l");
            case 0x06:
                Rlc(ref GetByteMemoryRef(reg.HL));
                return (15, "rlc (hl)");
            case 0x07:
                Rlc(ref reg.A);
                return (8, "rlc a");
            case 0x08:
                Rrc(ref reg.B);
                return (8, "rrc b");
            case 0x09:
                Rrc(ref reg.C);
                return (8, "rrc c");
            case 0x0A:
                Rrc(ref reg.D);
                return (8, "rrc d");
            case 0x0B:
                Rrc(ref reg.E);
                return (8, "rrc e");
            case 0x0C:
                Rrc(ref reg.H);
                return (8, "rrc h");
            case 0x0D:
                Rrc(ref reg.L);
                return (8, "rrc l");
            case 0x0E:
                Rrc(ref GetByteMemoryRef(reg.HL));
                return (15, "rrc (hl)");
            case 0x0F:
                Rrc(ref reg.A);
                return (8, "rrc a");
            case 0x10:
                Rl(ref reg.B);
                return (8, "rl b");
            case 0x11:
                Rl(ref reg.C);
                return (8, "rl c");
            case 0x12:
                Rl(ref reg.D);
                return (8, "rl d");
            case 0x13:
                Rl(ref reg.E);
                return (8, "rl e");
            case 0x14:
                Rl(ref reg.H);
                return (8, "rl h");
            case 0x15:
                Rl(ref reg.L);
                return (8, "rl l");
            case 0x16:
                Rl(ref GetByteMemoryRef(reg.HL));
                return (15, "rl (hl)");
            case 0x17:
                Rl(ref reg.A);
                return (8, "rl a");
            case 0x18:
                Rr(ref reg.B);
                return (8, "rr b");
            case 0x19:
                Rr(ref reg.C);
                return (8, "rr c");
            case 0x1A:
                Rr(ref reg.D);
                return (8, "rr d");
            case 0x1B:
                Rr(ref reg.E);
                return (8, "rr e");
            case 0x1C:
                Rr(ref reg.H);
                return (8, "rr h");
            case 0x1D:
                Rr(ref reg.L);
                return (8, "rr l");
            case 0x1E:
                Rr(ref GetByteMemoryRef(reg.HL));
                return (15, "rr (hl)");
            case 0x1F:
                Rr(ref reg.A);
                return (8, "rr a");
            case 0x20:
                Sla(ref reg.B);
                return (8, "sla b");
            case 0x21:
                Sla(ref reg.C);
                return (8, "sla c");
            case 0x22:
                Sla(ref reg.D);
                return (8, "sla d");
            case 0x23:
                Sla(ref reg.E);
                return (8, "sla e");
            case 0x24:
                Sla(ref reg.H);
                return (8, "sla h");
            case 0x25:
                Sla(ref reg.L);
                return (8, "sla l");
            case 0x26:
                Sla(ref GetByteMemoryRef(reg.HL));
                return (15, "sla (hl)");
            case 0x27:
                Sla(ref reg.A);
                return (8, "sla a");
            case 0x28:
                Sra(ref reg.B);
                return (8, "sra b");
            case 0x29:
                Sra(ref reg.C);
                return (8, "sra c");
            case 0x2A:
                Sra(ref reg.D);
                return (8, "sra d");
            case 0x2B:
                Sra(ref reg.E);
                return (8, "sra e");
            case 0x2C:
                Sra(ref reg.H);
                return (8, "sra h");
            case 0x2D:
                Sra(ref reg.L);
                return (8, "sra l");
            case 0x2E:
                Sra(ref GetByteMemoryRef(reg.HL));
                return (15, "sra (hl)");
            case 0x2F:
                Sra(ref reg.A);
                return (8, "sra a");
            case 0x30:
                Sll(ref reg.B);
                return (8, "sll b");
            case 0x31:
                Sll(ref reg.C);
                return (8, "sll c");
            case 0x32:
                Sll(ref reg.D);
                return (8, "sll d");
            case 0x33:
                Sll(ref reg.E);
                return (8, "sll e");
            case 0x34:
                Sll(ref reg.H);
                return (8, "sll h");
            case 0x35:
                Sll(ref reg.L);
                return (8, "sll l");
            case 0x36:
                Sll(ref GetByteMemoryRef(reg.HL));
                return (15, "sll (hl)");
            case 0x37:
                Sll(ref reg.A);
                return (8, "sll a");
            case 0x38:
                Srl(ref reg.B);
                return (8, "srl b");
            case 0x39:
                Srl(ref reg.C);
                return (8, "srl c");
            case 0x3A:
                Srl(ref reg.D);
                return (8, "srl d");
            case 0x3B:
                Srl(ref reg.E);
                return (8, "srl e");
            case 0x3C:
                Srl(ref reg.H);
                return (8, "srl h");
            case 0x3D:
                Srl(ref reg.L);
                return (8, "srl l");
            case 0x3E:
                Srl(ref GetByteMemoryRef(reg.HL));
                return (15, "srl (hl)");
            case 0x3F:
                Srl(ref reg.A);
                return (8, "srl a");
            case 0x40:
                Bit(0, reg.B);
                return (8, "bit 0,b");
            case 0x41:
                Bit(0, reg.C);
                return (8, "bit 0,c");
            case 0x42:
                Bit(0, reg.D);
                return (8, "bit 0,d");
            case 0x43:
                Bit(0, reg.E);
                return (8, "bit 0,e");
            case 0x44:
                Bit(0, reg.H);
                return (8, "bit 0,h");
            case 0x45:
                Bit(0, reg.L);
                return (8, "bit 0,l");
            case 0x46:
                Bit(0, GetByteMemory(reg.HL));
                return (12, "bit 0,(hl)");
            case 0x47:
                Bit(0, reg.A);
                return (8, "bit 0,a");
            case 0x48:
                Bit(1, reg.B);
                return (8, "bit 1,b");
            case 0x49:
                Bit(1, reg.C);
                return (8, "bit 1,c");
            case 0x4A:
                Bit(1, reg.D);
                return (8, "bit 1,d");
            case 0x4B:
                Bit(1, reg.E);
                return (8, "bit 1,e");
            case 0x4C:
                Bit(1, reg.H);
                return (8, "bit 1,h");
            case 0x4D:
                Bit(1, reg.L);
                return (8, "bit 1,l");
            case 0x4E:
                Bit(1, GetByteMemory(reg.HL));
                return (12, "bit 1,(hl)");
            case 0x4F:
                Bit(1, reg.A);
                return (8, "bit 1,a");
            case 0x50:
                Bit(2, reg.B);
                return (8, "bit 2,b");
            case 0x51:
                Bit(2, reg.C);
                return (8, "bit 2,c");
            case 0x52:
                Bit(2, reg.D);
                return (8, "bit 2,d");
            case 0x53:
                Bit(2, reg.E);
                return (8, "bit 2,e");
            case 0x54:
                Bit(2, reg.H);
                return (8, "bit 2,h");
            case 0x55:
                Bit(2, reg.L);
                return (8, "bit 2,l");
            case 0x56:
                Bit(2, GetByteMemory(reg.HL));
                return (12, "bit 2,(hl)");
            case 0x57:
                Bit(2, reg.A);
                return (8, "bit 2,a");
            case 0x58:
                Bit(3, reg.B);
                return (8, "bit 3,b");
            case 0x59:
                Bit(3, reg.C);
                return (8, "bit 3,c");
            case 0x5A:
                Bit(3, reg.D);
                return (8, "bit 3,d");
            case 0x5B:
                Bit(3, reg.E);
                return (8, "bit 3,e");
            case 0x5C:
                Bit(3, reg.H);
                return (8, "bit 3,h");
            case 0x5D:
                Bit(3, reg.L);
                return (8, "bit 3,l");
            case 0x5E:
                Bit(3, GetByteMemory(reg.HL));
                return (12, "bit 3,(hl)");
            case 0x5F:
                Bit(3, reg.A);
                return (8, "bit 3,a");
            case 0x60:
                Bit(4, reg.B);
                return (8, "bit 4,b");
            case 0x61:
                Bit(4, reg.C);
                return (8, "bit 4,c");
            case 0x62:
                Bit(4, reg.D);
                return (8, "bit 4,d");
            case 0x63:
                Bit(4, reg.E);
                return (8, "bit 4,e");
            case 0x64:
                Bit(4, reg.H);
                return (8, "bit 4,h");
            case 0x65:
                Bit(4, reg.L);
                return (8, "bit 4,l");
            case 0x66:
                Bit(4, GetByteMemory(reg.HL));
                return (12, "bit 4,(hl)");
            case 0x67:
                Bit(4, reg.A);
                return (8, "bit 4,a");
            case 0x68:
                Bit(5, reg.B);
                return (8, "bit 5,b");
            case 0x69:
                Bit(5, reg.C);
                return (8, "bit 5,c");
            case 0x6A:
                Bit(5, reg.D);
                return (8, "bit 5,d");
            case 0x6B:
                Bit(5, reg.E);
                return (8, "bit 5,e");
            case 0x6C:
                Bit(5, reg.H);
                return (8, "bit 5,h");
            case 0x6D:
                Bit(5, reg.L);
                return (8, "bit 5,l");
            case 0x6E:
                Bit(5, GetByteMemory(reg.HL));
                return (12, "bit 5,(hl)");
            case 0x6F:
                Bit(5, reg.A);
                return (8, "bit 5,a");
            case 0x70:
                Bit(6, reg.B);
                return (8, "bit 6,b");
            case 0x71:
                Bit(6, reg.C);
                return (8, "bit 6,c");
            case 0x72:
                Bit(6, reg.D);
                return (8, "bit 6,d");
            case 0x73:
                Bit(6, reg.E);
                return (8, "bit 6,e");
            case 0x74:
                Bit(6, reg.H);
                return (8, "bit 6,h");
            case 0x75:
                Bit(6, reg.L);
                return (8, "bit 6,l");
            case 0x76:
                Bit(6, GetByteMemory(reg.HL));
                return (12, "bit 6,(hl)");
            case 0x77:
                Bit(6, reg.A);
                return (8, "bit 6,a");
            case 0x78:
                Bit(7, reg.B);
                return (8, "bit 7,b");
            case 0x79:
                Bit(7, reg.C);
                return (8, "bit 7,c");
            case 0x7A:
                Bit(7, reg.D);
                return (8, "bit 7,d");
            case 0x7B:
                Bit(7, reg.E);
                return (8, "bit 7,e");
            case 0x7C:
                Bit(7, reg.H);
                return (8, "bit 7,h");
            case 0x7D:
                Bit(7, reg.L);
                return (8, "bit 7,l");
            case 0x7E:
                Bit(7, GetByteMemory(reg.HL));
                return (12, "bit 7,(hl)");
            case 0x7F:
                Bit(7, reg.A);
                return (8, "bit 7,a");
            case 0x80:
                Res(0, ref reg.B);
                return (8, "res 0,b");
            case 0x81:
                Res(0, ref reg.C);
                return (8, "res 0,c");
            case 0x82:
                Res(0, ref reg.D);
                return (8, "res 0,d");
            case 0x83:
                Res(0, ref reg.E);
                return (8, "res 0,e");
            case 0x84:
                Res(0, ref reg.H);
                return (8, "res 0,h");
            case 0x85:
                Res(0, ref reg.L);
                return (8, "res 0,l");
            case 0x86:
                Res(0, ref GetByteMemoryRef(reg.HL));
                return (15, "res 0,(hl)");
            case 0x87:
                Res(0, ref reg.A);
                return (8, "res 0,a");
            case 0x88:
                Res(1, ref reg.B);
                return (8, "res 1,b");
            case 0x89:
                Res(1, ref reg.C);
                return (8, "res 1,c");
            case 0x8A:
                Res(1, ref reg.D);
                return (8, "res 1,d");
            case 0x8B:
                Res(1, ref reg.E);
                return (8, "res 1,e");
            case 0x8C:
                Res(1, ref reg.H);
                return (8, "res 1,h");
            case 0x8D:
                Res(1, ref reg.L);
                return (8, "res 1,l");
            case 0x8E:
                Res(1, ref GetByteMemoryRef(reg.HL));
                return (15, "res 1,(hl)");
            case 0x8F:
                Res(1, ref reg.A);
                return (8, "res 1,a");
            case 0x90:
                Res(2, ref reg.B);
                return (8, "res 2,b");
            case 0x91:
                Res(2, ref reg.C);
                return (8, "res 2,c");
            case 0x92:
                Res(2, ref reg.D);
                return (8, "res 2,d");
            case 0x93:
                Res(2, ref reg.E);
                return (8, "res 2,e");
            case 0x94:
                Res(2, ref reg.H);
                return (8, "res 2,h");
            case 0x95:
                Res(2, ref reg.L);
                return (8, "res 2,l");
            case 0x96:
                Res(2, ref GetByteMemoryRef(reg.HL));
                return (15, "res 2,(hl)");
            case 0x97:
                Res(2, ref reg.A);
                return (8, "res 2,a");
            case 0x98:
                Res(3, ref reg.B);
                return (8, "res 3,b");
            case 0x99:
                Res(3, ref reg.C);
                return (8, "res 3,c");
            case 0x9A:
                Res(3, ref reg.D);
                return (8, "res 3,d");
            case 0x9B:
                Res(3, ref reg.E);
                return (8, "res 3,e");
            case 0x9C:
                Res(3, ref reg.H);
                return (8, "res 3,h");
            case 0x9D:
                Res(3, ref reg.L);
                return (8, "res 3,l");
            case 0x9E:
                Res(3, ref GetByteMemoryRef(reg.HL));
                return (15, "res 3,(hl)");
            case 0x9F:
                Res(3, ref reg.A);
                return (8, "res 3,a");
            case 0xA0:
                Res(4, ref reg.B);
                return (8, "res 4,b");
            case 0xA1:
                Res(4, ref reg.C);
                return (8, "res 4,c");
            case 0xA2:
                Res(4, ref reg.D);
                return (8, "res 4,d");
            case 0xA3:
                Res(4, ref reg.E);
                return (8, "res 4,e");
            case 0xA4:
                Res(4, ref reg.H);
                return (8, "res 4,h");
            case 0xA5:
                Res(4, ref reg.L);
                return (8, "res 4,l");
            case 0xA6:
                Res(4, ref GetByteMemoryRef(reg.HL));
                return (15, "res 4,(hl)");
            case 0xA7:
                Res(4, ref reg.A);
                return (8, "res 4,a");
            case 0xA8:
                Res(5, ref reg.B);
                return (8, "res 5,b");
            case 0xA9:
                Res(5, ref reg.C);
                return (8, "res 5,c");
            case 0xAA:
                Res(5, ref reg.D);
                return (8, "res 5,d");
            case 0xAB:
                Res(5, ref reg.E);
                return (8, "res 5,e");
            case 0xAC:
                Res(5, ref reg.H);
                return (8, "res 5,h");
            case 0xAD:
                Res(5, ref reg.L);
                return (8, "res 5,l");
            case 0xAE:
                Res(5, ref GetByteMemoryRef(reg.HL));
                return (15, "res 5,(hl)");
            case 0xAF:
                Res(5, ref reg.A);
                return (8, "res 5,a");
            case 0xB0:
                Res(6, ref reg.B);
                return (8, "res 6,b");
            case 0xB1:
                Res(6, ref reg.C);
                return (8, "res 6,c");
            case 0xB2:
                Res(6, ref reg.D);
                return (8, "res 6,d");
            case 0xB3:
                Res(6, ref reg.E);
                return (8, "res 6,e");
            case 0xB4:
                Res(6, ref reg.H);
                return (8, "res 6,h");
            case 0xB5:
                Res(6, ref reg.L);
                return (8, "res 6,l");
            case 0xB6:
                Res(6, ref GetByteMemoryRef(reg.HL));
                return (15, "res 6,(hl)");
            case 0xB7:
                Res(6, ref reg.A);
                return (8, "res 6,a");
            case 0xB8:
                Res(7, ref reg.B);
                return (8, "res 7,b");
            case 0xB9:
                Res(7, ref reg.C);
                return (8, "res 7,c");
            case 0xBA:
                Res(7, ref reg.D);
                return (8, "res 7,d");
            case 0xBB:
                Res(7, ref reg.E);
                return (8, "res 7,e");
            case 0xBC:
                Res(7, ref reg.H);
                return (8, "res 7,h");
            case 0xBD:
                Res(7, ref reg.L);
                return (8, "res 7,l");
            case 0xBE:
                Res(7, ref GetByteMemoryRef(reg.HL));
                return (15, "res 7,(hl)");
            case 0xBF:
                Res(7, ref reg.A);
                return (8, "res 7,a");
            case 0xC0:
                Set(0, ref reg.B);
                return (8, "set 0,b");
            case 0xC1:
                Set(0, ref reg.C);
                return (8, "set 0,c");
            case 0xC2:
                Set(0, ref reg.D);
                return (8, "set 0,d");
            case 0xC3:
                Set(0, ref reg.E);
                return (8, "set 0,e");
            case 0xC4:
                Set(0, ref reg.H);
                return (8, "set 0,h");
            case 0xC5:
                Set(0, ref reg.L);
                return (8, "set 0,l");
            case 0xC6:
                Set(0, ref GetByteMemoryRef(reg.HL));
                return (15, "set 0,(hl)");
            case 0xC7:
                Set(0, ref reg.A);
                return (8, "set 0,a");
            case 0xC8:
                Set(1, ref reg.B);
                return (8, "set 1,b");
            case 0xC9:
                Set(1, ref reg.C);
                return (8, "set 1,c");
            case 0xCA:
                Set(1, ref reg.D);
                return (8, "set 1,d");
            case 0xCB:
                Set(1, ref reg.E);
                return (8, "set 1,e");
            case 0xCC:
                Set(1, ref reg.H);
                return (8, "set 1,h");
            case 0xCD:
                Set(1, ref reg.L);
                return (8, "set 1,l");
            case 0xCE:
                Set(1, ref GetByteMemoryRef(reg.HL));
                return (15, "set 1,(hl)");
            case 0xCF:
                Set(1, ref reg.A);
                return (8, "set 1,a");
            case 0xD0:
                Set(2, ref reg.B);
                return (8, "set 2,b");
            case 0xD1:
                Set(2, ref reg.C);
                return (8, "set 2,c");
            case 0xD2:
                Set(2, ref reg.D);
                return (8, "set 2,d");
            case 0xD3:
                Set(2, ref reg.E);
                return (8, "set 2,e");
            case 0xD4:
                Set(2, ref reg.H);
                return (8, "set 2,h");
            case 0xD5:
                Set(2, ref reg.L);
                return (8, "set 2,l");
            case 0xD6:
                Set(2, ref GetByteMemoryRef(reg.HL));
                return (15, "set 2,(hl)");
            case 0xD7:
                Set(2, ref reg.A);
                return (8, "set 2,a");
            case 0xD8:
                Set(3, ref reg.B);
                return (8, "set 3,b");
            case 0xD9:
                Set(3, ref reg.C);
                return (8, "set 3,c");
            case 0xDA:
                Set(3, ref reg.D);
                return (8, "set 3,d");
            case 0xDB:
                Set(3, ref reg.E);
                return (8, "set 3,e");
            case 0xDC:
                Set(3, ref reg.H);
                return (8, "set 3,h");
            case 0xDD:
                Set(3, ref reg.L);
                return (8, "set 3,l");
            case 0xDE:
                Set(3, ref GetByteMemoryRef(reg.HL));
                return (15, "set 3,(hl)");
            case 0xDF:
                Set(3, ref reg.A);
                return (8, "set 3,a");
            case 0xE0:
                Set(4, ref reg.B);
                return (8, "set 4,b");
            case 0xE1:
                Set(4, ref reg.C);
                return (8, "set 4,c");
            case 0xE2:
                Set(4, ref reg.D);
                return (8, "set 4,d");
            case 0xE3:
                Set(4, ref reg.E);
                return (8, "set 4,e");
            case 0xE4:
                Set(4, ref reg.H);
                return (8, "set 4,h");
            case 0xE5:
                Set(4, ref reg.L);
                return (8, "set 4,l");
            case 0xE6:
                Set(4, ref GetByteMemoryRef(reg.HL));
                return (15, "set 4,(hl)");
            case 0xE7:
                Set(4, ref reg.A);
                return (8, "set 4,a");
            case 0xE8:
                Set(5, ref reg.B);
                return (8, "set 5,b");
            case 0xE9:
                Set(5, ref reg.C);
                return (8, "set 5,c");
            case 0xEA:
                Set(5, ref reg.D);
                return (8, "set 5,d");
            case 0xEB:
                Set(5, ref reg.E);
                return (8, "set 5,e");
            case 0xEC:
                Set(5, ref reg.H);
                return (8, "set 5,h");
            case 0xED:
                Set(5, ref reg.L);
                return (8, "set 5,l");
            case 0xEE:
                Set(5, ref GetByteMemoryRef(reg.HL));
                return (15, "set 5,(hl)");
            case 0xEF:
                Set(5, ref reg.A);
                return (8, "set 5,a");
            case 0xF0:
                Set(6, ref reg.B);
                return (8, "set 6,b");
            case 0xF1:
                Set(6, ref reg.C);
                return (8, "set 6,c");
            case 0xF2:
                Set(6, ref reg.D);
                return (8, "set 6,d");
            case 0xF3:
                Set(6, ref reg.E);
                return (8, "set 6,e");
            case 0xF4:
                Set(6, ref reg.H);
                return (8, "set 6,h");
            case 0xF5:
                Set(6, ref reg.L);
                return (8, "set 6,l");
            case 0xF6:
                Set(6, ref GetByteMemoryRef(reg.HL));
                return (15, "set 6,(hl)");
            case 0xF7:
                Set(6, ref reg.A);
                return (8, "set 6,a");
            case 0xF8:
                Set(7, ref reg.B);
                return (8, "set 7,b");
            case 0xF9:
                Set(7, ref reg.C);
                return (8, "set 7,c");
            case 0xFA:
                Set(7, ref reg.D);
                return (8, "set 7,d");
            case 0xFB:
                Set(7, ref reg.E);
                return (8, "set 7,e");
            case 0xFC:
                Set(7, ref reg.H);
                return (8, "set 7,h");
            case 0xFD:
                Set(7, ref reg.L);
                return (8, "set 7,l");
            case 0xFE:
                Set(7, ref GetByteMemoryRef(reg.HL));
                return (15, "set 7,(hl)");
            case 0xFF:
                Set(7, ref reg.A);
                return (8, "set 7,a");
            default:
                //throw new Exception($"\{op:X2\}");
                Console.WriteLine($"Unknown operation: CB {op:X2}");
                return (0, "UNKNOWN");
        }
    }
}
