using ForgottenRealms.Engine.CharacterFeature;
using ForgottenRealms.Engine.CharacterFeature.CreatePlayerFeature;
using ForgottenRealms.Engine.CharacterFeature.DropCharacterFeature;
using ForgottenRealms.Engine.CharacterFeature.ModifyCharacterFeature;
using ForgottenRealms.Engine.CharacterFeature.TrainCharacterFeature;
using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine;

public class ovr018
{
    private Set unk_4C13D = new Set(71, 79);
    private Set unk_4C15D = new Set(69, 83);

    private string[] menuStrings = {
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


    private bool[] menuFlags = {
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

    private readonly ovr017 _ovr017;
    private readonly ovr020 _ovr020;
    private readonly ovr024 _ovr024;
    private readonly ovr025 _ovr025;
    private readonly ovr026 _ovr026;
    private readonly ovr027 _ovr027;
    private readonly ovr034 _ovr034;
    private readonly seg037 _seg037;
    private readonly seg051 _seg051;
    private readonly DisplayDriver _displayDriver;
    private readonly CreatePlayerService _createPlayerService;
    private readonly DropCharacterService _dropCharacterService;
    private readonly TrainCharacterService _trainCharacterService;
    private readonly ModifyCharacterService _modifyCharacterService;
    private readonly HitPointTable _hitPointTable;
    private readonly AddPlayerAction _addPlayerAction;
    private readonly ConstitutionHitPointsAdjustmentTable _constitutionHitPointsAdjustmentTable;
    private readonly MainGameEngine _mainGameEngine;

    public ovr018(ovr017 ovr017, ovr020 ovr020, ovr024 ovr024, ovr025 ovr025, ovr026 ovr026, ovr027 ovr027, ovr034 ovr034, seg037 seg037, seg051 seg051, DisplayDriver displayDriver, CreatePlayerService createPlayerService, DropCharacterService dropCharacterService, TrainCharacterService trainCharacterService, ModifyCharacterService modifyCharacterService, HitPointTable hitPointTable, AddPlayerAction addPlayerAction, ConstitutionHitPointsAdjustmentTable constitutionHitPointsAdjustmentTable, MainGameEngine mainGameEngine)
    {
        _ovr017 = ovr017;
        _ovr020 = ovr020;
        _ovr024 = ovr024;
        _ovr025 = ovr025;
        _ovr026 = ovr026;
        _ovr027 = ovr027;
        _ovr034 = ovr034;
        _seg037 = seg037;
        _seg051 = seg051;
        _displayDriver = displayDriver;
        _createPlayerService = createPlayerService;
        _dropCharacterService = dropCharacterService;
        _trainCharacterService = trainCharacterService;
        _modifyCharacterService = modifyCharacterService;
        _hitPointTable = hitPointTable;
        _addPlayerAction = addPlayerAction;
        _constitutionHitPointsAdjustmentTable = constitutionHitPointsAdjustmentTable;
        _mainGameEngine = mainGameEngine;
    }

    internal void StartGameMenu()
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

            var inputKey = _ovr027.displayInput(out var controlKey, false, 1, new MenuColorSet(0, 0, 13), "C D M T H V A R L S B E J", "Choose a function ");

            _ovr027.ClearPromptArea();

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

    private void ReplaceMenus()
    {
        _seg037.DrawFrame_Outer();
        if (gbl.SelectedPlayer != null)
        {
            _ovr025.PartySummary(gbl.SelectedPlayer);
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
                _displayDriver.displayString(menuStrings[i][0].ToString(), 0, 15, yCol + 12, 2);

                var var_111 = _seg051.Copy(menuStrings[i].Length, 1, menuStrings[i]);
                _displayDriver.displayString(var_111, 0, 10, yCol + 12, 3);
                yCol++;
            }
        }
    }

    private bool ChangeSelectedPlayer(char inputKey, bool reclac_menus)
    {
        if (gbl.SelectedPlayer == null || unk_4C13D.MemberOf(inputKey) != true)
        {
            return reclac_menus;
        }

        var previousDuelClassState = gbl.SelectedPlayer.CanDuelClass();

        _ovr020.scroll_team_list(inputKey);
        _ovr025.PartySummary(gbl.SelectedPlayer);

        previousDuelClassState ^= gbl.SelectedPlayer.CanDuelClass();

        reclac_menus = previousDuelClassState && gbl.area2_ptr.training_class_mask > 0;

        return reclac_menus;
    }

    private bool ExecuteMenuOption(char inputKey, GameState gameStateBackup)
    {
        switch (inputKey)
        {
            case 'C':
                if (menuFlags[allow_create] == true) _createPlayerService.createPlayer();
                break;
            case 'D':
                if (menuFlags[allow_drop] == true) _dropCharacterService.dropPlayer();
                break;
            case 'M':
                if (menuFlags[allow_modify] == true) _modifyCharacterService.modifyPlayer();
                break;
            case 'T':
                if (menuFlags[allow_training] == true) _trainCharacterService.train_player();
                break;
            case 'H':
                if (menuFlags[allow_duelclass] == true) _ovr026.DuelClass(gbl.SelectedPlayer);
                break;
            case 'V':
                if (menuFlags[allow_view] == true) _ovr020.viewPlayer();
                break;

            case 'A':
                if (menuFlags[allow_add] == true) _addPlayerAction.AddPlayer();
                break;

            case 'R':
                if(menuFlags[allow_remove] == true) RemoveSelectedPlayer();
                break;

            case 'L':
                if (menuFlags[allow_load] == true) _ovr017.loadGameMenu();
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

    private void ExitGame()
    {
        char inputkey;
        if (menuFlags[allow_exit] == true)
        {
            inputkey = _ovr027.yes_no(gbl.alertMenuColors, "Quit to DOS ");

            if (inputkey == 'Y')
            {
                if (gbl.TeamList.Count > 0 &&
                    gbl.gameSaved == false)
                {

                    inputkey = _ovr027.yes_no(gbl.alertMenuColors, "Game not saved.  Quit anyway? ");
                    if (inputkey == 'N')
                    {
                        _ovr017.SaveGame();
                    }
                }

                if (inputkey == 'Y')
                {
                    _mainGameEngine.EngineStop();
                }
            }
        }
    }

    private bool BeginAdventuring(GameState gameStateBackup)
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
                        _seg037.DrawFrame_WildernessMap();
                    }
                    else
                    {
                        _seg037.draw8x8_03();
                    }
                    _ovr025.PartySummary(gbl.SelectedPlayer);
                }
                else
                {
                    if (gbl.area_ptr.LastEclBlockId == 0)
                    {
                        _seg037.draw8x8_03();
                    }
                }

                _ovr027.ClearPromptArea();
                gbl.area2_ptr.training_class_mask = 0;

                return true;
            }

        return false;
    }

    private void OpenSaveGameMenu()
    {
        if (gbl.TeamList.Count > 0) _ovr017.SaveGame();
    }

    private void RemoveSelectedPlayer()
    {
        if (gbl.SelectedPlayer != null)
        {
            if (gbl.SelectedPlayer.control_morale < Control.NPC_Base)
            {
                _ovr017.SavePlayer(string.Empty, gbl.SelectedPlayer);
                gbl.SelectedPlayer = FreeCurrentPlayer(gbl.SelectedPlayer, true, false);
            }
            else
            {
                _dropCharacterService.dropPlayer();
            }
        }
    }

    internal readonly byte[] classFlagsTable = { 0x02, 0x10, 0x08, 0x40, 0x40, 0x01, 0x04, 0x20 };

    internal int con_bonus(ClassId classId)
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
    internal void draw_highlight_stat(bool highlighted, byte edited_stat, int name_cursor_pos) /* sub_4E6F2 */
    {
        if (edited_stat >= 0 && edited_stat <= 5)
        {
            _ovr020.display_stat(highlighted, edited_stat);
        }
        else if (edited_stat == 6)
        {
            _ovr025.display_hp(highlighted, 18, 4, gbl.SelectedPlayer);
        }
        else if (edited_stat == 7)
        {
            if (highlighted == true)
            {
                _displayDriver.displaySpaceChar(1, gbl.SelectedPlayer.name.Length + 1);
                _displayDriver.displayString(gbl.SelectedPlayer.name, 0, 13, 1, 1);

                if (name_cursor_pos > gbl.SelectedPlayer.name.Length || gbl.SelectedPlayer.name[name_cursor_pos - 1] == ' ')
                {
                    _displayDriver.displayString("%", 0, 15, 1, name_cursor_pos);
                }
                else
                {
                    _displayDriver.displayString(gbl.SelectedPlayer.name[name_cursor_pos - 1].ToString(), 0, 15, 1, name_cursor_pos);
                }
            }
            else
            {
                _displayDriver.displayString(gbl.SelectedPlayer.name, 0, 10, 1, 1);
            }
        }
    }

    internal Player FreeCurrentPlayer(Player player, bool free_icon, bool leave_party_size) // free_players
    {
        var index = gbl.TeamList.IndexOf(player);

        if (index >= 0)
        {
            gbl.TeamList.RemoveAt(index);

            if (free_icon)
            {
                _ovr034.ReleaseCombatIcon(player.icon_id);
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

    internal int sub_506BA(Player player)
    {
        var class_count = 0;
        var levels_total = 0;

        for (var class_index = 0; class_index <= (int)ClassId.monk; class_index++)
        {
            if (player.ClassLevel[class_index] > 0)
            {
                levels_total += player.ClassLevel[class_index] + _hitPointTable.GetLevelBonusForClass((ClassId)class_index);
                class_count++;
            }
        }

        int con_adj = _constitutionHitPointsAdjustmentTable.get_con_hp_adj(player);

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

    private byte[] /* seg600:081A */ unk_16B2A = { 1, 1, 1, 1, 2, 1, 1, 2 };
    private byte[] /* seg600:0822 */ unk_16B32 = { 8, 8, 0xA, 0xA, 8, 4, 6, 4 };

    internal byte sub_509E0(byte arg_0, Player player)
    {
        byte var_4 = 0;

        for (var _class = 0; _class <= 7; _class++)
        {
            if (player.ClassLevel[_class] > 0 &&
                _trainCharacterService.IsAllowedToTrainClass(arg_0, (ClassId)_class) == true)
            {
                if (player.ClassLevel[_class] < gbl.max_class_hit_dice[_class])
                {
                    int var_5 = unk_16B2A[_class];

                    if (player.ClassLevel[_class] > 1)
                    {
                        var_5 = 1;
                    }

                    var var_2 = _ovr024.roll_dice(unk_16B32[_class], var_5);
                    var var_3 = _ovr024.roll_dice(unk_16B32[_class], var_5);

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
