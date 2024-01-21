using ForgottenRealms.Engine.Classes;
using ForgottenRealms.Engine.Classes.DaxFiles;

namespace ForgottenRealms.Engine;

public class DrawPictureAction
{
    private static int color_no_draw = 17;
    private static int color_re_color_from = 17;
    private static int color_re_color_to = 17;
    public void DrawPicture(DaxBlock dax_block, int rowY, int colX, int index)
    {
        DrawClippedPicture(dax_block, rowY, colX, index, 0, 320, 0, 200);
    }

    public void DrawCombatPicture(DaxBlock dax_block, int rowY, int colX, int index)
    {
        DrawClippedPicture(dax_block, rowY, colX, index, 8, 176, 8, 176);
    }

    private static void DrawClippedPicture(DaxBlock dax_block, int rowY, int colX, int index,
        int clipMinX, int clipMaxX, int clipMinY, int clipMaxY)
    {
        if (dax_block != null)
        {
            int offset = index * dax_block.bpp;

            int minY = rowY * 8;
            int maxY = minY + dax_block.height;

            int minX = colX * 8;
            int maxX = minX + (dax_block.width * 8);

            for (int pixY = minY; pixY < maxY; pixY++)
            {
                for (int pixX = minX; pixX < maxX; pixX++)
                {
                    if (pixX >= clipMinX && pixX < clipMaxX &&
                        pixY >= clipMinY && pixY < clipMaxY)
                    {
                        byte color = dax_block.data[offset];

                        if (color == color_no_draw)
                        { }
                        else if (color == color_re_color_from)
                        {
                            Display.SetPixel3(pixX, pixY, color_re_color_to);
                        }
                        else
                        {
                            Display.SetPixel3(pixX, pixY, color);
                        }
                    }

                    offset++;
                }
            }

            Display.Update();
        }
    }
}
