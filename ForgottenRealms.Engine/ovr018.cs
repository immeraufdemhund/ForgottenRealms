using ForgottenRealms.Engine.CharacterFeature;
using ForgottenRealms.Engine.CharacterFeature.CreatePlayerFeature;
using ForgottenRealms.Engine.CharacterFeature.DropCharacterFeature;
using ForgottenRealms.Engine.CharacterFeature.ModifyCharacterFeature;
using ForgottenRealms.Engine.CharacterFeature.TrainCharacterFeature;
using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine;

internal class ovr018
{
    private static readonly CreatePlayerService CreatePlayerService = new ();
    private static readonly DropCharacterService DropCharacterService = new();
    private static readonly TrainCharacterService TrainCharacterService = new();
    private static readonly ModifyCharacterService ModifyCharacterService = new();
    private static readonly HitPointTable HitPointTable = new ();
    private static readonly AddPlayerAction AddPlayerAction = new ();

    private static Set unk_4C13D = new Set(71, 79);
    private static Set unk_4C15D = new Set(69, 83);

    private static string[] menuStrings = {
        "Create New Character",
        "Drop Character",
        "Modify Character",
        "Train Character",
        "Human Change Classes",
        "View Character",
        "Add Character to Party",
        "Remove Character from Party",
        "Load Saved Game",
        "Save Current Game",
        "BEGIN Adventuring",
        "Exit to DOS"
    };


    private const int allow_create = 0;
    private const int allow_drop = 1;
    private const int allow_modify = 2;
    private const int allow_training = 3;
    private const int allow_duelclass = 4;
    private const int allow_view = 5;
    private const int allow_add = 6;
    private const int allow_remove = 7;
    private const int allow_load = 8;
    private const int allow_save = 9;
    private const int allow_begin = 10;
    private const int allow_exit = 11;


    private static bool[] menuFlags = {
        true,
        false,
        false,
        false,
        false,
        false,
        true,
        false,
        false,
        false,
        false,
        true
    };

    internal static void StartGameMenu()
    {
        var gameStateBackup = gbl.game_state;
        gbl.game_state = GameState.StartGameMenu;
        var reclac_menus = true;

        while (true)
        {
            if (reclac_menus == true)
            {
                ReplaceMenus();
                reclac_menus = false;
            }

            var inputKey = ovr027.displayInput(out var controlKey, false, 1, new MenuColorSet(0, 0, 13), "C D M T H V A R L S B E J", "Choose a function ");

            ovr027.ClearPromptArea();

            if (controlKey == true)
            {
                reclac_menus = ChangeSelectedPlayer(inputKey, reclac_menus);
            }
            else
            {
                if (unk_4C15D.MemberOf(inputKey) == false)
                {
                    gbl.gameSaved = false;
                }

                if (ExecuteMenuOption(inputKey, gameStateBackup))
                {
                    return;
                }

                reclac_menus = true;
            }
        }
    }

    private static void ReplaceMenus()
    {
        seg037.DrawFrame_Outer();
        if (gbl.SelectedPlayer != null)
        {
            ovr025.PartySummary(gbl.SelectedPlayer);
            menuFlags[allow_drop] = true;
            menuFlags[allow_modify] = true;

            if (gbl.area2_ptr.training_class_mask > 0 || Cheats.free_training == true)
            {
                menuFlags[allow_training] = true;
                menuFlags[allow_duelclass] = gbl.SelectedPlayer.CanDuelClass();
            }
            else
            {
                menuFlags[allow_training] = false;
                menuFlags[allow_duelclass] = false;
            }

            menuFlags[allow_view] = true;
            menuFlags[allow_remove] = true;
            menuFlags[allow_load] = false;
            menuFlags[allow_save] = true;
            menuFlags[allow_begin] = true;
        }
        else
        {
            menuFlags[allow_drop] = false;
            menuFlags[allow_modify] = false;
            menuFlags[allow_training] = false;
            menuFlags[allow_duelclass] = false;
            menuFlags[allow_view] = false;
            menuFlags[allow_remove] = false;
            menuFlags[allow_load] = true;
            menuFlags[allow_save] = false;
            menuFlags[allow_begin] = false;
        }

        var yCol = 0;
        for (var i = 0; i <= 11; i++)
        {
            if (menuFlags[i] == true)
            {
                seg041.displayString(menuStrings[i][0].ToString(), 0, 15, yCol + 12, 2);

                var var_111 = seg051.Copy(menuStrings[i].Length, 1, menuStrings[i]);
                seg041.displayString(var_111, 0, 10, yCol + 12, 3);
                yCol++;
            }
        }
    }

    private static bool ChangeSelectedPlayer(char inputKey, bool reclac_menus)
    {
        if (gbl.SelectedPlayer == null || unk_4C13D.MemberOf(inputKey) != true)
        {
            return reclac_menus;
        }

        var previousDuelClassState = gbl.SelectedPlayer.CanDuelClass();

        ovr020.scroll_team_list(inputKey);
        ovr025.PartySummary(gbl.SelectedPlayer);

        previousDuelClassState ^= gbl.SelectedPlayer.CanDuelClass();

        reclac_menus = previousDuelClassState && gbl.area2_ptr.training_class_mask > 0;

        return reclac_menus;
    }

    private static bool ExecuteMenuOption(char inputKey, GameState gameStateBackup)
    {
        switch (inputKey)
        {
            case 'C':
                if (menuFlags[allow_create] == true) CreatePlayerService.createPlayer();
                break;
            case 'D':
                if (menuFlags[allow_drop] == true) DropCharacterService.dropPlayer();
                break;
            case 'M':
                if (menuFlags[allow_modify] == true) ModifyCharacterService.modifyPlayer();
                break;
            case 'T':
                if (menuFlags[allow_training] == true) TrainCharacterService.train_player();
                break;
            case 'H':
                if (menuFlags[allow_duelclass] == true) ovr026.DuelClass(gbl.SelectedPlayer);
                break;
            case 'V':
                if (menuFlags[allow_view] == true) ovr020.viewPlayer();
                break;

            case 'A':
                if (menuFlags[allow_add] == true) AddPlayerAction.AddPlayer();
                break;

            case 'R':
                if(menuFlags[allow_remove] == true) RemoveSelectedPlayer();
                break;

            case 'L':
                if (menuFlags[allow_load] == true) ovr017.loadGameMenu();
                break;

            case 'S':
                if (menuFlags[allow_save] == true) OpenSaveGameMenu();
                break;

            case 'B':
                if (menuFlags[allow_begin] == true)
                {
                    if (BeginAdventuring(gameStateBackup))
                    {
                        return true;
                    }
                }
                break;

            case 'E':
                ExitGame();
                break;
        }

        return false;
    }

    private static void ExitGame()
    {
        char inputkey;
        if (menuFlags[allow_exit] == true)
        {
            inputkey = ovr027.yes_no(gbl.alertMenuColors, "Quit to DOS ");

            if (inputkey == 'Y')
            {
                if (gbl.TeamList.Count > 0 &&
                    gbl.gameSaved == false)
                {

                    inputkey = ovr027.yes_no(gbl.alertMenuColors, "Game not saved.  Quit anyway? ");
                    if (inputkey == 'N')
                    {
                        ovr017.SaveGame();
                    }
                }

                if (inputkey == 'Y')
                {
                    seg043.print_and_exit();
                }
            }
        }
    }

    private static bool BeginAdventuring(GameState gameStateBackup)
    {
        if ((gbl.TeamList.Count > 0 && gbl.inDemo == true) ||
                gbl.area_ptr.field_3FA == 0 || gbl.inDemo == true)
            {
                gbl.game_state = gameStateBackup;

                if (gbl.reload_ecl_and_pictures == false &&
                    gbl.lastDaxBlockId != 0x50)
                {
                    if (gbl.game_state == GameState.WildernessMap)
                    {
                        seg037.DrawFrame_WildernessMap();
                    }
                    else
                    {
                        seg037.draw8x8_03();
                    }
                    ovr025.PartySummary(gbl.SelectedPlayer);
                }
                else
                {
                    if (gbl.area_ptr.LastEclBlockId == 0)
                    {
                        seg037.draw8x8_03();
                    }
                }

                ovr027.ClearPromptArea();
                gbl.area2_ptr.training_class_mask = 0;

                return true;
            }

        return false;
    }

    private static void OpenSaveGameMenu()
    {
        if (gbl.TeamList.Count > 0) ovr017.SaveGame();
    }

    private static void RemoveSelectedPlayer()
    {
        if (gbl.SelectedPlayer != null)
        {
            if (gbl.SelectedPlayer.control_morale < Control.NPC_Base)
            {
                ovr017.SavePlayer(string.Empty, gbl.SelectedPlayer);
                gbl.SelectedPlayer = FreeCurrentPlayer(gbl.SelectedPlayer, true, false);
            }
            else
            {
                DropCharacterService.dropPlayer();
            }
        }
    }

    internal static readonly byte[] classFlagsTable = { 0x02, 0x10, 0x08, 0x40, 0x40, 0x01, 0x04, 0x20 };

    internal static int con_bonus(ClassId classId)
    {
        int bonus;
        var stat = gbl.SelectedPlayer.stats2.Con.full;

        if (stat == 3)
        {
            bonus = -2;
        }
        else if (stat >= 4 && stat <= 6)
        {
            bonus = -1;
        }
        else if (stat >= 7 && stat <= 14)
        {
            bonus = 0;
        }
        else if (stat == 15)
        {
            bonus = 1;
        }
        else if (stat == 16)
        {
            bonus = 1;
        }
        else if (classId == ClassId.fighter || classId == ClassId.ranger || classId == ClassId.paladin)
        {
            bonus = stat - 14;
        }
        else
        {
            bonus = 2;
        }

        return bonus;
    }

    /// <summary>
    /// nested function, has not been fix to be not nested.
    /// </summary>
    internal static void draw_highlight_stat(bool highlighted, byte edited_stat, int name_cursor_pos) /* sub_4E6F2 */
    {
        if (edited_stat >= 0 && edited_stat <= 5)
        {
            ovr020.display_stat(highlighted, edited_stat);
        }
        else if (edited_stat == 6)
        {
            ovr025.display_hp(highlighted, 18, 4, gbl.SelectedPlayer);
        }
        else if (edited_stat == 7)
        {
            if (highlighted == true)
            {
                seg041.displaySpaceChar(1, gbl.SelectedPlayer.name.Length + 1);
                seg041.displayString(gbl.SelectedPlayer.name, 0, 13, 1, 1);

                if (name_cursor_pos > gbl.SelectedPlayer.name.Length || gbl.SelectedPlayer.name[name_cursor_pos - 1] == ' ')
                {
                    seg041.displayString("%", 0, 15, 1, name_cursor_pos);
                }
                else
                {
                    seg041.displayString(gbl.SelectedPlayer.name[name_cursor_pos - 1].ToString(), 0, 15, 1, name_cursor_pos);
                }
            }
            else
            {
                seg041.displayString(gbl.SelectedPlayer.name, 0, 10, 1, 1);
            }
        }
    }

    internal static Player FreeCurrentPlayer(Player player, bool free_icon, bool leave_party_size) // free_players
    {
        var index = gbl.TeamList.IndexOf(player);

        if (index >= 0)
        {
            gbl.TeamList.RemoveAt(index);

            if (free_icon)
            {
                ovr034.ReleaseCombatIcon(player.icon_id);
            }

            if (leave_party_size == false)
            {
                gbl.area2_ptr.party_size--;
            }

            if (player.actions != null)
            {
                player.actions = null;
            }

            player.items.Clear();
            player.affects.Clear();

            index = index > 0 ? index - 1 : 0;
            if (gbl.TeamList.Count > 0)
            {
                return gbl.TeamList[index];
            }
        }

        return null;
    }

    /// <summary> seg600:4281 </summary>
    private static sbyte[] con_hp_adj = { 0, 0, 0, -2, -1, -1, -1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 };

    internal static sbyte get_con_hp_adj(Player player)
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


    internal static int sub_506BA(Player player)
    {
        var class_count = 0;
        var levels_total = 0;

        for (var class_index = 0; class_index <= (int)ClassId.monk; class_index++)
        {
            if (player.ClassLevel[class_index] > 0)
            {
                levels_total += player.ClassLevel[class_index] + HitPointTable.GetLevelBonusForClass((ClassId)class_index);
                class_count++;
            }
        }

        int con_adj = get_con_hp_adj(player);

        if (con_adj < 0)
        {
            if (levels_total > (System.Math.Abs(con_adj) + class_count))
            {
                levels_total = (levels_total + con_adj) / class_count;
            }
            else
            {
                levels_total = 1;
            }
        }
        else
        {
            levels_total = (levels_total + con_adj) / class_count;
        }

        return levels_total;
    }

    private static byte[] /* seg600:081A */ unk_16B2A = { 1, 1, 1, 1, 2, 1, 1, 2 };
    private static byte[] /* seg600:0822 */ unk_16B32 = { 8, 8, 0xA, 0xA, 8, 4, 6, 4 };

    internal static byte sub_509E0(byte arg_0, Player player)
    {
        byte var_4 = 0;

        for (var _class = 0; _class <= 7; _class++)
        {
            if (player.ClassLevel[_class] > 0 &&
                TrainCharacterService.IsAllowedToTrainClass(arg_0, (ClassId)_class) == true)
            {
                if (player.ClassLevel[_class] < gbl.max_class_hit_dice[_class])
                {
                    int var_5 = unk_16B2A[_class];

                    if (player.ClassLevel[_class] > 1)
                    {
                        var_5 = 1;
                    }

                    var var_2 = ovr024.roll_dice(unk_16B32[_class], var_5);
                    var var_3 = ovr024.roll_dice(unk_16B32[_class], var_5);

                    if (var_3 > var_2)
                    {
                        var_2 = var_3;
                    }

                    var_4 += var_2;
                }
                else
                {
                    if (_class == 2 || _class == 3)
                    {
                        var_4 = 3;
                    }
                    else if (_class == 4 || _class == 0 || _class == 6)
                    {
                        var_4 = 2;
                    }
                    else if (_class == 5)
                    {
                        var_4 = 1;
                    }
                }
            }
        }

        return var_4;
    }
}
