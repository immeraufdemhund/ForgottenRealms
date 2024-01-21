using ForgottenRealms.Engine.Classes;
using ForgottenRealms.Engine.Classes.DaxFiles;

namespace ForgottenRealms.Engine;

internal class seg040
{
    internal static void OverlayUnbounded(DaxBlock source, int arg_8, int itemIdex, int rowY, int colX)
    {
        draw_combat_picture(source, rowY + 1, colX + 1, itemIdex);
    }


    internal static void OverlayBounded(DaxBlock source, int arg_8, int itemIndex, int rowY, int colX) /* sub_E353 */
    {
        draw_combat_picture(source, rowY + 1, colX + 1, itemIndex);
    }


    internal static void ega_backup(DaxBlock dax_block, int rowY, int colX) /* ega_01 */
    {
        if (dax_block != null)
        {
            int offset = 0;

            int minY = rowY * 8;
            int maxY = minY + dax_block.height;

            int minX = colX * 8;
            int maxX = minX + (dax_block.width * 8);

            for (int pixY = minY; pixY < maxY; pixY++)
            {
                for (int pixX = minX; pixX < maxX; pixX++)
                {
                    dax_block.data[offset] = Display.GetPixel(pixX, pixY);
                    offset++;
                }
            }
        }
    }

    private static int color_no_draw = 17;
    private static int color_re_color_from = 17;
    private static int color_re_color_to = 17;

    internal static void draw_clipped_recolor(int from, int to)
    {
        color_re_color_from = from;
        color_re_color_to = to;
    }

    internal static void draw_clipped_nodraw(int color)
    {
        color_no_draw = color;
    }

    internal static void draw_combat_picture(DaxBlock dax_block, int rowY, int colX, int index)
    {
        new DrawPictureAction().DrawCombatPicture(dax_block, rowY, colX, index);
    }

    internal static void DrawOverlay()
    {
        //TODO this might be useful when we move to OpenGL.
    }

    internal static void SetPaletteColor(int color, int index)
    {
        int newColor = color;

        //if (color >= 8)
        //{
        //  newColor += 8;
        //}

        Display.SetEgaPalette(index, newColor);
    }


    internal static void DrawColorBlock(int color, int lineCount, int colWidth, int lineY, int colX)
    {
        int minY = lineY + 8;
        int maxY = minY + lineCount;

        int minX = (colX * 8) + 8;
        int maxX = minX + (colWidth * 8);

        for (int pixY = minY; pixY < maxY; pixY++)
        {
            for (int pixX = minX; pixX < maxX; pixX++)
            {
                if (pixX >= 0 && pixX < 320 && pixY >= 0 && pixY < 200)
                {
                    Display.SetPixel3(pixX, pixY, color);
                }
            }
        }
    }
}
