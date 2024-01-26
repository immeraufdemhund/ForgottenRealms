using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class CheckPartyCommand : IGameCommand
{
    private readonly ovr008 _ovr008;
    public CheckPartyCommand(ovr008 ovr008)
    {
        _ovr008 = ovr008;
    }

    public void Execute()
    {
        int var_4;
        ushort var_2;

        _ovr008.vm_LoadCmdSets(6);

        if (gbl.cmd_opps[1].Code == 1)
        {
            var_2 = gbl.cmd_opps[1].Word;
        }
        else
        {
            var_2 = _ovr008.vm_GetCmdValue(1);
        }

        var affect_id = (Affects)_ovr008.vm_GetCmdValue(2);

        var loc_a = gbl.cmd_opps[3].Word;
        var loc_b = gbl.cmd_opps[4].Word;
        var loc_c = gbl.cmd_opps[5].Word;
        var loc_d = gbl.cmd_opps[6].Word;

        var_4 = 0;
        byte val_a = 0x0FF;
        byte val_b = 0;
        byte val_c;

        var_2 -= 0x7fff;

        if (var_2 == 8001)
        {
            var affect_found = gbl.TeamList.Exists(player => player.HasAffect(affect_id));

            setMemoryFour(affect_found, 0, 0, 0, loc_a, loc_b, loc_c, loc_d);
        }
        else if (var_2 >= 0x00A5 && var_2 <= 0x00AC)
        {
            var index = var_2 - 0xA4;
            var count = 0;
            foreach (var player in gbl.TeamList)
            {
                count++;

                if (player.thief_skills[index - 1] < val_a)
                {
                    val_a = player.thief_skills[index - 1];
                }

                if (player.thief_skills[index - 1] > val_b)
                {
                    val_b = player.thief_skills[index - 1];
                }

                var_4 += player.thief_skills[index - 1];
            }

            val_c = (byte)(var_4 / count);

            setMemoryFour(false, val_c, val_b, val_a, loc_a, loc_b, loc_c, loc_d);
        }
        else if (var_2 == 0x9f)
        {
            var count = 0;
            foreach (var player in gbl.TeamList)
            {
                count++;

                if (player.movement < val_a)
                {
                    val_a = player.movement;
                }

                if (player.movement > val_b)
                {
                    val_b = player.movement;
                }

                var_4 += player.movement;
            }

            val_c = (byte)(var_4 / count);

            setMemoryFour(false, val_c, val_b, val_a, loc_a, loc_b, loc_c, loc_d);
        }
    }

    private void setMemoryFour(bool val_d, byte val_c, byte val_b, byte val_a,
        ushort loc_a, ushort loc_b, ushort loc_c, ushort loc_d) /* sub_273F6 */
    {
        _ovr008.vm_SetMemoryValue(val_a, loc_a);
        _ovr008.vm_SetMemoryValue(val_b, loc_b);
        _ovr008.vm_SetMemoryValue(val_c, loc_c);
        _ovr008.vm_SetMemoryValue(val_d ? (ushort)1 : (ushort)0, loc_d);
    }
}
