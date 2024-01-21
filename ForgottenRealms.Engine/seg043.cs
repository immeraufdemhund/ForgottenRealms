using ForgottenRealms.Engine.Classes;
using ForgottenRealms.Engine.Logging;

namespace ForgottenRealms.Engine;

public class seg043
{
    private static bool in_print_and_exit = false;

    public static void print_and_exit()
    {
        if (in_print_and_exit == false)
        {
            in_print_and_exit = true;

            new SoundDriver().PlaySound(Sound.sound_FF);

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
            if (seg049.KEYPRESSED() == true)
            {
                key = seg049.READKEY();
            }
            else
            {
                key = 0;
            }
        }
        else
        {
            key = seg049.READKEY();
        }

        if (key == 0x13)
        {
            new SoundDriver().PlaySound(Sound.sound_0);
        }

        if (Cheats.allow_keyboard_exit && key == 3)
        {
            print_and_exit();
        }

        if (key != 0)
        {
            while (seg049.KEYPRESSED() == true)
            {
                key = seg049.READKEY();
            }
        }

        return key;
    }

    public static void DumpPlayerAffects()
    {
        foreach (Player player in gbl.TeamList)
        {
            foreach (Affect affect in player.affects)
            {
                Logger.Debug("who: {0}  sp#: {1} - {2}", player.name, (int)affect.type, affect.type);
            }
        }
    }

    public static void ToggleCommandDebugging()
    {
        gbl.printCommands = !gbl.printCommands;

        if (gbl.printCommands == true)
        {
            Logger.Debug(System.DateTime.Now.ToString());
        }
    }

    internal static void clear_keyboard()
    {
        while (seg049.KEYPRESSED() == true)
        {
            GetInputKey();
        }
    }


    internal static void clear_one_keypress()
    {
        if (seg049.KEYPRESSED() == true)
        {
            GetInputKey();
        }
    }
}
