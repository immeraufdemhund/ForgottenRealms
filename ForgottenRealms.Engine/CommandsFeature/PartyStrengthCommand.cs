using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class PartyStrengthCommand : IGameCommand
{
    public void Execute()
    {
        ovr008.vm_LoadCmdSets(1);
        byte power_value = 0;

        foreach (Player player in gbl.TeamList)
        {
            int hit_points = player.hit_point_current;
            int armor_class = player.ac;
            int hit_bonus = player.hitBonus;

            int magic_power = player.SkillLevel(SkillType.MagicUser);
            int cleric_power = player.SkillLevel(SkillType.Cleric);

            if (armor_class > 60)
            {
                armor_class -= 60;
            }
            else
            {
                armor_class = 0;
            }

            if (hit_bonus > 39)
            {
                hit_bonus -= 39;
            }
            else
            {
                hit_bonus = 0;
            }

            power_value += (byte)(((cleric_power * 4) + hit_points + (armor_class * 5) + (hit_bonus * 5) + (magic_power * 8)) / 10);
        }

        ushort loc = gbl.cmd_opps[1].Word;
        ovr008.vm_SetMemoryValue(power_value, loc);
    }
}