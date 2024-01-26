using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class ClearBoxCommand : IGameCommand
{
    private readonly ovr025 _ovr025;
    private readonly ovr030 _ovr030;
    private readonly seg037 _seg037;
    public ClearBoxCommand(ovr025 ovr025, ovr030 ovr030, seg037 seg037)
    {
        _ovr025 = ovr025;
        _ovr030 = ovr030;
        _seg037 = seg037;
    }

    public void Execute()
    {
        gbl.ecl_offset++;

        VmLog.WriteLine("CMD_ClearBox:");

        _seg037.draw8x8_03();
        _ovr025.PartySummary(gbl.SelectedPlayer);
        _ovr025.display_map_position_time();

        _ovr030.DrawMaybeOverlayed(gbl.byte_1D556.frames[0].picture, true, 3, 3);
        _ovr025.display_map_position_time();
        gbl.byte_1EE98 = false;
    }
}
