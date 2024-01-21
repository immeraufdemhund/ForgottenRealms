using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class WhoCommand : IGameCommand
{
    public void Execute()
    {
        ovr008.vm_LoadCmdSets(1);
        var prompt = gbl.unk_1D972[1];

        VmLog.WriteLine("CMD_Who: Prompt: '{0}'", prompt);

        seg037.draw8x8_clear_area(TextRegion.NormalBottom);
        ovr025.selectAPlayer(ref gbl.SelectedPlayer, false, prompt);
    }
}
