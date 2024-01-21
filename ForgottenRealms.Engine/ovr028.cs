using ForgottenRealms.Engine.Classes;
using ForgottenRealms.Engine.Classes.DaxFiles;

namespace ForgottenRealms.Engine;

internal class MapCursor // ovr028
{
    private static int[] city_map_x = { /* unk_16D5A */
        0x04,0x0C,0x15,0x0B,0x1D,0x14,0x26,0x15,
        0x1E,0x1F,0x19,0x25,0x1C,0x1D,0x03,0x0C,
        0x19,0x1D,0x1D,0x21,0x13,0x10,0x09,0x10,
        0x14,0x15,0x19,0x19,0x1A,0x1F,0x25,0x22, 0x0F };

    private static int[] city_map_y = { /* unk_16D7A */
        0x0F,0x08,0x0B,0x04,0x0A,0x04,0x01,0x02,
        0x0D,0x0F,0x03,0x05,0x02,0x08,0x0C,0x0D,
        0x0A,0x0C,0x09,0x09,0x08,0x06,0x06,0x03,
        0x02,0x02,0x03,0x02,0x03,0x04,0x02,0x01, 0x00 };

    private static int loc_X; // word_1EF9C
    private static int loc_Y; // word_1EF9E
    private static readonly DrawPictureAction DrawPictureAction = new ();

    internal static void SetPosition(int currentCity) //sub_6E005
    {
        loc_X = city_map_x[currentCity];
        loc_Y = city_map_y[currentCity];
    }


    internal static void Draw() /* sub_6E02E */
    {
        seg040.ega_backup(gbl.cursor_bkup, loc_Y, loc_X);
        DrawPictureAction.DrawPicture(gbl.cursor, loc_Y, loc_X, 0);
    }


    internal static void Restore() /* sub_6E05D */
    {
        DrawPictureAction.DrawPicture(gbl.cursor_bkup, loc_Y, loc_X, 0);
    }
}
