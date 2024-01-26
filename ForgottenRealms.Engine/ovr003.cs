using System;
using System.Collections.Generic;
using ForgottenRealms.Engine.Classes;
using ForgottenRealms.Engine.CommandsFeature;
using ForgottenRealms.Engine.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace ForgottenRealms.Engine;

public class ovr003
{
    private static Dictionary<int, CmdItem> CommandTable = new Dictionary<int, CmdItem>();
    private readonly SoundDriver _soundDriver;
    private readonly ovr008 _ovr008;
    private readonly ovr015 _ovr015;
    private readonly ovr016 _ovr016;
    private readonly ovr018 _ovr018;
    private readonly ovr025 _ovr025;
    private readonly ovr029 _ovr029;
    private readonly ovr030 _ovr030;
    private readonly ovr031 _ovr031;
    private readonly IServiceProvider _serviceProvider;


    public ovr003(SoundDriver soundDriver, ovr008 ovr008, ovr015 ovr015, ovr016 ovr016,
        ovr018 ovr018, ovr025 ovr025, ovr029 ovr029, ovr030 ovr030, ovr031 ovr031,
        IServiceProvider serviceProvider)
    {
        _soundDriver = soundDriver;
        _ovr008 = ovr008;
        _ovr015 = ovr015;
        _ovr016 = ovr016;
        _ovr018 = ovr018;
        _ovr025 = ovr025;
        _ovr029 = ovr029;
        _ovr030 = ovr030;
        _ovr031 = ovr031;
        _serviceProvider = serviceProvider;
    }

    public void SetupCommandTable()
    {
        CommandTable.Add(0x00, new CmdItem(0, "EXIT", _serviceProvider.GetService<ExitCommand>(), _ovr008));
        CommandTable.Add(0x01, new CmdItem(1, "GOTO", _serviceProvider.GetService<GotoCommand>(), _ovr008));
        CommandTable.Add(0x02, new CmdItem(1, "GOSUB", _serviceProvider.GetService<GotoSubCommand>(), _ovr008));
        CommandTable.Add(0x03, new CmdItem(2, "COMPARE", _serviceProvider.GetService<CompareCommand>(), _ovr008));
        CommandTable.Add(0x04, new CmdItem(3, "ADD", _serviceProvider.GetService<AddSubDivMultiCommand>(), _ovr008));
        CommandTable.Add(0x05, new CmdItem(3, "SUBTRACT", _serviceProvider.GetService<AddSubDivMultiCommand>(), _ovr008));
        CommandTable.Add(0x06, new CmdItem(3, "DIVIDE", _serviceProvider.GetService<AddSubDivMultiCommand>(), _ovr008));
        CommandTable.Add(0x07, new CmdItem(3, "MULTIPLY", _serviceProvider.GetService<AddSubDivMultiCommand>(), _ovr008));
        CommandTable.Add(0x08, new CmdItem(2, "RANDOM", _serviceProvider.GetService<RandomCommand>(), _ovr008));
        CommandTable.Add(0x09, new CmdItem(2, "SAVE", _serviceProvider.GetService<SaveCommand>(), _ovr008));
        CommandTable.Add(0x0A, new CmdItem(1, "LOAD CHARACTER", _serviceProvider.GetService<LoadCharacterCommand>(), _ovr008));
        CommandTable.Add(0x0B, new CmdItem(3, "LOAD MONSTER", _serviceProvider.GetService<LoadMonsterCommand>(), _ovr008));
        CommandTable.Add(0x0C, new CmdItem(3, "SETUP MONSTER", _serviceProvider.GetService<SetupMonsterCommand>(), _ovr008));
        CommandTable.Add(0x0D, new CmdItem(0, "APPROACH", _serviceProvider.GetService<ApproachCommand>(), _ovr008));
        CommandTable.Add(0x0E, new CmdItem(1, "PICTURE", _serviceProvider.GetService<PictureCommand>(), _ovr008));
        CommandTable.Add(0x0F, new CmdItem(2, "INPUT NUMBER", _serviceProvider.GetService<InputNumberCommand>(), _ovr008));
        CommandTable.Add(0x10, new CmdItem(2, "INPUT STRING", _serviceProvider.GetService<InputStringCommand>(), _ovr008));
        CommandTable.Add(0x11, new CmdItem(1, "PRINT", _serviceProvider.GetService<PrintCommand>(), _ovr008));
        CommandTable.Add(0x12, new CmdItem(1, "PRINTCLEAR", _serviceProvider.GetService<PrintCommand>(), _ovr008));
        CommandTable.Add(0x13, new CmdItem(0, "RETURN", _serviceProvider.GetService<ReturnCommand>(), _ovr008));
        CommandTable.Add(0x14, new CmdItem(4, "COMPARE AND", _serviceProvider.GetService<CompareAndCommand>(), _ovr008));
        CommandTable.Add(0x15, new CmdItem(0, "VERTICAL MENU", _serviceProvider.GetService<VerticalMenuCommand>(), _ovr008));
        CommandTable.Add(0x16, new CmdItem(0, "IF =", new IfCommand(CommandTable), _ovr008));
        CommandTable.Add(0x17, new CmdItem(0, "IF <>", new IfCommand(CommandTable), _ovr008));
        CommandTable.Add(0x18, new CmdItem(0, "IF <", new IfCommand(CommandTable), _ovr008));
        CommandTable.Add(0x19, new CmdItem(0, "IF >", new IfCommand(CommandTable), _ovr008));
        CommandTable.Add(0x1A, new CmdItem(0, "IF <=", new IfCommand(CommandTable), _ovr008));
        CommandTable.Add(0x1B, new CmdItem(0, "IF >=", new IfCommand(CommandTable), _ovr008));
        CommandTable.Add(0x1C, new CmdItem(0, "CLEARMONSTERS", _serviceProvider.GetService<ClearMonstersCommand>(), _ovr008));
        CommandTable.Add(0x1D, new CmdItem(1, "PARTYSTRENGTH", _serviceProvider.GetService<PartyStrengthCommand>(), _ovr008));
        CommandTable.Add(0x1E, new CmdItem(6, "CHECKPARTY", _serviceProvider.GetService<CheckPartyCommand>(), _ovr008));
        CommandTable.Add(0x1F, new CmdItem(2, "notsure 0x1f", _serviceProvider.GetService<NullGameCommand>(), _ovr008));
        CommandTable.Add(0x20, new CmdItem(1, "NEWECL", _serviceProvider.GetService<NewECLCommand>(), _ovr008));
        CommandTable.Add(0x21, new CmdItem(3, "LOAD FILES", _serviceProvider.GetService<LoadFilesCommand>(), _ovr008));
        CommandTable.Add(0x22, new CmdItem(2, "PARTY SURPRISE", _serviceProvider.GetService<PartySurpriseCommand>(), _ovr008));
        CommandTable.Add(0x23, new CmdItem(4, "SURPRISE", _serviceProvider.GetService<SurpriseCommand>(), _ovr008));
        CommandTable.Add(0x24, new CmdItem(0, "COMBAT", _serviceProvider.GetService<CombatCommand>(), _ovr008));
        CommandTable.Add(0x25, new CmdItem(0, "ON GOTO", _serviceProvider.GetService<OnGotoGoSubCommand>(), _ovr008));
        CommandTable.Add(0x26, new CmdItem(0, "ON GOSUB", _serviceProvider.GetService<OnGotoGoSubCommand>(), _ovr008));
        CommandTable.Add(0x27, new CmdItem(8, "TREASURE", _serviceProvider.GetService<TreasureCommand>(), _ovr008));
        CommandTable.Add(0x28, new CmdItem(3, "ROB", _serviceProvider.GetService<RobCommand>(), _ovr008));
        CommandTable.Add(0x29, new CmdItem(14, "ENCOUNTER MENU", _serviceProvider.GetService<EncounterMenuCommand>(), _ovr008));
        CommandTable.Add(0x2A, new CmdItem(3, "GETTABLE", _serviceProvider.GetService<GetTableCommand>(), _ovr008));
        CommandTable.Add(0x2B, new CmdItem(0, "HORIZONTAL MENU", _serviceProvider.GetService<HorizontalMenuCommand>(), _ovr008));
        CommandTable.Add(0x2C, new CmdItem(6, "PARLAY", _serviceProvider.GetService<ParlayCommand>(), _ovr008));
        CommandTable.Add(0x2D, new CmdItem(1, "CALL", _serviceProvider.GetService<CallCommand>(), _ovr008));
        CommandTable.Add(0x2E, new CmdItem(5, "DAMAGE", _serviceProvider.GetService<DamageCommand>(), _ovr008));
        CommandTable.Add(0x2F, new CmdItem(3, "AND", _serviceProvider.GetService<AndOrCommand>(), _ovr008));
        CommandTable.Add(0x30, new CmdItem(3, "OR", _serviceProvider.GetService<AndOrCommand>(), _ovr008));
        CommandTable.Add(0x31, new CmdItem(0, "SPRITE OFF", _serviceProvider.GetService<SpriteOffCommand>(), _ovr008));
        CommandTable.Add(0x32, new CmdItem(1, "FIND ITEM", _serviceProvider.GetService<FindItemCommand>(), _ovr008));
        CommandTable.Add(0x33, new CmdItem(0, "PRINT RETURN", _serviceProvider.GetService<PrintReturnCommand>(), _ovr008));
        CommandTable.Add(0x34, new CmdItem(1, "ECL CLOCK", _serviceProvider.GetService<EclClockCommand>(), _ovr008));
        CommandTable.Add(0x35, new CmdItem(3, "SAVE TABLE", _serviceProvider.GetService<SaveTableCommand>(), _ovr008));
        CommandTable.Add(0x36, new CmdItem(1, "ADD NPC", _serviceProvider.GetService<AddNPCCommand>(), _ovr008));
        CommandTable.Add(0x37, new CmdItem(3, "LOAD PIECES", _serviceProvider.GetService<LoadFilesCommand>(), _ovr008));
        CommandTable.Add(0x38, new CmdItem(1, "PROGRAM", _serviceProvider.GetService<ProgramCommand>(), _ovr008));
        CommandTable.Add(0x39, new CmdItem(1, "WHO", _serviceProvider.GetService<WhoCommand>(), _ovr008));
        CommandTable.Add(0x3A, new CmdItem(0, "DELAY", _serviceProvider.GetService<DelayCommand>(), _ovr008));
        CommandTable.Add(0x3B, new CmdItem(3, "SPELL", _serviceProvider.GetService<SpellCommand>(), _ovr008));
        CommandTable.Add(0x3C, new CmdItem(1, "PROTECTION", _serviceProvider.GetService<ProtectionCommand>(), _ovr008));
        CommandTable.Add(0x3D, new CmdItem(0, "CLEAR BOX", _serviceProvider.GetService<ClearBoxCommand>(), _ovr008));
        CommandTable.Add(0x3E, new CmdItem(0, "DUMP", _serviceProvider.GetService<DumpCommand>(), _ovr008));
        CommandTable.Add(0x3F, new CmdItem(1, "FIND SPECIAL", _serviceProvider.GetService<FindSpecialCommand>(), _ovr008));
        CommandTable.Add(0x40, new CmdItem(1, "DESTROY ITEMS", _serviceProvider.GetService<DestroyItemsCommand>(), _ovr008));
    }

    internal void sub_29758()
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

                _ovr025.PartySummary(gbl.SelectedPlayer);
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
            _ovr008.load_ecl_dax(gbl.EclBlockId);
        }
        else
        {
            gbl.byte_1AB0B = true;
        }

        _ovr008.vm_init_ecl();

        RunEclVm(gbl.ecl_initial_entryPoint);

        if (gbl.inDemo == true)
        {
            while (gbl.TeamList.Count > 0)
            {
                _ovr018.FreeCurrentPlayer(gbl.TeamList[0], true, true);
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
                    _ovr025.LoadPic();
                }

                gbl.can_draw_bigpic = true;
                _ovr029.RedrawView();
            }

            gbl.reload_ecl_and_pictures = false;

            do
            {
                char var_1 = _ovr015.main_3d_world_menu();

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
                        _ovr029.RedrawView();

                        RunEclVm(gbl.SearchLocationAddr);

                        if (gbl.vmFlag01 == true)
                        {
                            sub_29677();
                        }

                        gbl.area2_ptr.search_flags = (ushort)gbl.search_flag_bkup;
                    }

                    if (gbl.party_killed == false)
                    {
                        var_1 = _ovr015.main_3d_world_menu();
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

                        _ovr015.locked_door();
                        _ovr029.RedrawView();

                        if (gbl.area_ptr.lastXPos != gbl.mapPosX ||
                            gbl.area_ptr.lastYPos != gbl.mapPosY)
                        {
                            _soundDriver.PlaySound(Sound.sound_a);
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

    internal void TryEncamp()
    {
        RunEclVm(gbl.PreCampCheckAddr);

        if (_ovr016.MakeCamp() == true)
        {
            _ovr025.LoadPic();
            RunEclVm(gbl.CampInterruptedAddr);
        }

        gbl.can_draw_bigpic = true;
        _ovr029.RedrawView();
        gbl.gameSaved = false;
    }


    private void RunEclVm(ushort offset) // sub_29607
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


    private void sub_29677()
    {
        do
        {
            _ovr030.DaxArrayFreeDaxBlocks(gbl.byte_1D556);
            gbl.byte_1D5AB = string.Empty;
            gbl.byte_1D5B5 = 0x0FF;
            gbl.vmFlag01 = false;
            gbl.mapWallRoof = _ovr031.get_wall_x2(gbl.mapPosY, gbl.mapPosX);

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
                    _ovr029.RedrawView();
                }
                gbl.vmFlag01 = false;

                RunEclVm(gbl.vm_run_addr_1);

                if (gbl.vmFlag01 == false)
                {
                    RunEclVm(gbl.SearchLocationAddr);

                    if (gbl.vmFlag01 == false)
                    {
                        gbl.SelectedPlayer = gbl.LastSelectedPlayer;
                        _ovr025.PartySummary(gbl.SelectedPlayer);
                    }
                }

            }
        } while (gbl.vmFlag01 == true);

        gbl.last_game_state = gbl.game_state;
    }
}
