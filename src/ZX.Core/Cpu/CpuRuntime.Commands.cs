using ZX.Core.Math;

namespace ZX.Core.Cpu;

public partial class CpuRuntime
{
    private void Di()
    {
        reg.IFF1 = false;
        reg.IFF2 = false;
    }

    private void Ei()
    {
        reg.IFF1 = true;
        reg.IFF2 = true;
    }

    private void Out(byte r)
    {
        var address = reg.BC;
        OutCommon(r, address);
    }

    private void Out(byte low, byte high)
    {
        ushort address = (ushort)(low | high << 8);
        OutCommon(reg.A, address);
    }

    private void OutCommon(byte r, ushort address)
    {
        var low = (byte)(address & 0b11111111);
        output[low] = r;
        OnOutput?.Invoke(low, r);
    }

    private void In()
    {
        NotImplemented();
    }

    private void In(ref byte r)
    {
        var address = reg.BC;
        InCommon(ref r, address);
    }

    private void In(ref byte r, byte port)
    {
        ushort address = (ushort)(reg.A << 8 | port);
        InCommon(ref r, address);
    }

    private void InCommon(ref byte r, ushort address)
        => r = output[address];

    private void Halt()
        => isHalt = true;

    private void Ccf()
    {
        //flag.S not affected
        //flag.Z not affected
        flag.H = flag.C;
        //flag.P not affected
        flag.N = false;
        flag.C = !flag.C;
    }

    private void Scf()
    {
        //flag.S not affected
        //flag.Z not affected
        flag.H = false;
        //flag.P not affected
        flag.N = false;
        flag.C = true;
    }

    private void Cpl()
    {
        reg.A = (byte)(~reg.A); //invert bits

        //flag.S not affected
        //flag.Z not affected
        flag.H = true;
        //flag.P not affected
        flag.N = true;
        //flag.C not affected
    }

    private void Daa()
    {
        flag.C |= (reg.A > 0x99);

        byte correction = flag.C ? (byte)0x60 : (byte)0;

        if (flag.H || (reg.A & 0x0F) > 0x09)
            correction |= 0x06;

        flag.H = flag.N
            ? (reg.A & 0x0F) < (correction & 0x0F)
            : ((reg.A & 0x0F) + (correction & 0x0F)) > 0x0F;

        reg.A = flag.N
            ? (byte)(reg.A - correction)
            : (byte)(reg.A + correction);

        UpdateFlagSZUP(reg.A);
    }

    private bool Djnz()
    {
        var fast = true;

        var offset = ReadByteOpcode();

        reg.B -= 1;

        if (reg.B != 0)
        {
            //why not step back here?
            JrCommon(offset);
            fast = false;
        }

        return fast;
    }

    private bool SameSign(byte a, byte b)
        => Sign(a) == Sign(b);

    private bool SameSign(ushort a, ushort b)
        => Sign(a) == Sign(b);

    private bool Sign(byte b)
        => (b & 0b_1000_0000) == 0b_1000_0000;

    private bool Sign(ushort b)
        => (b & 0b_1000_0000_0000_0000) == 0b_1000_0000_0000_0000;

    public void Add(ref byte r, byte value)
        => AddCommon(ref r, value, false);

    public void Adc(ref byte r, byte value)
        => AddCommon(ref r, value, true);

    private void AddCommon(ref byte r, byte value, bool useCarry)
    {
        var orig = r;

        var carryBit = useCarry && flag.C ? (byte)1 : (byte)0;
        r = (byte)(r + value + carryBit);

        UpdateFlagSZU(r);
        (flag.C, flag.H) = Bits.CalcAddCarry(orig, value, carryBit);
        flag.P = SameSign(orig, value) && !SameSign(orig, r);
        flag.N = false;
    }

    public void Add(ref ushort rr, ushort value)
        => AddCommon(ref rr, value, false, flags: false);

    public void Adc(ref ushort rr, ushort value)
        => AddCommon(ref rr, value, true, flags: true);

    private void AddCommon(ref ushort rr, ushort value, bool useCarry, bool flags)
    {
        var orig = rr;

        var carryBit = (useCarry && flag.C ? (byte)1 : (byte)0);

        rr = (ushort)(rr + value + carryBit);

        (flag.C, flag.H) = Bits.CalcAddCarry(orig, value, carryBit);
        flag.N = false;

        if (flags)
        {
            UpdateFlagSZU(rr);
            flag.P = SameSign(orig, value) && !SameSign(orig, rr);
        }
    }

    public void Sub(byte value)
        => reg.A = SubCommon(reg.A, value, false);

    private byte SubCommon(byte r, byte value, bool useCarry)
    {
        var carryBit = useCarry && flag.C ? (byte)1 : (byte)0;

        var result = (byte)(r - value - carryBit);

        UpdateFlagSZU(result);
        (flag.C, flag.H) = Bits.CalcSubCarry(r, value, carryBit);
        flag.P = !SameSign(r, value) && SameSign(value, result);
        flag.N = true;

        return result;
    }

    public void Sbc(ref byte r, byte value)
        => r = SubCommon(r, value, true);

    public void Sbc(ref ushort rr, ushort value)
        => SubCommon(ref rr, value, true);

    private void SubCommon(ref ushort rr, ushort value, bool useCarry)
    {
        ushort result;

        (result, flag.C, flag.H) = Bits.Sub(rr, value, useCarry & flag.C, 12);

        UpdateFlagSZU(result);
        flag.N = true;
        flag.P = !SameSign(rr, value) && SameSign(value, result);

        rr = result;
    }

    public void And(byte value)
        => And(ref reg.A, value);

    public void And(ref byte r, byte value)
    {
        r = (byte)(r & value);

        UpdateFlagSZUP(r);
        flag.C = false;
        flag.H = true;
        flag.N = false;
    }

    public void Xor(byte value)
        => Xor(ref reg.A, value);

    public void Xor(ref byte r, byte value)
    {
        r = (byte)(r ^ value);

        UpdateFlagSZUP(r);
        flag.H = false;
        flag.N = false;
        flag.C = false;
    }

    public void Or(byte value)
        => Or(ref reg.A, value);

    public void Or(ref byte r, byte value)
    {
        r = (byte)(r | value);

        UpdateFlagSZUP(r);
        flag.H = false;
        flag.N = false;
        flag.C = false;
    }

    public void Cp(byte value)
        => Cp(ref reg.A, value);

    public void Cp(ref byte r, byte value)
        => CpCommon(r, value);

    private void Cpi()
    {
        var flagCbackup = flag.C;

        var mem = GetByteMemoryRef(reg.HL);

        CpCommon(reg.A, mem);
        Inc(ref reg.HL);
        Dec(ref reg.BC);

        flag.P = (reg.BC != 0);
        flag.C = flagCbackup;
    }

    private void CpCommon(byte r, byte value)
        => SubCommon(r, value, false);

    private void Ex(ref ushort a, ref ushort b)
    {
        ExCommon(ref a, ref b);
    }

    private void Exx()
    {
        ExCommon(ref reg.BC, ref reg.BCs);
        ExCommon(ref reg.DE, ref reg.DEs);
        ExCommon(ref reg.HL, ref reg.HLs);
    }

    private void ExCommon(ref ushort a, ref ushort b)
        => (a, b) = (b, a);

    public void Push(ushort r)
    {
        reg.SP -= 2;
        Ld(ref GetUShortMemoryRef(reg.SP), r);
    }

    private void Pop(ref ushort r)
    {
        Ld(ref r, GetUShortMemory(reg.SP));
        reg.SP += 2;
    }

    private void Nop()
    {
    }

    private void Ldreg(ref byte r, byte val)
    {
        r = val;

        UpdateFlagSZU(r);
        //flag.C is unaffected
        flag.H = false;
        flag.N = false;
        flag.P = reg.IFF2;
    }

    private void Ld(ref byte r, byte val)
        => r = val;

    private void Ld(ref ushort rr, ushort val)
        => rr = val;

    private void Inc(ref byte r)
    {
        byte orig = r;

        r += 1;

        UpdateFlagSZU(r);
        //flag.C is unaffected
        flag.H = (orig & 0b_0000_1111) == 0b_0000_1111;
        flag.N = false;
        flag.P = orig == 0b_0111_1111;
    }

    private void Inc(ref ushort rr)
    {
        rr += 1;
        //No flags altered
    }

    private void Dec(ref byte r)
    {
        byte orig = r;

        r -= 1;

        UpdateFlagSZU(r);
        //flag.C is unaffected
        flag.H = (r & 0b_0000_1111) == 0b_0000_1111;
        flag.N = true;
        flag.P = orig == 0b_1000_0000;
    }

    private void Dec(ref ushort rr)
    {
        rr -= 1;
        //No flags altered
    }

    private void Rlc(ref byte r)
        => RlcCommon(ref r, r, true);

    private void Rlc(byte a, ref byte r)
        => RlcCommon(ref r, a, true);

    private void Rlca()
        => RlcCommon(ref reg.A, reg.A, false);

    private void RlcCommon(ref byte r, byte a, bool flags)
    {
        flag.C = (a >> 7) == 1;

        r = (byte)(a << 1);

        if (flag.C)
            r |= 1;

        if (flags)
            UpdateFlagSZUP(r);

        UpdateFlagsXY(r);

        flag.H = false;
        flag.N = false;
    }

    private void Rl(ref byte r)
    {
        r = RlCommon(r);

        UpdateFlagSZUP(r);
    }

    private void Rla()
        => reg.A = RlCommon(reg.A);

    private byte RlCommon(byte r)
    {
        var leaving = (r >> 7) == 1;

        byte ret = (byte)(r << 1);

        if (flag.C)
            ret |= 1;

        flag.C = leaving;

        flag.N = false;
        flag.H = false;

        return ret;
    }

    private void Rr(ref byte r)
    {
        RrCommon(ref r, r);

        UpdateFlagSZUP(r);
    }

    private void Rr(byte a, ref byte r)
        => RrCommon(ref r, a);

    private void Rra()
        => RrCommon(ref reg.A, reg.A);

    private void RrCommon(ref byte r, byte a)
    {
        var leaving = (a & 0b_0000_0001) == 0b_0000_0001;

        r = (byte)(a >> 1);

        if (flag.C)
            r |= 0b_1000_0000;

        flag.C = leaving;

        flag.N = false;
        flag.H = false;
    }

    /* RRC */

    private void Rrc(byte a, ref byte r)
        => r = RrcCommon(a);

    public void Rrc(ref byte r)
    {
        r = RrcCommon(r); //CHN flags

        UpdateFlagSZUP(r);
    }

    private void Rrca()
        => reg.A = RrcCommon(reg.A);

    private byte RrcCommon(byte r)
    {
        byte curry = (byte)((r & 1) << 7);

        byte result = (byte)((r >> 1) | curry);

        flag.C = curry != 0;
        flag.H = false;
        flag.N = false;

        return result;
    }

    /* Jump */

    private bool Jr(bool condition)
    {
        var fast = true;

        var offset = ReadByteOpcode();

        if (condition)
        {
            JrCommon(offset);
            fast = false;
        }

        return fast;
    }

    private void Jr()
        => JrCommon(ReadByteOpcode());

    private void JrCommon(byte offset)
        => reg.PC = (ushort)(reg.PC + unchecked((sbyte)offset));

    private void Jp(ushort address)
        => JpCommon(address);

    private void Jp(bool condition)
    {
        var address = ReadUShortOpcode();

        if (condition)
            JpCommon(address);
    }

    private void Jp()
        => JpCommon(ReadUShortOpcode());

    private void JpCommon(ushort address)
        => reg.PC = address;

    private void Rst(byte r)
        => CallCommon((ushort)r);

    private bool Call(bool condition)
    {
        var fast = true;

        var address = ReadUShortOpcode();

        if (condition)
        {
            CallCommon(address);
            fast = false;
        }

        return fast;
    }

    private void Call()
    {
        var address = ReadUShortOpcode();

        CallCommon(address);
    }

    private void CallCommon(ushort address)
    {
        currentDepthDebug++;

        callStackDebug.Push(reg.PC);

        Push(reg.PC);

        reg.PC = address;
    }

    private bool Ret(bool condition)
    {
        var fast = true;

        if (condition)
        {
            Ret();
            fast = false;
        }

        return fast;
    }

    private void Ret()
    {
        if (callStackDebug.Count != 0)
            callStackDebug.Pop();

        Pop(ref reg.PC);
        currentDepthDebug--;
    }

    private void Rrd()
    {
        ref byte r = ref GetByteMemoryRef(reg.HL);

        byte n_lo = (byte)(r & 0x0F);
        byte n_hi = (byte)((r >> 4) & 0x0F);
        byte a_lo = (byte)(reg.A & 0x0F);

        reg.A = (byte)((reg.A & 0xF0) | n_lo);
        r = (byte)((a_lo << 4) | n_hi);

        UpdateFlagSZUP(reg.A);
        flag.H = false;
        flag.N = false;
        //flag.C is unnafected
    }

    private void Rld()
    {
        ref byte r = ref GetByteMemoryRef(reg.HL);

        byte n_lo = (byte)(r & 0x0F);
        byte n_hi = (byte)((r >> 4) & 0x0F);
        byte a_lo = (byte)(reg.A & 0x0F);

        reg.A = (byte)((reg.A & 0xF0) | n_hi);
        r = (byte)((n_lo << 4) | a_lo);

        UpdateFlagSZUP(reg.A);
        flag.H = false;
        flag.N = false;
        //flag.C is unnafected
    }

    private void Rl(byte a, ref byte r)
        => r = RlCommon(a); //TODO: change return value to ref parameter

    private void Set(int index, ref byte a, ref byte r)
    {
        SetCommon(index, ref r, a);
        a = r; //additionally write to memory
    }

    private void Set(int index, ref byte r)
        => SetCommon(index, ref r, r);

    private void SetCommon(int index, ref byte r, byte a)
    {
        r = (byte)(a | (1 << index));
    }

    private void Res(int index, ref byte r)
        => ResCommon(index, ref r, r);

    private void Res(int index, ref byte a, ref byte r)
    {
        ResCommon(index, ref r, a);
        a = r; //additionally write to memory
    }

    private void ResCommon(int index, ref byte r, byte a)
    {
        r = (byte)(a & ~(1 << index));
    }

    private void Bit(int index, byte r)
    {
        byte result = (byte)(r & (1 << index));

        //flag.C not affected
        UpdateFlagSZU(result);
        flag.H = true; //There is an error in official documentation
        flag.P = result == 0;
        flag.N = false;
    }

    private void Srl(ref byte r)
        => SrlCommon(ref r, r);

    private void Srl(ref byte a, ref byte r)
    {
        SrlCommon(ref r, a);
        a = r; //additionally write to memory
    }

    //SRO
    private void SrlCommon(ref byte r, byte a)
    {
        flag.C = (a & 1) == 1;

        r = (byte)(a >> 1);

        UpdateFlagSZUP(r);
        flag.H = false;
        flag.N = false;
    }

    private void Sll(ref byte r)
        => SllCommon(ref r, r);

    private void Sll(byte a, ref byte r)
        => SllCommon(ref r, a);

    //SLIA (undocumented)
    private void SllCommon(ref byte r, byte a)
    {
        flag.C = (a >> 7) == 1;
        r = (byte)((a << 1) | 1); //set bit 0
        UpdateFlagSZUP(r);
        flag.H = false;
        flag.N = false;
    }

    private void Sra(ref byte r)
        => SraCommon(ref r, r);

    private void Sra(byte a, ref byte r)
        => SraCommon(ref r, a);

    private void SraCommon(ref byte r, byte a)
    {
        flag.C = (a & 1) == 1;
        r = (byte)((a >> 1) | (a & 0b_1000_0000)); //7 bit unchanged
        UpdateFlagSZUP(r);
        flag.H = false;
        flag.N = false;
    }

    private void Sla(ref byte r)
        => SlaCommon(ref r, r);

    private void Sla(byte a, ref byte r)
        => SlaCommon(ref r, a);

    private void SlaCommon(ref byte r, byte a)
    {
        flag.C = (a >> 7) == 1;
        r = (byte)(a << 1);

        UpdateFlagSZUP(r);
        flag.H = false;
        flag.N = false;
    }

    private bool Otdr()
    {
        NotImplemented();
        return false;
    }

    private bool Indr()
    {
        NotImplemented();
        return false;
    }

    private bool Cpdr()
    {
        NotImplemented();
        return false;
    }

    private bool Lddr()
    {
        Ld(ref GetByteMemoryRef(reg.DE), GetByteMemoryRef(reg.HL));

        Dec(ref reg.HL);
        Dec(ref reg.DE);
        Dec(ref reg.BC);

        var isFast = true;

        if (reg.BC != 0)
        {
            reg.PC -= 2;
            isFast = false;
        }

        //flag.S is unaffected
        //flag.Z is unaffected
        flag.H = false;
        flag.P = false;
        flag.N = false;
        //flag.C - nothing in manual

        return isFast;
    }

    private bool Otir()
    {
        NotImplemented();
        return false;
    }

    private bool Inir()
    {
        NotImplemented();
        return false;
    }

    private bool Cpir()
    {
        var mem = GetByteMemoryRef(reg.HL);

        Inc(ref reg.HL);
        Dec(ref reg.BC);

        var isFast = true;

        var diff = (byte)(reg.A - mem);

        if (reg.BC != 0 && diff != 0)
        {
            reg.PC -= 2; //repeat this command
            isFast = false;
        }

        UpdateFlagS(diff); //???
        flag.Z = diff == 0;
        (_, flag.H) = Bits.CalcSubCarry(reg.A, mem, 0); //???
        flag.P = reg.BC != 0;
        flag.N = true;
        //flag.C is unaffected

        return isFast;
    }

    private bool Ldir()
    {
        //TODO: two refresh cycles

        Ld(ref GetByteMemoryRef(reg.DE), GetByteMemoryRef(reg.HL));
        Inc(ref reg.DE);
        Inc(ref reg.HL);
        Dec(ref reg.BC);

        var isFast = true;

        if (reg.BC != 0)
        {
            reg.PC -= 2; //repeat this command
            isFast = false;
        }

        //flag.S is unaffected
        //flag.Z is unaffected
        flag.H = false;
        flag.P = false;
        flag.N = false;
        //flag.C is unaffected

        return isFast;
    }

    private void Ind()
    {
        NotImplemented();
    }

    private void Outd()
    {
        NotImplemented();
    }

    private void Cpd()
    {
        NotImplemented();
    }

    private void Ldd()
    {
        NotImplemented();
    }

    private void Outi()
    {
        NotImplemented();
    }

    private void Ini()
    {
        NotImplemented();
    }

    private void Ldi()
    {
        Ld(ref GetByteMemoryRef(reg.DE), GetByteMemoryRef(reg.HL));
        Inc(ref reg.HL);
        Inc(ref reg.DE);
        Dec(ref reg.BC);

        //flag.S is unnafected
        //flag.Z is unnafected
        flag.H = false;
        flag.P = (reg.BC != 0);
        flag.N = false;
        //flag.C is unnafected
    }

    private bool Otimr()
    {
        NotImplemented();
        return false;
    }

    private bool Otdmr()
    {
        NotImplemented();
        return false;
    }

    private void Otdm()
    {
        NotImplemented();
    }

    private void Otim()
    {
        NotImplemented();
    }

    private void Slp()
    {
        NotImplemented();
    }

    private void Reti()
    {
        reg.IFF1 = reg.IFF2;
        Ret();
    }

    private void Im(byte mode)
    {
        reg.InterruptMode = mode;
    }

    public void Retn()
    {
        reg.IFF1 ^= reg.IFF2;
        Ret();
    }

    private void Neg()
    {
        var original = reg.A;

        reg.A = SubCommon(0, reg.A, false);

        flag.P = (original == 0x80);
        flag.C = (original != 0x00);
    }

    private void Out0(byte port, byte value) //Z180
    {
        NotImplemented();
    }

    private void In(byte port)
    {
        NotImplemented();
    }

    private void In0(ref byte r, byte port) //Z180
    {
        NotImplemented();
    }

    private void NotImplemented()
    {
        if (throwOnNotImplemented)
            throw new NotImplementedException();
    }
}
