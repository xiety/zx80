using System.Device.Gpio;

namespace ZX.Platform.Pi;

public sealed class GpioKeyboardAdapter : KeyboardAdapterBase, IDisposable
{
    private readonly GpioController gpio;

    private const int PinUp = 6, PinDown = 19, PinLeft = 5, PinRight = 26;
    private const int PinKey1 = 21, PinKey2 = 20, PinKey3 = 16;

    public GpioKeyboardAdapter(byte[] input) : base(input)
    {
        gpio = new GpioController();
        int[] pins = [PinUp, PinDown, PinLeft, PinRight, PinKey1, PinKey2, PinKey3];

        foreach (var pin in pins)
            gpio.OpenPin(pin, PinMode.InputPullUp);
    }

    public void Update()
    {
        ProcessPin(PinUp, ZxKey.Q);
        ProcessPin(PinDown, ZxKey.A);
        ProcessPin(PinLeft, ZxKey.O);
        ProcessPin(PinRight, ZxKey.P);

        ProcessPin(PinKey1, ZxKey.Space);
        ProcessPin(PinKey2, ZxKey.Enter);
        ProcessPin(PinKey3, ZxKey.Shift);
    }

    private void ProcessPin(int pin, ZxKey key, ZxKey modifier = ZxKey.None)
    {
        var isPressed = gpio.Read(pin) == PinValue.Low;
        if (isPressed)
        {
            if (modifier != ZxKey.None)
                SetKeyDown(modifier);
            SetKeyDown(key);
        }
        else
        {
            SetKeyUp(key);
            if (modifier != ZxKey.None)
                SetKeyUp(modifier);
        }
    }

    public void Dispose()
        => gpio.Dispose();
}
