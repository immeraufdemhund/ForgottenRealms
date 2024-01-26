using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class ApproachCommand : IGameCommand
{
    private readonly ovr008 _ovr008;
    public ApproachCommand(ovr008 ovr008)
    {
        _ovr008 = ovr008;
    }

    public void Execute()
    {
        if (gbl.area2_ptr.encounter_distance > 0)
        {
            gbl.area2_ptr.encounter_distance--;

            _ovr008.sub_30580(gbl.encounter_flags, gbl.area2_ptr.encounter_distance, gbl.pic_block_id, gbl.sprite_block_id);
        }

        gbl.ecl_offset++;
    }
}
