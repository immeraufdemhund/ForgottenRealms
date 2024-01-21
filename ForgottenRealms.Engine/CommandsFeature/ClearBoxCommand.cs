using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class ClearBoxCommand : IGameCommand
{
    public void Execute()
    {
        gbl.ecl_offset++;

        VmLog.WriteLine("CMD_ClearBox:");

        seg037.draw8x8_03();
        ovr025.PartySummary(gbl.SelectedPlayer);
        ovr025.display_map_position_time();

        ovr030.DrawMaybeOverlayed(gbl.byte_1D556.frames[0].picture, true, 3, 3);
        ovr025.display_map_position_time();
        gbl.byte_1EE98 = false;
    }
}
