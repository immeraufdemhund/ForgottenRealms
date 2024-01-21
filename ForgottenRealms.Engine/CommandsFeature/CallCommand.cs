using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class CallCommand : IGameCommand
{
    public void Execute()
    {
        ovr008.vm_LoadCmdSets(1);

        var var_2 = gbl.cmd_opps[1].Word;
        var var_4 = (ushort)(var_2 - 0x7fff);

        VmLog.WriteLine("CMD_Call: {0:X}", var_4);

        switch (var_4)
        {
            case 0xAE11:
                gbl.mapWallRoof = ovr031.get_wall_x2(gbl.mapPosY, gbl.mapPosX);

                if (gbl.byte_1AB0B == true)
                {
                    if (gbl.spriteChanged == true ||
                        gbl.displayPlayerSprite ||
                        gbl.byte_1EE91 == true ||
                        gbl.positionChanged == true ||
                        gbl.byte_1EE94 == true)
                    {
                        gbl.can_draw_bigpic = true;
                        ovr029.RedrawView();
                        ovr025.display_map_position_time();
                        gbl.byte_1EE94 = false;
                        gbl.byte_1EE91 = false;
                        gbl.positionChanged = false;
                        gbl.spriteChanged = false;
                        gbl.displayPlayerSprite = false;

                        gbl.mapWallType = ovr031.getMap_wall_type(gbl.mapDirection, gbl.mapPosY, gbl.mapPosX);
                    }
                }

                break;

            case 1:
                ovr008.SetupDuel(true);
                break;

            case 2:
                ovr008.SetupDuel(false);
                break;

            case 0x3201:
                if (gbl.word_1EE76 == 8)
                {
                    new SoundDriver().PlaySound(Sound.sound_a);
                }
                else if (gbl.word_1EE76 == 10)
                {
                    new SoundDriver().PlaySound(Sound.sound_b);
                }
                else
                {
                    new SoundDriver().PlaySound(Sound.sound_a);
                }

                break;

            case 0x401F:
                ovr008.MovePositionForward();
                break;

            case 0x4019:
                if (gbl.area_ptr.inDungeon == 0)
                {
                    gbl.mapWallType = ovr031.getMap_wall_type(gbl.mapDirection, gbl.mapPosY, gbl.mapPosX);
                }

                break;

            case 0xE804:
                ovr030.DrawMaybeOverlayed(gbl.byte_1D556.CurrentPicture(), true, 3, 3);

                gbl.byte_1D556.NextFrame();

                DisplayDriver.GameDelay();
                break;
        }
    }
}
