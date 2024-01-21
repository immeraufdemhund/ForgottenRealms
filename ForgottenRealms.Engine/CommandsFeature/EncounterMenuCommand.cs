using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class EncounterMenuCommand : IGameCommand
{
    public void Execute()
    {
        ushort var_43D;
        int var_43B;
        byte var_43A;
        string displayText;
        bool useOverlay;
        bool clearTextArea;
        byte init_max;
        byte init_min;
        byte var_40A;
        byte var_408;
        byte var_407;
        string text = string.Empty; /* Simeon */
        string[] strings = new string[3];
        byte[] var_6 = new byte[5];
        int menu_selected;

        gbl.byte_1EE95 = true;
        gbl.bottomTextHasBeenCleared = false;
        gbl.DelayBetweenCharacters = true;

        ovr008.calc_group_movement(out init_min, out var_40A);

        ovr008.vm_LoadCmdSets(0x0e);

        gbl.sprite_block_id = (byte)ovr008.vm_GetCmdValue(1);
        gbl.area2_ptr.max_encounter_distance = ovr008.vm_GetCmdValue(2);
        gbl.pic_block_id = (byte)ovr008.vm_GetCmdValue(3);

        var_43D = gbl.cmd_opps[4].Word;

        for (int i = 0; i < 5; i++)
        {
            var_6[i] = (byte)ovr008.vm_GetCmdValue(i + 5);
        }

        for (int i = 0; i < 3; i++)
        {
            strings[i] = gbl.unk_1D972[i + 1];
        }

        var_407 = (byte)ovr008.vm_GetCmdValue(0x0d);
        var_408 = (byte)ovr008.vm_GetCmdValue(0x0e);

        gbl.area2_ptr.encounter_distance = ovr008.sub_304B4(gbl.mapDirection, gbl.mapPosY, gbl.mapPosX);

        if (gbl.area2_ptr.max_encounter_distance < gbl.area2_ptr.encounter_distance)
        {
            gbl.area2_ptr.encounter_distance = gbl.area2_ptr.max_encounter_distance;
        }

        ovr008.sub_30580(gbl.encounter_flags, gbl.area2_ptr.encounter_distance, gbl.pic_block_id, gbl.sprite_block_id);

        do
        {
            if (gbl.spriteChanged == false ||
                gbl.byte_1EE8D == false ||
                gbl.area_ptr.inDungeon == 0 ||
                gbl.lastDaxBlockId == 0x50)
            {
                useOverlay = false;
            }
            else
            {
                useOverlay = true;
            }

            clearTextArea = (gbl.area_ptr.inDungeon != 0);

            init_max = 0;
            gbl.textXCol = 1;
            gbl.textYCol = 0x11;

            switch (gbl.area2_ptr.encounter_distance)
            {
                case 0:
                    var_43B = 0;

                    do
                    {
                        text = strings[var_43B];
                        var_43B++;
                    } while (text.Length == 0 && var_43B < 3);
                    break;

                case 1:
                    var_43B = 1;

                    do
                    {
                        text = strings[var_43B];
                        var_43B++;

                        if (var_43B > 2)
                        {
                            var_43B = 0;
                        }
                    } while (text.Length == 0 && var_43B != 1);
                    break;

                case 2:
                    var_43B = 2;

                    do
                    {
                        text = strings[var_43B];

                        var_43B++;
                        if (var_43B > 2)
                        {
                            var_43B = 0;
                        }

                    } while (text.Length == 0 && var_43B != 2);
                    break;
            }

            if (text.Length == 0)
            {
                clearTextArea = false;
            }

            DisplayDriver.press_any_key(text, clearTextArea, 10, TextRegion.NormalBottom);

            if (gbl.area2_ptr.encounter_distance == 0 ||
                gbl.area_ptr.inDungeon == 0)
            {
                displayText = "~COMBAT ~WAIT ~FLEE ~PARLAY";
            }
            else
            {
                displayText = "~COMBAT ~WAIT ~FLEE ~ADVANCE";
            }

            menu_selected = ovr008.sub_317AA(useOverlay, false, gbl.defaultMenuColors, displayText, "");

            if (gbl.area2_ptr.encounter_distance == 0 ||
                gbl.area_ptr.inDungeon == 0)
            {
                if (menu_selected == 3)
                {
                    menu_selected = 4;
                }
            }

            var_43A = var_6[menu_selected];

            switch (var_43A)
            {
                case 0:
                    if (menu_selected != 2)
                    {
                        ovr008.vm_SetMemoryValue(1, var_43D);
                    }
                    else
                    {
                        if (init_min >= var_407)
                        {
                            ovr008.vm_SetMemoryValue(2, var_43D);
                        }
                        else
                        {
                            ovr008.vm_SetMemoryValue(1, var_43D);
                        }
                    }
                    break;

                case 1:
                    if (menu_selected == 0)
                    {
                        ovr008.vm_SetMemoryValue(1, var_43D);
                    }
                    else if (menu_selected == 1)
                    {
                        init_max = 1;
                        DisplayDriver.press_any_key("Both sides wait.", true, 10, TextRegion.NormalBottom);
                    }
                    else if (menu_selected == 2)
                    {
                        ovr008.vm_SetMemoryValue(2, var_43D);
                    }
                    else if (menu_selected == 3)
                    {
                        if (gbl.area2_ptr.encounter_distance != 0)
                        {
                            gbl.area2_ptr.encounter_distance--;

                            ovr008.sub_30580(gbl.encounter_flags, gbl.area2_ptr.encounter_distance, gbl.pic_block_id, gbl.sprite_block_id);
                        }
                        else
                        {
                            DisplayDriver.press_any_key("Both sides wait.", true, 10, TextRegion.NormalBottom);
                        }

                        init_max = 1;
                    }
                    else if (menu_selected == 4)
                    {
                        if (gbl.area2_ptr.encounter_distance > 0)
                        {
                            gbl.area2_ptr.encounter_distance--;
                            ovr008.sub_30580(gbl.encounter_flags, gbl.area2_ptr.encounter_distance, gbl.pic_block_id, gbl.sprite_block_id);
                            init_max = 1;
                        }
                        else
                        {
                            ovr008.vm_SetMemoryValue(3, var_43D);
                        }
                    }
                    break;

                case 2:
                    if (menu_selected == 0)
                    {
                        if (var_408 > var_40A)
                        {
                            ovr008.vm_SetMemoryValue(0, var_43D);

                            gbl.textXCol = 1;
                            gbl.textYCol = 0x11;
                            DisplayDriver.press_any_key("The monsters flee.", true, 10, TextRegion.NormalBottom);
                        }
                        else
                        {
                            ovr008.vm_SetMemoryValue(1, var_43D);
                        }
                    }
                    else if (menu_selected >= 1 && menu_selected <= 4)
                    {
                        ovr008.vm_SetMemoryValue(0, var_43D);

                        gbl.textXCol = 1;
                        gbl.textYCol = 0x11;
                        DisplayDriver.press_any_key("The monsters flee.", true, 10, TextRegion.NormalBottom);
                    }
                    break;

                case 3:
                    if (menu_selected == 0)
                    {
                        ovr008.vm_SetMemoryValue(1, var_43D);
                    }
                    else if (menu_selected == 1 || menu_selected == 3)
                    {
                        if (gbl.area2_ptr.encounter_distance != 0)
                        {
                            gbl.area2_ptr.encounter_distance--;

                            ovr008.sub_30580(gbl.encounter_flags, gbl.area2_ptr.encounter_distance, gbl.pic_block_id, gbl.sprite_block_id);
                        }
                        else
                        {
                            DisplayDriver.press_any_key("Both sides wait.", true, 10, TextRegion.NormalBottom);
                        }

                        init_max = 1;
                    }
                    else if (menu_selected == 2)
                    {
                        ovr008.vm_SetMemoryValue(2, var_43D);
                    }
                    else if (menu_selected == 4)
                    {
                        if (gbl.area2_ptr.encounter_distance <= 0)
                        {
                            ovr008.vm_SetMemoryValue(3, var_43D);
                        }
                        else
                        {
                            gbl.area2_ptr.encounter_distance--;

                            ovr008.sub_30580(gbl.encounter_flags, gbl.area2_ptr.encounter_distance, gbl.pic_block_id, gbl.sprite_block_id);
                            init_max = 1;
                        }
                    }
                    break;

                case 4:
                    if (menu_selected == 0)
                    {
                        ovr008.vm_SetMemoryValue(1, var_43D);
                    }
                    else if (menu_selected == 1 || menu_selected == 3 || menu_selected == 4)
                    {

                        if (gbl.area2_ptr.encounter_distance <= 0)
                        {
                            ovr008.vm_SetMemoryValue(3, var_43D);
                        }
                        else
                        {
                            gbl.area2_ptr.encounter_distance -= 1;

                            ovr008.sub_30580(gbl.encounter_flags, gbl.area2_ptr.encounter_distance, gbl.pic_block_id, gbl.sprite_block_id);
                            init_max = 1;
                        }
                    }
                    else if (menu_selected == 2)
                    {
                        ovr008.vm_SetMemoryValue(2, var_43D);
                    }

                    break;
            }
        } while (init_max != 0);

        ovr027.ClearPromptArea();
        gbl.DelayBetweenCharacters = false;
        gbl.byte_1EE95 = false;
    }
}