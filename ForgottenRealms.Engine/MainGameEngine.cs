using System.Threading;
using ForgottenRealms.Engine.Classes;
using ForgottenRealms.Engine.Classes.Combat;
using ForgottenRealms.Engine.Classes.DaxFiles;
using ForgottenRealms.Engine.Logging;
using Microsoft.Extensions.Logging;

namespace ForgottenRealms.Engine;

public class MainGameEngine
{
    private readonly CancellationTokenSource _cancellationTokenSource;
    private readonly TitleScreenAction _titleScreenAction;
    private readonly DaxBlockReader _daxBlockReader;
    private readonly SoundDriver _soundDriver;
    private readonly KeyboardService _keyboardService;
    private readonly DisplayDriver _displayDriver;
    private readonly ovr003 _ovr003;
    private readonly ovr004 _ovr004;
    private readonly ovr008 _ovr008;
    private readonly ovr013 _ovr013;
    private readonly ovr016 _ovr016;
    private readonly ovr018 _ovr018;
    private readonly ovr023 _ovr023;
    private readonly ovr027 _ovr027;
    private readonly ovr034 _ovr034;
    private readonly ovr038 _ovr038;
    private readonly seg051 _seg051;
    private readonly ILogger<MainGameEngine> _logger;

    public MainGameEngine(CancellationTokenSource cancellationTokenSource, TitleScreenAction titleScreenAction, DaxBlockReader daxBlockReader,
        SoundDriver soundDriver, KeyboardService keyboardService, DisplayDriver displayDriver,
        ovr003 ovr003, ovr004 ovr004, ovr008 ovr008,
        ovr013 ovr013, ovr016 ovr016, ovr018 ovr018, ovr023 ovr023, ovr027 ovr027, ovr034 ovr034,
        ovr038 ovr038, seg051 seg051, ILogger<MainGameEngine> logger)
    {
        _cancellationTokenSource = cancellationTokenSource;
        _titleScreenAction = titleScreenAction;
        _daxBlockReader = daxBlockReader;
        _soundDriver = soundDriver;
        _keyboardService = keyboardService;
        _displayDriver = displayDriver;
        _ovr003 = ovr003;
        _ovr004 = ovr004;
        _ovr008 = ovr008;
        _ovr013 = ovr013;
        _ovr016 = ovr016;
        _ovr018 = ovr018;
        _ovr023 = ovr023;
        _ovr027 = ovr027;
        _ovr034 = ovr034;
        _ovr038 = ovr038;
        _seg051 = seg051;
        _logger = logger;
        gbl.exe_path = System.IO.Directory.GetCurrentDirectory();
    }

    public void EngineStop()
    {
        if (!_cancellationTokenSource.IsCancellationRequested)
        {
            _soundDriver.PlaySound(Sound.sound_FF);
            Logger.Close();
            ItemLibrary.Write();
            _cancellationTokenSource.Cancel();
        }
    }

    public void PROGRAM()
    {
        /* Memory Init - Start */
        gbl.CombatMap = new CombatantMap[gbl.MaxCombatantCount + 1]; /* God damm 1-n arrays */
        for (int i = 0; i <= gbl.MaxCombatantCount; i++)
        {
            gbl.CombatMap[i] = new CombatantMap();
        }
        /* Memory Init - End */

        _ovr003.SetupCommandTable();

        InitFirst();

        ItemLibrary.Read();

        _soundDriver.PlaySound(Sound.sound_0);

        //Logging.Logger.Debug("Field_6 & 0x0F == 0");
        //foreach (var s in gbl.spellCastingTable )
        //{
        //    if (s != null && (s.field_6 & 0x0f) == 0)
        //    {
        //        Logging.Logger.Debug("{0} {1}", s.spellIdx, (Spells)s.spellIdx);
        //    }
        //}
        //Logging.Logger.Debug("");
        //Logging.Logger.Debug("Field_6 & 0x0F == 5");
        //foreach (var s in gbl.spellCastingTable)
        //{
        //    if (s != null && (s.field_6 & 0x0f) == 5)
        //    {
        //        Logging.Logger.Debug("{0} {1}", s.spellIdx, (Spells)s.spellIdx);
        //    }
        //}
        //Logging.Logger.Debug("");
        //Logging.Logger.Debug("Field_6 & 0x0F == 15");
        //foreach (var s in gbl.spellCastingTable)
        //{
        //    if (s != null && (s.field_6 & 0x0f) == 15)
        //    {
        //        Logging.Logger.Debug("{0} {1}", s.spellIdx, (Spells)s.spellIdx);
        //    }
        //}
        //Logging.Logger.Debug("");
        //Logging.Logger.Debug("Field_6 & 0x0F >= 8 <= 14");
        //foreach (var s in gbl.spellCastingTable)
        //{
        //    if (s != null)
        //    {
        //        int v = s.field_6 & 0x0f;
        //        if (v >= 8 && v <= 14)
        //        {
        //            Logging.Logger.Debug("{0} {1}", s.spellIdx, (Spells)s.spellIdx);

        //        }
        //    }
        //}
        //Logging.Logger.Debug("");
        //Logging.Logger.Debug("Field_6 & 0x0F otherwise");
        //foreach (var s in gbl.spellCastingTable)
        //{
        //    if (s != null)
        //    {
        //        int v = s.field_6 & 0x0f;
        //        if (v >= 1 && v <= 7 && v != 5)
        //        {
        //            Logging.Logger.Debug("{0} {1} {2}", s.spellIdx, (Spells)s.spellIdx, (v & 3)+1);
        //        }
        //    }
        //}
        //Logging.Logger.Debug("");



        if (Cheats.skip_title_screen == false)
        {
            _titleScreenAction.ShowTitleScreen();
        }

        gbl.displayInputSecondsToWait = 30;
        gbl.displayInputTimeoutValue = 'D';

        char inputKey = _ovr027.displayInput(false, 0, gbl.defaultMenuColors, "Play Demo", "Curse of the Azure Bonds v1.3 ");

        gbl.displayInputSecondsToWait = 0;
        gbl.displayInputTimeoutValue = '\0';

        if (inputKey == 'D')
        {
            gbl.inDemo = true;
        }

        if (Cheats.skip_copy_protection == false &&
            gbl.inDemo == false)
        {
            _ovr004.copy_protection();
        }

        while (true)
        {
            if (gbl.inDemo == true)
            {
                gbl.game_area = 1;
                gbl.game_speed_var = 9;
            }
            else
            {
                gbl.game_area = 2;
            }

            if (gbl.inDemo == false)
            {
                _ovr018.StartGameMenu();
            }

            _ovr003.sub_29758();

            InitAgain();

            if (gbl.inDemo == true)
            {
                _titleScreenAction.ShowTitleScreen();
                _keyboardService.clear_keyboard();

                gbl.displayInputSecondsToWait = 10;
                gbl.displayInputTimeoutValue = 'D';

                inputKey = _ovr027.displayInput(false, 0, gbl.defaultMenuColors, "Play Demo", "Curse of the Azure Bonds v1.3 ");

                gbl.displayInputSecondsToWait = 0;
                gbl.displayInputTimeoutValue = '\0';

                gbl.inDemo = (inputKey == 'D');

                if (Cheats.skip_copy_protection == false &&
                    gbl.inDemo == false)
                {
                    _ovr004.copy_protection();
                }

                _soundDriver.PlaySound(Sound.sound_0);
            }
        }
    }

    private void InitFirst() /* sub_39054 */
    {
        _seg051.Randomize();

        gbl.area_ptr = new Area1();
        gbl.area2_ptr = new Area2();
        gbl.stru_1B2CA = new Struct_1B2CA();
        gbl.ecl_ptr = new EclBlock();
        gbl.dax_8x8d1_201 = new byte[177, 8];
        gbl.geo_ptr.LoadData(new byte[0x402]);

        _ovr016.BuildEffectNameMap();

        for (int i = 0; i < gbl.cmdOppsLimit; i++)
        {
            gbl.cmd_opps[i] = new Opperation();
            gbl.cmd_opps[i].getMemoryValue = _ovr008.vm_GetMemoryValue;
        }

        gbl.cursor_bkup = new DaxBlock(0, 1, 1, 8);
        gbl.cursor = new DaxBlock(0, 1, 1, 8);

        _seg051.FillChar(0xf, gbl.cursor.bpp, gbl.cursor.data);

        gbl.dax24x24Set = null;
        gbl.dword_1C8FC = null;

        gbl.dax24x24Set = new DaxBlock(0, 0x30, 3, 0x18);

        gbl.area_ptr.Clear();

        gbl.area_ptr.inDungeon = 1;
        gbl.area_ptr.LastEclBlockId = 0;

        gbl.area2_ptr.Clear();

        gbl.stru_1B2CA.Clear();
        gbl.ecl_ptr.Clear();


        gbl.combat_icons = new CombatIcon[26];
        for (int i = 0; i < 26; i++)
        {
            gbl.combat_icons[i] = new CombatIcon();
        }

        gbl.byte_1AD44 = 2;
        gbl.current_head_id = 0xff;
        gbl.current_body_id = 0xff;
        gbl.headX_dax = null;
        gbl.bodyX_dax = null;

        gbl.byte_1D556 = new DaxArray();

        gbl.bigpic_dax = null;
        gbl.items_pointer = new System.Collections.Generic.List<Item>();

        gbl.mapPosX = 0;
        gbl.mapPosY = 0;
        gbl.mapDirection = 0;
        gbl.mapWallType = 0;
        gbl.mapWallRoof = 0;

        gbl.mapPosX = 7;
        gbl.mapPosY = 0x0D;
        gbl.mapDirection = 0;

        gbl.can_bash_door = true;
        gbl.can_pick_door = true;
        gbl.can_knock_door = true;

        gbl.byte_1AD44 = 3;

        gbl.setBlocks[0] = new gbl.SetBlock(1, 0);
        gbl.setBlocks[1] = new gbl.SetBlock();
        gbl.setBlocks[2] = new gbl.SetBlock();

        //gbl.AnimationsOn = true;
        //gbl.PicsOn = true;
        gbl.DelayBetweenCharacters = true;
        gbl.reload_ecl_and_pictures = false;
        gbl.rest_incounter_count = 0;

        gbl.TeamList.Clear();
        gbl.SelectedPlayer = null;

        gbl.ecl_offset = 0x8000;
        gbl.game_speed_var = 4;
        gbl.inDemo = false;
        gbl.game_area = 1;
        gbl.game_area_backup = 1;
        gbl.mapAreaDisplay = false;
        gbl.area2_ptr.party_size = 0;
        gbl.menuScreenIndex = 1;
        gbl.combat_type = CombatType.normal;
        gbl.displayPlayerStatusLine18 = false;
        gbl.search_flag_bkup = 0;
        gbl.spriteChanged = false;
        gbl.party_killed = false;
        gbl.byte_1BF12 = 1;
        gbl.displayPlayerSprite = false;
        gbl.lastDaxFile = string.Empty;
        gbl.byte_1D5AB = string.Empty;
        gbl.lastDaxBlockId = 0x0FF;
        gbl.byte_1D5B5 = 0x0FF;
        gbl.gameSaved = false;
        gbl.byte_1EE95 = false;
        gbl.focusCombatAreaOnPlayer = true;
        gbl.bigpic_block_id = 0x0FF;
        gbl.silent_training = false;
        gbl.menuSelectedWord = 1;
        gbl.game_state = GameState.DungeonMap;
        gbl.last_game_state = 0;
        gbl.applyItemAffect = false;
        gbl.sky_dax_250 = null;
        gbl.sky_dax_251 = null;
        gbl.sky_dax_252 = null;
        gbl.gameWon = false;
        _displayDriver.Load8x8Tiles();
        _ovr027.ClearPromptArea();
        _displayDriver.displayString("Loading...Please Wait", 0, 10, 0x18, 0);

        for (gbl.byte_1AD44 = 0; gbl.byte_1AD44 <= 0x0b; gbl.byte_1AD44++)
        {
            _ovr034.chead_cbody_comspr_icon((byte)(gbl.byte_1AD44 + 0x0D), gbl.byte_1AD44, "COMSPR");
        }

        _ovr034.chead_cbody_comspr_icon(0x19, 0x19, "COMSPR");

        gbl.sky_dax_250 = _daxBlockReader.LoadDax(13, 1, 250, "SKY");
        gbl.sky_dax_251 = _daxBlockReader.LoadDax(13, 1, 251, "SKY");
        gbl.sky_dax_252 = _daxBlockReader.LoadDax(13, 1, 252, "SKY");

        gbl.ItemDataTable = new ItemDataTable("ITEMS");

        _ovr023.setup_spells();
    }

    private void InitAgain() /* sub_396E5 */
    {
        gbl.area_ptr.Clear();
        gbl.area_ptr.inDungeon = 1;
        gbl.area_ptr.LastEclBlockId = 0;
        gbl.area2_ptr.Clear();
        gbl.stru_1B2CA.Clear();
        gbl.ecl_ptr.Clear();

        gbl.mapPosX = 0;
        gbl.mapPosY = 0;
        gbl.mapDirection = 0;
        gbl.mapWallType = 0;
        gbl.mapWallRoof = 0;

        gbl.mapPosX = 7;
        gbl.mapPosY = 0x0D;
        gbl.mapDirection = 2;

        gbl.can_bash_door = true;
        gbl.can_pick_door = true;
        gbl.can_knock_door = true;

        gbl.byte_1AD44 = 3;

        gbl.setBlocks[0].blockId = 0;
        gbl.setBlocks[0].setId = 1;
        gbl.setBlocks[1].Reset();
        gbl.setBlocks[2].Reset();

        gbl.DelayBetweenCharacters = true;
        gbl.reload_ecl_and_pictures = false;
        gbl.rest_incounter_count = 0;

        gbl.TeamList.Clear();
        gbl.SelectedPlayer = null;

        gbl.ecl_offset = 0x8000;
        gbl.game_speed_var = 4;
        gbl.game_area = 1;
        gbl.game_area_backup = 1;
        gbl.mapAreaDisplay = false;
        gbl.area2_ptr.party_size = 0;
        gbl.menuScreenIndex = 1;
        gbl.combat_type = CombatType.normal;
        gbl.displayPlayerStatusLine18 = false;
        gbl.search_flag_bkup = 0;
        gbl.spriteChanged = false;
        gbl.party_killed = false;
        gbl.byte_1BF12 = 1;
        gbl.displayPlayerSprite = false;
        gbl.lastDaxFile = string.Empty;
        gbl.byte_1D5AB = string.Empty;
        gbl.lastDaxBlockId = 0x0FF;
        gbl.byte_1D5B5 = 0x0FF;
        gbl.gameSaved = false;
        gbl.byte_1EE95 = false;
        gbl.focusCombatAreaOnPlayer = true;
        gbl.bigpic_block_id = 0x0FF;
        gbl.silent_training = false;
        _ovr027.ClearPromptArea();
        gbl.menuSelectedWord = 1;
        gbl.game_state = GameState.DungeonMap;
        gbl.last_game_state = 0;
        gbl.applyItemAffect = false;
        gbl.gameWon = false;
    }
}
