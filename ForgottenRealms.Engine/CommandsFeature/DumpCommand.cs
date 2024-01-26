using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class DumpCommand : IGameCommand
{
    private readonly ovr018 _ovr018;
    private readonly ovr025 _ovr025;
    public DumpCommand(ovr018 ovr018, ovr025 ovr025)
    {
        _ovr018 = ovr018;
        _ovr025 = ovr025;
    }

    public void Execute()
    {
        gbl.ecl_offset++;

        VmLog.WriteLine("CMD_Dump: Player: {0}", gbl.SelectedPlayer);

        gbl.SelectedPlayer = _ovr018.FreeCurrentPlayer(gbl.SelectedPlayer, true, false);

        gbl.LastSelectedPlayer = gbl.SelectedPlayer;

        _ovr025.PartySummary(gbl.SelectedPlayer);
    }
}
