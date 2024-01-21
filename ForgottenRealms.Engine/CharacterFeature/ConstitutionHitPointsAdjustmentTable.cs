using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CharacterFeature;

public class ConstitutionHitPointsAdjustmentTable
{
    private static sbyte[] con_hp_adj = { 0, 0, 0, -2, -1, -1, -1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 };

    internal sbyte get_con_hp_adj(Player player)
    {
        sbyte hp_adj = 0;

        for (var class_index = 0; class_index <= (byte)ClassId.monk; class_index++)
        {
            if (player.ClassLevel[class_index] > 0 &&
                player.ClassLevel[class_index] < gbl.max_class_hit_dice[class_index])
            {
                hp_adj += con_hp_adj[player.stats2.Con.full];

                if (player._class == ClassId.fighter ||
                    player._class == ClassId.paladin ||
                    player._class == ClassId.ranger)
                {
                    var con = player.stats2.Con.full;

                    if (con == 17)
                    {
                        hp_adj++;
                    }
                    else if (con == 18)
                    {
                        hp_adj += 2;
                    }
                    else if (con == 19 || con == 20)
                    {
                        hp_adj += 3;
                    }
                    else if (con >= 21 && con <= 23)
                    {
                        hp_adj += 4;
                    }
                    else if (con == 24 || con == 25)
                    {
                        hp_adj += 5;
                    }
                }

                if (class_index == (byte)ClassId.ranger &&
                    player.ClassLevel[class_index] == 1)
                {
                    hp_adj *= 2;
                }
            }
        }

        return hp_adj;
    }
}
