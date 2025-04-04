//This file is autogenerated, do not change it
#pragma warning disable CS0168 // Variable is declared but never used
#pragma warning disable CS0162 // Unreachable code detected
namespace ZX.Core.Cpu;

public partial class CpuRuntime
{
    private (int, string) InterpretED()
    {
        bool isFast; //cannot be declared inside multiple switch cases
        var op = ReadByteOpcode();

        switch (op)
        {
            case 0x00:
                In0(ref reg.B, ReadByteOpcode());
                return (12, "in0 b,(n)");
            case 0x01:
                Out0(ReadByteOpcode(), reg.B);
                return (13, "out0 (n),b");
            case 0x04:
                Neg();
                return (8, "neg");
            case 0x08:
                In0(ref reg.C, ReadByteOpcode());
                return (12, "in0 c,(n)");
            case 0x09:
                Out0(ReadByteOpcode(), reg.C);
                return (13, "out0 (n),c");
            case 0x0C:
                Neg();
                return (8, "neg");
            case 0x10:
                In0(ref reg.D, ReadByteOpcode());
                return (12, "in0 d,(n)");
            case 0x11:
                Out0(ReadByteOpcode(), reg.D);
                return (13, "out0 (n),d");
            case 0x14:
                Neg();
                return (8, "neg");
            case 0x18:
                In0(ref reg.E, ReadByteOpcode());
                return (12, "in0 e,(n)");
            case 0x19:
                Out0(ReadByteOpcode(), reg.E);
                return (13, "out0 (n),e");
            case 0x1C:
                Neg();
                return (8, "neg");
            case 0x20:
                In0(ref reg.H, ReadByteOpcode());
                return (12, "in0 h,(n)");
            case 0x21:
                Out0(ReadByteOpcode(), reg.H);
                return (13, "out0 (n),h");
            case 0x24:
                Neg();
                return (8, "neg");
            case 0x28:
                In0(ref reg.L, ReadByteOpcode());
                return (12, "in0 l,(n)");
            case 0x29:
                Out0(ReadByteOpcode(), reg.L);
                return (13, "out0 (n),l");
            case 0x2C:
                Neg();
                return (8, "neg");
            case 0x34:
                Neg();
                return (8, "neg");
            case 0x38:
                In0(ref reg.A, ReadByteOpcode());
                return (12, "in0 a,(n)");
            case 0x39:
                Out0(ReadByteOpcode(), reg.A);
                return (13, "out0 (n),a");
            case 0x3C:
                Neg();
                return (8, "neg");
            case 0x40:
                In(ref reg.B);
                return (12, "in b,(c)");
            case 0x41:
                Out(reg.B);
                return (12, "out (c),b");
            case 0x42:
                Sbc(ref reg.HL, reg.BC);
                return (15, "sbc hl,bc");
            case 0x43:
                Ld(ref GetUShortMemoryRef(ReadUShortOpcode()), reg.BC);
                return (20, "ld (nn),bc");
            case 0x44:
                Neg();
                return (8, "neg");
            case 0x45:
                Retn();
                return (14, "retn");
            case 0x46:
                Im(0);
                return (8, "im 0");
            case 0x47:
                Ld(ref reg.I, reg.A);
                return (9, "ld i,a");
            case 0x48:
                In(ref reg.C);
                return (12, "in c,(c)");
            case 0x49:
                Out(reg.C);
                return (12, "out (c),c");
            case 0x4A:
                Adc(ref reg.HL, reg.BC);
                return (15, "adc hl,bc");
            case 0x4B:
                Ld(ref reg.BC, GetUShortMemory(ReadUShortOpcode()));
                return (20, "ld bc,(nn)");
            case 0x4C:
                Neg();
                return (8, "neg");
            case 0x4D:
                Reti();
                return (14, "reti");
            case 0x4E:
                Im(0);
                return (8, "im 0");
            case 0x4F:
                Ld(ref reg.R, reg.A);
                return (9, "ld r,a");
            case 0x50:
                In(ref reg.D);
                return (12, "in d,(c)");
            case 0x51:
                Out(reg.D);
                return (12, "out (c),d");
            case 0x52:
                Sbc(ref reg.HL, reg.DE);
                return (15, "sbc hl,de");
            case 0x53:
                Ld(ref GetUShortMemoryRef(ReadUShortOpcode()), reg.DE);
                return (20, "ld (nn),de");
            case 0x54:
                Neg();
                return (8, "neg");
            case 0x55:
                Retn();
                return (14, "retn");
            case 0x56:
                Im(1);
                return (8, "im 1");
            case 0x57:
                Ldreg(ref reg.A, reg.I);
                return (9, "ldreg a,i");
            case 0x58:
                In(ref reg.E);
                return (12, "in e,(c)");
            case 0x59:
                Out(reg.E);
                return (12, "out (c),e");
            case 0x5A:
                Adc(ref reg.HL, reg.DE);
                return (15, "adc hl,de");
            case 0x5B:
                Ld(ref reg.DE, GetUShortMemory(ReadUShortOpcode()));
                return (20, "ld de,(nn)");
            case 0x5C:
                Neg();
                return (8, "neg");
            case 0x5D:
                Retn();
                return (14, "retn");
            case 0x5E:
                Im(2);
                return (8, "im 2");
            case 0x5F:
                Ldreg(ref reg.A, reg.R);
                return (9, "ldreg a,r");
            case 0x60:
                In(ref reg.H);
                return (12, "in h,(c)");
            case 0x61:
                Out(reg.H);
                return (12, "out (c),h");
            case 0x62:
                Sbc(ref reg.HL, reg.HL);
                return (15, "sbc hl,hl");
            case 0x63:
                Ld(ref GetUShortMemoryRef(ReadUShortOpcode()), reg.HL);
                return (20, "ld (nn),hl");
            case 0x64:
                Neg();
                return (8, "neg");
            case 0x65:
                Retn();
                return (14, "retn");
            case 0x66:
                Im(0);
                return (8, "im 0");
            case 0x67:
                Rrd();
                return (18, "rrd");
            case 0x68:
                In(ref reg.L);
                return (12, "in l,(c)");
            case 0x69:
                Out(reg.L);
                return (12, "out (c),l");
            case 0x6A:
                Adc(ref reg.HL, reg.HL);
                return (15, "adc hl,hl");
            case 0x6B:
                Ld(ref reg.HL, GetUShortMemory(ReadUShortOpcode()));
                return (20, "ld hl,(nn)");
            case 0x6C:
                Neg();
                return (8, "neg");
            case 0x6D:
                Retn();
                return (14, "retn");
            case 0x6E:
                Im(0);
                return (8, "im 0");
            case 0x6F:
                Rld();
                return (18, "rld");
            case 0x70:
                In();
                return (12, "in (c)");
            case 0x71:
                Out(0);
                return (12, "out (c),0");
            case 0x72:
                Sbc(ref reg.HL, reg.SP);
                return (15, "sbc hl,sp");
            case 0x73:
                Ld(ref GetUShortMemoryRef(ReadUShortOpcode()), reg.SP);
                return (20, "ld (nn),sp");
            case 0x74:
                Neg();
                return (8, "neg");
            case 0x75:
                Retn();
                return (15, "retn");
            case 0x76:
                Slp();
                return (8, "slp");
            case 0x78:
                In(ref reg.A);
                return (12, "in a,(c)");
            case 0x79:
                Out(reg.A);
                return (12, "out (c),a");
            case 0x7A:
                Adc(ref reg.HL, reg.SP);
                return (15, "adc hl,sp");
            case 0x7B:
                Ld(ref reg.SP, GetUShortMemory(ReadUShortOpcode()));
                return (20, "ld sp,(nn)");
            case 0x7C:
                Neg();
                return (8, "neg");
            case 0x7D:
                Retn();
                return (14, "retn");
            case 0x7E:
                Im(2);
                return (8, "im 2");
            case 0x83:
                Otim();
                return (14, "otim");
            case 0x8B:
                Otdm();
                return (14, "otdm");
            case 0x93:
                isFast = Otimr();
                return (isFast ? 14 : 16, "otimr");
            case 0x9B:
                isFast = Otdmr();
                return (isFast ? 14 : 16, "otdmr");
            case 0xA0:
                Ldi();
                return (16, "ldi");
            case 0xA1:
                Cpi();
                return (16, "cpi");
            case 0xA2:
                Ini();
                return (16, "ini");
            case 0xA3:
                Outi();
                return (16, "outi");
            case 0xA8:
                Ldd();
                return (16, "ldd");
            case 0xA9:
                Cpd();
                return (16, "cpd");
            case 0xAA:
                Ind();
                return (16, "ind");
            case 0xAB:
                Outd();
                return (16, "outd");
            case 0xB0:
                isFast = Ldir();
                return (isFast ? 16 : 21, "ldir");
            case 0xB1:
                isFast = Cpir();
                return (isFast ? 16 : 21, "cpir");
            case 0xB2:
                isFast = Inir();
                return (isFast ? 16 : 21, "inir");
            case 0xB3:
                isFast = Otir();
                return (isFast ? 16 : 21, "otir");
            case 0xB8:
                isFast = Lddr();
                return (isFast ? 16 : 21, "lddr");
            case 0xB9:
                isFast = Cpdr();
                return (isFast ? 16 : 21, "cpdr");
            case 0xBA:
                isFast = Indr();
                return (isFast ? 16 : 21, "indr");
            case 0xBB:
                isFast = Otdr();
                return (isFast ? 16 : 21, "otdr");
            default:
                //throw new Exception($"\{op:X2\}");
                Console.WriteLine($"Unknown operation: ED {op:X2}");
                return (0, "UNKNOWN");
        }
    }
}
