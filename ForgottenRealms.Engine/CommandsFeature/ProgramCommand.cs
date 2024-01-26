using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class ProgramCommand : IGameCommand
{
    private readonly MainGameEngine _mainGameEngine;
    private readonly ovr003 _ovr003;
    private readonly ovr008 _ovr008;
    private readonly ovr017 _ovr017;
    private readonly ovr018 _ovr018;
    private readonly ovr019 _ovr019;
    private readonly ovr025 _ovr025;
    private readonly ovr027 _ovr027;

    public ProgramCommand(MainGameEngine mainGameEngine, ovr003 ovr003, ovr008 ovr008, ovr017 ovr017, ovr018 ovr018, ovr019 ovr019, ovr025 ovr025, ovr027 ovr027)
    {
        _mainGameEngine = mainGameEngine;
        _ovr003 = ovr003;
        _ovr008 = ovr008;
        _ovr017 = ovr017;
        _ovr018 = ovr018;
        _ovr019 = ovr019;
        _ovr025 = ovr025;
        _ovr027 = ovr027;
    }

    public void Execute()
    {
        _ovr008.vm_LoadCmdSets(1);
        var var_1 = (byte)_ovr008.vm_GetCmdValue(1);

        if (gbl.restore_player_ptr == true)
        {
            gbl.SelectedPlayer = gbl.LastSelectedPlayer;
            gbl.restore_player_ptr = false;
        }


        if (var_1 == 0)
        {
            _ovr018.StartGameMenu();
            if (gbl.lastDaxBlockId != 0x50 &&
                gbl.area_ptr.inDungeon == 0)
            {
                _ovr025.LoadPic();
            }
        }
        else if (var_1 == 8)
        {
            _ovr019.end_game_text();
            gbl.gameWon = true;
            gbl.area_ptr.field_3FA = 0xff;
            gbl.area2_ptr.training_class_mask = 0xff;

            foreach (var player in gbl.TeamList)
            {
                var play_ptr = player;
                play_ptr.hit_point_current = play_ptr.hit_point_max;
                play_ptr.health_status = Status.okey;
                play_ptr.in_combat = true;
            }

            _ovr018.StartGameMenu();
            var saveYes = _ovr027.yes_no(gbl.defaultMenuColors, "You've won. Save before quitting? ");

            if (saveYes == 'Y')
            {
                _ovr017.SaveGame();
            }

            _mainGameEngine.EngineStop();
        }
        else if (var_1 == 9)
        {
            var ecl_bkup = gbl.ecl_offset;
            _ovr003.TryEncamp();
            gbl.ecl_offset = ecl_bkup;
            new ExitCommand().Execute();
        }
        else if (var_1 == 3)
        {
            gbl.party_killed = true;
            new ExitCommand().Execute();
        }
    }
}
