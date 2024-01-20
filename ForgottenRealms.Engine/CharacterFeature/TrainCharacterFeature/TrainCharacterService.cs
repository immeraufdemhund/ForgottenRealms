using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CharacterFeature.TrainCharacterFeature;

public class TrainCharacterService
{
    private static byte[] classMasks = { 2, 2, 8, 0x10, 0x20, 1, 4, 4 };

    public bool IsAllowedToTrainClass(byte arg_0, ClassId classId)
    {
        return (classMasks[(int)classId] & arg_0) != 0;
    }

    internal void train_player()
    {
        if (gbl.SelectedPlayer.health_status != Status.okey &&
            Cheats.free_training == false)
        {
            seg041.DisplayStatusText(0, 14, "we only train conscious people");
            return;
        }

        if (gbl.SelectedPlayer.Money.GetGoldWorth() < 1000 &&
            Cheats.free_training == false &&
            gbl.silent_training == false &&
            gbl.gameWon == false)
        {
            seg041.DisplayStatusText(0, 14, "Training costs 1000 gp.");
            return;
        }


        byte classesExpTrainMask = 0;
        byte classesToTrainMask = 0;
        byte class_lvl = 123; /* Simeon */

        byte trainerClassMask = gbl.area2_ptr.training_class_mask;
        Player player = gbl.SelectedPlayer;

        int var_5 = 0;

        for (int _class = 0; _class <= 7; _class++)
        {
            if (player.ClassLevel[_class] > 0)
            {
                classesToTrainMask += classMasks[_class];
                class_lvl = player.ClassLevel[_class];

                if (Limits.RaceClassLimit(class_lvl, player, (ClassId)_class) == false)
                {
                    if ((ovr018.exp_table[_class, class_lvl] > 0) &&
                        (ovr018.exp_table[_class, class_lvl] <= player.exp ||
                         Cheats.free_training == true))
                    {
                        if (Cheats.free_training == true)
                        {
                            int tmpExp = ovr018.exp_table[_class, class_lvl];
                            if (tmpExp > 0)
                            {
                                if (tmpExp > player.exp)
                                {
                                    player.exp = tmpExp;
                                }
                            }
                        }

                        classesExpTrainMask += classMasks[_class];

                        int next_lvl_exp = ovr018.exp_table[_class, (class_lvl + 1)];

                        if (next_lvl_exp > 0)
                        {
                            if (player.exp >= next_lvl_exp &&
                                next_lvl_exp > var_5)
                            {
                                var_5 = next_lvl_exp - 1;
                            }
                        }
                    }
                }
            }
        }

        if (gbl.silent_training == false)
        {
            int max_class = 0;
            int max_exp = 0;

            for (int _class = 0; _class <= 7; _class++)
            {
                if ((classMasks[_class] & classesExpTrainMask) != 0)
                {
                    if (ovr018.exp_table[_class, class_lvl] > max_exp)
                    {
                        max_exp = ovr018.exp_table[_class, class_lvl];
                        max_class = _class;
                    }
                }
            }


            if (max_exp > 0)
            {
                classesExpTrainMask = classMasks[max_class];
                int var_9 = ovr018.exp_table[max_class, class_lvl + 1];

                if (var_9 > 0 &&
                    player.exp >= var_9 &&
                    var_9 > var_5)
                {
                    var_5 = var_9 - 1;
                }
            }
        }

        if (var_5 > 0 && gbl.silent_training == false)
        {
            //player_ptr.exp = var_5;
        }

        if ((classesToTrainMask & trainerClassMask) == 0 &&
            gbl.silent_training == false &&
            Cheats.free_training == false)
        {
            seg041.DisplayStatusText(0, 14, "We don't train that class here");
            return;
        }

        if ((classesExpTrainMask & trainerClassMask) == 0)
        {
            if (gbl.silent_training == true)
            {
                gbl.can_train_no_more = true;
            }

            if (gbl.silent_training == false &&
                Cheats.free_training == false)
            {
                seg041.DisplayStatusText(0, 14, "Not Enough Experience");
                return;
            }
        }


        byte actualTrainingClassesMask;
        if (Cheats.free_training == false)
        {
            actualTrainingClassesMask = (byte)(classesExpTrainMask & trainerClassMask);
        }
        else
        {
            actualTrainingClassesMask = classesExpTrainMask;
        }

        bool skipBits = false;
        if (gbl.silent_training == true)
        {
            skipBits = true;
        }

        if (skipBits == false)
        {
            seg037.draw8x8_clear_area(0x16, 0x26, 1, 1);

            int y_offset = 4;

            ovr025.displayPlayerName(false, y_offset, 4, gbl.SelectedPlayer);

            seg041.displayString(" will become:", 0, 10, y_offset, player.name.Length + 4);

            for (int _class = 0; _class <= 7; _class++)
            {
                if (player.ClassLevel[_class] > 0 &&
                    (classMasks[_class] & actualTrainingClassesMask) != 0)
                {
                    y_offset++;

                    if (y_offset == 5)
                    {
                        string text = System.String.Format("    a level {0} {1}",
                            player.ClassLevel[_class] + 1, ovr020.classString[_class]);

                        seg041.displayString(text, 0, 10, y_offset, 6);
                    }
                    else
                    {
                        string text = System.String.Format("and a level {0} {1}",
                            player.ClassLevel[_class] + 1, ovr020.classString[_class]);

                        seg041.displayString(text, 0, 10, y_offset, 6);
                    }
                }
            }
        }

        if (skipBits || ovr027.yes_no(gbl.defaultMenuColors, "Do you wish to train? ") == 'Y')
        {
            if (skipBits == false)
            {
                ovr025.string_print01("Congratulations...");

                if (Cheats.free_training == false &&
                    gbl.gameWon == false)
                {
                    player.Money.SubtractGoldWorth(1000);
                }
            }

            byte class_count = 0;
            byte oldMagicUserLvl = player.magic_user_lvl;
            byte oldRangeLevel = player.ranger_lvl;
            player.classFlags = 0;

            for (int _class = 0; _class <= 7; _class++)
            {
                if (player.ClassLevel[_class] > 0)
                {
                    class_count++;

                    if ((classMasks[_class] & actualTrainingClassesMask) != 0)
                    {
                        player.ClassLevel[_class] += 1;
                        if (player.lost_lvls > 0)
                        {
                            player.lost_hp -= (byte)(player.lost_hp / player.lost_lvls);
                            player.lost_lvls -= 1;
                        }
                    }
                }
            }

            ovr026.ReclacClassBonuses(gbl.SelectedPlayer);

            if (gbl.silent_training == false)
            {
                if (player.magic_user_lvl > oldMagicUserLvl ||
                    player.ranger_lvl > 8)
                {
                    int index = -1;
                    byte newSpellId;
                    bool var_1D;

                    do
                    {
                        newSpellId = ovr020.spell_menu2(out var_1D, ref index, SpellSource.Learn, SpellLoc.choose);
                    } while (newSpellId <= 0 && var_1D == true);

                    if (newSpellId > 0)
                    {
                        player.LearnSpell((Spells)newSpellId);
                    }
                }
            }

            if (gbl.silent_training == true)
            {
                switch (player.magic_user_lvl)
                {
                    case 2:
                        player.LearnSpell(Spells.magic_missile);
                        break;

                    case 3:
                        player.LearnSpell(Spells.stinking_cloud);
                        player.LearnSpell(Spells.protect_from_evil_MU);
                        break;

                    case 4:
                        player.LearnSpell(Spells.knock);
                        break;

                    case 5:
                        player.LearnSpell(Spells.fireball);
                        break;
                }
            }

            if (player.HitDice <= player.multiclassLevel)
            {
                return;
            }

            short var_F = ovr018.sub_509E0(actualTrainingClassesMask, gbl.SelectedPlayer);

            int max_hp_increase = var_F / class_count;

            if (max_hp_increase == 0)
            {
                max_hp_increase = 1;
            }

            player.hit_point_rolled += (byte)max_hp_increase;

            int var_15 = ovr018.get_con_hp_adj(gbl.SelectedPlayer);

            max_hp_increase = (var_F + var_15) / class_count;

            if (max_hp_increase < 1)
            {
                max_hp_increase = 1;
            }

            int hp_lost = player.hit_point_max - player.hit_point_current;

            player.hit_point_max += (byte)max_hp_increase;
            player.hit_point_current = (byte)(player.hit_point_max - hp_lost);
        }
    }
}
