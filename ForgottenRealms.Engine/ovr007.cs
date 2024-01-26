using System.Collections.Generic;
using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine;

public class ovr007
{
    private readonly ovr008 _ovr008;
    private readonly ovr020 _ovr020;
    private readonly ovr022 _ovr022;
    private readonly ovr025 _ovr025;
    private readonly ovr027 _ovr027;
    private readonly seg037 _seg037;
    private readonly DisplayDriver _displayDriver;

    public ovr007(ovr008 ovr008, ovr020 ovr020, ovr022 ovr022, ovr025 ovr025, ovr027 ovr027, seg037 seg037, DisplayDriver displayDriver)
    {
        _ovr008 = ovr008;
        _ovr020 = ovr020;
        _ovr022 = ovr022;
        _ovr025 = ovr025;
        _ovr027 = ovr027;
        _seg037 = seg037;
        _displayDriver = displayDriver;
    }

    internal void CityShop() // sub_2F6E7
    {
        bool reloadPics = false; /* Simeon */
        bool items_on_ground;
        bool money_on_ground;
        char inputKey;

        gbl.game_state = GameState.Shop;
        gbl.redrawBoarder = (gbl.area_ptr.inDungeon == 0);

        _ovr025.LoadPic();
        gbl.redrawBoarder = true;
        _ovr025.PartySummary(gbl.SelectedPlayer);

        gbl.pooled_money.ClearAll();

        bool exitShop = false;

        gbl.items_pointer.ForEach(item => _ovr025.ItemDisplayNameBuild(false, false, 0, 0, item));

        do
        {
            _ovr022.treasureOnGround(out items_on_ground, out money_on_ground);

            string text;
            if (money_on_ground == true)
            {
                text = "Buy View Take Pool Share Appraise Exit";
            }
            else
            {
                text = "Buy View Pool Appraise Exit";
            }

            bool controlKey;

            inputKey = _ovr027.displayInput(out controlKey, false, 1, gbl.defaultMenuColors, text, string.Empty);

            switch (inputKey)
            {
                case 'B':
                    shop_buy();
                    break;

                case 'V':
                    _ovr020.viewPlayer();
                    break;

                case 'T':
                    _ovr022.TakePoolMoney();
                    break;

                case 'P':
                    if (controlKey == false)
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
                    _ovr022.treasureOnGround(out items_on_ground, out money_on_ground);

                    if (money_on_ground == true)
                    {
                        _displayDriver.press_any_key("As you Leave the Shopkeeper says, \"Excuse me but you have Left Some Money here.\"  ", true, 10, TextRegion.NormalBottom);
                        _displayDriver.press_any_key("Do you want to go back and get your Money?", false, 15, TextRegion.NormalBottom);

                        int menu_selected = _ovr008.sub_317AA(false, false, gbl.defaultMenuColors, "~Yes ~No", "");

                        if (menu_selected == 1)
                        {
                            exitShop = true;
                        }
                        else
                        {
                            _seg037.draw8x8_clear_area(0x16, 0x26, 17, 1);
                        }
                    }
                    else
                    {
                        exitShop = true;
                    }
                    break;

                case 'G':
                    _ovr020.scroll_team_list(inputKey);
                    break;

                case 'O':
                    _ovr020.scroll_team_list(inputKey);
                    break;
            }

            if (inputKey == 'B' ||
                inputKey == 'T')
            {
                _ovr025.LoadPic();
            }
            else if (reloadPics == true)
            {
                _ovr025.LoadPic();
                reloadPics = false;
            }

            _ovr025.PartySummary(gbl.SelectedPlayer);

        } while (exitShop == false);
    }

    internal bool PlayerAddItem(Item item) /*was overloaded */
    {
        bool wouldOverload;
        if (_ovr020.canCarry(item, gbl.SelectedPlayer) == true)
        {
            _ovr025.string_print01("Overloaded");
            wouldOverload = true;
        }
        else
        {
            wouldOverload = false;

            gbl.SelectedPlayer.items.Add(item.ShallowClone());

            _ovr025.reclac_player_values(gbl.SelectedPlayer);
        }

        return wouldOverload;
    }

    private char ShopChooseItem(ref int index, out Item selectedItem) // sub_2F04E
    {
        List<MenuItem> list = new List<MenuItem>();
        foreach (var item in gbl.items_pointer)
        {
            if (item._value == 0)
            {
                item._value = 1;
            }

            int val = ItemsValue(item);

            list.Insert(0, new MenuItem(string.Format("{0,-21}{1,9}", item.name.Trim(), val), item));
        }

        gbl.menuSelectedWord = 0;

        MenuItem mi;
        selectedItem = null;

        char input_key = _ovr027.sl_select_item(out mi, ref index, ref gbl.shopRedrawMenuItems, true, list,
            0x16, 0x26, 1, 1, gbl.defaultMenuColors, "Buy", "Items: ");

        if (mi != null)
        {
            selectedItem = mi.Item;
        }

        foreach (var item in gbl.items_pointer)
        {
            _ovr025.ItemDisplayNameBuild(false, false, 0, 0, item);
        }

        return input_key;
    }

    private int ItemsValue(Item item_ptr)
    {
        int val;
        switch (gbl.area2_ptr.field_6DA)
        {
            case 0x01:
                val = item_ptr._value >> 4;
                break;

            case 0x02:
                val = item_ptr._value >> 3;
                break;

            case 0x04:
                val = item_ptr._value >> 2;
                break;

            case 0x08:
                val = item_ptr._value >> 1;
                break;

            case 0x20:
                val = item_ptr._value << 1;
                break;

            case 0x40:
                val = item_ptr._value << 2;
                break;

            case 0x80:
                val = item_ptr._value << 3;
                break;

            default:
                val = item_ptr._value;
                break;
        }
        return val;
    }

    private void shop_buy() /* sub_2F474 */
    {
        _seg037.DrawFrame_Outer();
        gbl.shopRedrawMenuItems = true;

        int index = 0;
        while (true)
        {
            Item item;
            char input_key = ShopChooseItem(ref index, out item);

            if (input_key != 'B' && input_key != 0x0d)
            {
                return;
            }
            else
            {
                int item_cost = ItemsValue(item);
                int player_gold = gbl.SelectedPlayer.Money.GetGoldWorth();

                if (item_cost <= gbl.SelectedPlayer.Money.GetGoldWorth())
                {
                    bool overloaded = PlayerAddItem(item);

                    if (overloaded == false)
                    {
                        player_gold -= item_cost;
                        gbl.SelectedPlayer.Money.SubtractGoldWorth(item_cost);
                    }
                }
                else if (item_cost <= gbl.pooled_money.GetGoldWorth())
                {
                    bool overloaded = PlayerAddItem(item);

                    if (overloaded == false)
                    {
                        gbl.pooled_money.SubtractGoldWorth(item_cost);
                    }
                }
                else
                {
                    _ovr025.string_print01("Not enough Money.");
                }
            }
        }
    }
}
