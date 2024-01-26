using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class CallCommand : IGameCommand
{
    private readonly SoundDriver _soundDriver;
    private readonly DisplayDriver _displayDriver;
    private readonly ovr008 _ovr008;
    private readonly ovr025 _ovr025;
    private readonly ovr029 _ovr029;
    private readonly ovr030 _ovr030;
    private readonly ovr031 _ovr031;

    public CallCommand(SoundDriver soundDriver, DisplayDriver displayDriver, ovr008 ovr008, ovr025 ovr025, ovr029 ovr029, ovr030 ovr030, ovr031 ovr031)
    {
        _soundDriver = soundDriver;
        _displayDriver = displayDriver;
        _ovr008 = ovr008;
        _ovr025 = ovr025;
        _ovr029 = ovr029;
        _ovr030 = ovr030;
        _ovr031 = ovr031;
    }

    public void Execute()
    {
        _ovr008.vm_LoadCmdSets(1);

        var var_2 = gbl.cmd_opps[1].Word;
        var var_4 = (ushort)(var_2 - 0x7fff);

        VmLog.WriteLine("CMD_Call: {0:X}", var_4);

        switch (var_4)
        {
            case 0xAE11:
                gbl.mapWallRoof = _ovr031.get_wall_x2(gbl.mapPosY, gbl.mapPosX);

                if (gbl.byte_1AB0B == true)
                {
                    if (gbl.spriteChanged == true ||
                        gbl.displayPlayerSprite ||
                        gbl.byte_1EE91 == true ||
                        gbl.positionChanged == true ||
                        gbl.byte_1EE94 == true)
                    {
                        gbl.can_draw_bigpic = true;
                        _ovr029.RedrawView();
                        _ovr025.display_map_position_time();
                        gbl.byte_1EE94 = false;
                        gbl.byte_1EE91 = false;
                        gbl.positionChanged = false;
                        gbl.spriteChanged = false;
                        gbl.displayPlayerSprite = false;

                        gbl.mapWallType = _ovr031.getMap_wall_type(gbl.mapDirection, gbl.mapPosY, gbl.mapPosX);
                    }
                }

                break;

            case 1:
                _ovr008.SetupDuel(true);
                break;

            case 2:
                _ovr008.SetupDuel(false);
                break;

            case 0x3201:
                if (gbl.word_1EE76 == 8)
                {
                    _soundDriver.PlaySound(Sound.sound_a);
                }
                else if (gbl.word_1EE76 == 10)
                {
                    _soundDriver.PlaySound(Sound.sound_b);
                }
                else
                {
                    _soundDriver.PlaySound(Sound.sound_a);
                }

                break;

            case 0x401F:
                _ovr008.MovePositionForward();
                break;

            case 0x4019:
                if (gbl.area_ptr.inDungeon == 0)
                {
                    gbl.mapWallType = _ovr031.getMap_wall_type(gbl.mapDirection, gbl.mapPosY, gbl.mapPosX);
                }

                break;

            case 0xE804:
                _ovr030.DrawMaybeOverlayed(gbl.byte_1D556.CurrentPicture(), true, 3, 3);

                gbl.byte_1D556.NextFrame();

                _displayDriver.GameDelay();
                break;
        }
    }
}
