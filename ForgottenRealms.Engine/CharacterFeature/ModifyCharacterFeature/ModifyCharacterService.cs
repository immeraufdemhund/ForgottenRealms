using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CharacterFeature.ModifyCharacterFeature;

public class ModifyCharacterService
{
    private readonly HitPointTable _hitPointTable = new ();

    internal void modifyPlayer()
    {
        bool controlkey;
        char inputkey;

        if (Cheats.allow_player_modify == false &&
            (gbl.SelectedPlayer.exp != 0 &&
             gbl.SelectedPlayer.exp != 8333 &&
             gbl.SelectedPlayer.exp != 12500 &&
             gbl.SelectedPlayer.exp != 25000) ||
            gbl.SelectedPlayer.multiclassLevel != 0)
        {
            DisplayDriver.DisplayStatusText(0, 14, gbl.SelectedPlayer.name + " can't be modified.");
            return;
        }

        ovr020.playerDisplayFull(gbl.SelectedPlayer);

        PlayerStats stats_bkup = new PlayerStats();
        stats_bkup.Assign(gbl.SelectedPlayer.stats2);

        byte orig_hp_max = gbl.SelectedPlayer.hit_point_max;

        string nameBackup = gbl.SelectedPlayer.name;

        int name_cursor_pos = 1;
        byte edited_stat = 7;
        ovr018.draw_highlight_stat(false, edited_stat, name_cursor_pos);
        edited_stat = 0;
        ovr018.draw_highlight_stat(true, edited_stat, name_cursor_pos);
        Player player = gbl.SelectedPlayer;

        do
        {
            if (edited_stat == 7)
            {
                while (seg049.KEYPRESSED() == false)
                {
                    /* empty */
                }

                inputkey = (char)seg043.GetInputKey();

                if (inputkey == 0)
                {
                    inputkey = (char)seg043.GetInputKey();
                    controlkey = true;
                }
                else
                {
                    controlkey = false;
                }

                if (inputkey == 0x1B)
                {
                    inputkey = '\0';
                }
            }
            else
            {
                inputkey = ovr027.displayInput(out controlkey, false, 1, gbl.defaultMenuColors, "Keep Exit", "Modify: ");
            }

            ovr018.draw_highlight_stat(false, edited_stat, name_cursor_pos);

            if (controlkey == true)
            {
                var selectedPlayerMaxHp = _hitPointTable.calc_max_hp(gbl.SelectedPlayer);
                switch (inputkey)
                {
                    case 'S':
                        if (edited_stat == 7 && gbl.SelectedPlayer.name.Length > 1)
                        {
                            if (name_cursor_pos == gbl.SelectedPlayer.name.Length)
                            {
                                gbl.SelectedPlayer.name = gbl.SelectedPlayer.name.Substring(0, gbl.SelectedPlayer.name.Length - 1);
                                name_cursor_pos = (byte)gbl.SelectedPlayer.name.Length;
                            }
                            else
                            {
                                string part_a = gbl.SelectedPlayer.name.Substring(0, name_cursor_pos);
                                string part_b = gbl.SelectedPlayer.name.Substring(name_cursor_pos + 1, gbl.SelectedPlayer.name.Length - name_cursor_pos);
                                gbl.SelectedPlayer.name = part_a + part_b;
                            }
                        }
                        break;

                    case 'O':
                        edited_stat++;

                        if (edited_stat > 7)
                        {
                            edited_stat = 0;
                        }
                        break;

                    case 'G':
                        edited_stat -= 1;

                        if (edited_stat == 0xff)
                        {
                            edited_stat = 7;
                        }
                        break;

                    case 'K':
                        if (edited_stat < 6)
                        {
                            int stat_var = edited_stat;
                            int race = (int)player.race;
                            int sex = player.sex;

                            player.stats2.Dec(stat_var);

                            switch ((Stat)stat_var)
                            {
                                case Stat.STR:
                                    if (player.stats2.Str00.cur > 0)
                                    {
                                        player.stats2.Str00.Dec();
                                        player.stats2.Str.Inc();
                                    }
                                    else
                                    {
                                        player.stats2.Str.EnforceRaceSexLimits(race, sex);
                                    }
                                    player.stats2.Str.EnforceClassLimits((int)player._class);
                                    break;

                                case Stat.INT:
                                    player.stats2.Int.EnforceRaceSexLimits(race, sex);
                                    player.stats2.Int.EnforceClassLimits((int)player._class);
                                    break;

                                case Stat.WIS:
                                    player.stats2.Wis.EnforceRaceSexLimits(race, sex);
                                    player.stats2.Wis.EnforceClassLimits((int)player._class);

                                    if (player.spellCastCount[0, 0] > 0)
                                    {
                                        player.spellCastCount[0, 0] = 1;
                                    }
                                    break;

                                case Stat.DEX:
                                    player.stats2.Dex.EnforceRaceSexLimits(race, sex);
                                    player.stats2.Dex.EnforceClassLimits((int)player._class);
                                    break;

                                case Stat.CON:
                                    player.stats2.Con.EnforceRaceSexLimits(race, sex);
                                    player.stats2.Con.EnforceClassLimits((int)player._class);

                                    if (selectedPlayerMaxHp < player.hit_point_max)
                                    {
                                        player.hit_point_max = (byte)selectedPlayerMaxHp;
                                    }

                                    player.hit_point_current = player.hit_point_max;
                                    edited_stat = 6;
                                    ovr018.draw_highlight_stat(false, edited_stat, name_cursor_pos);
                                    edited_stat = 4;
                                    break;

                                case Stat.CHA:
                                    player.stats2.Cha.EnforceRaceSexLimits(race, sex);
                                    player.stats2.Cha.EnforceClassLimits((int)player._class);
                                    break;
                            }
                        }
                        else if (edited_stat == 6)
                        {
                            player.hit_point_max -= 1;

                            if (ovr018.sub_506BA(gbl.SelectedPlayer) > player.hit_point_max)
                            {
                                player.hit_point_max = (byte)ovr018.sub_506BA(player);
                            }

                            player.hit_point_current = player.hit_point_max; ;
                        }
                        else
                        {
                            if (name_cursor_pos == 1)
                            {
                                name_cursor_pos = (byte)gbl.SelectedPlayer.name.Length;
                            }
                            else
                            {
                                name_cursor_pos -= 1;
                            }
                        }
                        break;

                    case 'M':
                        if (edited_stat < 6)
                        {
                            int stat_var = edited_stat;
                            int race = (int)player.race;
                            int sex = player.sex;

                            player.stats2.Inc(stat_var);
                            switch ((Stat)stat_var)
                            {
                                case Stat.STR:
                                    player.stats2.Str.EnforceRaceSexLimits(race, sex);

                                    if( player.stats2.Str.full == 18 &&
                                        (player.fighter_lvl >0 || player.ranger_lvl > 0 || player.paladin_lvl > 0) )
                                    {
                                        player.stats2.Str00.Inc();
                                        player.stats2.Str00.EnforceRaceSexLimits(race, sex);
                                    }
                                    else
                                    {
                                        player.stats2.Str00.Load(0);
                                    }
                                    break;

                                case Stat.INT:
                                    player.stats2.Int.EnforceRaceSexLimits(race, sex);
                                    break;

                                case Stat.WIS:
                                    player.stats2.Wis.EnforceRaceSexLimits(race, sex);

                                    if (player.spellCastCount[0, 0] > 0)
                                    {
                                        player.spellCastCount[0, 0] = 1;
                                    }
                                    break;

                                case Stat.DEX:
                                    player.stats2.Dex.EnforceRaceSexLimits(race, sex);
                                    break;

                                case Stat.CON:
                                    player.stats2.Con.EnforceRaceSexLimits(race, sex);

                                    if (ovr018.sub_506BA(gbl.SelectedPlayer) > player.hit_point_max)
                                    {
                                        player.hit_point_max = (byte)ovr018.sub_506BA(player);
                                    }

                                    player.hit_point_current = player.hit_point_max;
                                    edited_stat = 6;
                                    ovr018.draw_highlight_stat(false, edited_stat, name_cursor_pos);
                                    edited_stat = 4;
                                    break;

                                case Stat.CHA:
                                    player.stats2.Cha.EnforceRaceSexLimits(race, sex);
                                    break;
                            }
                        }
                        else
                        {
                            if (edited_stat == 6)
                            {
                                player.hit_point_max += 1;

                                if (selectedPlayerMaxHp < player.hit_point_max)
                                {
                                    player.hit_point_max = (byte)selectedPlayerMaxHp;
                                }

                                player.hit_point_current = player.hit_point_max;
                            }
                            else
                            {
                                if (name_cursor_pos == player.name.Length + 1)
                                {
                                    name_cursor_pos = 1;
                                }
                                else
                                {
                                    name_cursor_pos++;
                                }
                            }
                        }
                        break;
                }
            }
            else
            {
                if (inputkey == 0x0d)
                {
                    edited_stat++;

                    if (edited_stat > 7)
                    {
                        edited_stat = 0;
                    }
                }
                else if (inputkey == 0x08)
                {
                    if (name_cursor_pos > 1 && edited_stat > 6)
                    {
                        int len = gbl.SelectedPlayer.name.Length;
                        int del = name_cursor_pos - 1;

                        /* delete char from name */
                        string s = string.Empty;
                        if (del > 0)
                        {
                            s = gbl.SelectedPlayer.name.Substring(0, del);
                        }

                        if ((len - del) > 0)
                        {
                            s += gbl.SelectedPlayer.name.Substring(del + 1);
                        }

                        gbl.SelectedPlayer.name = s;

                        if (name_cursor_pos > gbl.SelectedPlayer.name.Length)
                        {
                            name_cursor_pos = (byte)gbl.SelectedPlayer.name.Length;
                        }
                    }
                }
                else if (inputkey >= 0x20 && inputkey <= 0x7A)
                {
                    if (edited_stat > 6)
                    {
                        if (name_cursor_pos <= 15)
                        {
                            string s = string.Empty;
                            int len = player.name.Length;
                            int insert = name_cursor_pos - 1;

                            if (insert > 0)
                            {
                                s = player.name.Substring(0, insert);
                            }
                            s += inputkey;
                            if (len - insert > 0)
                            {
                                s += player.name.Substring(insert + 1);
                            }

                            player.name = s;

                            name_cursor_pos++;
                            if (name_cursor_pos > 15)
                            {
                                name_cursor_pos = 15;
                            }

                            if (name_cursor_pos > player.name.Length)
                            {
                                player.name.PadRight(name_cursor_pos, ' ');
                            }
                            inputkey = '\0';
                        }
                    }
                    else if (inputkey == 0x45)
                    {
                        gbl.SelectedPlayer.stats2.Assign(stats_bkup);

                        gbl.SelectedPlayer.hit_point_max = orig_hp_max;
                        gbl.SelectedPlayer.hit_point_current = gbl.SelectedPlayer.hit_point_max;

                        gbl.SelectedPlayer.name = nameBackup;

                        ovr025.reclac_player_values(gbl.SelectedPlayer);
                        return;
                    }
                }
                else if (inputkey == 0)
                {
                    gbl.SelectedPlayer.stats2.Assign(stats_bkup);

                    gbl.SelectedPlayer.hit_point_max = orig_hp_max;
                    gbl.SelectedPlayer.name = nameBackup;

                    gbl.SelectedPlayer.hit_point_current = gbl.SelectedPlayer.hit_point_max;
                    ovr025.reclac_player_values(gbl.SelectedPlayer);
                    return;
                }
            }

            ovr025.reclac_player_values(gbl.SelectedPlayer);
            ovr020.display_player_stats01();

            ovr018.draw_highlight_stat(true, edited_stat, name_cursor_pos);
        } while (controlkey == true || inputkey != 0x4B);

        ovr026.calc_cleric_spells(true, gbl.SelectedPlayer);

        gbl.SelectedPlayer.npcTreasureShareCount = 1;

        player = gbl.SelectedPlayer;
        orig_hp_max = 0;
        byte hp_count = 0;

        for (int var_33 = 0; var_33 < 8; var_33++)
        {
            if (player.ClassLevel[var_33] > 0)
            {
                if (player.ClassLevel[var_33] < gbl.max_class_hit_dice[var_33])
                {
                    if ((ClassId)var_33 == ClassId.ranger)
                    {
                        orig_hp_max += (byte)((player.ClassLevel[var_33] + 1) * (ovr018.con_bonus((ClassId)var_33)));
                    }
                    else
                    {
                        orig_hp_max += (byte)(player.ClassLevel[var_33] * (ovr018.con_bonus((ClassId)var_33)));
                    }
                }
                else
                {
                    orig_hp_max += (byte)((gbl.max_class_hit_dice[var_33] - 1) * ovr018.con_bonus((ClassId)var_33));
                }
                hp_count++;
            }
        }

        orig_hp_max /= hp_count;

        player.hit_point_rolled = (byte)(player.hit_point_max - orig_hp_max);

        //for (int stat_var = 0; stat_var <= 5; stat_var++)
        //{
        //    gbl.SelectedPlayer.stats2[stat_var].cur = gbl.SelectedPlayer.stats2[stat_var].full;
        //}

        //gbl.SelectedPlayer.stats2.Str00.full = gbl.SelectedPlayer.stats2.Str00.cur;
    }
}
