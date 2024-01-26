using System;
using ForgottenRealms.Engine.Classes;
using ForgottenRealms.Engine.Classes.DaxFiles;

namespace ForgottenRealms.Engine;

public class DisplayDriver
{
    internal int[,] bounds = new int[3, 4] {
        { 0x16, 0x26, 0x11, 1 },
        { 0x16, 0x26, 0x15, 1 },
        { 0x15, 0x26, 1, 0x17 } // TextRegion.CombatSummary
    };
    //const char[] syms = { '!', ',', '-', '.', ':', ';', '?' };
    private Set puncutation = new Set(33, 44, 45, 46, 58, 59, 63); // "!,-.:;?" // 33,44,45,46,58,59,63

    private readonly DaxFileDecoder _daxFileDecoder;
    private readonly KeyboardDriver _keyboardDriver;
    private readonly KeyboardService _keyboardService;
    private readonly seg051 _seg051;

    public DisplayDriver(DaxFileDecoder daxFileDecoder, KeyboardDriver keyboardDriver, KeyboardService keyboardService, seg051 seg051)
    {
        _daxFileDecoder = daxFileDecoder;
        _keyboardDriver = keyboardDriver;
        _keyboardService = keyboardService;
        _seg051 = seg051;
    }

    internal void DrawRectangle(byte color, int yEnd, int xEnd, int yStart, int xStart)
    {
        xStart *= 8;
        xEnd = (xEnd + 1) * 8;
        yStart *= 8;
        yEnd = (yEnd + 1) * 8;

        for (int x = xStart; x < xEnd; x++)
        {
            for (int y = yStart; y < yEnd; y++)
            {
                Display.SetPixel3(x, y, color);
            }
        }
    }

    internal void Load8x8Tiles() // load_8x8d1_201
    {
        byte[] block_ptr;
        short block_size;

        _daxFileDecoder.LoadDecodeDax(out block_ptr, out block_size, 201, "8X8d1.dax");

        if (block_size != 0)
        {
            for (int i = 0, j = 0; i < block_size && j < 177; i += 8, j++)
            {
                for (int k = 0; k < 8 && (i + k) < block_size; k++)
                {
                    gbl.dax_8x8d1_201[j, k] = block_ptr[i + k];
                }
            }
        }
    }

    internal void display_char01(char ch, int repeatCount, int bgColor, int fgColor, int y, int x)
    {
        if (x < 40 &&
            y < 25)
        {
            char index = (char)(char.ToUpper(ch) % 0x40);

            for (int i = 0; i < 8; i++)
            {
                gbl.monoCharData[i] = gbl.dax_8x8d1_201[index, i];
            }

            for (int i = 0; i < repeatCount; i++)
            {
                Display.DisplayMono8x8(x + i, y, gbl.monoCharData, bgColor, fgColor);
            }
        }
    }

    internal void displaySpaceChar(int yCol, int xCol)
    {
        if (xCol >= 0 && xCol <= 0x27 &&
            yCol >= 0 && yCol <= 0x18)
        {
            display_char01(' ', 1, 0, 0, yCol, xCol);

            Display.Update();
        }
    }

    [Obsolete]
    internal void displayString(string str, int bgColor, int fgColor, int yCol, int xCol)
    {
        DisplayString(str, bgColor, fgColor, yCol, xCol);
    }

    public void DisplayString(string str, int bgColor, int fgColor, int yCol, int xCol)
    {
        if (xCol <= 0x27 && yCol <= 0x27)
        {
            foreach (char ch in str)
            {
                display_char01(ch, 1, bgColor, fgColor, yCol, xCol);
                xCol++;
            }
            Display.Update();
        }
    }

    internal void press_any_key(string text, bool clearArea, int fgColor, TextRegion region)
    {
        int r = (int)region;
        press_any_key(text, clearArea, fgColor, bounds[r, 0], bounds[r, 1], bounds[r, 2], bounds[r, 3]);
    }

    internal void press_any_key(string text, bool clearArea, int fgColor,
        int yEnd, int xEnd, int yStart, int xStart)
    {
        if (xStart > 0x27 || yStart > 0x18 ||
            xEnd > 0x27 && yEnd > 0x27)
        {
            return;
        }

        if (gbl.textXCol < xStart ||
            gbl.textXCol > xEnd ||
            gbl.textYCol < yStart ||
            gbl.textYCol > yEnd)
        {
            gbl.textXCol = xStart;
            gbl.textYCol = yStart;
        }

        if (clearArea == true)
        {
            DrawRectangle(0, yEnd, xEnd, yStart, xStart);
            gbl.textXCol = xStart;
            gbl.textYCol = yStart;
        }

        int text_start = 1;
        int input_lenght = text.Length;

        if (input_lenght != 0)
        {
            do
            {
                int text_end = text_start;
                //text.LastIndexOfAny(syms, text_start);

                while (text_end < input_lenght &&
                       puncutation.MemberOf(text[text_end - 1]) == true)
                {
                    text_end++;
                }

                while (text_end < input_lenght &&
                       puncutation.MemberOf(text[text_end - 1]) == false &&
                       text[text_end - 1] != ' ')
                {
                    text_end++;
                }

                if (text[text_end - 1] != ' ')
                {
                    while (text_end + 1 < input_lenght &&
                           puncutation.MemberOf(text[text_end]) == true)
                    {
                        text_end++;
                    }
                }

                if (((text_end - text_start) + gbl.textXCol) > xEnd)
                {
                    if (((text_end - text_start) + gbl.textXCol) == xEnd &&
                        text[text_end - 1] == ' ')
                    {
                        text_end -= 1;
                        text_start = displayStringSlow(text, text_start, text_end, fgColor);
                    }

                    gbl.textXCol = xStart;
                    gbl.textYCol++;
                    text_skip_space(text, input_lenght, ref text_start);

                    if (gbl.textYCol > yEnd &&
                        text_start < input_lenght)
                    {
                        gbl.textXCol = xStart;
                        gbl.textYCol = yStart;

                        DisplayAndPause("Press any key to continue", 13);
                        _keyboardService.clear_keyboard();

                        DrawRectangle(0, yEnd, xEnd, yStart, xStart);

                        text_start = displayStringSlow(text, text_start, text_end, fgColor);
                    }
                }
                else
                {
                    text_start = displayStringSlow(text, text_start, text_end, fgColor);
                    Display.Update();
                }
            } while (text_start <= input_lenght);

            if (gbl.textXCol > xEnd)
            {
                gbl.textXCol = xStart;
                gbl.textYCol++;
            }
        }
    }

    internal string getUserInputString(byte inputLen, byte bgColor, byte fgColor, string prompt)
    {
        ClearPromptArea();
        displayString(prompt, bgColor, fgColor, 0x18, 0);

        int xPos = prompt.Length;

        char ch;
        string resultString = string.Empty;

        do
        {
            ch = (char)_keyboardService.GetInputKey();

            if (ch >= 0x20 && ch <= 0x7A)
            {
                if (resultString.Length < inputLen)
                {
                    resultString += ch.ToString();

                    //var_2A++;

                    displayString(ch.ToString(), 0, 15, 0x18, xPos++);
                }
            }
            else if (ch == 8 && resultString.Length > 0)
            {
                resultString = _seg051.Copy(resultString.Length - 1, 0, resultString);

                displaySpaceChar(24, --xPos);
                //xPos -= 1;
            }

        } while (ch != 0x0d && ch != 0x1B && gbl.inDemo == false);

        ClearPromptArea();

        return resultString.ToUpper();
    }

    private void ClearPromptArea() => DrawRectangle(0, 0x18, 0x27, 0x18, 0);

    internal ushort getUserInputShort(byte bgColor, byte fgColor, string prompt)
    {
        bool good_input;
        int value = 0;

        do
        {
            string input = getUserInputString(6, bgColor, fgColor, prompt);

            good_input = int.TryParse(input, out value);

            if (good_input)
            {
                good_input = (value < 0x00010000 && value >= 0);
            }
        } while (good_input == false);

        return (ushort)value;
    }

    internal void DisplayAndPause(string txt, byte fgColor) // displayAndDebug
    {
        ClearPromptArea();

        displayString(txt, 0, fgColor, 0x18, 0);
        _keyboardService.GetInputKey();
    }

    /// <summary>
    /// Gets the centi seconds since midnight
    /// </summary>
    internal int time01() // time01
    {
        System.DateTime dt = System.DateTime.Now;

        return (dt.Hour * 360000) + (dt.Minute * 6000) + (dt.Second * 100) + (dt.Millisecond / 10);
    }

    internal void ClearScreen()
    {
        DrawRectangle(0, 0x18, 0x27, 0, 0);
    }

    internal void DisplayStatusText(byte bgColor, byte fgColor, string text) /* sub_10ECF */
    {
        ClearPromptArea();

        displayString(text, bgColor, fgColor, 0x18, 0);

        GameDelay();

        ClearPromptArea();
    }

    internal void GameDelay()
    {
        //Display.Update();
        _keyboardDriver.SysDelay(gbl.game_speed_var * 100);
    }

    private int displayStringSlow(string text
        , int text_index, int text_length, int fgColor) // sub_107DE
    {
        while (text_index <= text_length)
        {
            display_char01(text[text_index - 1], 1, 0, fgColor, gbl.textYCol, gbl.textXCol);

            if (gbl.DelayBetweenCharacters)
            {
                _keyboardDriver.SysDelay(gbl.game_speed_var * 3);
            }

            text_index += 1;
            gbl.textXCol++;
        }

        return text_index;
    }

    private void text_skip_space(string text, int text_max, ref int text_index) /* sub_10854 */
    {
        while (text_index < text_max &&
               text[text_index - 1] == ' ')
        {
            text_index += 1;
        }
    }
}
