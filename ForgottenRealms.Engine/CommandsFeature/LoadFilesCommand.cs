using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class LoadFilesCommand : IGameCommand
{
    private readonly ovr008 _ovr008;
    private readonly ovr025 _ovr025;
    private readonly ovr030 _ovr030;
    private readonly ovr031 _ovr031;
    private readonly seg037 _seg037;
    public LoadFilesCommand(ovr008 ovr008, ovr025 ovr025, ovr030 ovr030, ovr031 ovr031, seg037 seg037)
    {
        _ovr008 = ovr008;
        _ovr025 = ovr025;
        _ovr030 = ovr030;
        _ovr031 = ovr031;
        _seg037 = seg037;
    }

    public void Execute()
    {
        _ovr008.vm_LoadCmdSets(3);

        gbl.byte_1AB0B = true;

        var var_3 = (byte)_ovr008.vm_GetCmdValue(1);
        var var_2 = (byte)_ovr008.vm_GetCmdValue(2);
        var var_1 = (byte)_ovr008.vm_GetCmdValue(3);

        VmLog.WriteLine("CMD_LoadFile: {0} A: {1} B: {2} C: {3}",
            gbl.command == 0x21 ? "Files" : "Pieces", var_1, var_2, var_3);


        if (gbl.command == 0x21)
        {
            gbl.filesLoaded = true;

            if (var_3 != 0xff &&
                var_3 != 0x7f &&
                gbl.area_ptr.inDungeon != 0)
            {
                gbl.area_ptr.current_3DMap_block_id = var_3;
                _ovr031.Load3DMap(var_3);
                gbl.area2_ptr.field_592 = 0;
            }

            if (var_1 != 0xff &&
                gbl.area_ptr.inDungeon == 0 &&
                gbl.lastDaxBlockId != 0x50)
            {
                _ovr030.load_bigpic(0x79);
            }
        }
        else
        {
            gbl.byte_1AB0C = true;

            if (var_3 == 0x7F)
            {
                _ovr031.LoadWalldef(1, 0);
            }
            else
            {
                if (gbl.area_ptr.field_1CE != 0 &&
                    gbl.area_ptr.field_1D0 != 0)
                {
                    if (var_3 != 0xff)
                    {
                        _ovr031.LoadWalldef(1, var_3);
                    }

                    if (var_1 != 0xff)
                    {
                        _ovr031.LoadWalldef(3, var_1);
                    }
                }
                else
                {
                    if (var_3 != 0xff)
                    {
                        _ovr031.LoadWalldef(1, var_3);
                    }
                    else
                    {
                        gbl.setBlocks[0].Reset();
                    }

                    if (var_2 != 0xff)
                    {
                        _ovr031.LoadWalldef(2, var_2);
                    }
                    else
                    {
                        gbl.setBlocks[1].Reset();
                    }

                    if (var_1 != 0xff)
                    {
                        _ovr031.LoadWalldef(3, var_1);
                    }
                    else
                    {
                        gbl.setBlocks[2].Reset();
                    }
                }
            }
        }


        if (gbl.byte_1AB0C == true &&
            gbl.filesLoaded == true &&
            gbl.last_game_state == GameState.WildernessMap)
        {
            if (gbl.game_state != GameState.WildernessMap &&
                gbl.byte_1EE98 == true)
            {
                _seg037.draw8x8_03();
                _ovr025.PartySummary(gbl.SelectedPlayer);
                _ovr025.display_map_position_time();
            }

            gbl.byte_1EE98 = false;
        }
    }
}
