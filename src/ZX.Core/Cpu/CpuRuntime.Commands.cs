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

    private void Out(byte r, byte port)
    {
        ushort address = (ushort)(reg.A << 8 | port);
        OutCommon(r, address);
    }

    private void OutCommon(byte r, ushort address)
    {
        output[address] = r;
    }

    private void In()
    {
        throw new NotSupportedException("Z180");
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
    {
        r = output[address];
    }

    private void Halt()
    {
        isHalt = true;
    }

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

    //Contents of A are inverted
    private void Cpl()
    {
        reg.A = (byte)(~reg.A); //invert bits

        //flag.S not affected
        //flag.Z not affected
        //TODO: UpdateFlagH(reg.A);
        //flag.P not affected
        flag.N = true;
        //flag.C not affected
    }

    private void Daa()
    {
        //TODO: correct BCD addition or substraction
        //throw new NotSupportedException();
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

    public void Add(ref byte r, byte value)
    {
        var orig = r;
        var signedA = (sbyte)r;
        var signedB = (sbyte)value;

        r = (byte)(signedA + signedB);

        UpdateFlagS(r);
        UpdateFlagZ(r);
        (flag.C, flag.H) = Bits.CalcAddCarry(signedA, signedB);
        flag.P = SameSign(value, orig) && !SameSign(orig, r);
        flag.N = false;
    }

    private bool SameSign(byte a, byte b)
        => Sign(a) == Sign(b);

    private bool SameSign(ushort a, ushort b)
        => Sign(a) == Sign(b);

    private bool Sign(byte b)
        => (b & 0b_1000_0000) == 0b_1000_0000;

    private bool Sign(ushort b)
        => (b & 0b_1000_0000_0000_0000) == 0b_1000_0000_0000_0000;

    public void Add(ref ushort rr, ushort value)
    {
        var orig = rr;
        var signedA = (short)rr;
        var signedB = (short)value;

        rr = (ushort)(signedA + signedB);

        //flag.S is unaffected
        //flag.Z is unaffected
        (flag.C, flag.H) = Bits.CalcAddCarry(signedA, signedB);
        //flag.H = ((orig ^ value ^ rr) >> 8 & 0x10) != 0;
        //flag.C = (rr + value) > 0xFFFF; 
        //flag.P is unaffected? // = SameSign(value, orig) && !SameSign(orig, rr);
        flag.N = false;
    }

    public void Adc(ref byte r, byte value)
    {
        var orig = r;
        var signedA = (sbyte)r;
        var signedB = (sbyte)value;
        var carryBit = (flag.C ? 1 : 0);

        r = (byte)(signedA + signedB + carryBit);

        UpdateFlagS(r);
        UpdateFlagZ(r);
        (flag.C, flag.H) = Bits.CalcAddCarry(signedA, signedB);
        flag.P = SameSign(orig, value) && !SameSign(orig, r);
        flag.N = false;
    }

    public void Adc(ref ushort r, ushort value)
    {
        throw new NotImplementedException();
    }

    public void Sub(byte value)
    {
        Sub(ref reg.A, value);
    }

    public void Sub(ref byte r, byte value)
    {
        r = SubCommon(r, value);
    }

    private byte SubCommon(byte r, byte value)
    {
        var signedA = (sbyte)r;
        var signedB = (sbyte)value;

        var result = (byte)(signedA - signedB);

        UpdateFlagSZU(result);
        (flag.C, flag.H) = Bits.CalcSubCarry(signedA, signedB);
        flag.P = !SameSign(r, value) && SameSign(value, result);
        flag.N = true;

        return result;
    }

    public void Sbc(ref byte r, byte value)
    {
        var orig = r;
        var signedA = (sbyte)r;
        var signedB = (sbyte)value;
        var carryBit = (flag.C ? 1 : 0);

        r = (byte)(signedA - (signedB + carryBit));

        UpdateFlagSZU(r);
        (flag.C, flag.H) = Bits.CalcSubCarry(signedA, signedB); //TODO
        flag.P = false; //TODO: HOW TO?
        flag.N = false;
    }

    public void Sbc(ref ushort rr, ushort value)
    {
        //var signedA = (short)rr;
        //var signedB = (short)value;

        //var carryBit = (flag.C ? 1 : 0);
        //rr = (ushort)(rr - value - carryBit);

        //flag.H - borrow from bit 12
        (rr, flag.C, flag.H) = Bits.Sub(rr, value, flag.C, 12);

        UpdateFlagS(rr);
        UpdateFlagZ(rr);
        flag.N = true;
    }

    public void And(byte value)
    {
        And(ref reg.A, value);
    }

    public void And(ref byte r, byte value)
    {
        r = (byte)(r & value);

        UpdateFlagSZU(r);
        flag.C = false;
        flag.H = true;
        UpdateFlagPWithParity(r);
        flag.N = false;
    }

    public void Xor(byte value)
    {
        Xor(ref reg.A, value);
    }

    public void Xor(ref byte r, byte value)
    {
        r = (byte)(r ^ value);

        UpdateFlagS(r);
        UpdateFlagZ(r);
        flag.H = false;
        UpdateFlagPWithParity(r);
        flag.N = false;
        flag.C = false;
    }

    public void Or(byte value)
        => Or(ref reg.A, value);

    public void Or(ref byte r, byte value)
    {
        r = (byte)(r | value);

        UpdateFlagS(r);
        UpdateFlagZ(r);
        flag.H = false;
        flag.P = false; //TODO: how?
        flag.N = false;
        flag.C = false;
    }

    public void Cp(byte value)
        => Cp(ref reg.A, value);

    public void Cp(ref byte r, byte value)
    {
        r = CpCommon(r, value);
    }

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

    private byte CpCommon(byte r, byte value)
    {
        //call sub, ignore result
        SubCommon(r, value);
        return r;
    }

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
    {
        (a, b) = (b, a);
    }

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
    {
        r = val;
    }

    private void Ld(ref ushort rr, ushort val)
    {
        rr = val;
    }

    private void Inc(ref byte r)
    {
        byte orig = r;

        r += 1;

        UpdateFlagSZU(r);
        //flag.C is unaffected
        flag.H = (orig & 0b_0000_1111) == 0b_0000_1111;
        flag.N = false;
        flag.P = orig == 0b_01111_1111;
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
    {
        r = RlcCommon(r);
    }

    private void Rlca()
    {
        reg.A = RlcCommon(reg.A);
    }

    private byte RlcCommon(byte r)
    {
        flag.C = (r & 0b_1000_0000) == 0b_1000_0000;

        byte ret = (byte)(r << 1);

        if (flag.C)
            ret |= 0b_0000_0001;

        UpdateFlagS(r);
        UpdateFlagZ(r);
        flag.H = false;
        UpdateFlagPWithParity(r);
        flag.N = false;

        return ret;
    }

    private void Rl(ref byte r)
    {
        r = RlCommon(r);

        UpdateFlagPWithParity(r);
        UpdateFlagZ(r);
        UpdateFlagS(r);
    }

    private void Rla()
    {
        reg.A = RlCommon(reg.A);
    }

    private byte RlCommon(byte r)
    {
        var leaving = (r & 0b_1000_0000) == 0b_1000_0000;

        byte ret = r <<= 1;

        if (flag.C)
            ret |= 0b_0000_0001;

        flag.C = leaving;

        flag.N = false;
        flag.H = false;

        return ret;
    }

    private void Rr(ref byte r)
    {
        r = RrCommon(r);

        UpdateFlagPWithParity(r);
        UpdateFlagZ(r);
        UpdateFlagS(r);
    }

    private void Rra()
    {
        reg.A = RrCommon(reg.A);
    }

    private byte RrCommon(byte r)
    {
        var leaving = (r & 0b_0000_0001) == 0b_0000_0001;

        byte ret = (byte)(r >> 1);

        if (flag.C)
            ret |= 0b_1000_0000;

        flag.C = leaving;

        flag.N = false;
        flag.H = false;

        return ret;
    }

    /* RRC */

    private void Rrc(byte a, ref byte r)
    {
        r = RrcCommon(a);
    }

    public void Rrc(ref byte r)
    {
        r = RrcCommon(r); //CHN flags

        UpdateFlagSZU(r);
        UpdateFlagPWithParity(r);
    }

    private void Rrca()
    {
        reg.A = RrcCommon(reg.A);
    }

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
    {
        JrCommon(ReadByteOpcode());
    }

    private void JrCommon(byte offset)
    {
        reg.PC = (ushort)(reg.PC + unchecked((sbyte)offset));
    }

    private void Jp(ushort address)
    {
        JpCommon(address);
    }

    private void Jp(bool condition)
    {
        var address = ReadUShortOpcode();

        if (condition)
            JpCommon(address);
    }

    private void Jp()
    {
        JpCommon(ReadUShortOpcode());
    }

    private void JpCommon(ushort address)
    {
        reg.PC = address;
    }

    private void Rst(byte r)
    {
        CallCommon((ushort)r);
    }

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
        if (callStackDebug.Any())
            callStackDebug.Pop();

        Pop(ref reg.PC);
        currentDepthDebug--;
    }

    private void Rrd()
    {
        throw new NotImplementedException();
    }

    private void Rld()
    {
        ref byte r = ref GetByteMemoryRef(reg.HL);

        byte n_lo = (byte)(r & 0b_0000_1111);
        byte n_hi = (byte)((r & 0b_1111_0000) >> 4);

        byte a_lo = (byte)(reg.A & 0b_0000_1111);

        reg.A &= 0b_1111_0000;
        reg.A |= n_hi;

        r = 0;
        r |= n_lo;
        r |= (byte)(a_lo << 4);

        UpdateFlagS(reg.A);
        UpdateFlagZ(reg.A);
        flag.H = false;
        UpdateFlagPWithParity(reg.A);
        flag.N = false;
        //flag.C is unnafected
    }

    private void Rlc(byte a, ref byte r)
    {
        r = RlcCommon(a);
    }

    private void Rl(byte a, ref byte r)
    {
        r = RlCommon(a);
    }

    private void Rr(byte a, ref byte r)
    {
        r = RrCommon(a);
    }

    private void Sla(byte a, ref byte r)
    {
        r = SlaCommon(a);
    }

    private void Sra(byte a, ref byte r)
    {
        r = SraCommon(a);
    }

    private void Sll(byte a, ref byte r)
    {
        r = SllCommon(a);
    }

    private void Srl(byte a, ref byte r)
    {
        r = SrlCommon(a);
    }

    private void Res(int index, byte a, ref byte r)
    {
        r = ResCommon(index, a);
    }

    private void Set(int index, byte a, ref byte r)
    {
        r = SetCommon(index, a);
    }

    private void Set(int index, ref byte r)
    {
        r = SetCommon(index, r);
    }

    private byte SetCommon(int index, byte r)
    {
        var ret = (byte)(r | (1 << index));
        return ret;
    }

    private void Res(int index, ref byte r)
    {
        r = ResCommon(index, r);
    }

    private byte ResCommon(int index, byte r)
    {
        var ret = (byte)(r & ~(1 << index));
        return ret;
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
    {
        r = SrlCommon(r);
    }

    //Move to right, right bit to carry
    private byte SrlCommon(byte r)
    {
        flag.C = (r & 1) == 1;

        byte ret = (byte)(r >> 1);

        flag.S = false;
        UpdateFlagZ(ret);
        flag.H = false;
        UpdateFlagPWithParity(ret);
        flag.N = false;

        return ret;
    }

    private void Sll(ref byte r)
    {
        r = SllCommon(r);
    }

    private byte SllCommon(byte r)
    {
        flag.C = (r & 0b_1000_000) == 0b_1000_000;
        byte ret = (byte)((r << 1) | 1); //set bit 0
        return ret;
    }

    private void Sra(ref byte r)
    {
        r = SraCommon(r);
    }

    private byte SraCommon(byte r)
    {
        flag.C = (r & 1) == 1;
        byte ret = (byte)((r >> 1) | (r & 0b_1000_0000)); //7 bit unchanged
        return ret;
    }

    private void Sla(ref byte r)
    {
        r = SlaCommon(r);
    }

    private byte SlaCommon(byte r)
    {
        flag.C = (r & 0b_1000_0000) == 0b_1000_0000;
        byte ret = (byte)(r << 1);
        return ret;
    }

    private ref byte GetPort(byte r)
    {
        throw new NotImplementedException();
    }

    private bool Otdr()
    {
        throw new NotImplementedException();
    }

    private bool Indr()
    {
        throw new NotImplementedException();
    }

    private bool Cpdr()
    {
        throw new NotImplementedException();
    }

    private bool Lddr()
    {
        Ld(ref GetByteMemoryRef(reg.DE), GetByteMemoryRef(reg.HL));

        Dec(ref reg.HL);
        Dec(ref reg.DE);
        Dec(ref reg.BC);

        if (reg.BC != 0)
        {
            reg.PC -= 2;
        }

        //flag.S is unaffected
        //flag.Z is unaffected
        flag.H = false;
        flag.P = false;
        flag.N = false;
        //flag.C - nothing in manual

        return false; //TODO
    }

    private bool Otir()
    {
        throw new NotImplementedException();
    }

    private bool Inir()
    {
        throw new NotImplementedException();
    }

    //TODO: p/v flag on block transfer instructions

    private bool Cpir()
    {
        var mem = GetByteMemoryRef(reg.HL);

        Inc(ref reg.HL);
        Dec(ref reg.BC);

        if (reg.BC != 0 && mem != reg.A)
        {
            reg.PC -= 2; //repeat this command
        }

        //flag.S //TODO: ???
        flag.Z = mem == reg.A;
        //flag.H //TODO: ???
        flag.P = reg.BC != 0;
        flag.N = true;
        //flag.C is unaffected

        return false; //TODO
    }

    private bool Ldir()
    {
        //TODO: two refresh cycles

        Ld(ref GetByteMemoryRef(reg.DE), GetByteMemoryRef(reg.HL));
        Inc(ref reg.DE);
        Inc(ref reg.HL);
        Dec(ref reg.BC);

        if (reg.BC != 0)
        {
            reg.PC -= 2; //repeat this command
        }

        //flag.S is unaffected
        //flag.Z is unaffected
        flag.H = false;
        flag.P = false;
        flag.N = false;
        //flag.C is unaffected

        return false; //TODO
    }

    private void Ind()
    {
        throw new NotImplementedException();
    }

    private void Outd()
    {
        throw new NotImplementedException();
    }

    private void Cpd()
    {
        throw new NotImplementedException();
    }

    private void Ldd()
    {
        throw new NotImplementedException();
    }

    private void Outi()
    {
        throw new NotImplementedException();
    }

    private void Ini()
    {
        throw new NotImplementedException();
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
        throw new NotImplementedException();
    }

    private bool Otdmr()
    {
        throw new NotImplementedException();
    }

    private bool Otdm()
    {
        throw new NotImplementedException();
    }

    private bool Otim()
    {
        throw new NotImplementedException();
    }

    private bool Slp()
    {
        throw new NotImplementedException();
    }

    private bool Tstio(byte v)
    {
        throw new NotImplementedException();
    }

    private void Reti()
    {
        reg.IFF1 = reg.IFF2;
        Ret();
    }

    private bool Mlt(ref ushort bC)
    {
        throw new NotImplementedException();
    }

    private void Im(byte mode)
    {
        reg.InterruptMode = mode;
    }

    public void Retn()
    {
        reg.IFF1 = reg.IFF1 ^ reg.IFF2;
        Ret();
    }

    private void Neg()
    {
        var original = reg.A;

        reg.A = SubCommon(0, reg.A);

        flag.P = (original == 0x80);
        flag.C = (original != 0x00);
    }

    private void Out0(byte port, byte value) //Z180
    {
        throw new NotSupportedException("Z180");
    }

    private void In(byte port)
    {
        throw new NotSupportedException("Z180");
    }

    private void In0(ref byte r, byte port) //Z180
    {
        throw new NotSupportedException("Z180");
    }

    private void Tst(byte value) //Z180
    {
        throw new NotSupportedException("Z180");
    }
}
