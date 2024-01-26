using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CharacterFeature;

public class HitPointTable
{
    private readonly ovr018 _ovr018;

    public HitPointTable(ovr018 ovr018)
    {
        _ovr018 = ovr018;
    }

    internal int calc_max_hp(Player player) /* sub_50793 */
    {
        int class_count = 0;
        int max_hp = 0;

        for (int class_index = 0; class_index <= 7; class_index++)
        {
            if (player.ClassLevel[class_index] > 0)
            {
                hp_calc hpt = hp_calc_table[class_index];

                int var_4 = _ovr018.con_bonus((ClassId)class_index);

                if (player.ClassLevel[class_index] < gbl.max_class_hit_dice[class_index])
                {
                    class_count++;
                    max_hp += (var_4 + hpt.dice) * (player.ClassLevel[class_index] + hpt.lvl_bonus);
                }
                else
                {
                    class_count++;
                    int over_count = (player.ClassLevel[class_index] - gbl.max_class_hit_dice[class_index]) + 1;

                    max_hp = hpt.max_base + (over_count * hpt.max_mult);
                }
            }
        }

        max_hp /= class_count;

        return max_hp;
    }

    public int GetLevelBonusForClass(ClassId classIndex)
    {
        return hp_calc_table[(int)classIndex].lvl_bonus;
    }

    private class hp_calc
    {
        public hp_calc(int _dice, int _lvl, int _base, int _mult) { dice = _dice; lvl_bonus = _lvl; max_base = _base; max_mult = _mult; }

        public int dice;
        public int lvl_bonus;
        public int max_base;
        public int max_mult;
    }

    private static hp_calc[] hp_calc_table = {
        new hp_calc(8, 0, 0x48, 2), // Cleric
        new hp_calc(8, 0, 0x70, 0), // Druid
        new hp_calc(10, 0, 0x5A, 3), // Fighter
        new hp_calc(10, 0, 0x5A, 3), // Paladin
        new hp_calc(8, 1, 0x58, 2), // Ranger
        new hp_calc(4, 0, 0x2c, 1), // Magic User
        new hp_calc(6, 0, 0x3c, 2), // Thief
        new hp_calc(4, 1, 0x48, 0), // Monk
    };
}
