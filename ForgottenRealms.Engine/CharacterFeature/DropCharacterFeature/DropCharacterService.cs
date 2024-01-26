using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CharacterFeature.DropCharacterFeature;

public class DropCharacterService
{
    private readonly MainGameEngine _mainGameEngine;
    private readonly ovr017 _ovr017;
    private readonly ovr018 _ovr018;
    private readonly ovr025 _ovr025;
    private readonly ovr027 _ovr027;
    private readonly seg037 _seg037;

    public DropCharacterService(MainGameEngine mainGameEngine, ovr017 ovr017, ovr018 ovr018, ovr025 ovr025, ovr027 ovr027, seg037 seg037)
    {
        _mainGameEngine = mainGameEngine;
        _ovr017 = ovr017;
        _ovr018 = ovr018;
        _ovr025 = ovr025;
        _ovr027 = ovr027;
        _seg037 = seg037;
    }

    public void DropPlayer() // drop_player
    {
        if (gbl.TeamList.Count == 1)
        {
            if (_ovr027.yes_no(gbl.alertMenuColors, "quit TO DOS: ") == 'Y')
            {
                _ovr018.FreeCurrentPlayer(gbl.TeamList[0], true, false);
                _mainGameEngine.EngineStop();
            }
        }
        else
        {
            _ovr025.DisplayPlayerStatusString(false, 10, "will be gone", gbl.SelectedPlayer);

            if (_ovr027.yes_no(gbl.alertMenuColors, "Drop from party? ") == 'Y')
            {
                if (gbl.SelectedPlayer.in_combat == true)
                {
                    _ovr025.DisplayPlayerStatusString(true, 10, "bids you farewell", gbl.SelectedPlayer);
                }
                else
                {
                    _ovr025.DisplayPlayerStatusString(true, 10, "is dumped in a ditch", gbl.SelectedPlayer);
                }

                gbl.SelectedPlayer = _ovr018.FreeCurrentPlayer(gbl.SelectedPlayer, true, false);
                _seg037.draw8x8_clear_area(0x0b, 0x26, 1, 0x11);

                _ovr025.PartySummary(gbl.SelectedPlayer);
            }
            else
            {
                _ovr025.DisplayPlayerStatusString(true, 10, "Breathes A sigh of relief", gbl.SelectedPlayer);
            }
        }
    }

    internal void dropPlayer()
    {
        if (gbl.SelectedPlayer != null)
        {
            Player player = gbl.SelectedPlayer;

            if (_ovr027.yes_no(gbl.alertMenuColors, "Drop " + player.name + " forever? ") == 'Y' &&
                _ovr027.yes_no(gbl.alertMenuColors, "Are you sure? ") == 'Y')
            {
                if (player.in_combat == false)
                {
                    _ovr025.string_print01("You dump " + player.name + " out back.");
                }
                else
                {
                    _ovr025.string_print01(player.name + " bids you farewell.");
                }

                _ovr017.remove_player_file(player);
                gbl.SelectedPlayer = _ovr018.FreeCurrentPlayer(gbl.SelectedPlayer, true, false);
            }
            else
            {
                _ovr025.string_print01(player.name + " breathes a sigh of relief.");
            }
        }

        _ovr025.PartySummary(gbl.SelectedPlayer);
    }
}
