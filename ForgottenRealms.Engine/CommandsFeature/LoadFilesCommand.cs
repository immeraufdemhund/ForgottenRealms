using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class LoadFilesCommand : IGameCommand
{
    public void Execute()
    {
        ovr008.vm_LoadCmdSets(3);

        gbl.byte_1AB0B = true;

        byte var_3 = (byte)ovr008.vm_GetCmdValue(1);
        byte var_2 = (byte)ovr008.vm_GetCmdValue(2);
        byte var_1 = (byte)ovr008.vm_GetCmdValue(3);

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
                ovr031.Load3DMap(var_3);
                gbl.area2_ptr.field_592 = 0;
            }

            if (var_1 != 0xff &&
                gbl.area_ptr.inDungeon == 0 &&
                gbl.lastDaxBlockId != 0x50)
            {
                ovr030.load_bigpic(0x79);
            }
        }
        else
        {
            gbl.byte_1AB0C = true;

            if (var_3 == 0x7F)
            {
                ovr031.LoadWalldef(1, 0);
            }
            else
            {
                if (gbl.area_ptr.field_1CE != 0 &&
                    gbl.area_ptr.field_1D0 != 0)
                {
                    if (var_3 != 0xff)
                    {
                        ovr031.LoadWalldef(1, var_3);
                    }

                    if (var_1 != 0xff)
                    {
                        ovr031.LoadWalldef(3, var_1);
                    }
                }
                else
                {
                    if (var_3 != 0xff)
                    {
                        ovr031.LoadWalldef(1, var_3);
                    }
                    else
                    {
                        gbl.setBlocks[0].Reset();
                    }

                    if (var_2 != 0xff)
                    {
                        ovr031.LoadWalldef(2, var_2);
                    }
                    else
                    {
                        gbl.setBlocks[1].Reset();
                    }

                    if (var_1 != 0xff)
                    {
                        ovr031.LoadWalldef(3, var_1);
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
                seg037.draw8x8_03();
                ovr025.PartySummary(gbl.SelectedPlayer);
                ovr025.display_map_position_time();
            }
            gbl.byte_1EE98 = false;
        }
    }
}