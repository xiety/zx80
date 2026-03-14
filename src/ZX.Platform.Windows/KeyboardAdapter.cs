namespace ZX.Platform.Windows;

public class KeyboardAdapter(byte[] input) : KeyboardAdapterBase(input)
{
    public void SetKeyDown(Keys keyCode)
    {
        if (keyMap.TryGetValue(keyCode, out var zxKey))
            SetKeyDown(zxKey);
    }

    public void SetKeyUp(Keys keyCode)
    {
        if (keyMap.TryGetValue(keyCode, out var zxKey))
            SetKeyUp(zxKey);
    }

    private static readonly Dictionary<Keys, ZxKey> keyMap = new()
    {
        [Keys.ShiftKey] = ZxKey.Shift,
        [Keys.Z] = ZxKey.Z,
        [Keys.X] = ZxKey.X,
        [Keys.C] = ZxKey.C,
        [Keys.V] = ZxKey.V,
        [Keys.A] = ZxKey.A,
        [Keys.S] = ZxKey.S,
        [Keys.D] = ZxKey.D,
        [Keys.F] = ZxKey.F,
        [Keys.G] = ZxKey.G,
        [Keys.Q] = ZxKey.Q,
        [Keys.W] = ZxKey.W,
        [Keys.E] = ZxKey.E,
        [Keys.R] = ZxKey.R,
        [Keys.T] = ZxKey.T,
        [Keys.D1] = ZxKey.D1,
        [Keys.D2] = ZxKey.D2,
        [Keys.D3] = ZxKey.D3,
        [Keys.D4] = ZxKey.D4,
        [Keys.D5] = ZxKey.D5,
        [Keys.D0] = ZxKey.D0,
        [Keys.D9] = ZxKey.D9,
        [Keys.D8] = ZxKey.D8,
        [Keys.D7] = ZxKey.D7,
        [Keys.D6] = ZxKey.D6,
        [Keys.P] = ZxKey.P,
        [Keys.O] = ZxKey.O,
        [Keys.I] = ZxKey.I,
        [Keys.U] = ZxKey.U,
        [Keys.Y] = ZxKey.Y,
        [Keys.Enter] = ZxKey.Enter,
        [Keys.Oem5] = ZxKey.Enter,
        [Keys.L] = ZxKey.L,
        [Keys.K] = ZxKey.K,
        [Keys.J] = ZxKey.J,
        [Keys.H] = ZxKey.H,
        [Keys.Space] = ZxKey.Space,
        [Keys.ControlKey] = ZxKey.Ctrl,
        [Keys.M] = ZxKey.M,
        [Keys.N] = ZxKey.N,
        [Keys.B] = ZxKey.B,
        [Keys.Back] = ZxKey.Backspace
    };
}
