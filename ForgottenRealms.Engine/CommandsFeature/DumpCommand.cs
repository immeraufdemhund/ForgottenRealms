using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class DumpCommand : IGameCommand
{
    public void Execute()
    {
        gbl.ecl_offset++;

        VmLog.WriteLine("CMD_Dump: Player: {0}", gbl.SelectedPlayer);

        gbl.SelectedPlayer = ovr018.FreeCurrentPlayer(gbl.SelectedPlayer, true, false);

        gbl.LastSelectedPlayer = gbl.SelectedPlayer;

        ovr025.PartySummary(gbl.SelectedPlayer);
    }
}
