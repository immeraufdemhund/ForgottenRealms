using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class WhoCommand : IGameCommand
{
    private readonly ovr008 _ovr008;
    private readonly ovr025 _ovr025;
    private readonly seg037 _seg037;
    public WhoCommand(ovr008 ovr008, ovr025 ovr025, seg037 seg037)
    {
        _ovr008 = ovr008;
        _ovr025 = ovr025;
        _seg037 = seg037;
    }

    public void Execute()
    {
        _ovr008.vm_LoadCmdSets(1);
        var prompt = gbl.unk_1D972[1];

        VmLog.WriteLine("CMD_Who: Prompt: '{0}'", prompt);

        _seg037.draw8x8_clear_area(TextRegion.NormalBottom);
        _ovr025.selectAPlayer(ref gbl.SelectedPlayer, false, prompt);
    }
}
