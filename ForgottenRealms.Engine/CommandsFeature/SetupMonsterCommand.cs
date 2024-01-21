using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class SetupMonsterCommand : IGameCommand
{
    public void Execute()
    {
        ovr008.vm_LoadCmdSets(3);

        byte sprite_id = (byte)ovr008.vm_GetCmdValue(1);
        byte max_distance = (byte)ovr008.vm_GetCmdValue(2);
        byte pic_id = (byte)ovr008.vm_GetCmdValue(3);

        VmLog.WriteLine("CMD_SetupMonster: sprite id: {0} area2_ptr.field_580: {1} pic id: {2}", sprite_id, max_distance, pic_id);

        gbl.sprite_block_id = sprite_id;
        gbl.area2_ptr.max_encounter_distance = max_distance;
        gbl.pic_block_id = pic_id;

        gbl.area2_ptr.encounter_distance = ovr008.sub_304B4(gbl.mapDirection, gbl.mapPosY, gbl.mapPosX);

        if (gbl.area2_ptr.max_encounter_distance < gbl.area2_ptr.encounter_distance)
        {
            gbl.area2_ptr.encounter_distance = gbl.area2_ptr.max_encounter_distance;
        }
        ovr008.sub_30580(gbl.encounter_flags, gbl.area2_ptr.encounter_distance, gbl.pic_block_id, gbl.sprite_block_id);
    }
}