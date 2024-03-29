using System.Collections.Generic;
using ForgottenRealms.Engine.AffectsFeature;
using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine;

internal enum SpellLoc
{
    memory = 0,
    grimoire = 1,
    scroll = 2,
    scrolls = 3,
    choose = 4,
    memorize = 5,
    scribe = 6
}

public class ovr020
{
    private Set asc_54B50 = new Set(73, 83, 84 );
    private Set unk_54B03 = new Set(0, 69 );

    internal string[] sexString = { "Male", "Female" };
    internal string[] raceString = { "Monster", "Dwarf", "Elf", "Gnome",
        "Half-Elf", "Halfling", "Half-Orc", "Human" };

    internal string[] alignmentString = { "Lawful Good", "Lawful Neutral", "Lawful Evil",
        "Neutral Good", "True Neutral", "Neutral Evil",
        "Chaotic Good", "Chaotic Neutral", "Chaotic Evil" };

    internal string[] classString = { "Cleric", "Druid", "Fighter", "Paladin", "Ranger",
        "Magic-User", "Thief", "Monk", "Cleric/Fighter",
        "Cleric/Fighter/Magic-User", "Cleric/Ranger",
        "Cleric/Magic-User","Cleric/Thief", "Fighter/Magic-User",
        "Fighter/Thief", "Fighter/Magic-User/Thief",
        "Magic-User/Thief" };

    private string[] statShortString = { "STR ", "INT ", "WIS ", "DEX ", "CON ", "CHA " };

    internal string[] statusString = { "Okay", "Animated", "tempgone", "Running",
        "Unconscious", "Dying", "Dead", "Stoned",
        "Gone" };

    private string[] moneyString = { "Copper", "Silver", "Electrum", "Gold", "Platinum",
        "Gems", "Jewelry" };

    private readonly ApplyAffectTable _ovr013;
    private readonly ovr022 _ovr022;
    private readonly ovr023 _ovr023;
    private readonly ovr024 _ovr024;
    private readonly ovr025 _ovr025;
    private readonly ovr026 _ovr026;
    private readonly ovr027 _ovr027;
    private readonly ovr033 _ovr033;
    private readonly seg037 _seg037;
    private readonly seg051 _seg051;
    private readonly DisplayDriver _displayDriver;

    public ovr020(ApplyAffectTable ovr013, ovr022 ovr022, ovr023 ovr023, ovr024 ovr024, ovr025 ovr025, ovr026 ovr026, ovr027 ovr027, ovr033 ovr033, seg037 seg037, seg051 seg051, DisplayDriver displayDriver)
    {
        _ovr013 = ovr013;
        _ovr022 = ovr022;
        _ovr023 = ovr023;
        _ovr024 = ovr024;
        _ovr025 = ovr025;
        _ovr026 = ovr026;
        _ovr027 = ovr027;
        _ovr033 = ovr033;
        _seg037 = seg037;
        _seg051 = seg051;
        _displayDriver = displayDriver;
    }

    internal void playerDisplayFull(Player player)
    {
        _seg037.DrawFrame_Outer();

        _ovr025.displayPlayerName(false, 1, 1, player);

        if (player.control_morale >= Control.NPC_Base)
        {
            _displayDriver.displayString("(NPC)", 0, 10, 1, player.name.Length + 3);
        }

        int xCol = 1;

        string text2 = sexString[player.sex];

        _displayDriver.displayString(sexString[player.sex], 0, 15, 3, xCol);

        xCol += (byte)(text2.Length + 1);
        text2 = raceString[(int)player.race];
        _displayDriver.displayString(text2, 0, 15, 3, xCol);

        xCol += (byte)(text2.Length + 1);
        string text = "Age " + player.age.ToString();

        _displayDriver.displayString(text, 0, 15, 3, xCol);

        text2 = alignmentString[player.alignment];
        _displayDriver.displayString(text2, 0, 15, 4, 1);

        text2 = classString[(int)player._class];
        _displayDriver.displayString(text2, 0, 15, 5, 1);

        for (int stat = 0; stat < 6; stat++)
        {
            text2 = statShortString[stat];
            _displayDriver.displayString(text2, 0, 10, stat + 7, 1);
            display_stat(false, stat);
        }

        displayMoney();
        _displayDriver.displayString("Level", 0, 15, 15, 1);

        bool displaySlash = false;
        text2 = string.Empty;

        for (int classIdx = 0; classIdx <= 7; classIdx++)
        {
            byte tmp = player.ClassLevelsOld[classIdx];

            if (player.ClassLevel[classIdx] > 0 ||
                (tmp < _ovr026.HumanCurrentClassLevel_Zero(player) && tmp > 0))
            {
                if (displaySlash)
                {
                    text2 += "/";
                }

                text2 += (player.ClassLevel[classIdx] + player.ClassLevelsOld[classIdx]).ToString();

                displaySlash = true;
            }
        }

        _displayDriver.displayString(text2, 0, 15, 15, 7);

        text = "Exp " + player.exp.ToString();
        _displayDriver.displayString(text, 0, 15, 15, 17);

        display_player_stats01();
        int yCol = 20;

        if (player.activeItems.primaryWeapon != null)
        {
            _displayDriver.displayString("Weapon", 0, 15, yCol, 1);
            _ovr025.ItemDisplayNameBuild(true, false, yCol, 8, player.activeItems.primaryWeapon);
        }

        yCol++;
        if (player.activeItems.armor != null)
        {
            _displayDriver.displayString("Armor", 0, 15, yCol, 2);
            _ovr025.ItemDisplayNameBuild(true, false, yCol, 8, player.activeItems.armor);
        }

        yCol++;

        _displayDriver.displayString("Status", 0, 15, yCol, 1);
        _displayDriver.displayString(statusString[(int)player.health_status], 0, 10, yCol, 8);
    }

    internal void displayMoney()
    {
        _seg037.draw8x8_clear_area(14, 26, 7, 12);

        int yCol = 7;

        for (int coinType = 6; coinType >= 0; coinType--)
        {
            if (gbl.SelectedPlayer.Money.GetCoins(coinType) > 0)
            {
                string text = string.Format("{0,8} {1}", Money.names[coinType], gbl.SelectedPlayer.Money.GetCoins(coinType));
                _displayDriver.displayString(text, 0, 10, yCol, 12);

                yCol++;
            }
        }
    }


    internal void display_player_stats01()
    {
        Player player = gbl.SelectedPlayer;

        _ovr025.reclac_player_values(player);
        int yCol = 0x11;

        _displayDriver.displayString("AC    ", 0, 15, yCol, 1);
        _displayDriver.displayString(player.DisplayAc.ToString(), 0, 10, yCol, 4);

        _displayDriver.displayString("HP    ", 0, 15, yCol + 1, 1);
        _ovr025.display_hp(false, yCol + 1, 4, player);

        int xCol = 8;

        _displayDriver.displayString("THAC0   ", 0, 15, yCol, xCol + 1);
        _displayDriver.displayString((0x3c - player.hitBonus).ToString(), 0, 10, yCol, xCol + 7);


        string damage = string.Format("{0}d{1}{2}{3}", player.attack1_DiceCount, player.attack1_DiceSize,
            player.attack1_DamageBonus > 0 ? "+" : "", player.attack1_DamageBonus != 0 ? player.attack1_DamageBonus.ToString() : "");

        _displayDriver.displayString("Damage  ", 0, 15, yCol + 1, xCol);
        _displayDriver.displayString(damage, 0, 10, yCol + 1, xCol + 7);

        xCol = 0x16;
        _displayDriver.displayString("Encumbrance  ", 0, 15, yCol, xCol);
        _displayDriver.displayString(player.weight.ToString(), 0, 10, yCol, xCol + 12);

        int movement = player.movement;

        if (player.HasAffect(Affects.slow) == true)
        {
            movement *= 2;
        }

        if (player.HasAffect(Affects.haste) == true)
        {
            movement /= 2;
        }

        _displayDriver.displayString("Movement ", 0, 15, yCol + 1, xCol + 3);
        _displayDriver.displayString(movement.ToString(), 0, 10, yCol + 1, xCol + 12);
    }


    internal void display_stat(bool highlighted, int stat_index)
    {
        int color = highlighted ? 0x0D : 0x0A;
        int col_x = 5;
        _seg037.draw8x8_clear_area(stat_index + 7, 0x0b, stat_index + 7, col_x);

        if (gbl.SelectedPlayer.stats2[stat_index].full < 10)
        {
            col_x++;
        }

        string s = gbl.SelectedPlayer.stats2[stat_index].full.ToString();
        _displayDriver.displayString(s, 0, color, stat_index + 7, col_x);

        if (stat_index == 0 &&
            gbl.SelectedPlayer.stats2.Str.full == 18 &&
            gbl.SelectedPlayer.stats2.Str00.cur > 0)
        {
            string text = gbl.SelectedPlayer.stats2.Str00.cur.ToString();

            if (gbl.SelectedPlayer.stats2.Str00.cur < 10)
            {
                text = "0" + text;
            }

            if (gbl.SelectedPlayer.stats2.Str00.cur == 100)
            {
                text = "00";
            }

            _displayDriver.displayString("(" + text + ")", 0, color, 7, 7);
        }
    }

    internal bool viewPlayer()
    {
        if (gbl.game_state == GameState.Combat)
        {
            _ovr033.Color_0_8_normal();
        }

        char input_key = ' ';
        bool arg_0 = false;

        gbl.tradeWith = gbl.SelectedPlayer;

        playerDisplayFull(gbl.SelectedPlayer);

        while (unk_54B03.MemberOf(input_key) == false && arg_0 == false)
        {
            string text = string.Empty;
            bool hasSpells = gbl.SelectedPlayer.spellList.HasSpells();
            bool hasMoney = gbl.SelectedPlayer.Money.AnyMoney();

            if (gbl.SelectedPlayer.items.Count > 0)
            {
                text += "Items ";
            }

            if (hasSpells == true)
            {
                text += "Spells ";
            }

            if (gbl.SelectedPlayer.control_morale < Control.NPC_Base ||
                gbl.SelectedPlayer.in_combat == false ||
                gbl.SelectedPlayer.health_status == Status.animated)
            {
                if (hasMoney && gbl.game_state != GameState.Combat)
                {
                    text += "Trade ";
                }
            }

            if (hasMoney)
            {
                text += "Drop ";
            }

            if (CanCastHeal(gbl.SelectedPlayer) == true)
            {
                text += "Heal ";
            }

            if (CanCastCureDiseases(gbl.SelectedPlayer) == true)
            {
                text += "Cure ";
            }

            text += "Exit";

            input_key = _ovr027.displayInput(false, 0, gbl.defaultMenuColors, text, string.Empty);

            int index = -1;

            switch (input_key)
            {
                case 'I':
                    PlayerItemsMenu(ref arg_0);
                    break;

                case 'S':
                    spell_menu2(out hasSpells, ref index, 0, SpellLoc.memory);
                    break;

                case 'T':
                    tradeCoin();
                    break;

                case 'D':
                    drop_coin();
                    displayMoney();
                    break;

                case 'H':
                    PaladinHeal(gbl.SelectedPlayer);
                    break;

                case 'C':
                    PaladinCureDisease(gbl.SelectedPlayer);
                    break;
            }

            if (arg_0 == false &&
                asc_54B50.MemberOf(input_key) == true)
            {
                playerDisplayFull(gbl.SelectedPlayer);
            }
        }

        if (gbl.game_state == GameState.Combat)
        {
            _ovr033.Color_0_8_inverse();
        }
        _ovr025.LoadPic();

        return arg_0;
    }


    internal bool CanSellDropTradeItem(Item item) // sub_54EC1
    {
        bool canSellDropTradeItem = false;

        if (item.readied)
        {
            _ovr025.string_print01("Must be unreadied");
        }
        else if (item.IsScroll() == false)
        {
            canSellDropTradeItem = true;
        }
        else if ((int)item.affect_1 > 0x7F || (int)item.affect_2 > 0x7F || (int)item.affect_3 > 0x7F)
        {
            _ovr025.displayPlayerName(false, 15, 1, gbl.SelectedPlayer);

            gbl.textXCol = gbl.SelectedPlayer.name.Length + 2;
            gbl.textYCol = 0x15;

            _displayDriver.press_any_key(" was going to scribe from that scroll", false, 14, TextRegion.Normal2);
            if (_ovr027.yes_no(gbl.defaultMenuColors, "is it Okay to lose it? ") == 'Y')
            {
                canSellDropTradeItem = true;
            }
        }
        else
        {
            canSellDropTradeItem = true;
        }

        _seg037.draw8x8_clear_area(TextRegion.Normal2);

        return canSellDropTradeItem;
    }


    internal void ItemDisplayStats(Item arg_0) /*sub_550A6*/
    {
        _seg037.DrawFrame_Outer();

        _displayDriver.displayString("itemptr:      ", 0, 10, 1, 1);
        _displayDriver.displayString(arg_0.type.ToString(), 0, 10, 1, 0x14);

        _displayDriver.displayString("namenum(1):   ", 0, 10, 2, 1);
        _displayDriver.displayString(arg_0.namenum1.ToString(), 0, 10, 2, 0x14);

        _displayDriver.displayString("namenum(2):   ", 0, 10, 3, 1);
        _displayDriver.displayString(arg_0.namenum2.ToString(), 0, 10, 3, 0x14);

        _displayDriver.displayString("namenum(3):   ", 0, 10, 4, 1);
        _displayDriver.displayString(arg_0.namenum3.ToString(), 0, 10, 4, 0x14);

        _displayDriver.displayString("plus:         ", 0, 10, 5, 1);
        _displayDriver.displayString(arg_0.plus.ToString(), 0, 10, 5, 0x14);

        _displayDriver.displayString("plussave:     ", 0, 10, 6, 1);
        _displayDriver.displayString(arg_0.plus_save.ToString(), 0, 10, 6, 0x14);

        _displayDriver.displayString("ready:        ", 0, 10, 7, 1);
        _displayDriver.displayString(arg_0.readied.ToString(), 0, 10, 7, 0x14);

        _displayDriver.displayString("identified:   ", 0, 10, 8, 1);
        _displayDriver.displayString(arg_0.hidden_names_flag.ToString(), 0, 10, 8, 0x14);

        _displayDriver.displayString("cursed:       ", 0, 10, 9, 1);
        _displayDriver.displayString(arg_0.cursed.ToString(), 0, 10, 9, 0x14);

        _displayDriver.displayString("value:        ", 0, 10, 10, 1);
        _displayDriver.displayString(arg_0._value.ToString(), 0, 10, 10, 0x14);

        _displayDriver.displayString("special(1):   ", 0, 10, 11, 1);
        _displayDriver.displayString(arg_0.affect_1.ToString(), 0, 10, 11, 0x14);

        _displayDriver.displayString("special(2):   ", 0, 10, 12, 1);
        _displayDriver.displayString(arg_0.affect_2.ToString(), 0, 10, 12, 0x14);

        _displayDriver.displayString("special(3):   ", 0, 10, 13, 1);
        _displayDriver.displayString(arg_0.affect_3.ToString(), 0, 10, 13, 0x14);

        _displayDriver.displayString("dice large:   ", 0, 10, 14, 1);
        _displayDriver.displayString(gbl.ItemDataTable[arg_0.type].diceCountLarge.ToString(), 0, 10, 14, 0x14);

        _displayDriver.displayString("sides large:  ", 0, 10, 15, 1);
        _displayDriver.displayString(gbl.ItemDataTable[arg_0.type].diceSizeLarge.ToString(), 0, 10, 15, 0x14);

        _displayDriver.DisplayAndPause("press a key", 10);
    }

    private Set unk_554EE = new Set(0, 69);

    internal void PlayerItemsMenu(ref bool arg_0) /*use_item*/
    {
        Player player = gbl.SelectedPlayer;
        char inputKey = ' ';

        bool redraw_items = true;
        bool redraw_player = true;
        int dummy_index = 0;

        while (unk_554EE.MemberOf(inputKey) == false &&
               arg_0 == false &&
               player.items.Count > 0)
        {
            int oldItemCount = player.items.Count;

            if (player.items.Count > 0)
            {
                string text = "Ready";

                if (Cheats.view_item_stats)
                {
                    text += " View";
                }

                if (player.in_combat == true &&
                    gbl.area_ptr.field_1CA == 0 &&
                    (gbl.game_state == GameState.Camping || gbl.game_state == GameState.WildernessMap ||
                     gbl.game_state == GameState.DungeonMap || gbl.game_state == GameState.Combat ||
                     (player.actions != null && player.actions.can_use == true)))
                {
                    text += " Use";
                }

                if (player.control_morale < Control.NPC_Base ||
                    player.in_combat == false ||
                    player.health_status == Status.animated)
                {
                    if (gbl.game_state != GameState.Combat)
                    {
                        text += " Trade";
                    }
                }

                text += " Drop";

                if (player.items.Count < Player.MaxItems)
                {
                    text += " Halve";
                }

                text += " Join";

                if (gbl.game_state == GameState.Shop)
                {
                    if (player.control_morale < Control.NPC_Base ||
                        player.in_combat == false ||
                        player.health_status == Status.animated)
                    {
                        text += " Sell";
                    }

                    text += " Id";
                }

                player.items.ForEach(item => _ovr025.ItemDisplayNameBuild(false, true, 0, 0, item));


                if (redraw_player == true || gbl.byte_1D2C8 == true)
                {
                    _seg037.draw8x8_07();

                    _ovr025.displayPlayerName(true, 1, 1, player);

                    _displayDriver.displayString("Items", 0, 10, 1, player.name.Length + 4);
                    _displayDriver.displayString("Ready Item", 0, 15, 3, 1);

                    redraw_items = true;
                    redraw_player = false;
                    gbl.byte_1D2C8 = false;
                }

                var menulist = player.items.ConvertAll<MenuItem>(item => new MenuItem(item.name, item));
                MenuItem menuitem;

                inputKey = _ovr027.sl_select_item(out menuitem, ref dummy_index, ref redraw_items, true,
                    menulist, 0x16, 0x26, 5, 1, gbl.defaultMenuColors, text, string.Empty);

                Item curr_item = menuitem != null ? menuitem.Item : null;

                if (curr_item != null)
                {
                    switch (inputKey)
                    {
                        case 'V':
                            ItemDisplayStats(curr_item);
                            redraw_items = true;
                            redraw_player = true;
                            break;

                        case 'R':
                            ready_Item(curr_item);
                            break;

                        case 'U':
                            if (curr_item.readied == false)
                            {
                                _ovr025.string_print01("Must be Readied");
                                inputKey = ' ';
                            }
                            else if (curr_item.IsScroll() == true ||
                                     (curr_item.affect_2 > 0 && (int)curr_item.affect_3 < 0x80))
                            {
                                UseMagicItem(ref arg_0, curr_item);
                                if (gbl.game_state != GameState.Combat)
                                {
                                    arg_0 = false;
                                }

                                if (arg_0 == false)
                                {
                                    redraw_player = true;
                                }
                            }
                            break;

                        case 'T':
                            if (CanSellDropTradeItem(curr_item) == true)
                            {
                                trade_item(curr_item);
                            }
                            else
                            {
                                inputKey = ' ';
                            }
                            redraw_player = true;
                            break;

                        case 'D':
                            if (CanSellDropTradeItem(curr_item) == true)
                            {
                                _ovr025.ItemDisplayNameBuild(false, false, 0, 0, curr_item);

                                _displayDriver.press_any_key("Your " + curr_item.name + " will be gone forever", true, 14, 22, 0x26, 21, 1);

                                if (_ovr027.yes_no(gbl.defaultMenuColors, "Drop It? ") == 'Y')
                                {
                                    _ovr025.lose_item(curr_item, gbl.SelectedPlayer);
                                    redraw_items = true;
                                }

                                _seg037.draw8x8_clear_area(TextRegion.Normal2);
                            }
                            else
                            {
                                inputKey = ' ';
                            }
                            break;

                        case 'H':
                            halve_items(curr_item);
                            break;

                        case 'J':
                            join_items(curr_item);
                            break;

                        case 'S':
                            if (CanSellDropTradeItem(curr_item) == true)
                            {
                                ShopSellItem(curr_item);
                            }
                            else
                            {
                                inputKey = ' ';
                            }
                            break;

                        case 'I':
                            IdentifyItem(ref redraw_items, curr_item);
                            break;
                    }
                }

                _ovr025.reclac_player_values(player);
            }

            if (player.items.Count != oldItemCount)
            {
                redraw_items = true;
            }
        }
    }


    /*seg600:44B6 unk_1A7C6*/
    public readonly byte[,] MU_spell_lvl_learn = {
        {1, 0, 0, 0, 0},
        {0, 1, 0, 0, 0},
        {1, 1, 0, 0, 0},
        {1, 0, 1, 0, 0},
        {0, 0, 1, 0, 0},
        {0, 1, 0, 1, 0},
        {0, 0, 1, 1, 0},
        {0, 0, 0, 0, 1},
        {0, 1, 0, 0, 1},
        {0, 0, 1, 1, 1},
        {0, 0, 0, 1, 1}  };

    internal void calc_items_effects(bool add_item, Item item) /*sub_55B04*/
    {
        Player player = gbl.SelectedPlayer;

        int masked_affect = (int)item.affect_3 & 0x7F;

        switch (masked_affect)
        {
            case 0: // apply affect_2
                gbl.applyItemAffect = true;
                _ovr013.CallAffectTable((add_item) ? Effect.Add : Effect.Remove, item, player, item.affect_3);
                break;

            case 1: // ring of wizardy
                if (add_item == true)
                {
                    player.spellCastCount[2, 0] *= 2;
                    player.spellCastCount[2, 1] *= 2;
                    player.spellCastCount[2, 2] *= 2;
                }
                else
                {
                    int muSkillLevel = player.SkillLevel(SkillType.MagicUser);

                    player.spellCastCount[2, 0] = 0;
                    player.spellCastCount[2, 1] = 0;
                    player.spellCastCount[2, 2] = 0;
                    player.spellCastCount[2, 3] = 0;
                    player.spellCastCount[2, 4] = 0;

                    player.spellCastCount[2, 0] = 1;

                    for (int sp_lvl = 0; sp_lvl < (muSkillLevel - 1); sp_lvl++)
                    {
                        /* unk_1A7C6 = seg600:44B6 */
                        player.spellCastCount[2, 0] += MU_spell_lvl_learn[sp_lvl, 0];
                        player.spellCastCount[2, 1] += MU_spell_lvl_learn[sp_lvl, 1];
                        player.spellCastCount[2, 2] += MU_spell_lvl_learn[sp_lvl, 2];
                        player.spellCastCount[2, 3] += MU_spell_lvl_learn[sp_lvl, 3];
                        player.spellCastCount[2, 4] += MU_spell_lvl_learn[sp_lvl, 4];
                    }

                    byte[] spCounts = new byte[5];
                    _seg051.FillChar(0, 5, spCounts);

                    var removeList = new List<int>();

                    foreach (int id in player.spellList.IdList())
                    {
                        if (gbl.spellCastingTable[id].spellClass == SpellClass.MagicUser)
                        {
                            int spLvl = gbl.spellCastingTable[id].spellLevel - 1;
                            spCounts[spLvl] += 1;

                            if (spCounts[spLvl] > player.spellCastCount[2, spLvl])
                            {
                                removeList.Add(id);
                            }
                        }
                    }

                    foreach (var id in removeList)
                    {
                        player.spellList.ClearSpell(id);
                    }
                }
                break;

            case 2: // Gauntlets of Dexterity
                _ovr024.CalcStatBonuses(Stat.DEX, player);
                _ovr026.reclac_thief_skills(player);
                break;

            case 4:
                if (((int)item.affect_2 & 0x0f) != player.alignment)
                {
                    item.readied = false;
                    int damage = (int)item.affect_2 << 4;

                    gbl.damage_flags = DamageType.Magic;
                    if (gbl.game_state == GameState.Combat)
                    {
                        _ovr025.RedrawCombatScreen();
                    }

                    _ovr024.damage_person(false, 0, damage, player);
                    gbl.byte_1D2C8 = true;
                }
                break;

            case 5:
                _ovr024.CalcStatBonuses(Stat.STR, player);
                break;

            case 6: // Girdle of the Dwarves
                _ovr024.CalcStatBonuses(Stat.CON, player);
                _ovr024.CalcStatBonuses(Stat.CHA, player);
                break;

            case 8: //Ioun Stone
                switch ((int)item.affect_2)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        _ovr024.CalcStatBonuses((Stat)item.affect_2, player);
                        break;
                }
                break;

            case 9:
                if (add_item == false)
                {
                    _ovr024.remove_affect(null, Affects.spiritual_hammer, player);
                }
                break;

            case 10:
                _ovr024.CalcStatBonuses(Stat.DEX, player);
                break;

            case 11: // Gloves of Thievery
                _ovr026.reclac_thief_skills(player);
                break;

            case 12:
                _ovr024.CalcStatBonuses(Stat.INT, player);
                break;

            case 13:
                _ovr024.CalcStatBonuses(Stat.STR, player);
                _ovr024.CalcStatBonuses(Stat.INT, player);
                break;
        }
    }

    private enum Weld
    {
        Ok = 0,
        WrongClass = 1,
        AlreadyUsingX = 2,
        HandsFull = 3
    };

    internal void ready_Item(Item item)
    {
        bool magic_item = ((int)item.affect_3 > 0x7f);

        Player player = gbl.SelectedPlayer;

        if (item.readied)
        {
            // Remove
            if (item.cursed == true)
            {
                _ovr025.string_print01("It's Cursed");
            }
            else
            {
                item.readied = false;

                if (magic_item == true)
                {
                    calc_items_effects(false, item);
                }
            }
            return;
        }
        else
        {
            // Weld
            Weld result = Weld.Ok;

            if ((player.weaponsHandsUsed + item.HandsCount()) > 2)
            {
                result = Weld.HandsFull;
            }

            ItemSlot item_slot = gbl.ItemDataTable[item.type].item_slot;

            if (item_slot >= ItemSlot.slot_0 && item_slot <= ItemSlot.slot_8)
            {
                if (player.activeItems[item_slot] != null)
                {
                    result = Weld.AlreadyUsingX;
                }
            }
            else if (item_slot == ItemSlot.slot_9)
            {
                if (player.activeItems.Item_ptr_02 != null)
                {
                    result = Weld.AlreadyUsingX;
                }
            }

            if (item.type == ItemType.Arrow)
            {
                if (player.activeItems.arrows != null)
                {
                    result = Weld.AlreadyUsingX;
                    item_slot = ItemSlot.slot_11;
                }
            }

            if (item.type == ItemType.Quarrel)
            {
                if (player.activeItems.quarrels != null)
                {
                    result = Weld.AlreadyUsingX;
                    item_slot = ItemSlot.Quarrel;
                }
            }

            if ((player.classFlags & gbl.ItemDataTable[item.type].classFlags) == 0)
            {
                result = Weld.WrongClass;
            }
            result = Weld.Ok;
            switch (result)
            {
                case Weld.Ok:
                    item.readied = true;
                    if (magic_item == true)
                    {
                        calc_items_effects(true, item);
                    }
                    break;

                case Weld.WrongClass:
                    _ovr025.string_print01("Wrong Class");
                    break;

                case Weld.AlreadyUsingX:
                    _ovr025.ItemDisplayNameBuild(false, false, 0, 0, player.activeItems[item_slot]);
                    _ovr025.string_print01("already using " + player.activeItems[item_slot].name);
                    break;

                case Weld.HandsFull:
                    if (gbl.game_state != GameState.Combat ||
                        player.quick_fight == QuickFight.False)
                    {
                        _ovr025.string_print01("Your hands are full!");
                    }
                    break;
            }
        }
    }


    internal void trade_item(Item item)
    {
        Player player = gbl.tradeWith;
        _ovr025.LoadPic();

        _ovr025.selectAPlayer(ref player, true, "Trade with Whom?");

        if (player != null)
        {
            gbl.tradeWith = player;
            if (canCarry(item, player) == true)
            {
                _ovr025.string_print01("Overloaded");
            }
            else
            {
                player.items.Add(item);
                _ovr025.lose_item(item, gbl.SelectedPlayer);
                _ovr025.reclac_player_values(player);
            }
        }
    }


    internal void halve_items(Item item)
    {
        int half_number = item.count / 2;

        if (half_number > 0)
        {
            int half_and_remander = item.count - half_number;

            Item new_item = item.ShallowClone();
            item.count = half_and_remander;

            new_item.count = half_number;
            new_item.readied = false;

            gbl.SelectedPlayer.items.Add(new_item);
        }
        else
        {
            _ovr025.string_print01("Can't halve that");
        }
    }


    internal void join_items(Item item) /*sub_56285*/
    {
        var actual = gbl.SelectedPlayer.items.Find(i => i == item);

        var match = gbl.SelectedPlayer.items.FindAll(i =>
        {
            return (i != item &&
                    i.count > 0 &&
                    i.namenum1 == item.namenum1 &&
                    i.namenum2 == item.namenum2 &&
                    i.namenum3 == item.namenum3 &&
                    i.type == item.type &&
                    i.plus == item.plus &&
                    i.plus_save == item.plus_save &&
                    i.cursed == item.cursed &&
                    i.weight == item.weight &&
                    i.affect_1 == item.affect_1 &&
                    (int)i.affect_1 < 2 &&
                    i.affect_2 == item.affect_2 &&
                    i.affect_3 == item.affect_3);
        });

        foreach (var item_ptr in match)
        {
            if (item_ptr.count + item.count <= 255)
            {
                item.count += item_ptr.count;
                _ovr025.lose_item(item_ptr, gbl.SelectedPlayer);
            }
            else
            {
                int tempCount = 255 - (item.count + item_ptr.count);
                item.count = 255;
                item_ptr.count = tempCount;

                item = item_ptr;
            }
        }
    }


    internal void UseMagicItem(ref bool arg_0, Item item) // sub_56478
    {
        gbl.spell_from_item = false;
        int spellId = 0;

        if (item.IsScroll() == true)
        {
            gbl.currentScroll = item;

            bool dummy_bool;
            int dummy_index = -1;
            spellId = spell_menu2(out dummy_bool, ref dummy_index, SpellSource.Cast, SpellLoc.scroll);
        }
        else if (item.affect_2 > 0 && (int)item.affect_3 < 0x80)
        {
            gbl.spell_from_item = true;
            spellId = (int)item.affect_2 & 0x7F;
        }

        if (spellId == 0)
        {
            arg_0 = false;
        }
        else
        {
            if (gbl.game_state == GameState.Combat &&
                gbl.SelectedPlayer.quick_fight == QuickFight.False)
            {
                _ovr025.RedrawCombatScreen();
            }

            if (gbl.spell_from_item == true)
            {
                _ovr025.DisplayPlayerStatusString(false, 10, "uses an item", gbl.SelectedPlayer);

                if (gbl.game_state == GameState.Combat)
                {
                    _displayDriver.displayString("Item:", 0, 10, 0x17, 0);

                    _ovr025.ItemDisplayNameBuild(true, false, 0x17, 5, item);
                }
                else
                {
                    _ovr025.ItemDisplayNameBuild(true, false, 0x16, 1, item);
                }

                _displayDriver.GameDelay();
                _ovr025.ClearPlayerTextArea();
            }

            gbl.spell_from_item = true;

            if (item.IsScroll() == true)
            {
                if (gbl.SelectedPlayer.SkillLevel(SkillType.MagicUser) > 0 ||
                    gbl.SelectedPlayer.SkillLevel(SkillType.Cleric) > 0)
                {
                    _ovr023.sub_5D2E1(ref arg_0, false, gbl.SelectedPlayer.quick_fight, spellId);
                }
                else if (gbl.SelectedPlayer.thief_lvl > 9 &&
                         _ovr024.roll_dice(100, 1) <= 75)
                {
                    _ovr023.sub_5D2E1(ref arg_0, false, gbl.SelectedPlayer.quick_fight, spellId);
                }
                else
                {
                    _ovr025.DisplayPlayerStatusString(true, gbl.textYCol, "oops!", gbl.SelectedPlayer);
                }
            }
            else
            {
                _ovr023.sub_5D2E1(ref arg_0, false, gbl.SelectedPlayer.quick_fight, spellId);
            }

            gbl.spell_from_item = false;

            if (gbl.game_state == GameState.Combat &&
                gbl.spellCastingTable[spellId].whenCast != SpellWhen.Camp)
            {
                arg_0 = true;
                _ovr025.clear_actions(gbl.SelectedPlayer);
            }
        }

        if (arg_0 == true)
        {
            if (item.IsScroll() == true)
            {
                _ovr023.remove_spell_from_scroll(spellId, item, gbl.SelectedPlayer);
            }
            else if (item.affect_1 > 0)
            {
                if (item.count > 1)
                {
                    item.count -= 1;
                }
                else
                {
                    item.affect_1 -= 1;
                    if (item.affect_1 == 0)
                    {
                        _ovr025.lose_item(item, gbl.SelectedPlayer);
                    }
                }
            }
        }
    }


    internal void ShopSellItem(Item item) // sell_Item
    {
        int item_value = 0;

        if (item._value > 0)
        {
            item_value = item._value / 2;
        }

        if (item.count > 1)
        {
            if (item.type != ItemType.Arrow &&
                item.type != ItemType.Quarrel)
            {
                item_value = (item.count * item_value) / 20;
            }
            else
            {
                item_value *= item.count;
            }
        }

        _ovr025.ItemDisplayNameBuild(false, false, 0, 0, item);

        string offer = "I'll give you " + item_value.ToString() + " gold pieces for your " + item.name;

        _displayDriver.press_any_key(offer, true, 14, TextRegion.Normal2);

        if (_ovr027.yes_no(gbl.defaultMenuColors, "Is It a Deal? ") == 'Y')
        {
            _ovr025.string_print01("Sold!");

            _ovr025.lose_item(item, gbl.SelectedPlayer);

            int plat = item_value / 5;
            int gold = item_value % 5;
            int overflow;

            if (_ovr022.willOverload(out overflow, plat + gold, gbl.SelectedPlayer) == true)
            {
                _ovr025.string_print01("Overloaded. Money will be put in pool.");

                if (overflow > plat)
                {
                    gbl.SelectedPlayer.Money.AddCoins(Money.Platinum, plat);
                }
                else
                {
                    gbl.SelectedPlayer.Money.AddCoins(Money.Platinum, overflow);
                    gbl.pooled_money.AddCoins(Money.Platinum, plat - overflow);
                }

                gbl.SelectedPlayer.Money.AddCoins(Money.Gold, gold);
            }
            else
            {
                gbl.SelectedPlayer.Money.AddCoins(Money.Platinum, plat);
                gbl.SelectedPlayer.Money.AddCoins(Money.Gold, gold);
            }
        }

        _seg037.draw8x8_clear_area(TextRegion.Normal2);
    }


    internal void IdentifyItem(ref bool arg_0, Item item)
    {
        bool id_item = false;
        _ovr025.ItemDisplayNameBuild(false, false, 0, 0, item);

        _displayDriver.press_any_key("For 200 gold pieces I'll identify your " + item.name, true, 14, TextRegion.Normal2);

        if (_ovr027.yes_no(gbl.defaultMenuColors, "Is It a Deal? ") == 'Y')
        {
            int cost = 200;
            if (cost <= gbl.SelectedPlayer.Money.GetGoldWorth())
            {
                id_item = true;
                gbl.SelectedPlayer.Money.SubtractGoldWorth(cost);
            }
            else
            {
                if (cost <= gbl.pooled_money.GetGoldWorth())
                {
                    id_item = true;

                    gbl.pooled_money.SubtractGoldWorth(cost);
                }
                else
                {
                    _ovr025.string_print01("Not Enough Money");
                }
            }
        }

        if (id_item == true)
        {
            if (item.hidden_names_flag == 0)
            {
                _displayDriver.press_any_key("I can't tell anything new about your " + item.name, true, 14, TextRegion.Normal2);
            }
            else
            {
                item.hidden_names_flag = 0;
                _ovr025.ItemDisplayNameBuild(false, false, 0, 0, item);

                _displayDriver.press_any_key("It looks like some sort of " + item.name, true, 14, TextRegion.Normal2);

                arg_0 = true;
            }

            _displayDriver.GameDelay();
        }

        _seg037.draw8x8_clear_area(TextRegion.Normal2);
    }


    internal void tradeCoin()
    {
        bool finished = false;
        do
        {
            Player dest = gbl.tradeWith;
            _ovr025.LoadPic();

            _ovr025.selectAPlayer(ref dest, true, "Trade to?");

            if (dest == null)
            {
                finished = true;
            }
            else
            {
                bool noMoneyLeft;

                playerDisplayFull(gbl.SelectedPlayer);
                do
                {
                    displayMoney();
                    gbl.tradeWith = dest;

                    List<MenuItem> list = new List<MenuItem>();

                    for (int coin = 0; coin <= 6; coin++)
                    {
                        if (gbl.SelectedPlayer.Money.GetCoins(coin) > 0)
                        {
                            list.Add(new MenuItem(string.Format("{0,8} {1}", moneyString[coin], gbl.SelectedPlayer.Money.GetCoins(coin))));
                        }
                    }

                    int dummyIndex = 0;
                    bool dummyBool = true;
                    MenuItem selected;

                    _ovr027.sl_select_item(out selected, ref dummyIndex, ref dummyBool, true,
                        list, 13, 0x19, 7, 12, gbl.defaultMenuColors, " Select", "Select type of coin ");

                    if (selected == null)
                    {
                        noMoneyLeft = true;
                    }
                    else
                    {
                        string text;

                        int money_slot = _ovr022.GetMoneyIndexFromString(out text, selected.Text);

                        text = "How much " + text + "will you trade? ";

                        short num_coins = _ovr022.AskNumberValue(10, text, gbl.SelectedPlayer.Money.GetCoins(money_slot));

                        _ovr022.trade_money(money_slot, num_coins, dest, gbl.SelectedPlayer);

                        finished = noMoneyLeft = !gbl.SelectedPlayer.Money.AnyMoney();
                    }

                    list.Clear();

                } while (noMoneyLeft == false);
            }
        } while (finished == false);
    }


    internal void drop_coin()
    {
        bool noMoreMoney;

        do
        {
            displayMoney();
            List<MenuItem> menuList = new List<MenuItem>();

            for (int coin = 0; coin < 7; coin++)
            {
                if (gbl.SelectedPlayer.Money.GetCoins(coin) != 0)
                {
                    menuList.Add(new MenuItem(string.Format("{0,8} {1}", moneyString[coin], gbl.SelectedPlayer.Money.GetCoins(coin))));
                }
            }

            int index = 0;
            bool redrawMenuItems = true;

            MenuItem selected;
            _ovr027.sl_select_item(out selected, ref index, ref redrawMenuItems, true, menuList, 13, 0x19, 7,
                12, gbl.defaultMenuColors, " Select", "Select type of coin ");

            if (selected == null)
            {
                noMoreMoney = true;
            }
            else
            {
                string text;

                int money_slot = _ovr022.GetMoneyIndexFromString(out text, selected.Text);

                text = "How much " + text + "will you drop? ";

                short num_coins = _ovr022.AskNumberValue(10, text, gbl.SelectedPlayer.Money.GetCoins(money_slot));

                _ovr022.DropCoins(money_slot, num_coins, gbl.SelectedPlayer);

                noMoreMoney = !gbl.SelectedPlayer.Money.AnyMoney();
            }

            menuList.Clear();
        } while (noMoreMoney == false);
    }


    internal bool canCarry(Item item, Player player)
    {
        _ovr025.reclac_player_values(player);
        bool too_heavy = false;

        if (player.items.Count >= Player.MaxItems)
        {
            too_heavy = true;
        }

        int item_weight = item.weight;

        if (item.count > 0)
        {
            item_weight *= item.count;
        }

        if ((player.weight + item_weight) > (_ovr025.max_encumberance(player) + 1500))
        {
            too_heavy = true;
        }

        return too_heavy;
    }


    internal void scroll_team_list(char input_key)
    {
        int index = gbl.TeamList.IndexOf(gbl.SelectedPlayer);

        if (gbl.TeamList.Count > 0)
        {
            if (input_key == 'O')
            {
                //next
                index = (index + 1) % gbl.TeamList.Count;
                gbl.SelectedPlayer = gbl.TeamList[index];
            }
            else if (input_key == 'G')
            {
                // previous
                index = (index - 1 + gbl.TeamList.Count) % gbl.TeamList.Count;
                gbl.SelectedPlayer = gbl.TeamList[index];
            }
        }
    }

    internal byte spell_menu2(out bool arg_0, ref int index, SpellSource arg_8, SpellLoc spl_location)
    {
        string text;
        byte result;

        switch (spl_location)
        {
            case SpellLoc.memory:

                text = "in Memory";
                break;

            case SpellLoc.grimoire:

                text = "in Grimoire";
                break;

            case SpellLoc.scroll:
                text = "on Scroll";
                break;

            case SpellLoc.scrolls:
                text = "on Scrolls";
                break;

            case SpellLoc.choose:
                text = "to Choose";
                break;

            case SpellLoc.memorize:
                text = "to Memorize";
                break;

            case SpellLoc.scribe:
                text = "to Scribe";
                break;

            default:
                text = string.Empty;
                break;
        }

        arg_0 = _ovr023.BuildSpellList(spl_location);

        if (arg_0 == true)
        {
            if (index < 0 ||
                arg_8 == SpellSource.Cast)
            {
                if (gbl.game_state != GameState.Combat)
                {
                    if (arg_8 == SpellSource.Memorize)
                    {
                        _seg037.draw8x8_05();
                    }
                    else
                    {
                        _seg037.draw8x8_07();
                    }
                }
                else
                {
                    _seg037.DrawFrame_Outer();
                }
            }

            _ovr025.displayPlayerName(true, 1, 1, gbl.SelectedPlayer);

            _displayDriver.displayString("Spells " + text, 0, 10, 1, gbl.SelectedPlayer.name.Length + 4);

            result = _ovr023.spell_menu(ref index, arg_8);
        }
        else
        {
            result = 0;
        }

        return result;
    }


    internal bool CanCastHeal(Player player) /* sub_575F0 */
    {
        return (player.SkillLevel(SkillType.Paladin) > 0 &&
                gbl.game_state != GameState.Combat &&
                player.health_status == Status.okey &&
                player.HasAffect(Affects.paladinDailyHealCast) == false);
    }


    internal bool CanCastCureDiseases(Player player) /* sub_57655 */
    {
        return (player.SkillLevel(SkillType.Paladin) > 0 &&
                gbl.game_state != GameState.Combat &&
                player.health_status == Status.okey &&
                player.paladinCuresLeft > 0);
    }


    internal void PaladinHeal(Player player)
    {
        _ovr025.LoadPic();
        Player target = gbl.TeamList[0];

        _ovr025.selectAPlayer(ref target, true, "Heal whom? ");

        if (target == null)
        {
            playerDisplayFull(gbl.SelectedPlayer);
            return;
        }

        int healAmount = player.SkillLevel(SkillType.Paladin) * 2;

        if (_ovr024.heal_player(0, healAmount, target) == true)
        {
            _ovr025.string_print01(target.name + " feels better");
        }
        else
        {
            _ovr025.string_print01(target.name + " is unaffected");
        }

        _ovr024.add_affect(false, 0, 1440, Affects.paladinDailyHealCast, player);
        playerDisplayFull(gbl.SelectedPlayer);
    }

    private Affects[] paladinCureableDiseases = { // unk_16B39
        Affects.helpless,
        Affects.cause_disease_1,
        Affects.weaken,
        Affects.cause_disease_2,
        Affects.hot_fire_shield,
        Affects.affect_39 };

    internal void PaladinCureDisease(Player player) /* sub_577EC */
    {
        _ovr025.LoadPic();
        Player target = gbl.TeamList[0];

        _ovr025.selectAPlayer(ref target, true, "Cure whom? ");

        if (target == null)
        {
            playerDisplayFull(gbl.SelectedPlayer);
        }
        else
        {
            bool is_diseased = System.Array.Exists(paladinCureableDiseases, affect => target.HasAffect(affect));

            char input = 'Y';

            if (is_diseased == false)
            {
                _ovr025.DisplayPlayerStatusString(false, 0, "is not diseased", target);

                input = _ovr027.yes_no(gbl.defaultMenuColors, "cure anyway: ");

                _ovr025.ClearPlayerTextArea();
            }

            if (input == 'Y')
            {
                gbl.cureSpell = true;
                System.Array.ForEach(paladinCureableDiseases, affect => _ovr024.remove_affect(null, affect, target));

                gbl.cureSpell = false;

                if (player.paladinCuresLeft > 0)
                {
                    player.paladinCuresLeft--;
                }

                if (player.HasAffect(Affects.paladinDailyCureRefresh) == false)
                {
                    _ovr024.add_affect(true, 0, 0x2760, Affects.paladinDailyCureRefresh, player);
                }

                _ovr025.string_print01(target.name + " is cured");
            }

            playerDisplayFull(gbl.SelectedPlayer);
        }
    }
}
