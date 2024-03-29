using System;
using System.Collections.Generic;
using ForgottenRealms.Engine.AffectsFeature;
using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine;

public class TempleShopService
{
    private Affects[] disease_types = {  Affects.helpless,  Affects.cause_disease_1,
        Affects.weaken, Affects.cause_disease_2,
        Affects.animate_dead, Affects.affect_39 };

    private string[] temple_sl = { "Cure Blindness", "Cure Disease", "Cure Light Wounds", "Cure Serious Wounds", "Cure Critical Wounds", "Heal", "Neutralize Poison", "Raise Dead", "Remove Curse", "Stone to Flesh", "Exit" };

    private readonly DisplayDriver _displayDriver;
    private readonly ovr008 _ovr008;
    private readonly ovr020 _ovr020;
    private readonly ovr022 _ovr022;
    private readonly ovr023 _ovr023;
    private readonly ovr024 _ovr024;
    private readonly ovr025 _ovr025;
    private readonly ovr027 _ovr027;
    private readonly seg037 _seg037;

    public TempleShopService(DisplayDriver displayDriver, ovr008 ovr008, ovr020 ovr020, ovr022 ovr022, ovr023 ovr023, ovr024 ovr024, ovr025 ovr025, ovr027 ovr027, seg037 seg037)
    {
        _displayDriver = displayDriver;
        _ovr008 = ovr008;
        _ovr020 = ovr020;
        _ovr022 = ovr022;
        _ovr023 = ovr023;
        _ovr024 = ovr024;
        _ovr025 = ovr025;
        _ovr027 = ovr027;
        _seg037 = seg037;
    }

    internal void temple_shop()
    {
        bool reloadPics = false;

        gbl.game_state = GameState.Shop;
        gbl.redrawBoarder = (gbl.area_ptr.inDungeon == 0);

        _ovr025.LoadPic();
        gbl.redrawBoarder = true;
        _ovr025.PartySummary(gbl.SelectedPlayer);

        gbl.pooled_money.ClearAll();

        bool stop_loop = false;

        do
        {
            bool items_present;
            bool money_present;

            _ovr022.treasureOnGround(out items_present, out money_present);
            string text;
            if (money_present == true)
            {
                text = "Heal View Take Pool Share Appraise Exit";
            }
            else
            {
                text = "Heal View Pool Appraise Exit";
            }

            bool ctrl_key;
            char input_key = _ovr027.displayInput(out ctrl_key, false, 1, gbl.defaultMenuColors, text, string.Empty);

            switch (input_key)
            {
                case 'H':
                    if (ctrl_key == false)
                    {
                        temple_heal();
                    }
                    break;

                case 'V':
                    _ovr020.viewPlayer();
                    break;

                case 'T':
                    _ovr022.TakePoolMoney();
                    break;

                case 'P':
                    if (ctrl_key == false)
                    {
                        _ovr022.poolMoney();
                    }
                    break;

                case 'S':
                    _ovr022.share_pooled();
                    break;

                case 'A':
                    reloadPics = _ovr022.appraiseGemsJewels();
                    break;

                case 'E':
                    _ovr022.treasureOnGround(out items_present, out money_present);

                    if (money_present == true)
                    {
                        string prompt = "~Yes ~No";

                        _displayDriver.press_any_key("As you leave a priest says, \"Excuse me but you have left some money here\" ", true, 10, TextRegion.NormalBottom);
                        _displayDriver.press_any_key("Do you want to go back and retrieve your money?", true, 10, TextRegion.NormalBottom);
                        int menu_selected = _ovr008.sub_317AA(false, false, gbl.defaultMenuColors, prompt, "");

                        if (menu_selected == 1)
                        {
                            stop_loop = true;
                        }
                        else
                        {
                            _seg037.draw8x8_clear_area(0x16, 0x26, 17, 1);
                        }
                    }
                    else
                    {
                        stop_loop = true;
                    }

                    break;

                case 'G':
                    _ovr020.scroll_team_list(input_key);
                    break;

                case 'O':
                    _ovr020.scroll_team_list(input_key);
                    break;
            }

            if (input_key == 'B' ||
                input_key == 'T')
            {
                _ovr025.LoadPic();
            }
            else if (reloadPics == true)
            {
                _ovr025.LoadPic();
                reloadPics = false;
            }

            _ovr025.PartySummary(gbl.SelectedPlayer);
        } while (stop_loop == false);
    }

    private void temple_heal()
    {
        int sl_index = 0;

        bool end_shop = false;

        List<MenuItem> stringList = new List<MenuItem>(10);

        for (int i = 0; i < 10; i++)
        {
            stringList.Add(new MenuItem(temple_sl[i]));
        }

        _ovr027.ClearPromptAreaNoUpdate();
        bool redrawMenuItems = true;
        _seg037.DrawFrame_WildernessMap();

        do
        {
            string text = gbl.SelectedPlayer.name + ", how can we help you?";
            _displayDriver.displayString(text, 0, 15, 1, 1);
            MenuItem dummySelected;

            char sl_output = _ovr027.sl_select_item(out dummySelected, ref sl_index, ref redrawMenuItems, false,
                stringList, 15, 0x26, 4, 2, gbl.defaultMenuColors, "Heal Exit", string.Empty);

            if (sl_output == 'H' || sl_output == 0x0d)
            {
                switch (sl_index)
                {
                    case 0:
                        cure_blindness();
                        break;

                    case 1:
                        cure_disease();
                        break;

                    case 2:
                        cure_wounds(1);
                        break;

                    case 3:
                        cure_wounds(2);
                        break;

                    case 4:
                        cure_wounds(3);
                        break;

                    case 5:
                        cure_wounds(4);
                        break;

                    case 6:
                        cure_poison2();
                        break;

                    case 7:
                        raise_dead();
                        break;

                    case 8:
                        remove_curse();
                        break;

                    case 9:
                        stone_to_flesh();
                        break;

                    case 10:
                        end_shop = true;
                        break;
                }
            }
            else if (sl_output == 0)
            {
                end_shop = true;
            }

        } while (end_shop == false);

        stringList.Clear();

        _ovr025.LoadPic();
        _ovr025.PartySummary(gbl.SelectedPlayer);
    }

    private bool buy_cure(int cost, string cure_name) /* buy_cure */
    {
        string text = string.Format("{0} will only cost {1} gold pieces.", cure_name, cost);
        _displayDriver.press_any_key(text, true, 10, TextRegion.NormalBottom);

        bool buy = false;

        if ('Y' == _ovr027.yes_no(gbl.defaultMenuColors, "pay for cure "))
        {
            if (cost <= gbl.SelectedPlayer.Money.GetGoldWorth())
            {
                gbl.SelectedPlayer.Money.SubtractGoldWorth(cost);
                buy = true;
            }
            else if (cost <= gbl.pooled_money.GetGoldWorth())
            {
                gbl.pooled_money.SubtractGoldWorth(cost);
                buy = true;
            }
            else
            {
                _ovr025.string_print01("Not enough money.");
                buy = false;
            }
        }

        if (buy)
        {
            _ovr025.ClearPlayerTextArea();
            _ovr025.DisplayPlayerStatusString(true, 0, "is cured.", gbl.SelectedPlayer);
        }

        return buy;
    }

    private void cure_blindness()
    {
        bool cast = true;

        if (gbl.SelectedPlayer.HasAffect(Affects.blinded) == false)
        {
            cast = CastCureAnyway("is not blind.");
        }

        if (cast)
        {
            if (buy_cure(1000, "Cure Blindness"))
            {
                _ovr024.remove_affect(null, Affects.blinded, gbl.SelectedPlayer);
            }
        }
    }

    private void cure_disease()
    {
        bool is_diseased = Array.Exists(disease_types, aff => gbl.SelectedPlayer.HasAffect(aff));

        bool cast = true;
        if (is_diseased == false)
        {
            cast = CastCureAnyway("is not diseased.");
        }

        if (cast)
        {
            if (buy_cure(1000, "Cure Disease"))
            {
                gbl.cureSpell = true;
                for (int i = 0; i < 6; i++)
                {
                    _ovr024.remove_affect(null, disease_types[i], gbl.SelectedPlayer);
                }

                gbl.cureSpell = false;
            }
        }
    }

    private void cure_wounds(int healType)
    {
        switch (healType)
        {
            case 1:
                if (buy_cure(100, "Cure Light Wounds"))
                {
                    int heal_amount = _ovr024.roll_dice(8, 1);
                    _ovr024.heal_player(0, heal_amount, gbl.SelectedPlayer);
                }
                break;

            case 2:
                if (buy_cure(350, "Cure Serious Wounds"))
                {
                    int heal_amount = _ovr024.roll_dice(8, 2) + 1;
                    _ovr024.heal_player(0, heal_amount, gbl.SelectedPlayer);
                }
                break;

            case 3:
                if (buy_cure(600, "Cure Critical Wounds"))
                {
                    int heal_amount = _ovr024.roll_dice(8, 3) + 3;
                    _ovr024.heal_player(0, heal_amount, gbl.SelectedPlayer);
                }
                break;

            case 4:
                if (buy_cure(5000, "Heal"))
                {
                    int heal_amount = gbl.SelectedPlayer.hit_point_max;
                    heal_amount -= gbl.SelectedPlayer.hit_point_current;
                    heal_amount -= _ovr024.roll_dice(4, 1);

                    _ovr024.heal_player(0, heal_amount, gbl.SelectedPlayer);
                    _ovr024.remove_affect(null, Affects.blinded, gbl.SelectedPlayer);

                    for (int i = 0; i < 6; i++)
                    {
                        _ovr024.remove_affect(null, disease_types[i], gbl.SelectedPlayer);
                    }

                    _ovr024.remove_affect(null, Affects.feeblemind, gbl.SelectedPlayer);

                    _ovr024.CalcStatBonuses(Stat.INT, gbl.SelectedPlayer);
                    _ovr024.CalcStatBonuses(Stat.WIS, gbl.SelectedPlayer);
                }
                break;
        }
    }

    private void raise_dead()
    {
        Player player = gbl.SelectedPlayer;
        bool player_dead = false;

        if (player.health_status == Status.dead ||
            player.health_status == Status.animated)
        {
            player_dead = true;
        }

        if (player_dead == true ||
            (player_dead == false && CastCureAnyway("is not dead.")))
        {
            if (buy_cure(5500, "Raise Dead") && player_dead == true)
            {
                gbl.cureSpell = true;

                _ovr024.remove_affect(null, Affects.animate_dead, player);
                _ovr024.remove_affect(null, Affects.poisoned, player);

                gbl.cureSpell = false;

                player.hit_point_current = 1;
                player.health_status = Status.okey;
                player.in_combat = true;

                if (player.stats2.Con.full <= 0)
                {
                    player.stats2.Con.full--;
                }

                int var_107;
                if (player.hit_point_max > player.hit_point_rolled)
                {
                    var_107 = player.hit_point_max - player.hit_point_rolled;
                }
                else
                {
                    var_107 = 0;
                }

                int var_108 = 0;

                if (player.stats2.Con.full >= 14)
                {
                    for (int classIdx = 0; classIdx <= 7; classIdx++)
                    {
                        if (player.ClassLevel[classIdx] > 0)
                        {
                            if (classIdx == 2)
                            {
                                var_108 += (player.stats2.Con.full - 14) * player.fighter_lvl;
                            }
                            else if (player.stats2.Con.full > 15)
                            {
                                var_108 += player.ClassLevel[classIdx] * 2;
                            }
                            else
                            {
                                var_108 += player.ClassLevel[classIdx];
                            }
                        }
                    }

                    if (var_108 > 0)
                    {
                        var_107 /= var_108;
                    }

                    if (player.stats2.Con.full < 17 ||
                        player.fighter_lvl > 0 ||
                        player.fighter_lvl > player.multiclassLevel)
                    {
                        player.hit_point_max = (byte)var_107;
                    }
                }
            }
        }
    }

    private void cure_poison2()
    {
        bool isPoisoned = gbl.SelectedPlayer.HasAffect(Affects.poisoned);

        if (isPoisoned == true ||
            (isPoisoned == false && CastCureAnyway("is not poisoned.")))
        {
            if (buy_cure(1000, "Neutralize Poison"))
            {
                gbl.cureSpell = true;

                _ovr024.remove_affect(null, Affects.poisoned, gbl.SelectedPlayer);
                _ovr024.remove_affect(null, Affects.slow_poison, gbl.SelectedPlayer);
                _ovr024.remove_affect(null, Affects.poison_damage, gbl.SelectedPlayer);

                gbl.cureSpell = false;
            }
        }
    }

    private void remove_curse()
    {
        bool has_curse_items = gbl.SelectedPlayer.items.Find(item => item.cursed) != null;
        bool cast = true;

        if (has_curse_items == false &&
            gbl.SelectedPlayer.HasAffect(Affects.bestow_curse) == false)
        {
            cast = CastCureAnyway("is not cursed.");
        }

        if (cast && buy_cure(3500, "Remove Curse"))
        {
            gbl.spellTargets.Clear();
            gbl.spellTargets.Add(gbl.SelectedPlayer);
            _ovr023.SpellRemoveCurse();
        }
    }

    private void stone_to_flesh()
    {
        if (gbl.SelectedPlayer.health_status == Status.stoned ||
            (gbl.SelectedPlayer.health_status != Status.stoned && CastCureAnyway("is not stoned.")))
        {
            if (buy_cure(2000, "Stone to Flesh") &&
                gbl.SelectedPlayer.health_status == Status.stoned)
            {
                gbl.SelectedPlayer.health_status = Status.okey;
                gbl.SelectedPlayer.in_combat = true;
                gbl.SelectedPlayer.hit_point_current = 1;
            }
        }
    }

    private bool CastCureAnyway(string text)
    {
        _ovr025.DisplayPlayerStatusString(false, 0, text, gbl.SelectedPlayer);

        char ret_val = _ovr027.yes_no(gbl.defaultMenuColors, "cast cure anyway: ");

        _ovr025.ClearPlayerTextArea();

        return ret_val == 'Y';
    }
}
