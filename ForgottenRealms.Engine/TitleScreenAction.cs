using System;
using ForgottenRealms.Engine.Classes;
using ForgottenRealms.Engine.Classes.DaxFiles;

namespace ForgottenRealms.Engine;

public class TitleScreenAction
{
    private readonly DrawPictureAction _drawPictureAction = new ();
    private readonly DaxBlockReader _daxBlockReader = new ();
    private readonly SoundDriver _soundDriver = new ();
    private readonly DisplayDriver _displayDriver = new ();

    public void ShowTitleScreen()
    {
        DaxBlock dax_ptr;

        dax_ptr = _daxBlockReader.LoadDax(0, 0, 1, "Title");
        _drawPictureAction.DrawPicture(dax_ptr, 0, 0, 0);

        delay_or_key(5);

        dax_ptr = _daxBlockReader.LoadDax(0, 0, 2, "Title");
        _drawPictureAction.DrawPicture(dax_ptr, 0, 0, 0);

        dax_ptr = _daxBlockReader.LoadDax(0, 0, 3, "Title");
        _drawPictureAction.DrawPicture(dax_ptr, 0x0b, 6, 0);
        delay_or_key(10);

        dax_ptr = _daxBlockReader.LoadDax(0, 0, 4, "Title");

        _soundDriver.PlaySound(Sound.sound_d);

        _drawPictureAction.DrawPicture(dax_ptr, 0x0b, 0, 0);
        delay_or_key(10);

        _displayDriver.ClearScreen();
        ShowCredits();
        delay_or_key(10);

        _displayDriver.ClearScreen();
    }

    private static void delay_or_key(int seconds)
    {
        seg043.clear_keyboard();

        var timeEnd = DateTime.Now.AddSeconds(seconds);

        while (seg049.KEYPRESSED() == false &&
               DateTime.Now < timeEnd)
        {
            System.Threading.Thread.Sleep(100);
        }

        seg043.clear_keyboard();
    }

    private static void ShowCredits()
    {
        Display.UpdateStop();

        seg037.draw8x8_02();

        DisplayDriver.displayString("based on the tsr novel 'azure bonds'", 0, 10, 1, 2);
        DisplayDriver.displayString("by:", 0, 10, 2, 6);
        DisplayDriver.displayString("kate novak", 0, 11, 2, 9);
        DisplayDriver.displayString("and", 0, 10, 2, 0x14);
        DisplayDriver.displayString("jeff grubb", 0, 11, 2, 0x18);
        DisplayDriver.displayString("scenario created by:", 0, 10, 4, 0x0a);
        DisplayDriver.displayString("tsr, inc.", 0, 0x0e, 5, 0x0b);
        DisplayDriver.displayString("and", 0, 0x0a, 5, 0x15);
        DisplayDriver.displayString("ssi", 0, 0x0e, 5, 0x19);
        DisplayDriver.displayString("jeff grubb", 0, 0x0b, 6, 0x0e);
        DisplayDriver.displayString("george mac donald", 0x0, 0x0B, 0x7, 0x0B);
        DisplayDriver.displayString("game created by:", 0x0, 0x0A, 0x9, 0x1);
        DisplayDriver.displayString("ssi special projects", 0x0, 0x0E, 0x9, 0x12);
        DisplayDriver.displayString("project leader:", 0x0, 0x0E, 0x0B, 0x2);
        DisplayDriver.displayString("george mac donald", 0x0, 0x0B, 0x0C, 0x2);
        DisplayDriver.displayString("programming:", 0x0, 0x0E, 0x0E, 0x2);
        DisplayDriver.displayString("scot bayless", 0x0, 0x0B, 0x0F, 0x2);
        DisplayDriver.displayString("russ brown", 0x0, 0x0B, 0x10, 0x2);
        DisplayDriver.displayString("michael mancuso", 0x0, 0x0B, 0x11, 0x2);
        DisplayDriver.displayString("development:", 0x0, 0x0E, 0x13, 0x2);
        DisplayDriver.displayString("david shelley", 0x0, 0x0B, 0x14, 0x2);
        DisplayDriver.displayString("michael mancuso", 0x0, 0x0B, 0x15, 0x2);
        DisplayDriver.displayString("oran kangas", 0x0, 0x0B, 0x16, 0x2);
        DisplayDriver.displayString("graphic arts:", 0x0, 0x0E, 0x0B, 0x16);
        DisplayDriver.displayString("tom wahl", 0x0, 0x0B, 0x0C, 0x16);
        DisplayDriver.displayString("fred butts", 0x0, 0x0B, 0x0D, 0x16);
        DisplayDriver.displayString("susan manley", 0x0, 0x0B, 0x0E, 0x16);
        DisplayDriver.displayString("mark johnson", 0x0, 0x0B, 0x0F, 0x16);
        DisplayDriver.displayString("cyrus lum", 0x0, 0x0B, 0x10, 0x16);
        DisplayDriver.displayString("playtesting:", 0x0, 0x0E, 0x12, 0x16);
        DisplayDriver.displayString("jim jennings", 0x0, 0x0B, 0x13, 0x16);
        DisplayDriver.displayString("james kucera", 0x0, 0x0B, 0x14, 0x16);
        DisplayDriver.displayString("rick white", 0x0, 0x0B, 0x15, 0x16);
        DisplayDriver.displayString("robert daly", 0x0, 0x0B, 0x16, 0x16);

        Display.UpdateStart();
    }
}
