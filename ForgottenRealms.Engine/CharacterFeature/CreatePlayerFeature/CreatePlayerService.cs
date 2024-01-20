using System;
using System.Collections.Generic;
using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CharacterFeature.CreatePlayerFeature;

public class CreatePlayerService
{
    private readonly IconBuilder _iconBuilder = new ();

    internal void createPlayer()
    {
        bool menuRedraw;
        bool showExit;
        byte var_20;
        short var_1E;
        byte var_1B;

        char input_key;
        int index;
        MenuItem selected;

        Player player = new Player();

        for (int i = 0; i < 6; i++)
        {
            player.icon_colours[i] = (byte)(((gbl.default_icon_colours[i] + 8) << 4) + gbl.default_icon_colours[i]);
        }

        player.base_ac = 50;
        player.thac0 = 40;
        player.health_status = Status.okey;
        player.in_combat = true;
        player.field_DE = 1;
        player.mod_id = (byte)seg051.Random(256);
        player.icon_id = 0x0A;

        List<MenuItem> var_C = new List<MenuItem>();
        var_C.Add(new MenuItem("Pick Race", true));

        var_C.Add(new MenuItem("  " + ovr020.raceString[1]));
        var_C.Add(new MenuItem("  " + ovr020.raceString[2]));
        var_C.Add(new MenuItem("  " + ovr020.raceString[3]));
        var_C.Add(new MenuItem("  " + ovr020.raceString[4]));
        var_C.Add(new MenuItem("  " + ovr020.raceString[5]));
        var_C.Add(new MenuItem("  " + ovr020.raceString[7]));

        index = 0;
        menuRedraw = true;
        showExit = true;

        do
        {
            input_key = ovr027.sl_select_item(out selected, ref index, ref menuRedraw, showExit, var_C,
                22, 38, 2, 1, gbl.defaultMenuColors, "Select", string.Empty);

            if (input_key == '\0')
            {
                var_C.Clear();
                return;
            }
        } while (input_key != 'S');

        if (index == 6)
        {
            index++;
        }

        player.race = (Race)index;

        switch (player.race)
        {
            case Race.halfling:
                player.icon_size = 1;
                ovr024.add_affect(false, 0xff, 0, Affects.con_saving_bonus, player);
                break;

            case Race.dwarf:
                player.icon_size = 1;
                ovr024.add_affect(false, 0xff, 0, Affects.con_saving_bonus, player);
                ovr024.add_affect(false, 0xff, 0, Affects.dwarf_vs_orc, player);
                ovr024.add_affect(false, 0xff, 0, Affects.dwarf_and_gnome_vs_giants, player);
                break;

            case Race.gnome:
                player.icon_size = 1;
                ovr024.add_affect(false, 0xff, 0, Affects.con_saving_bonus, player);
                ovr024.add_affect(false, 0xff, 0, Affects.gnome_vs_man_sized_giant, player);
                ovr024.add_affect(false, 0xff, 0, Affects.dwarf_and_gnome_vs_giants, player);
                ovr024.add_affect(false, 0xff, 0, Affects.affect_30, player);
                break;

            case Race.elf:
                player.icon_size = 2;
                ovr024.add_affect(false, 0xff, 0, Affects.elf_resist_sleep, player);

                break;

            case Race.half_elf:
                player.icon_size = 2;
                ovr024.add_affect(false, 0xff, 0, Affects.halfelf_resistance, player);
                break;

            default:
                player.icon_size = 2;
                break;
        }

        /* Gender */

        var_C.Clear();

        var_C.Add(new MenuItem("Pick Gender", true));
        var_C.Add(new MenuItem("  " + ovr020.sexString[0]));
        var_C.Add(new MenuItem("  " + ovr020.sexString[1]));

        index = 1;
        showExit = true;
        menuRedraw = true;

        do
        {
            input_key = ovr027.sl_select_item(out selected, ref index, ref menuRedraw, showExit, var_C,
                22, 38, 2, 1, gbl.defaultMenuColors, "Select", string.Empty);

            if (input_key == '\0')
            {
                var_C.Clear();
                player = null;
                return;
            }
        } while (input_key != 'S');


        player.sex = (byte)(index - 1);
        var_C.Clear();

        var_C.Add(new MenuItem("Pick Class", true));

        var ClassList = gbl.RaceClasses[(int)player.race];
        if (player.race != Race.human && Cheats.no_race_class_restrictions)
        {
            ClassList = gbl.RaceClasses[(int)Race.human + 1];
        }

        foreach (var _class in ClassList)
        {
            var_C.Add(new MenuItem("  " + ovr020.classString[(int)_class]));
        }

        index = 1;
        showExit = true;
        menuRedraw = true;

        do
        {
            input_key = ovr027.sl_select_item(out selected, ref index, ref menuRedraw, showExit, var_C,
                22, 38, 2, 1, gbl.defaultMenuColors, "Select", string.Empty);

            if (input_key == '\0')
            {
                var_C.Clear();
                player = null;
                return;
            }
        } while (input_key != 'S');

        player.exp = 25000;
        player._class = ClassList[index - 1];
        player.HitDice = 1;

        if (player._class >= ClassId.cleric && player._class <= ClassId.fighter)
        {
            player.ClassLevel[(int)player._class] = 1;
        }
        else if (player._class >= ClassId.magic_user && player._class <= ClassId.monk)
        {
            player.ClassLevel[(int)player._class] = 1;
        }
        else if (player._class == ClassId.paladin)
        {
            player.paladinCuresLeft = 1;
            player.paladin_lvl = 1;
            ovr024.add_affect(false, 0xff, 0, Affects.protection_from_evil, player);
        }
        else if (player._class == ClassId.ranger)
        {
            player.ranger_lvl = 1;
            ovr024.add_affect(false, 0xff, 0, Affects.ranger_vs_giant, player);
        }
        else if (player._class == ClassId.mc_c_f)
        {
            player.cleric_lvl = 1;
            player.fighter_lvl = 1;
            player.exp = 12500;
        }
        else if (player._class == ClassId.mc_c_f_m)
        {
            player.cleric_lvl = 1;
            player.fighter_lvl = 1;
            player.magic_user_lvl = 1;
            player.exp = 8333;
        }
        else if (player._class == ClassId.mc_c_r)
        {
            player.cleric_lvl = 1;
            player.ranger_lvl = 1;
            ovr024.add_affect(false, 0xff, 0, Affects.ranger_vs_giant, player);
            player.exp = 12500;
        }
        else if (player._class == ClassId.mc_c_mu)
        {
            player.cleric_lvl = 1;
            player.magic_user_lvl = 1;
            player.exp = 12500;
        }
        else if (player._class == ClassId.mc_c_t)
        {
            player.cleric_lvl = 1;
            player.thief_lvl = 1;
            player.exp = 12500;
        }
        else if (player._class == ClassId.mc_f_mu)
        {
            player.fighter_lvl = 1;
            player.magic_user_lvl = 1;
            player.exp = 12500;
        }
        else if (player._class == ClassId.mc_f_t)
        {
            player.fighter_lvl = 1;
            player.thief_lvl = 1;
            player.exp = 12500;
        }
        else if (player._class == ClassId.mc_f_mu_t)
        {
            player.fighter_lvl = 1;
            player.magic_user_lvl = 1;
            player.thief_lvl = 1;
            player.exp = 8333;
        }
        else if (player._class == ClassId.mc_mu_t)
        {
            player.magic_user_lvl = 1;
            player.thief_lvl = 1;
            player.exp = 8333;
        }

        if (player.thief_lvl > 0)
        {
            ovr026.reclac_thief_skills(player);
        }

        player.classFlags = 0;
        player.thac0 = 0;

        for (int class_idx = 0; class_idx <= 7; class_idx++)
        {
            if (player.ClassLevel[class_idx] > 0)
            {
                int skill_lvl = player.ClassLevel[class_idx];

                if (ovr018.thac0_table[class_idx, skill_lvl] > player.thac0)
                {
                    player.thac0 = ovr018.thac0_table[class_idx, skill_lvl];
                }

                player.classFlags += ovr018.unk_1A1B2[class_idx];
            }
        }

        ovr026.reclac_saving_throws(player);
        var_C.Clear();

        int alignments = gbl.class_alignments[(int)player._class, 0];

        var_C.Add(new MenuItem("Pick Alignment", true));

        for (int i = 1; i <= alignments; i++)
        {
            var_C.Add(new MenuItem("  " + ovr020.alignmentString[gbl.class_alignments[(int)player._class, i]]));
        }

        index = 1;
        showExit = true;
        menuRedraw = true;

        do
        {
            input_key = ovr027.sl_select_item(out selected, ref index, ref menuRedraw, showExit, var_C,
                22, 38, 2, 1, gbl.defaultMenuColors, "Select", string.Empty);


            if (input_key == '\0')
            {
                var_C.Clear();

                player = null;
                return;
            }
        } while (input_key != 'S');

        player.alignment = gbl.class_alignments[(int)player._class, index];

        var_C.Clear();

        if (player._class <= ClassId.monk)
        {
            AgeTable v5 = gbl.race_ages[(int)player.race][player._class];

            player.age = (short)(ovr024.roll_dice(v5.DiceSize, v5.DiceCount) + v5.BaseAge);
        }
        else
        {
            int race = (int)player.race;

            switch (player._class)
            {
                case ClassId.mc_c_f:
                case ClassId.mc_c_f_m:
                case ClassId.mc_c_t:
                case ClassId.mc_c_r:
                    player.age = (short)(gbl.race_ages[race][0].BaseAge + (gbl.race_ages[race][0].DiceCount * gbl.race_ages[race][0].DiceSize));
                    break;

                case ClassId.mc_f_mu:
                case ClassId.mc_f_mu_t:
                case ClassId.mc_mu_t:
                    player.age = (short)(gbl.race_ages[race][6].BaseAge + (gbl.race_ages[race][6].DiceCount * gbl.race_ages[race][6].DiceSize));
                    break;

                case ClassId.mc_f_t:
                    player.age = (short)(gbl.race_ages[race][2].BaseAge + (gbl.race_ages[race][2].DiceCount * gbl.race_ages[race][2].DiceSize));
                    break;
            }
        }

        Player gblPlayerPtrBkup = gbl.SelectedPlayer;
        gbl.SelectedPlayer = player;
        ovr020.playerDisplayFull(player);

        do
        {
            for (int class_idx = 0; class_idx <= 7; class_idx++)
            {
                if (player.ClassLevel[class_idx] > 0)
                {
                    player.ClassLevel[class_idx] = 1;
                }
            }

            player.stats2.Str.full = 0;
            player.stats2.Int.full = 0;
            player.stats2.Wis.full = 0;
            player.stats2.Dex.full = 0;
            player.stats2.Con.full = 0;
            player.stats2.Cha.full = 0;
            player.stats2.Str00.full = 0;

            for (int i = 0; i < 6; i++)
            {
                player.stats2.Str.full = Math.Max(player.stats2.Str.full, ovr024.roll_dice(6, 3) + 1);
                player.stats2.Int.full = Math.Max(player.stats2.Int.full, ovr024.roll_dice(6, 3) + 1);
                player.stats2.Wis.full = Math.Max(player.stats2.Wis.full, ovr024.roll_dice(6, 3) + 1);
                player.stats2.Dex.full = Math.Max(player.stats2.Dex.full, ovr024.roll_dice(6, 3) + 1);
                player.stats2.Con.full = Math.Max(player.stats2.Con.full, ovr024.roll_dice(6, 3) + 1);
                player.stats2.Cha.full = Math.Max(player.stats2.Cha.full, ovr024.roll_dice(6, 3) + 1);
            }

            int race = (int)player.race;
            int sex = player.sex;

            for (var_1B = 0; var_1B < 6; var_1B++)
            {
                switch ((Stat)var_1B)
                {
                    case Stat.STR:
                        player.stats2.Str.AgeEffects(race, player.age);
                        player.stats2.Str.EnforceRaceSexLimits(race, sex);
                        player.stats2.Str.EnforceClassLimits((int)player._class);

                        if (player.stats2.Str.full == 18)
                        {
                            if (player.fighter_lvl > 0 ||
                                player.ranger_lvl > 0 ||
                                player.paladin_lvl > 0)
                            {
                                player.stats2.Str00.Load(seg051.Random(100) + 1);
                                player.stats2.Str00.EnforceRaceSexLimits(race, sex);
                            }
                        }

                        break;

                    case Stat.INT:
                        player.stats2.Int.AgeEffects(race, player.age);
                        player.stats2.Int.EnforceRaceSexLimits(race, sex);
                        player.stats2.Int.EnforceClassLimits((int)player._class);
                        break;

                    case Stat.WIS:
                        player.stats2.Wis.AgeEffects(race, player.age);
                        player.stats2.Wis.EnforceRaceSexLimits(race, sex);
                        player.stats2.Wis.EnforceClassLimits((int)player._class);

                        if (player.stats2.Wis.full < 13 &&
                            player._class >= ClassId.mc_c_f && player._class <= ClassId.mc_c_t)
                        {
                            // Multi-Class Cleric
                            player.stats2.Wis.full = 13;
                        }

                        break;

                    case Stat.DEX:
                        player.stats2.Dex.AgeEffects(race, player.age);
                        player.stats2.Dex.EnforceRaceSexLimits(race, sex);
                        player.stats2.Dex.EnforceClassLimits((int)player._class);
                        break;

                    case Stat.CON:
                        player.stats2.Con.AgeEffects(race, player.age);
                        player.stats2.Con.EnforceRaceSexLimits(race, sex);
                        player.stats2.Con.EnforceClassLimits((int)player._class);
                        break;

                    case Stat.CHA:
                        player.stats2.Cha.AgeEffects(race, player.age);
                        player.stats2.Cha.EnforceRaceSexLimits(race, sex);
                        player.stats2.Cha.EnforceClassLimits((int)player._class);
                        break;
                }

                ovr020.display_stat(false, var_1B);
            }

            player.hit_point_current = player.hit_point_max;
            player.attacksCount = 2;
            player.attack1_DiceCountBase = 1;
            player.attack1_DiceSizeBase = 2;
            player.field_125 = 1;
            player.base_movement = 12;
            var_20 = 0;

            for (int i = 0; i < 5; i++)
            {
                player.spellCastCount[0, i] = 0;
                player.spellCastCount[1, i] = 0;
                player.spellCastCount[2, i] = 0;
            }

            for (int class_idx = 0; class_idx <= 7; class_idx++)
            {
                if (player.ClassLevel[class_idx] > 0)
                {
                    if (class_idx == 0)
                    {
                        player.spellCastCount[0, 0] = 1;
                    }
                    else if (class_idx == 5)
                    {
                        player.spellCastCount[2, 0] = 1;
                    }

                    //var_21 += ovr024.roll_dice(unk_1A8C4[class_idx], unk_1A8C3[class_idx]);
                    //TODO this was not used in original code.

                    if (class_idx == 0)
                    {
                        ovr026.calc_cleric_spells(false, player);

                        foreach (Spells spell in System.Enum.GetValues(typeof(Spells)))
                        {
                            SpellEntry stru = gbl.spellCastingTable[(int)spell];

                            if (stru.spellClass == 0 && stru.spellLevel == 1)
                            {
                                player.LearnSpell(spell);
                            }
                        }
                    }
                    else if (class_idx == 5)
                    {
                        player.LearnSpell(Spells.detect_magic_MU);
                        player.LearnSpell(Spells.read_magic);
                        player.LearnSpell(Spells.enlarge);
                        player.LearnSpell(Spells.sleep);
                    }

                    var_20++;
                }
            }

            player.Money.SetCoins(Money.Platinum, 300);
            player.hit_point_rolled = ovr018.sub_509E0(0xff, player);
            player.hit_point_max = player.hit_point_rolled;

            var_1E = ovr018.get_con_hp_adj(player);

            if (var_1E < 0)
            {
                if (player.hit_point_max > (System.Math.Abs(var_1E) + var_20))
                {
                    player.hit_point_max = (byte)((player.hit_point_max + var_1E) / var_20);
                }
                else
                {
                    player.hit_point_max = 1;
                }
            }
            else
            {
                player.hit_point_max = (byte)((player.hit_point_max + var_1E) / var_20);
            }

            player.hit_point_current = player.hit_point_max;
            player.hit_point_rolled = (byte)(player.hit_point_rolled / var_20);
            byte trainingClassMaskBackup = gbl.area2_ptr.training_class_mask;

            ovr017.SilentTrainPlayer();

            gbl.area2_ptr.training_class_mask = trainingClassMaskBackup;
            bool first_lvl = true;
            string text = string.Empty;

            for (int class_idx = 0; class_idx <= 7; class_idx++)
            {
                if (player.ClassLevel[class_idx] > 0 ||
                    (player.ClassLevelsOld[class_idx] < ovr026.HumanCurrentClassLevel_Zero(player) &&
                     player.ClassLevelsOld[class_idx] > 0))
                {
                    if (first_lvl == false)
                    {
                        text += "/";
                    }

                    byte b = player.ClassLevelsOld[class_idx];
                    b += player.ClassLevel[class_idx];

                    text += b.ToString();

                    first_lvl = false;
                }
            }

            seg041.displayString(text, 0, 15, 15, 7);
            ovr020.display_player_stats01();
            ovr020.displayMoney();

            input_key = ovr027.yes_no(gbl.defaultMenuColors, "Reroll stats? ");
        } while (input_key != 'N');

        ovr020.playerDisplayFull(player);

        do
        {
            player.name = seg041.getUserInputString(15, 0, 13, "Character name: ");
        } while (player.name.Length == 0);

        _iconBuilder.Show();

        //for (var_1B = 0; var_1B <= 5; var_1B++)
        //{
        //    player.stats2[var_1B].cur = player.stats2[var_1B].full;
        //}

        player.stats2.Str00.full = player.stats2.Str00.cur;

        input_key = ovr027.yes_no(gbl.defaultMenuColors, "Save " + player.name + "? ");

        if (input_key == 'Y')
        {
            ovr017.SavePlayer(string.Empty, player);
        }

        gbl.SelectedPlayer = gblPlayerPtrBkup;
    }
}
