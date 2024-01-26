using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class PictureCommand : IGameCommand
{
    private readonly ovr008 _ovr008;
    private readonly ovr029 _ovr029;
    private readonly ovr030 _ovr030;
    public PictureCommand(ovr008 ovr008, ovr029 ovr029, ovr030 ovr030)
    {
        _ovr008 = ovr008;
        _ovr029 = ovr029;
        _ovr030 = ovr030;
    }

    public void Execute()
    {
        _ovr008.vm_LoadCmdSets(1);
        var blockId = (byte)_ovr008.vm_GetCmdValue(1);

        if (blockId != 0xff)
        {
            gbl.encounter_flags[1] = true;
            gbl.spriteChanged = true;

            if (gbl.area2_ptr.HeadBlockId == 0xff)
            {
                gbl.byte_1EE8D = true;

                if (blockId >= 0x78)
                {
                    _ovr030.load_bigpic(blockId);
                    _ovr030.draw_bigpic();
                    gbl.can_draw_bigpic = false;
                }
                else
                {
                    _ovr030.load_pic_final(ref gbl.byte_1D556, 0, blockId, "PIC");
                    _ovr030.DrawMaybeOverlayed(gbl.byte_1D556.frames[0].picture, true, 3, 3);
                }
            }
            else
            {
                _ovr008.set_and_draw_head_body(blockId, (byte)gbl.area2_ptr.HeadBlockId);
            }
        }
        else
        {
            if ((gbl.last_game_state != GameState.DungeonMap || gbl.game_state == GameState.DungeonMap) &&
                (gbl.spriteChanged == true || gbl.displayPlayerSprite))
            {
                gbl.can_draw_bigpic = true;
                _ovr029.RedrawView();
                gbl.spriteChanged = false;
                gbl.displayPlayerSprite = false;
                gbl.byte_1EE8D = true;
            }

            gbl.encounter_flags[0] = false;
            gbl.encounter_flags[1] = false;
        }
    }
}
