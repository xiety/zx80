namespace ZX.Platform.Windows;

public class KeyboardAdapter
{
    private readonly byte[] output;

    private readonly Dictionary<Keys, Item> keys = [];

    public KeyboardAdapter(byte[] output)
    {
        this.output = output;

        SetupKeys(0xFEFE, Keys.ShiftKey /*Caps Shift*/, Keys.Z, Keys.X, Keys.C, Keys.V);
        SetupKeys(0xFDFE, Keys.A, Keys.S, Keys.D, Keys.F, Keys.G);
        SetupKeys(0xFBFE, Keys.Q, Keys.W, Keys.E, Keys.R, Keys.T);
        SetupKeys(0xF7FE, Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5);
        SetupKeys(0xEFFE, Keys.D0, Keys.D9, Keys.D8, Keys.D7, Keys.D6);
        SetupKeys(0xDFFE, Keys.P, Keys.O, Keys.I, Keys.U, Keys.Y);
        SetupKeys(0xBFFE, Keys.Enter, Keys.L, Keys.K, Keys.J, Keys.H);
        SetupKeys(0x7FFE, Keys.Space, Keys.ControlKey /*Symbol Shift*/, Keys.M, Keys.N, Keys.B);
    }

    private void SetupKeys(ushort address, Keys k0, Keys k1, Keys k2, Keys k3, Keys k4)
    {
        output[address] = 255;

        keys.Add(k0, new(address, 1 << 0));
        keys.Add(k1, new(address, 1 << 1));
        keys.Add(k2, new(address, 1 << 2));
        keys.Add(k3, new(address, 1 << 3));
        keys.Add(k4, new(address, 1 << 4));
    }

    public void SetKeyDown(Keys keyCode)
    {
        if (keyCode == Keys.Oem5) // Key '\'
            keyCode = Keys.Enter;

        if (keyCode == Keys.Back)
        {
            KeyDownInternal(Keys.ShiftKey);
            KeyDownInternal(Keys.D0);
        }
        else
        {
            KeyDownInternal(keyCode);
        }
    }

    private void KeyDownInternal(Keys keyCode)
    {
        if (keys.TryGetValue(keyCode, out var item))
            output[item.Address] &= (byte)~item.Bit; //set bit to zero
    }

    public void SetKeyUp(Keys keyCode)
    {
        if (keyCode == Keys.Oem5) // Key '\'
            keyCode = Keys.Enter;

        if (keyCode == Keys.Back)
        {
            KeyUpInternal(Keys.ShiftKey);
            KeyUpInternal(Keys.D0);
        }
        else
        {
            KeyUpInternal(keyCode);
        }
    }

    private void KeyUpInternal(Keys keyCode)
    {
        if (keys.TryGetValue(keyCode, out var item))
            output[item.Address] |= (byte)item.Bit; //set bit to one
    }

    record Item(ushort Address, byte Bit);
}
