using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CharacterFeature.DropCharacterFeature;

public class DropCharacterService
{
    public void DropPlayer() // drop_player
    {
        if (gbl.TeamList.Count == 1)
        {
            if (ovr027.yes_no(gbl.alertMenuColors, "quit TO DOS: ") == 'Y')
            {
                ovr018.FreeCurrentPlayer(gbl.TeamList[0], true, false);
                seg043.print_and_exit();
            }
        }
        else
        {
            ovr025.DisplayPlayerStatusString(false, 10, "will be gone", gbl.SelectedPlayer);

            if (ovr027.yes_no(gbl.alertMenuColors, "Drop from party? ") == 'Y')
            {
                if (gbl.SelectedPlayer.in_combat == true)
                {
                    ovr025.DisplayPlayerStatusString(true, 10, "bids you farewell", gbl.SelectedPlayer);
                }
                else
                {
                    ovr025.DisplayPlayerStatusString(true, 10, "is dumped in a ditch", gbl.SelectedPlayer);
                }

                gbl.SelectedPlayer = ovr018.FreeCurrentPlayer(gbl.SelectedPlayer, true, false);
                seg037.draw8x8_clear_area(0x0b, 0x26, 1, 0x11);

                ovr025.PartySummary(gbl.SelectedPlayer);
            }
            else
            {
                ovr025.DisplayPlayerStatusString(true, 10, "Breathes A sigh of relief", gbl.SelectedPlayer);
            }
        }
    }

    internal void dropPlayer()
    {
        if (gbl.SelectedPlayer != null)
        {
            Player player = gbl.SelectedPlayer;

            if (ovr027.yes_no(gbl.alertMenuColors, "Drop " + player.name + " forever? ") == 'Y' &&
                ovr027.yes_no(gbl.alertMenuColors, "Are you sure? ") == 'Y')
            {
                if (player.in_combat == false)
                {
                    ovr025.string_print01("You dump " + player.name + " out back.");
                }
                else
                {
                    ovr025.string_print01(player.name + " bids you farewell.");
                }

                ovr017.remove_player_file(player);
                gbl.SelectedPlayer = ovr018.FreeCurrentPlayer(gbl.SelectedPlayer, true, false);
            }
            else
            {
                ovr025.string_print01(player.name + " breathes a sigh of relief.");
            }
        }

        ovr025.PartySummary(gbl.SelectedPlayer);
    }
}
