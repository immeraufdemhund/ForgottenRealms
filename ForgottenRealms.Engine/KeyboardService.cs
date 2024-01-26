using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine;

public class KeyboardService
{
    private readonly SoundDriver _soundDriver;
    private readonly KeyboardDriver _keyboardDriver;

    public KeyboardService(SoundDriver soundDriver, KeyboardDriver keyboardDriver)
    {
        _soundDriver = soundDriver;
        _keyboardDriver = keyboardDriver;
    }

    internal byte GetInputKey()
    {
        byte key;

        if (gbl.inDemo == true)
        {
            if (_keyboardDriver.KEYPRESSED() == true)
            {
                key = _keyboardDriver.READKEY();
            }
            else
            {
                key = 0;
            }
        }
        else
        {
            key = _keyboardDriver.READKEY();
        }

        if (key == 0x13)
        {
            _soundDriver.PlaySound(Sound.sound_0);
        }

        if (Cheats.allow_keyboard_exit && key == 3)
        {
            // this causes a circular reference
            //_mainGameEngine.EngineStop();
        }

        if (key != 0)
        {
            while (_keyboardDriver.KEYPRESSED() == true)
            {
                key = _keyboardDriver.READKEY();
            }
        }

        return key;
    }

    internal void clear_keyboard()
    {
        while (_keyboardDriver.KEYPRESSED() == true)
        {
            GetInputKey();
        }
    }

    internal void clear_one_keypress()
    {
        if (_keyboardDriver.KEYPRESSED() == true)
        {
            GetInputKey();
        }
    }
}
