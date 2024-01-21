using System.Collections.Generic;
using ForgottenRealms.Engine.Classes;
using ForgottenRealms.Engine.CommandsFeature;
using ForgottenRealms.Engine.Logging;

namespace ForgottenRealms.Engine;

internal class ovr003
{
    private static Dictionary<int, CmdItem> CommandTable = new Dictionary<int, CmdItem>();

    public static void SetupCommandTable()
    {
        CommandTable.Add(0x00, new CmdItem(0, "EXIT", new ExitCommand()));
        CommandTable.Add(0x01, new CmdItem(1, "GOTO", new GotoCommand()));
        CommandTable.Add(0x02, new CmdItem(1, "GOSUB", new GotoSubCommand()));
        CommandTable.Add(0x03, new CmdItem(2, "COMPARE", new CompareCommand()));
        CommandTable.Add(0x04, new CmdItem(3, "ADD", new AddSubDivMultiCommand()));
        CommandTable.Add(0x05, new CmdItem(3, "SUBTRACT", new AddSubDivMultiCommand()));
        CommandTable.Add(0x06, new CmdItem(3, "DIVIDE", new AddSubDivMultiCommand()));
        CommandTable.Add(0x07, new CmdItem(3, "MULTIPLY", new AddSubDivMultiCommand()));
        CommandTable.Add(0x08, new CmdItem(2, "RANDOM", new RandomCommand()));
        CommandTable.Add(0x09, new CmdItem(2, "SAVE", new SaveCommand()));
        CommandTable.Add(0x0A, new CmdItem(1, "LOAD CHARACTER", new LoadCharacterCommand()));
        CommandTable.Add(0x0B, new CmdItem(3, "LOAD MONSTER", new LoadMonsterCommand()));
        CommandTable.Add(0x0C, new CmdItem(3, "SETUP MONSTER", new SetupMonsterCommand()));
        CommandTable.Add(0x0D, new CmdItem(0, "APPROACH", new ApproachCommand()));
        CommandTable.Add(0x0E, new CmdItem(1, "PICTURE", new PictureCommand()));
        CommandTable.Add(0x0F, new CmdItem(2, "INPUT NUMBER", new InputNumberCommand()));
        CommandTable.Add(0x10, new CmdItem(2, "INPUT STRING", new InputStringCommand()));
        CommandTable.Add(0x11, new CmdItem(1, "PRINT", new PrintCommand()));
        CommandTable.Add(0x12, new CmdItem(1, "PRINTCLEAR", new PrintCommand()));
        CommandTable.Add(0x13, new CmdItem(0, "RETURN", new ReturnCommand()));
        CommandTable.Add(0x14, new CmdItem(4, "COMPARE AND", new CompareAndCommand()));
        CommandTable.Add(0x15, new CmdItem(0, "VERTICAL MENU", new VerticalMenuCommand()));
        CommandTable.Add(0x16, new CmdItem(0, "IF =", new IfCommand(CommandTable)));
        CommandTable.Add(0x17, new CmdItem(0, "IF <>", new IfCommand(CommandTable)));
        CommandTable.Add(0x18, new CmdItem(0, "IF <", new IfCommand(CommandTable)));
        CommandTable.Add(0x19, new CmdItem(0, "IF >", new IfCommand(CommandTable)));
        CommandTable.Add(0x1A, new CmdItem(0, "IF <=", new IfCommand(CommandTable)));
        CommandTable.Add(0x1B, new CmdItem(0, "IF >=", new IfCommand(CommandTable)));
        CommandTable.Add(0x1C, new CmdItem(0, "CLEARMONSTERS", new ClearMonstersCommand()));
        CommandTable.Add(0x1D, new CmdItem(1, "PARTYSTRENGTH", new PartyStrengthCommand()));
        CommandTable.Add(0x1E, new CmdItem(6, "CHECKPARTY", new CheckPartyCommand()));
        CommandTable.Add(0x1F, new CmdItem(2, "notsure 0x1f", null));
        CommandTable.Add(0x20, new CmdItem(1, "NEWECL", new NewECLCommand()));
        CommandTable.Add(0x21, new CmdItem(3, "LOAD FILES", new LoadFilesCommand()));
        CommandTable.Add(0x22, new CmdItem(2, "PARTY SURPRISE", new PartySurpriseCommand()));
        CommandTable.Add(0x23, new CmdItem(4, "SURPRISE", new SurpriseCommand()));
        CommandTable.Add(0x24, new CmdItem(0, "COMBAT", new CombatCommand()));
        CommandTable.Add(0x25, new CmdItem(0, "ON GOTO", new OnGotoGoSubCommand()));
        CommandTable.Add(0x26, new CmdItem(0, "ON GOSUB", new OnGotoGoSubCommand()));
        CommandTable.Add(0x27, new CmdItem(8, "TREASURE", new TreasureCommand()));
        CommandTable.Add(0x28, new CmdItem(3, "ROB", new RobCommand()));
        CommandTable.Add(0x29, new CmdItem(14, "ENCOUNTER MENU", new EncounterMenuCommand()));
        CommandTable.Add(0x2A, new CmdItem(3, "GETTABLE", new GetTableCommand()));
        CommandTable.Add(0x2B, new CmdItem(0, "HORIZONTAL MENU", new HorizontalMenuCommand()));
        CommandTable.Add(0x2C, new CmdItem(6, "PARLAY", new ParlayCommand()));
        CommandTable.Add(0x2D, new CmdItem(1, "CALL", new CallCommand()));
        CommandTable.Add(0x2E, new CmdItem(5, "DAMAGE", new DamageCommand()));
        CommandTable.Add(0x2F, new CmdItem(3, "AND", new AndOrCommand()));
        CommandTable.Add(0x30, new CmdItem(3, "OR", new AndOrCommand()));
        CommandTable.Add(0x31, new CmdItem(0, "SPRITE OFF", new SpriteOffCommand()));
        CommandTable.Add(0x32, new CmdItem(1, "FIND ITEM", new FindItemCommand()));
        CommandTable.Add(0x33, new CmdItem(0, "PRINT RETURN", new PrintReturnCommand()));
        CommandTable.Add(0x34, new CmdItem(1, "ECL CLOCK", new EclClockCommand()));
        CommandTable.Add(0x35, new CmdItem(3, "SAVE TABLE", new SaveTableCommand()));
        CommandTable.Add(0x36, new CmdItem(1, "ADD NPC", new AddNPCCommand()));
        CommandTable.Add(0x37, new CmdItem(3, "LOAD PIECES", new LoadFilesCommand()));
        CommandTable.Add(0x38, new CmdItem(1, "PROGRAM", new ProgramCommand()));
        CommandTable.Add(0x39, new CmdItem(1, "WHO", new WhoCommand()));
        CommandTable.Add(0x3A, new CmdItem(0, "DELAY", new DelayCommand()));
        CommandTable.Add(0x3B, new CmdItem(3, "SPELL", new SpellCommand()));
        CommandTable.Add(0x3C, new CmdItem(1, "PROTECTION", new ProtectionCommand()));
        CommandTable.Add(0x3D, new CmdItem(0, "CLEAR BOX", new ClearBoxCommand()));
        CommandTable.Add(0x3E, new CmdItem(0, "DUMP", new DumpCommand()));
        CommandTable.Add(0x3F, new CmdItem(1, "FIND SPECIAL", new FindSpecialCommand()));
        CommandTable.Add(0x40, new CmdItem(1, "DESTROY ITEMS", new DestroyItemsCommand()));
    }

    internal static void sub_29758()
    {
        gbl.LastSelectedPlayer = gbl.SelectedPlayer;

        gbl.can_draw_bigpic = true;
        gbl.byte_1AB0C = false;
        gbl.filesLoaded = false;
        gbl.restore_player_ptr = false;
        gbl.byte_1AB0B = false;
        gbl.byte_1EE98 = true;
        gbl.game_state = GameState.DungeonMap;
        gbl.vmFlag01 = false;

        if (gbl.area_ptr.LastEclBlockId == 0)
        {
            gbl.byte_1EE98 = false;

            if (gbl.inDemo == true)
            {
                gbl.EclBlockId = 0x52;
            }
            else
            {
                gbl.EclBlockId = 1;

                ovr025.PartySummary(gbl.SelectedPlayer);
            }
        }
        else
        {
            gbl.EclBlockId = (byte)(gbl.area_ptr.LastEclBlockId);
        }

        if (gbl.area_ptr.inDungeon == 0)
        {
            gbl.game_state = GameState.WildernessMap;
        }

        if (gbl.reload_ecl_and_pictures == true ||
            gbl.area_ptr.LastEclBlockId == 0)
        {
            ovr008.load_ecl_dax(gbl.EclBlockId);
        }
        else
        {
            gbl.byte_1AB0B = true;
        }

        ovr008.vm_init_ecl();

        RunEclVm(gbl.ecl_initial_entryPoint);

        if (gbl.inDemo == true)
        {
            while (gbl.TeamList.Count > 0)
            {
                ovr018.FreeCurrentPlayer(gbl.TeamList[0], true, true);
            }
            gbl.SelectedPlayer = null;
        }
        else
        {
            if (gbl.vmFlag01 == false)
            {
                gbl.area_ptr.LastEclBlockId = gbl.EclBlockId;
            }
            else
            {
                sub_29677();
            }

            if (gbl.game_state != GameState.WildernessMap &&
                gbl.reload_ecl_and_pictures == true)
            {
                if (gbl.byte_1EE98 == true)
                {
                    ovr025.LoadPic();
                }

                gbl.can_draw_bigpic = true;
                ovr029.RedrawView();
            }

            gbl.reload_ecl_and_pictures = false;

            do
            {
                char var_1 = ovr015.main_3d_world_menu();

                gbl.LastSelectedPlayer = gbl.SelectedPlayer;

                if (gbl.vmFlag01 == false)
                {
                    gbl.area_ptr.LastEclBlockId = gbl.EclBlockId;
                }

                while ((gbl.area2_ptr.search_flags > 1 || char.ToUpper(var_1) == 'E') &&
                       gbl.party_killed == false)
                {
                    if (char.ToUpper(var_1) == 'E')
                    {
                        TryEncamp();
                    }
                    else
                    {
                        gbl.search_flag_bkup = gbl.area2_ptr.search_flags & 1;
                        gbl.area2_ptr.search_flags = 1;
                        gbl.can_draw_bigpic = true;
                        ovr029.RedrawView();

                        RunEclVm(gbl.SearchLocationAddr);

                        if (gbl.vmFlag01 == true)
                        {
                            sub_29677();
                        }

                        gbl.area2_ptr.search_flags = (ushort)gbl.search_flag_bkup;
                    }

                    if (gbl.party_killed == false)
                    {
                        var_1 = ovr015.main_3d_world_menu();
                        gbl.LastSelectedPlayer = gbl.SelectedPlayer;
                    }
                }


                if (gbl.party_killed == false)
                {
                    RunEclVm(gbl.vm_run_addr_1);
                }

                if (gbl.vmFlag01 == true)
                {
                    sub_29677();
                }
                else
                {
                    if (gbl.party_killed == false)
                    {
                        gbl.area_ptr.lastXPos = (short)gbl.mapPosX;
                        gbl.area_ptr.lastYPos = (short)gbl.mapPosY;

                        ovr015.locked_door();
                        ovr029.RedrawView();

                        if (gbl.area_ptr.lastXPos != gbl.mapPosX ||
                            gbl.area_ptr.lastYPos != gbl.mapPosY)
                        {
                            new SoundDriver().PlaySound(Sound.sound_a);
                        }

                        gbl.spriteChanged = false;
                        gbl.byte_1EE8D = true;
                        RunEclVm(gbl.SearchLocationAddr);
                        if (gbl.vmFlag01 == true)
                        {
                            sub_29677();
                        }
                    }
                }
            } while (gbl.party_killed == false);

            gbl.party_killed = false;
        }
    }

    internal static void TryEncamp()
    {
        RunEclVm(gbl.PreCampCheckAddr);

        if (ovr016.MakeCamp() == true)
        {
            ovr025.LoadPic();
            RunEclVm(gbl.CampInterruptedAddr);
        }

        gbl.can_draw_bigpic = true;
        ovr029.RedrawView();
        gbl.gameSaved = false;
    }


    private static void RunEclVm(ushort offset) // sub_29607
    {
        gbl.ecl_offset = offset;
        gbl.stopVM = false;

        //System.Console.Out.WriteLine("RunEclVm {0,4:X} start", offset);

        while (gbl.stopVM == false &&
               gbl.party_killed == false)
        {
            gbl.command = gbl.ecl_ptr[gbl.ecl_offset + 0x8000];

            VmLog.Write("0x{0:X} ", gbl.ecl_offset);

            CmdItem cmd;
            if (CommandTable.TryGetValue(gbl.command, out cmd))
            {
                if (gbl.printCommands)
                {
                    Logger.Debug("{0} 0x{1:X}", cmd.Name(), gbl.command);
                }
                cmd.Run();
            }
            else
            {
                Logger.Log("Unknown command id {0}", gbl.command);
            }
        }

        gbl.stopVM = false;
    }


    private static void sub_29677()
    {
        do
        {
            ovr030.DaxArrayFreeDaxBlocks(gbl.byte_1D556);
            gbl.byte_1D5AB = string.Empty;
            gbl.byte_1D5B5 = 0x0FF;
            gbl.vmFlag01 = false;
            gbl.mapWallRoof = ovr031.get_wall_x2(gbl.mapPosY, gbl.mapPosX);

            gbl.area2_ptr.tried_to_exit_map = false;

            gbl.LastSelectedPlayer = gbl.SelectedPlayer;

            RunEclVm(gbl.ecl_initial_entryPoint);

            if (gbl.vmFlag01 == false)
            {
                gbl.area_ptr.LastEclBlockId = gbl.EclBlockId;
            }

            if (gbl.vmFlag01 == false)
            {
                if (((gbl.last_game_state != GameState.DungeonMap || gbl.game_state == GameState.DungeonMap) && gbl.byte_1AB0B == true) ||
                    (gbl.last_game_state == GameState.DungeonMap && gbl.game_state == GameState.DungeonMap))
                {
                    ovr029.RedrawView();
                }
                gbl.vmFlag01 = false;

                RunEclVm(gbl.vm_run_addr_1);

                if (gbl.vmFlag01 == false)
                {
                    RunEclVm(gbl.SearchLocationAddr);

                    if (gbl.vmFlag01 == false)
                    {
                        gbl.SelectedPlayer = gbl.LastSelectedPlayer;
                        ovr025.PartySummary(gbl.SelectedPlayer);
                    }
                }

            }
        } while (gbl.vmFlag01 == true);

        gbl.last_game_state = gbl.game_state;
    }
}
