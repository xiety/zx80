using System.Device.Gpio;

namespace ZX.Platform.Pi;

public sealed class GpioKempstonAdapter : KeyboardAdapterBase, IDisposable
{
    private readonly GpioController gpio;
    private readonly byte[] ioPorts;

    private const int PinUp = 6;
    private const int PinDown = 19;
    private const int PinLeft = 5;
    private const int PinRight = 26;
    private const int PinPress = 13;
    private const int PinKey1 = 21;
    private const int PinKey2 = 20;
    private const int PinKey3 = 16;

    private const ushort KempstonPort = 0x001F;

    public GpioKempstonAdapter(byte[] input) : base(input)
    {
        ioPorts = input;
        gpio = new GpioController();
        int[] pins = [PinUp, PinDown, PinLeft, PinRight, PinPress, PinKey1, PinKey2, PinKey3];

        foreach (var pin in pins)
            gpio.OpenPin(pin, PinMode.InputPullUp);
    }

    public void Update()
    {
        UpdateKempston();

        ProcessPin(PinKey3, ZxKey.Enter);
    }

    private void UpdateKempston()
    {
        byte kempstonState = 0;

        if (IsPressed(PinRight))
            kempstonState |= 0b0000_0001;
        if (IsPressed(PinLeft))
            kempstonState |= 0b0000_0010;
        if (IsPressed(PinDown))
            kempstonState |= 0b0000_0100;
        if (IsPressed(PinUp) || IsPressed(PinKey2))
            kempstonState |= 0b0000_1000;
        if (IsPressed(PinPress) || IsPressed(PinKey1))
            kempstonState |= 0b0001_0000;

        ioPorts[KempstonPort] = kempstonState;
    }

    private void ProcessPin(int pin, ZxKey key)
    {
        if (IsPressed(pin))
            SetKeyDown(key);
        else
            SetKeyUp(key);
    }

    private bool IsPressed(int pin)
        => gpio.Read(pin) == PinValue.Low;

    public void Dispose()
        => gpio.Dispose();
}
