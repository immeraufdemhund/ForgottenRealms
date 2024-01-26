using ForgottenRealms.Engine.Classes;
using ForgottenRealms.Engine.Classes.Combat;

namespace ForgottenRealms.Engine.CharacterFeature.CreatePlayerFeature;

public class IconBuilder
{
    private static Set unk_4FE94 = new Set(0, 69);

    private readonly DisplayDriver _displayDriver;
    private readonly ovr017 _ovr017;
    private readonly ovr027 _ovr027;
    private readonly ovr033 _ovr033;
    private readonly ovr034 _ovr034;
    private readonly seg037 _seg037;
    private readonly seg040 _seg040;

    public IconBuilder(DisplayDriver displayDriver, ovr017 ovr017, ovr027 ovr027, ovr033 ovr033, ovr034 ovr034, seg037 seg037, seg040 seg040)
    {
        _displayDriver = displayDriver;
        _ovr017 = ovr017;
        _ovr027 = ovr027;
        _ovr033 = ovr033;
        _ovr034 = ovr034;
        _seg037 = seg037;
        _seg040 = seg040;
    }

    internal void Show()
    {
        Player player_ptr2;
        Player player;
        char var_1B = '\0'; /* Simeon */
        byte var_1A = 0; /* Simeon */
        bool second_color = false;
        byte color_index = 0;
        byte[] bkup_colours = new byte[6];
        byte var_8;
        byte weaponIcon;
        byte headIcon;
        char inputKey;

        string[] iconStrings = {   "",
            "Parts 1st-color 2nd-color Size Exit",
            "Head Weapon Exit",
            "Weapon Body xxxx Shield Arm Leg Exit",
            " Keep Exit",
            "Next Prev Keep Exit" };

        _seg037.DrawFrame_Outer();
        _ovr033.Color_0_8_inverse();

        do
        {
            _ovr017.LoadPlayerCombatIcon(false);

            player = gbl.SelectedPlayer;

            var_8 = 1;
            System.Array.Copy(player.icon_colours, bkup_colours, 6);

            byte bkup_icon_id = player.icon_id;
            player.icon_id = 0x0C;
            _ovr017.LoadPlayerCombatIcon(false);
            player.icon_id = bkup_icon_id;

            headIcon = player.head_icon;
            weaponIcon = player.weapon_icon;
            byte bkup_size = player.icon_size;

            duplicateCombatIcon(true, 12, player.icon_id);
            drawIconEditorIcons(2, 1);

            _displayDriver.displayString("old", 0, 15, 6, 8);
            _displayDriver.displayString("ready   action", 0, 15, 10, 3);
            _displayDriver.displayString("new", 0, 15, 12, 8);
            _displayDriver.displayString("ready   action", 0, 15, 16, 3);

            do
            {
                drawIconEditorIcons(4, 1);

                string text;
                if (var_8 == 4)
                {
                    if (player.icon_size == 2)
                    {
                        text = "Small" + iconStrings[4];
                    }
                    else
                    {
                        text = "Large" + iconStrings[4];
                    }
                }
                else
                {
                    text = iconStrings[var_8];
                }

                bool specialKey;

                inputKey = _ovr027.displayInput(out specialKey, false, 0, gbl.defaultMenuColors, text, string.Empty);

                if (specialKey == false)
                {
                    switch (var_8)
                    {
                        case 1:
                            var_1A = 1;

                            switch (inputKey)
                            {
                                case 'P':
                                    var_8 = 2;
                                    break;

                                case '1':
                                    var_8 = 3;
                                    second_color = false;

                                    iconStrings[3] = "Weapon Body Hair Shield Arm Leg Exit";
                                    break;

                                case '2':
                                    var_8 = 3;
                                    second_color = true;

                                    iconStrings[3] = "Weapon Body Face Shield Arm Leg Exit";
                                    break;

                                case 'S':
                                    var_8 = 4;
                                    break;

                                case 'E':
                                    var_1A = 0;
                                    break;
                            }

                            break;

                        case 2:
                            var_1A = 2;
                            if (unk_4FE94.MemberOf(inputKey) == true)
                            {
                                var_8 = 1;
                            }
                            else
                            {
                                var_1B = inputKey;
                                var_8 = 5;
                            }
                            break;

                        case 3:
                            var_1A = 3;

                            switch (inputKey)
                            {
                                case 'W':
                                    color_index = 5;
                                    break;

                                case 'B':
                                    color_index = 0;
                                    break;

                                case 'H':
                                    color_index = 3;
                                    break;

                                case 'F':
                                    color_index = 3;
                                    break;

                                case 'S':
                                    color_index = 4;
                                    break;

                                case 'A':
                                    color_index = 1;
                                    break;

                                case 'L':
                                    color_index = 2;
                                    break;

                                default:
                                    color_index = 0;
                                    break;
                            }

                            if (unk_4FE94.MemberOf(inputKey) == true)
                            {
                                var_8 = 1;
                            }
                            else
                            {
                                var_8 = 5;
                            }
                            break;

                        case 4:
                            switch (inputKey)
                            {
                                case 'L':
                                    player.icon_size = 2;
                                    _ovr017.LoadPlayerCombatIcon(false);
                                    break;

                                case 'S':
                                    player.icon_size = 1;
                                    _ovr017.LoadPlayerCombatIcon(false);
                                    break;

                                case 'K':
                                    bkup_size = player.icon_size;
                                    var_8 = 1;
                                    inputKey = ' ';
                                    break;

                                case 'E':
                                    goto case '\0';

                                case '\0':
                                    player.icon_size = bkup_size;
                                    var_8 = 1;
                                    inputKey = ' ';
                                    break;
                            }

                            _ovr017.LoadPlayerCombatIcon(false);
                            break;

                        case 5:
                            if (var_1A == 2)
                            {
                                if (var_1B == 'H')
                                {
                                    if (inputKey == 0x50)
                                    {
                                        player.head_icon = (byte)Sys.WrapMinMax(player.head_icon - 1, 0, 13);
                                    }
                                    else if (inputKey == 'N')
                                    {
                                        player.head_icon = (byte)Sys.WrapMinMax(player.head_icon + 1, 0, 13);
                                    }
                                    else if (inputKey == 'K')
                                    {
                                        player_ptr2 = gbl.SelectedPlayer;
                                        headIcon = player_ptr2.head_icon;
                                        var_8 = var_1A;
                                        inputKey = ' ';
                                    }
                                    else if (inputKey == 'E' || inputKey == '\0')
                                    {
                                        player.head_icon = headIcon;
                                        var_8 = var_1A;
                                        inputKey = ' ';
                                    }

                                    _ovr017.LoadPlayerCombatIcon(false);
                                }
                                else if (var_1B == 'W')
                                {
                                    if (inputKey == 'P')
                                    {
                                        if (player.weapon_icon > 0)
                                        {
                                            player.weapon_icon -= 1;
                                        }
                                        else
                                        {
                                            player.weapon_icon = 0x1F;
                                        }
                                    }
                                    else if (inputKey == 'N')
                                    {
                                        if (player.weapon_icon < 0x1F)
                                        {
                                            player.weapon_icon += 1;
                                        }
                                        else
                                        {
                                            player.weapon_icon = 0;
                                        }
                                    }
                                    else if (inputKey == 'K')
                                    {
                                        player_ptr2 = gbl.SelectedPlayer;
                                        weaponIcon = player_ptr2.weapon_icon;
                                        var_8 = var_1A;
                                        inputKey = ' ';
                                    }
                                    else if (inputKey == 'E' || inputKey == '\0')
                                    {
                                        player.weapon_icon = weaponIcon;
                                        var_8 = var_1A;
                                        inputKey = ' ';
                                    }

                                    _ovr017.LoadPlayerCombatIcon(false);
                                }
                            }
                            else if (var_1A == 3)
                            {
                                byte low_color = (byte)(player.icon_colours[color_index] & 0x0F);
                                byte high_color = (byte)((player.icon_colours[color_index] & 0xF0) >> 4);

                                if (inputKey == 'N')
                                {
                                    if (second_color == true)
                                    {
                                        high_color = (byte)((high_color + 1) % 16);
                                    }
                                    else
                                    {
                                        low_color = (byte)((low_color + 1) % 16);
                                    }

                                    player.icon_colours[color_index] = (byte)(low_color + (high_color << 4));
                                }
                                else if (inputKey == 'P')
                                {
                                    if (second_color == true)
                                    {
                                        high_color = (byte)((high_color - 1) & 0x0F);
                                    }
                                    else
                                    {
                                        low_color = (byte)((low_color - 1) & 0x0F);
                                    }

                                    player.icon_colours[color_index] = (byte)(low_color + (high_color << 4));
                                }
                                else if (inputKey == 'K')
                                {
                                    System.Array.Copy(player.icon_colours, bkup_colours, 6);
                                    var_8 = var_1A;
                                    inputKey = ' ';
                                }
                                else if (inputKey == 'E' || inputKey == '\0')
                                {
                                    System.Array.Copy(bkup_colours, player.icon_colours, 6);
                                    var_8 = var_1A;
                                    inputKey = ' ';
                                }
                            }
                            break;
                    }
                }

                duplicateCombatIcon(true, 12, player.icon_id);

            } while (var_1A != 0 || unk_4FE94.MemberOf(inputKey) == false);

            player.head_icon = headIcon;
            player.weapon_icon = weaponIcon;
            player.icon_size = bkup_size;

            System.Array.Copy(bkup_colours, player.icon_colours, 6);

            duplicateCombatIcon(true, 12, player.icon_id);
            duplicateCombatIcon(false, player.icon_id, 12);

            _ovr027.ClearPromptArea();
            _ovr034.ReleaseCombatIcon(12);

            inputKey = _ovr027.yes_no(gbl.defaultMenuColors, "Is this icon ok? ");

        } while (inputKey != 'Y');

        _ovr033.Color_0_8_normal();
    }

    private void drawIconEditorIcons(sbyte titleY, sbyte titleX)
    {
        _seg040.DrawColorBlock(0, 24, 12, titleY * 24, titleX * 3);

        _ovr034.draw_combat_icon(25, Icon.Normal, 0, titleY, titleX);
        _ovr034.draw_combat_icon(25, Icon.Attack, 0, titleY, titleX + 3);

        _ovr034.draw_combat_icon(12, Icon.Normal, 0, titleY, titleX);
        _ovr034.draw_combat_icon(12, Icon.Attack, 0, titleY, titleX + 3);

        _seg040.DrawOverlay();
    }

    private static void duplicateCombatIcon(bool recolour, byte destIndex, byte sourceIndex)
    {
        gbl.combat_icons[destIndex].DuplicateIcon(recolour, gbl.combat_icons[sourceIndex], gbl.SelectedPlayer);
    }
}
