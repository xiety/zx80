//This file is autogenerated, do not change it
#pragma warning disable CS0168 // Variable is declared but never used
#pragma warning disable CS0162 // Unreachable code detected
namespace ZX.Core.Cpu;

public partial class CpuRuntime
{
    private (int, string) InterpretFD()
    {
        bool isFast; //cannot be declared inside multiple switch cases
        var op = ReadByteOpcode();

        switch (op)
        {
            case 0x04:
                Inc(ref reg.B);
                return (8, "inc b");
            case 0x05:
                Dec(ref reg.B);
                return (8, "dec b");
            case 0x06:
                Ld(ref reg.B, ReadByteOpcode());
                return (11, "ld b,n");
            case 0x09:
                Add(ref reg.IY, reg.BC);
                return (15, "add iy,bc");
            case 0x0C:
                Inc(ref reg.C);
                return (8, "inc c");
            case 0x0D:
                Dec(ref reg.C);
                return (8, "dec c");
            case 0x0E:
                Ld(ref reg.C, ReadByteOpcode());
                return (11, "ld c,n");
            case 0x14:
                Inc(ref reg.D);
                return (8, "inc d");
            case 0x15:
                Dec(ref reg.D);
                return (8, "dec d");
            case 0x16:
                Ld(ref reg.D, ReadByteOpcode());
                return (11, "ld d,n");
            case 0x19:
                Add(ref reg.IY, reg.DE);
                return (15, "add iy,de");
            case 0x1C:
                Inc(ref reg.E);
                return (8, "inc e");
            case 0x1D:
                Dec(ref reg.E);
                return (8, "dec e");
            case 0x1E:
                Ld(ref reg.E, ReadByteOpcode());
                return (11, "ld e,n");
            case 0x21:
                Ld(ref reg.IY, ReadUShortOpcode());
                return (14, "ld iy,nn");
            case 0x22:
                Ld(ref GetUShortMemoryRef(ReadUShortOpcode()), reg.IY);
                return (20, "ld (nn),iy");
            case 0x23:
                Inc(ref reg.IY);
                return (10, "inc iy");
            case 0x24:
                Inc(ref reg.IYH);
                return (8, "inc iyh");
            case 0x25:
                Dec(ref reg.IYH);
                return (8, "dec iyh");
            case 0x26:
                Ld(ref reg.IYH, ReadByteOpcode());
                return (11, "ld iyh,n");
            case 0x29:
                Add(ref reg.IY, reg.IY);
                return (15, "add iy,iy");
            case 0x2A:
                Ld(ref reg.IY, GetUShortMemory(ReadUShortOpcode()));
                return (20, "ld iy,(nn)");
            case 0x2B:
                Dec(ref reg.IY);
                return (10, "dec iy");
            case 0x2C:
                Inc(ref reg.IYL);
                return (8, "inc iyl");
            case 0x2D:
                Dec(ref reg.IYL);
                return (8, "dec iyl");
            case 0x2E:
                Ld(ref reg.IYL, ReadByteOpcode());
                return (11, "ld iyl,n");
            case 0x34:
                Inc(ref GetByteMemoryRef(reg.IY, ReadByteOpcode()));
                return (23, "inc (iy+d)");
            case 0x35:
                Dec(ref GetByteMemoryRef(reg.IY, ReadByteOpcode()));
                return (23, "dec (iy+d)");
            case 0x36:
                Ld(ref GetByteMemoryRef(reg.IY, ReadByteOpcode()), ReadByteOpcode());
                return (19, "ld (iy+d),n");
            case 0x39:
                Add(ref reg.IY, reg.SP);
                return (15, "add iy,sp");
            case 0x3C:
                Inc(ref reg.A);
                return (8, "inc a");
            case 0x3D:
                Dec(ref reg.A);
                return (8, "dec a");
            case 0x3E:
                Ld(ref reg.A, ReadByteOpcode());
                return (11, "ld a,n");
            case 0x40:
                Ld(ref reg.B, reg.B);
                return (8, "ld b,b");
            case 0x41:
                Ld(ref reg.B, reg.C);
                return (8, "ld b,c");
            case 0x42:
                Ld(ref reg.B, reg.D);
                return (8, "ld b,d");
            case 0x43:
                Ld(ref reg.B, reg.E);
                return (8, "ld b,e");
            case 0x44:
                Ld(ref reg.B, reg.IYH);
                return (8, "ld b,iyh");
            case 0x45:
                Ld(ref reg.B, reg.IYL);
                return (8, "ld b,iyl");
            case 0x46:
                Ld(ref reg.B, GetByteMemory(reg.IY, ReadByteOpcode()));
                return (19, "ld b,(iy+d)");
            case 0x47:
                Ld(ref reg.B, reg.A);
                return (8, "ld b,a");
            case 0x48:
                Ld(ref reg.C, reg.B);
                return (8, "ld c,b");
            case 0x49:
                Ld(ref reg.C, reg.C);
                return (8, "ld c,c");
            case 0x4A:
                Ld(ref reg.C, reg.D);
                return (8, "ld c,d");
            case 0x4B:
                Ld(ref reg.C, reg.E);
                return (8, "ld c,e");
            case 0x4C:
                Ld(ref reg.C, reg.IYH);
                return (8, "ld c,iyh");
            case 0x4D:
                Ld(ref reg.C, reg.IYL);
                return (8, "ld c,iyl");
            case 0x4E:
                Ld(ref reg.C, GetByteMemory(reg.IY, ReadByteOpcode()));
                return (19, "ld c,(iy+d)");
            case 0x4F:
                Ld(ref reg.C, reg.A);
                return (8, "ld c,a");
            case 0x50:
                Ld(ref reg.D, reg.B);
                return (8, "ld d,b");
            case 0x51:
                Ld(ref reg.D, reg.C);
                return (8, "ld d,c");
            case 0x52:
                Ld(ref reg.D, reg.D);
                return (8, "ld d,d");
            case 0x53:
                Ld(ref reg.D, reg.E);
                return (8, "ld d,e");
            case 0x54:
                Ld(ref reg.D, reg.IYH);
                return (8, "ld d,iyh");
            case 0x55:
                Ld(ref reg.D, reg.IYL);
                return (8, "ld d,iyl");
            case 0x56:
                Ld(ref reg.D, GetByteMemory(reg.IY, ReadByteOpcode()));
                return (19, "ld d,(iy+d)");
            case 0x57:
                Ld(ref reg.D, reg.A);
                return (8, "ld d,a");
            case 0x58:
                Ld(ref reg.E, reg.B);
                return (8, "ld e,b");
            case 0x59:
                Ld(ref reg.E, reg.C);
                return (8, "ld e,c");
            case 0x5A:
                Ld(ref reg.E, reg.D);
                return (8, "ld e,d");
            case 0x5B:
                Ld(ref reg.E, reg.E);
                return (8, "ld e,e");
            case 0x5C:
                Ld(ref reg.E, reg.IYH);
                return (8, "ld e,iyh");
            case 0x5D:
                Ld(ref reg.E, reg.IYL);
                return (8, "ld e,iyl");
            case 0x5E:
                Ld(ref reg.E, GetByteMemory(reg.IY, ReadByteOpcode()));
                return (19, "ld e,(iy+d)");
            case 0x5F:
                Ld(ref reg.E, reg.A);
                return (8, "ld e,a");
            case 0x60:
                Ld(ref reg.IYH, reg.B);
                return (8, "ld iyh,b");
            case 0x61:
                Ld(ref reg.IYH, reg.C);
                return (8, "ld iyh,c");
            case 0x62:
                Ld(ref reg.IYH, reg.D);
                return (8, "ld iyh,d");
            case 0x63:
                Ld(ref reg.IYH, reg.E);
                return (8, "ld iyh,e");
            case 0x64:
                Ld(ref reg.IYH, reg.IYH);
                return (8, "ld iyh,iyh");
            case 0x65:
                Ld(ref reg.IYH, reg.IYL);
                return (8, "ld iyh,iyl");
            case 0x66:
                Ld(ref reg.H, GetByteMemory(reg.IY, ReadByteOpcode()));
                return (19, "ld h,(iy+d)");
            case 0x67:
                Ld(ref reg.IYH, reg.A);
                return (8, "ld iyh,a");
            case 0x68:
                Ld(ref reg.IYL, reg.B);
                return (8, "ld iyl,b");
            case 0x69:
                Ld(ref reg.IYL, reg.C);
                return (8, "ld iyl,c");
            case 0x6A:
                Ld(ref reg.IYL, reg.D);
                return (8, "ld iyl,d");
            case 0x6B:
                Ld(ref reg.IYL, reg.E);
                return (8, "ld iyl,e");
            case 0x6C:
                Ld(ref reg.IYL, reg.IYH);
                return (8, "ld iyl,iyh");
            case 0x6D:
                Ld(ref reg.IYL, reg.IYL);
                return (8, "ld iyl,iyl");
            case 0x6E:
                Ld(ref reg.L, GetByteMemory(reg.IY, ReadByteOpcode()));
                return (19, "ld l,(iy+d)");
            case 0x6F:
                Ld(ref reg.IYL, reg.A);
                return (8, "ld iyl,a");
            case 0x70:
                Ld(ref GetByteMemoryRef(reg.IY, ReadByteOpcode()), reg.B);
                return (19, "ld (iy+d),b");
            case 0x71:
                Ld(ref GetByteMemoryRef(reg.IY, ReadByteOpcode()), reg.C);
                return (19, "ld (iy+d),c");
            case 0x72:
                Ld(ref GetByteMemoryRef(reg.IY, ReadByteOpcode()), reg.D);
                return (19, "ld (iy+d),d");
            case 0x73:
                Ld(ref GetByteMemoryRef(reg.IY, ReadByteOpcode()), reg.E);
                return (19, "ld (iy+d),e");
            case 0x74:
                Ld(ref GetByteMemoryRef(reg.IY, ReadByteOpcode()), reg.H);
                return (19, "ld (iy+d),h");
            case 0x75:
                Ld(ref GetByteMemoryRef(reg.IY, ReadByteOpcode()), reg.L);
                return (19, "ld (iy+d),l");
            case 0x77:
                Ld(ref GetByteMemoryRef(reg.IY, ReadByteOpcode()), reg.A);
                return (19, "ld (iy+d),a");
            case 0x78:
                Ld(ref reg.A, reg.B);
                return (8, "ld a,b");
            case 0x79:
                Ld(ref reg.A, reg.C);
                return (8, "ld a,c");
            case 0x7A:
                Ld(ref reg.A, reg.D);
                return (8, "ld a,d");
            case 0x7B:
                Ld(ref reg.A, reg.E);
                return (8, "ld a,e");
            case 0x7C:
                Ld(ref reg.A, reg.IYH);
                return (8, "ld a,iyh");
            case 0x7D:
                Ld(ref reg.A, reg.IYL);
                return (8, "ld a,iyl");
            case 0x7E:
                Ld(ref reg.A, GetByteMemory(reg.IY, ReadByteOpcode()));
                return (19, "ld a,(iy+d)");
            case 0x7F:
                Ld(ref reg.A, reg.A);
                return (8, "ld a,a");
            case 0x80:
                Add(ref reg.A, reg.B);
                return (8, "add a,b");
            case 0x81:
                Add(ref reg.A, reg.C);
                return (8, "add a,c");
            case 0x82:
                Add(ref reg.A, reg.D);
                return (8, "add a,d");
            case 0x83:
                Add(ref reg.A, reg.E);
                return (8, "add a,e");
            case 0x84:
                Add(ref reg.A, reg.IYH);
                return (8, "add a,iyh");
            case 0x85:
                Add(ref reg.A, reg.IYL);
                return (8, "add a,iyl");
            case 0x86:
                Add(ref reg.A, GetByteMemory(reg.IY, ReadByteOpcode()));
                return (19, "add a,(iy+d)");
            case 0x87:
                Add(ref reg.A, reg.A);
                return (8, "add a,a");
            case 0x88:
                Adc(ref reg.A, reg.B);
                return (8, "adc a,b");
            case 0x89:
                Adc(ref reg.A, reg.C);
                return (8, "adc a,c");
            case 0x8A:
                Adc(ref reg.A, reg.D);
                return (8, "adc a,d");
            case 0x8B:
                Adc(ref reg.A, reg.E);
                return (8, "adc a,e");
            case 0x8C:
                Adc(ref reg.A, reg.IYH);
                return (8, "adc a,iyh");
            case 0x8D:
                Adc(ref reg.A, reg.IYL);
                return (8, "adc a,iyl");
            case 0x8E:
                Adc(ref reg.A, GetByteMemory(reg.IY, ReadByteOpcode()));
                return (19, "adc a,(iy+d)");
            case 0x8F:
                Adc(ref reg.A, reg.A);
                return (8, "adc a,a");
            case 0x90:
                Sub(reg.B);
                return (8, "sub b");
            case 0x91:
                Sub(reg.C);
                return (8, "sub c");
            case 0x92:
                Sub(reg.D);
                return (8, "sub d");
            case 0x93:
                Sub(reg.E);
                return (8, "sub e");
            case 0x94:
                Sub(reg.IYH);
                return (8, "sub iyh");
            case 0x95:
                Sub(reg.IYL);
                return (8, "sub iyl");
            case 0x96:
                Sub(GetByteMemory(reg.IY, ReadByteOpcode()));
                return (19, "sub (iy+d)");
            case 0x97:
                Sub(reg.A);
                return (8, "sub a");
            case 0x98:
                Sbc(ref reg.A, reg.B);
                return (8, "sbc a,b");
            case 0x99:
                Sbc(ref reg.A, reg.C);
                return (8, "sbc a,c");
            case 0x9A:
                Sbc(ref reg.A, reg.D);
                return (8, "sbc a,d");
            case 0x9B:
                Sbc(ref reg.A, reg.E);
                return (8, "sbc a,e");
            case 0x9C:
                Sbc(ref reg.A, reg.IYH);
                return (8, "sbc a,iyh");
            case 0x9D:
                Sbc(ref reg.A, reg.IYL);
                return (8, "sbc a,iyl");
            case 0x9E:
                Sbc(ref reg.A, GetByteMemory(reg.IY, ReadByteOpcode()));
                return (19, "sbc a,(iy+d)");
            case 0x9F:
                Sbc(ref reg.A, reg.A);
                return (8, "sbc a,a");
            case 0xA0:
                And(reg.B);
                return (8, "and b");
            case 0xA1:
                And(reg.C);
                return (8, "and c");
            case 0xA2:
                And(reg.D);
                return (8, "and d");
            case 0xA3:
                And(reg.E);
                return (8, "and e");
            case 0xA4:
                And(reg.IYH);
                return (8, "and iyh");
            case 0xA5:
                And(reg.IYL);
                return (8, "and iyl");
            case 0xA6:
                And(GetByteMemory(reg.IY, ReadByteOpcode()));
                return (19, "and (iy+d)");
            case 0xA7:
                And(reg.A);
                return (8, "and a");
            case 0xA8:
                Xor(reg.B);
                return (8, "xor b");
            case 0xA9:
                Xor(reg.C);
                return (8, "xor c");
            case 0xAA:
                Xor(reg.D);
                return (8, "xor d");
            case 0xAB:
                Xor(reg.E);
                return (8, "xor e");
            case 0xAC:
                Xor(reg.IYH);
                return (8, "xor iyh");
            case 0xAD:
                Xor(reg.IYL);
                return (8, "xor iyl");
            case 0xAE:
                Xor(GetByteMemory(reg.IY, ReadByteOpcode()));
                return (19, "xor (iy+d)");
            case 0xAF:
                Xor(reg.A);
                return (8, "xor a");
            case 0xB0:
                Or(reg.B);
                return (8, "or b");
            case 0xB1:
                Or(reg.C);
                return (8, "or c");
            case 0xB2:
                Or(reg.D);
                return (8, "or d");
            case 0xB3:
                Or(reg.E);
                return (8, "or e");
            case 0xB4:
                Or(reg.IYH);
                return (8, "or iyh");
            case 0xB5:
                Or(reg.IYL);
                return (8, "or iyl");
            case 0xB6:
                Or(GetByteMemory(reg.IY, ReadByteOpcode()));
                return (19, "or (iy+d)");
            case 0xB7:
                Or(reg.A);
                return (8, "or a");
            case 0xB8:
                Cp(reg.B);
                return (8, "cp b");
            case 0xB9:
                Cp(reg.C);
                return (8, "cp c");
            case 0xBA:
                Cp(reg.D);
                return (8, "cp d");
            case 0xBB:
                Cp(reg.E);
                return (8, "cp e");
            case 0xBC:
                Cp(reg.IYH);
                return (8, "cp iyh");
            case 0xBD:
                Cp(reg.IYL);
                return (8, "cp iyl");
            case 0xBE:
                Cp(GetByteMemory(reg.IY, ReadByteOpcode()));
                return (19, "cp (iy+d)");
            case 0xBF:
                Cp(reg.A);
                return (8, "cp a");
            case 0xCB:
                return InterpretFDCB();
            case 0xE1:
                Pop(ref reg.IY);
                return (14, "pop iy");
            case 0xE3:
                Ex(ref GetUShortMemoryRef(reg.SP), ref reg.IY);
                return (23, "ex (sp),iy");
            case 0xE5:
                Push(reg.IY);
                return (15, "push iy");
            case 0xE9:
                Jp(reg.IY);
                return (8, "jp (iy)");
            case 0xF9:
                Ld(ref reg.SP, reg.IY);
                return (10, "ld sp,iy");
            default:
                //throw new Exception($"\{op:X2\}");
                Console.WriteLine($"Unknown operation: FD {op:X2}");
                return (0, "UNKNOWN");
        }
    }
}
