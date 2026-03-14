using System.Device.Gpio;
using System.Device.Spi;

namespace ZX.Platform.Pi;

public sealed class St7789Display : IDisposable
{
    private readonly SpiDevice spi;
    private readonly GpioController gpio;
    private readonly int dcPin;
    private readonly int rstPin;
    private readonly int blPin;
    private readonly byte[] buffer;
    private readonly ushort width;
    private readonly ushort height;
    private const int MaxSpiChunkSize = 4096;

    public byte[] Buffer => buffer;

    public St7789Display(ushort width = 240, ushort height = 240, int dc = 25, int rst = 27, int bl = 24)
    {
        this.width = width;
        this.height = height;
        dcPin = dc;
        rstPin = rst;
        blPin = bl;
        buffer = new byte[width * height * 2];
        gpio = new GpioController();

        gpio.OpenPin(dcPin, PinMode.Output);
        gpio.OpenPin(rstPin, PinMode.Output);
        gpio.OpenPin(blPin, PinMode.Output);
        gpio.Write(blPin, PinValue.High);

        var settings = new SpiConnectionSettings(0, 0) { ClockFrequency = 40_000_000 };
        spi = SpiDevice.Create(settings);

        gpio.Write(rstPin, PinValue.High);
        Thread.Sleep(100);
        gpio.Write(rstPin, PinValue.Low);
        Thread.Sleep(100);
        gpio.Write(rstPin, PinValue.High);
        Thread.Sleep(100);

        SendCommand(Register.SleepOut);
        Thread.Sleep(120);

        SendCommand(Register.MadCtl, 0x70);
        SendCommand(Register.ColMod, 0x05);
        SendCommand(Register.PorchSet, 0x0C, 0x0C, 0x00, 0x33, 0x33);
        SendCommand(Register.GateCtrl, 0x00);
        SendCommand(Register.VcomSet, 0x3F);
        SendCommand(Register.LcmCtrl, 0x2C);
        SendCommand(Register.VdvVrhEn, 0x01);
        SendCommand(Register.VrhSet, 0x0D);
        SendCommand(Register.FrCtrl, 0x0F);
        SendCommand(Register.PwrCtrl1, 0xA7);
        SendCommand(Register.PwrCtrl1, 0xA4, 0xA1);
        SendCommand(Register.UnknownD6, 0xA1);
        SendCommand(Register.PvGamma, 0xF0, 0x00, 0x02, 0x01, 0x00, 0x00, 0x27, 0x43, 0x3F, 0x33, 0x0E, 0x0E, 0x26, 0x2E);
        SendCommand(Register.NvGamma, 0xF0, 0x07, 0x0D, 0x0D, 0x0B, 0x16, 0x26, 0x43, 0x3E, 0x3F, 0x19, 0x19, 0x31, 0x3A);
        SendCommand(Register.InversionOn);
        SendCommand(Register.DispOn);
    }

    public void SendCommand(Register register, params ReadOnlySpan<byte> data)
    {
        gpio.Write(dcPin, PinValue.Low);
        spi.WriteByte((byte)register);

        if (data.Length > 0)
        {
            gpio.Write(dcPin, PinValue.High);
            for (var i = 0; i < data.Length; i += MaxSpiChunkSize)
            {
                var end = Math.Min(i + MaxSpiChunkSize, data.Length);
                spi.Write(data[i..end]);
            }
        }
    }

    public void Clear()
        => Array.Clear(buffer);

    public void Flush()
    {
        SendCommand(Register.CaSet, 0, 0, (byte)((width - 1) >> 8), (byte)((width - 1) & 0xFF));
        SendCommand(Register.RaSet, 0, 0, (byte)((height - 1) >> 8), (byte)((height - 1) & 0xFF));
        SendCommand(Register.MemWrite, buffer);
    }

    public void TurnOff()
    {
        Clear();
        Flush();
        SendCommand(Register.DisplayOff);
        SendCommand(Register.SleepIn);
        gpio.Write(blPin, PinValue.Low);
    }

    public void Dispose()
    {
        TurnOff();
        spi.Dispose();
        gpio.Dispose();
    }

    public enum Register : byte
    {
        SleepIn = 0x10,
        SleepOut = 0x11,
        InversionOn = 0x21,
        DisplayOff = 0x28,
        DispOn = 0x29,
        CaSet = 0x2A,
        RaSet = 0x2B,
        MemWrite = 0x2C,
        MadCtl = 0x36,
        ColMod = 0x3A,
        PorchSet = 0xB2,
        GateCtrl = 0xB7,
        VcomSet = 0xBB,
        LcmCtrl = 0xC0,
        VdvVrhEn = 0xC2,
        VrhSet = 0xC3,
        FrCtrl = 0xC6,
        PwrCtrl1 = 0xD0,
        UnknownD6 = 0xD6,
        PvGamma = 0xE0,
        NvGamma = 0xE1
    }
}
