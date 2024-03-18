using ZX.Core.Cpu;

namespace ZX.Core.FileFormats;

public class SnaFileFormat
{
    /*
    Offset   Size   Description
       ------------------------------------------------------------------------
       0        1      byte   I
       1        8      word   HL',DE',BC',AF'
       9        10     word   HL,DE,BC,IY,IX
       $13/19   1      byte   Interrupt (bit 2 contains IFF2, 1=EI/0=DI)
       20       1      byte   R
       21       4      words  AF,SP
       $19/25   1      byte   IntMode (0=IM0/1=IM1/2=IM2)
       26       1      byte   BorderColor (0..7, not used by Spectrum 1.7)
       27       49152  bytes  RAM dump 16384..65535
       ------------------------------------------------------------------------
       Total: 49179 bytes
    */

    /*
    $00  I
    $01  HL'    
    $03  DE'
    $05  BC'
    $07  AF'
    $09  HL
    $0B  DE
    $0D  BC
    $0F  IY
    $11  IX
    $13  IFF2    [Only bit 2 is defined: 1 for EI, 0 for DI]
    $14  R
    $15  AF
    $17  SP
    $19  Interrupt mode: 0, 1 or 2
    $1A  Border colour
    */

    public (Registers, byte[]) Load(string filename)
        => Load(File.ReadAllBytes(filename));

    public (Registers, byte[]) Load(byte[] data)
    {
        var reg = new Registers
        {
            I = data[0],

            HLs = ToUShort(data, 1),
            DEs = ToUShort(data, 3),
            BCs = ToUShort(data, 5),
            AFs = ToUShort(data, 7),

            HL = ToUShort(data, 9),
            DE = ToUShort(data, 0xB),
            BC = ToUShort(data, 0xD),
            IY = ToUShort(data, 0xF),
            IX = ToUShort(data, 0x11),

            IFF2 = ((data[0x13] >> 2) & 1) == 1, //bit 2

            R = data[0x14],
            AF = ToUShort(data, 0x15),
            SP = ToUShort(data, 0x17),

            InterruptMode = data[0x19]
        };

        return (reg, data[0x1B..]);
    }

    private static ushort ToUShort(byte[] array, int index)
        => (ushort)(array[index + 1] << 8 | array[index]);

    public void Save(string filename, Registers reg, byte[] memory)
    {
        var bin = Save(reg, memory);
        File.WriteAllBytes(filename, bin);
    }

    private byte[] Save(Registers reg, byte[] memory)
    {
        var output = new byte[49179];

        PutRegIntoArray(reg, output);

        Array.Copy(memory, 0x4000, output, 0x1B, memory.Length - 0x4000);

        return output;
    }

    private static void PutRegIntoArray(Registers reg, byte[] output)
    {
        output[0] = reg.I;

        FromUShort(output, 1, reg.HLs);
        FromUShort(output, 3, reg.DEs);
        FromUShort(output, 5, reg.BCs);
        FromUShort(output, 7, reg.AFs);

        FromUShort(output, 9, reg.HL);
        FromUShort(output, 0xB, reg.DE);
        FromUShort(output, 0xD, reg.BC);
        FromUShort(output, 0xF, reg.IY);
        FromUShort(output, 0x11, reg.IX);

        byte bits = (byte)((reg.IFF2 ? 1 : 0) << 2);
        output[0x13] = bits;

        output[0x14] = reg.R;

        FromUShort(output, 0x15, reg.AF);
        FromUShort(output, 0x17, reg.SP);

        output[0x19] = reg.InterruptMode;
    }

    private static void FromUShort(byte[] array, int index, ushort value)
    {
        array[index] = (byte)(value & 0xFF);
        array[index + 1] = (byte)(value >> 8);
    }
}
