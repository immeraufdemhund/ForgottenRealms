using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class ProgramCommand : IGameCommand
{
    public void Execute()
    {
        ovr008.vm_LoadCmdSets(1);
        var var_1 = (byte)ovr008.vm_GetCmdValue(1);

        if (gbl.restore_player_ptr == true)
        {
            gbl.SelectedPlayer = gbl.LastSelectedPlayer;
            gbl.restore_player_ptr = false;
        }


        if (var_1 == 0)
        {
            ovr018.StartGameMenu();
            if (gbl.lastDaxBlockId != 0x50 &&
                gbl.area_ptr.inDungeon == 0)
            {
                ovr025.LoadPic();
            }
        }
        else if (var_1 == 8)
        {
            ovr019.end_game_text();
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

            ovr018.StartGameMenu();
            var saveYes = ovr027.yes_no(gbl.defaultMenuColors, "You've won. Save before quitting? ");

            if (saveYes == 'Y')
            {
                ovr017.SaveGame();
            }

            KeyboardService.print_and_exit();
        }
        else if (var_1 == 9)
        {
            var ecl_bkup = gbl.ecl_offset;
            ovr003.TryEncamp();
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
