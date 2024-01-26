using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class SpellCommand : IGameCommand
{
    private readonly ovr008 _ovr008;
    public SpellCommand(ovr008 ovr008)
    {
        _ovr008 = ovr008;
    }

    public void Execute()
    {
        _ovr008.vm_LoadCmdSets(3);

        var spell_id = (byte)_ovr008.vm_GetCmdValue(1);
        var loc_a = gbl.cmd_opps[2].Word;
        var loc_b = gbl.cmd_opps[3].Word;

        byte spell_index = 1;
        byte player_index = 0;

        var spell_found = false;

        foreach (var player in gbl.TeamList)
        {
            spell_index = 1;

            foreach (var id in player.spellList.IdList())
            {
                if (id == spell_id)
                {
                    spell_found = true;
                    break;
                }

                spell_index += 1;
            }

            if (spell_found)
            {
                break;
            }

            player_index++;
        }

        if (spell_found == false)
        {
            player_index--;
            spell_index = 0x0FF;
        }

        VmLog.WriteLine("CMD_Spell: spell_id: {0} loc a: {1} val a: {2} loc b: {3} val b: {4}",
            spell_id, new MemLoc(loc_a), spell_index, new MemLoc(loc_b), player_index);

        _ovr008.vm_SetMemoryValue(spell_index, loc_a);
        _ovr008.vm_SetMemoryValue(player_index, loc_b);
    }
}
