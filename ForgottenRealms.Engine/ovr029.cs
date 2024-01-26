using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine;

public class ovr029
{
    private int[] sky_colours = new int[]{ /* seg600:0A8A unk_16D9A*/
        0x00, 0x0F, 0x04, 0x0B, 0x0D, 0x02, 0x09, 0x0E, 0x00, 0x0F, 0x04, 0x0B, 0x0D, 0x02 , 0x09, 0x0E};

    private readonly ovr030 _ovr030;
    private readonly ovr031 _ovr031;

    public ovr029(ovr030 ovr030, ovr031 ovr031)
    {
        _ovr030 = ovr030;
        _ovr031 = ovr031;
    }

    internal void RedrawView() /* sub_6F0BA */
    {
        if (gbl.lastDaxBlockId == 0x50)
        {
            gbl.can_draw_bigpic = false;
        }

        if (gbl.party_killed == false)
        {
            if (gbl.area_ptr.inDungeon != 0)
            {
                gbl.mapWallRoof = _ovr031.get_wall_x2(gbl.mapPosY, gbl.mapPosX);

                if (gbl.mapWallRoof > 0x7F)
                {
                    // indoor
                    gbl.sky_colour = sky_colours[gbl.area_ptr.indoor_sky_colour];
                }
                else
                {
                    // outdoors
                    gbl.sky_colour = sky_colours[gbl.area_ptr.outdoor_sky_colour];
                }

                if (gbl.area_ptr.block_area_view != 0 &&
                    Cheats.always_show_areamap == false)
                {
                    gbl.mapAreaDisplay = false;
                }

                _ovr031.Draw3dWorld(gbl.mapDirection, gbl.mapPosY, gbl.mapPosX);
            }
            else if (gbl.can_draw_bigpic == true)
            {
                _ovr030.draw_bigpic();
            }

            gbl.can_draw_bigpic = false;
        }
    }
}
