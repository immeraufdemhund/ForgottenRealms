using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine;

public class ovr004
{
    private static string[] codeWheel = {
        "CWLNRTESSCEDCSHSISERRRNSHSSTSSNNHSHN",
        "LAASRDAIILIDSUGADAEEOEGRLSELIITESOIO",
        "LRUNIMMORIIGRRIUPTIIUELIMLHMIXACGRIL",
        "Z0LIOHEUVNODSGEOGXYWISIOCRARLRARRHOI",
        "AMTELRLUIYNAEOOITOUELRREREUIMADPPFAB",
        "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890"
    };

    private readonly ovr034 _ovr034;
    private readonly seg037 _seg037;
    private readonly seg040 _seg040;
    private readonly seg051 _seg051;
    private readonly KeyboardDriver _keyboardDriver;
    private readonly SoundDriver _soundDriver;
    private readonly MainGameEngine _mainGameEngine;
    private readonly DisplayDriver _displayDriver;

    public ovr004(ovr034 ovr034, seg037 seg037, seg040 seg040, seg051 seg051, KeyboardDriver keyboardDriver, SoundDriver soundDriver, MainGameEngine mainGameEngine, DisplayDriver displayDriver)
    {
        _ovr034 = ovr034;
        _seg037 = seg037;
        _seg040 = seg040;
        _seg051 = seg051;
        _keyboardDriver = keyboardDriver;
        _soundDriver = soundDriver;
        _mainGameEngine = mainGameEngine;
        _displayDriver = displayDriver;
    }

    internal void copy_protection()
    {
        string code_path_str;
        char input_expected;
        char input_key;

        _ovr034.Load24x24Set(0x1A, 0, 1, "tiles");
        _ovr034.Load24x24Set(0x16, 0x1A, 2, "tiles");

        _seg037.DrawFrame_Outer();

        _displayDriver.displayString("Align the espruar and dethek runes", 0, 10, 2, 3);
        _displayDriver.displayString("shown below, on translation wheel", 0, 10, 3, 3);
        _displayDriver.displayString("like this:", 0, 10, 4, 3);
        int attempt = 0;

        do
        {

            int var_6 = _seg051.Random(26);
            int var_7 = _seg051.Random(22);

            _ovr034.DrawIsoTile(var_6, 3, 0x11);
            _ovr034.DrawIsoTile(var_7 + 0x1a, 7, 0x11);

            _seg040.DrawOverlay();
            int code_path = _seg051.Random(3);

            switch (code_path)
            {
                case 0:
                    code_path_str = "-..-..-..";
                    break;

                case 1:
                    code_path_str = "- - - - -";
                    break;

                case 2:
                    code_path_str = ".........";
                    break;

                default:
                    code_path_str = string.Empty;
                    break;
            }

            int code_row = _seg051.Random(6);

            string text = "Type the character in box number " + (6 - code_row);

            _displayDriver.displayString(text, 0, 10, 12, 3);

            _displayDriver.displayString("under the ", 0, 10, 13, 3);
            _displayDriver.displayString(code_path_str, 0, 15, 13, 14);
            _displayDriver.displayString("path.", 0, 10, 13, 0x19);

            int code_index = var_6 + 0x22 - var_7 + (code_path * 12) + ((5 - code_row) << 1);

            while (code_index < 0)
            {
                code_index += 36;
            }

            while (code_index > 35)
            {
                code_index -= 36;
            }

            input_expected = codeWheel[code_row][code_index];

            string input = _displayDriver.getUserInputString(1, 0, 13, "type character and press return: ");

            input_key = (input == null || input.Length == 0) ? ' ' : input[0];
            attempt++;

            if (input_key != input_expected)
            {
                _displayDriver.DisplayStatusText(0, 14, "Sorry, that's incorrect.");
            }
            else
            {
                return;
            }
        } while (input_key != input_expected && attempt < 3);

        if (attempt >= 3)
        {
            _soundDriver.PlaySound(Sound.sound_1);
            _soundDriver.PlaySound(Sound.sound_5);
            gbl.game_speed_var = 9;
            _displayDriver.DisplayStatusText(0, 14, "An unseen force hurls you into the abyss!");
            _keyboardDriver.SysDelay(0x3E8);
            _mainGameEngine.EngineStop();
        }
    }
}
