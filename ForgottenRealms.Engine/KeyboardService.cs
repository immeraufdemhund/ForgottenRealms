using ForgottenRealms.Engine.Classes;
using ForgottenRealms.Engine.Logging;

namespace ForgottenRealms.Engine;

public class KeyboardService
{
    private static bool in_print_and_exit = false;
    private static readonly SoundDriver SoundDriver = new ();

    public static void print_and_exit()
    {
        if (in_print_and_exit == false)
        {
            in_print_and_exit = true;

            SoundDriver.PlaySound(Sound.sound_FF);

            Logger.Close();

            ItemLibrary.Write();

            MainGameEngine.EngineStop();
        }
    }


    internal static byte GetInputKey()
    {
        byte key;

        if (gbl.inDemo == true)
        {
            if (KeyboardDriver.KEYPRESSED() == true)
            {
                key = KeyboardDriver.READKEY();
            }
            else
            {
                key = 0;
            }
        }
        else
        {
            key = KeyboardDriver.READKEY();
        }

        if (key == 0x13)
        {
            SoundDriver.PlaySound(Sound.sound_0);
        }

        if (Cheats.allow_keyboard_exit && key == 3)
        {
            print_and_exit();
        }

        if (key != 0)
        {
            while (KeyboardDriver.KEYPRESSED() == true)
            {
                key = KeyboardDriver.READKEY();
            }
        }

        return key;
    }

    internal static void clear_keyboard()
    {
        while (KeyboardDriver.KEYPRESSED() == true)
        {
            GetInputKey();
        }
    }


    internal static void clear_one_keypress()
    {
        if (KeyboardDriver.KEYPRESSED() == true)
        {
            GetInputKey();
        }
    }
}