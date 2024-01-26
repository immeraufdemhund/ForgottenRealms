using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class SetupMonsterCommand : IGameCommand
{
    private readonly ovr008 _ovr008;
    public SetupMonsterCommand(ovr008 ovr008)
    {
        _ovr008 = ovr008;
    }

    public void Execute()
    {
        _ovr008.vm_LoadCmdSets(3);

        var sprite_id = (byte)_ovr008.vm_GetCmdValue(1);
        var max_distance = (byte)_ovr008.vm_GetCmdValue(2);
        var pic_id = (byte)_ovr008.vm_GetCmdValue(3);

        VmLog.WriteLine("CMD_SetupMonster: sprite id: {0} area2_ptr.field_580: {1} pic id: {2}", sprite_id, max_distance, pic_id);

        gbl.sprite_block_id = sprite_id;
        gbl.area2_ptr.max_encounter_distance = max_distance;
        gbl.pic_block_id = pic_id;

        gbl.area2_ptr.encounter_distance = _ovr008.sub_304B4(gbl.mapDirection, gbl.mapPosY, gbl.mapPosX);

        if (gbl.area2_ptr.max_encounter_distance < gbl.area2_ptr.encounter_distance)
        {
            gbl.area2_ptr.encounter_distance = gbl.area2_ptr.max_encounter_distance;
        }

        _ovr008.sub_30580(gbl.encounter_flags, gbl.area2_ptr.encounter_distance, gbl.pic_block_id, gbl.sprite_block_id);
    }
}
