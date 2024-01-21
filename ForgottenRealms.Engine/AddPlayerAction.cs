using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine;

public class AddPlayerAction
{
    public void AddPlayer()
    {
        seg037.draw8x8_clear_area(0x16, 0x26, 1, 1);

        var input_key = ovr027.displayInput(false, 0, gbl.defaultMenuColors, "Curse Pool Hillsfar Exit", "Add from where? ");

        switch (input_key)
        {
            case 'C':
                gbl.import_from = ImportSource.Curse;
                break;

            case 'P':
                gbl.import_from = ImportSource.Pool;
                break;

            case 'H':
                gbl.import_from = ImportSource.Hillsfar;
                break;

            case 'E':
            case '\0':
                return;
        }

        ovr017.BuildLoadablePlayersLists(out var strList, out var nameList);

        if (nameList.Count <= 0)
        {
            return;
        }

        var pc_count = 0;

        var strList_index = 0;
        var menuRedraw = true;

        do
        {
            var showExit = true;
            input_key = ovr027.sl_select_item(out var select_sl, ref strList_index, ref menuRedraw, showExit, nameList,
                22, 38, 2, 1, gbl.defaultMenuColors, "Add", "Add a character: ");

            if ((input_key == 13 || input_key == 'A') &&
                select_sl.Text[0] != '*')
            {
                ovr027.ClearPromptArea();

                var new_player = new Player();

                var var_10 = ovr027.getStringListEntry(strList, strList_index);

                ovr017.import_char01(ref new_player, var_10.Text);

                select_sl.Text = "* " + select_sl.Text;
                pc_count = 0;

                if (gbl.TeamList.Count == 0)
                {
                    gbl.area2_ptr.party_size = 0;
                    ovr017.AssignPlayerIconId(new_player);

                    ovr017.LoadPlayerCombatIcon(true);
                }
                else
                {
                    var paladin_present = false;
                    var paladins_name = "";
                    var evil_present = false;
                    var ranger_count = 0;
                    var found = false;

                    foreach (var tmp_player in gbl.TeamList)
                    {
                        if (tmp_player.name == new_player.name &&
                            tmp_player.mod_id == new_player.mod_id)
                        {
                            found = true;
                            break;
                        }

                        if (tmp_player.control_morale < Control.NPC_Base)
                        {
                            pc_count++;
                        }

                        if (tmp_player.ranger_lvl > 0)
                        {
                            ranger_count++;
                        }

                        if ((tmp_player.alignment + 1) % 3 == 0)
                        {
                            evil_present = true;
                        }

                        if (tmp_player.paladin_lvl > 0)
                        {
                            paladin_present = true;
                            paladins_name = tmp_player.name;
                        }
                    }

                    if (found == false &&
                        ((new_player.control_morale < Control.NPC_Base && pc_count < 6) ||
                         (new_player.control_morale >= Control.NPC_Base && gbl.area2_ptr.party_size < 8)) &&
                        (new_player.paladin_lvl == 0 || evil_present == false) &&
                        (new_player.ranger_lvl == 0 || ranger_count < 3) &&
                        (((new_player.alignment + 1) % 3) != 0 || paladin_present == false))
                    {
                        ovr017.AssignPlayerIconId(new_player);
                        ovr017.LoadPlayerCombatIcon(true);

                        if (new_player.control_morale < Control.NPC_Base)
                        {
                            pc_count++;
                        }
                    }
                    else
                    {
                        select_sl.Text = select_sl.Text.Substring(2);

                        if (new_player.paladin_lvl > 0 && evil_present == true)
                        {
                            ovr025.string_print01("paladins do not join with evil scum");
                            DisplayDriver.GameDelay();
                        }
                        else if (new_player.ranger_lvl > 0 && ranger_count > 2)
                        {
                            ovr025.string_print01("too many rangers in party");
                        }
                        else if (((new_player.alignment + 1) % 3) == 0 &&
                                 paladin_present == true)
                        {
                            ovr025.string_print01(paladins_name + " will tolerate no evil!");
                        }

                        new_player = null; // FreeMem( Player.StructSize, player_ptr1 );
                    }
                }
            }

        } while (input_key != 0x45 && input_key != '\0' && pc_count <= 5 && gbl.area2_ptr.party_size <= 7);

        nameList.Clear();
    }
}
