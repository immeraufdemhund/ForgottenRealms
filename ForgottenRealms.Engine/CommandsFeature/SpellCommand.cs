using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class SpellCommand : IGameCommand
{
    public void Execute()
    {
        ovr008.vm_LoadCmdSets(3);

        byte spell_id = (byte)ovr008.vm_GetCmdValue(1);
        ushort loc_a = gbl.cmd_opps[2].Word;
        ushort loc_b = gbl.cmd_opps[3].Word;

        byte spell_index = 1;
        byte player_index = 0;

        bool spell_found = false;

        foreach (Player player in gbl.TeamList)
        {
            spell_index = 1;

            foreach (int id in player.spellList.IdList())
            {
                if (id == spell_id)
                {
                    spell_found = true;
                    break;
                }

                spell_index += 1;
            }

            if (spell_found) break;

            player_index++;
        }

        if (spell_found == false)
        {
            player_index--;
            spell_index = 0x0FF;
        }

        VmLog.WriteLine("CMD_Spell: spell_id: {0} loc a: {1} val a: {2} loc b: {3} val b: {4}",
            spell_id, new MemLoc(loc_a), spell_index, new MemLoc(loc_b), player_index);

        ovr008.vm_SetMemoryValue(spell_index, loc_a);
        ovr008.vm_SetMemoryValue(player_index, loc_b);
    }
}