namespace ZX.Platform;

public enum ZxKey
{
    None, Shift, Z, X, C, V, A, S, D, F, G, Q, W, E, R, T,
    D1, D2, D3, D4, D5, D0, D9, D8, D7, D6, P, O, I, U, Y,
    Enter, L, K, J, H, Space, Ctrl, M, N, B, Backspace
}

public abstract class KeyboardAdapterBase
{
    private readonly byte[] input;
    private readonly Dictionary<ZxKey, Item> keys = [];

    protected KeyboardAdapterBase(byte[] input)
    {
        this.input = input;
        SetupKeys(0xFEFE, ZxKey.Shift, ZxKey.Z, ZxKey.X, ZxKey.C, ZxKey.V);
        SetupKeys(0xFDFE, ZxKey.A, ZxKey.S, ZxKey.D, ZxKey.F, ZxKey.G);
        SetupKeys(0xFBFE, ZxKey.Q, ZxKey.W, ZxKey.E, ZxKey.R, ZxKey.T);
        SetupKeys(0xF7FE, ZxKey.D1, ZxKey.D2, ZxKey.D3, ZxKey.D4, ZxKey.D5);
        SetupKeys(0xEFFE, ZxKey.D0, ZxKey.D9, ZxKey.D8, ZxKey.D7, ZxKey.D6);
        SetupKeys(0xDFFE, ZxKey.P, ZxKey.O, ZxKey.I, ZxKey.U, ZxKey.Y);
        SetupKeys(0xBFFE, ZxKey.Enter, ZxKey.L, ZxKey.K, ZxKey.J, ZxKey.H);
        SetupKeys(0x7FFE, ZxKey.Space, ZxKey.Ctrl, ZxKey.M, ZxKey.N, ZxKey.B);
    }

    private void SetupKeys(ushort address, ZxKey k0, ZxKey k1, ZxKey k2, ZxKey k3, ZxKey k4)
    {
        input[address] = 255;
        keys.Add(k0, new(address, 1 << 0));
        keys.Add(k1, new(address, 1 << 1));
        keys.Add(k2, new(address, 1 << 2));
        keys.Add(k3, new(address, 1 << 3));
        keys.Add(k4, new(address, 1 << 4));
    }

    public void SetKeyDown(ZxKey keyCode)
    {
        if (keyCode == ZxKey.Backspace)
        {
            KeyDownInternal(ZxKey.Shift);
            KeyDownInternal(ZxKey.D0);
        }
        else
            KeyDownInternal(keyCode);
    }

    public void SetKeyUp(ZxKey keyCode)
    {
        if (keyCode == ZxKey.Backspace)
        {
            KeyUpInternal(ZxKey.Shift);
            KeyUpInternal(ZxKey.D0);
        }
        else
            KeyUpInternal(keyCode);
    }

    private void KeyDownInternal(ZxKey keyCode)
    {
        if (keys.TryGetValue(keyCode, out var item))
            input[item.Address] &= (byte)~item.Bit;
    }

    private void KeyUpInternal(ZxKey keyCode)
    {
        if (keys.TryGetValue(keyCode, out var item))
            input[item.Address] |= (byte)item.Bit;
    }

    private record Item(ushort Address, byte Bit);
}
