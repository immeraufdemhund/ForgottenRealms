using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class GotoCommand : IGameCommand
{
    public void Execute()
    {
        ovr008.vm_LoadCmdSets(1);
        var newOffset = gbl.cmd_opps[1].Word;

        VmLog.WriteLine("CMD_Goto: was: 0x{0:X} now: 0x{1:X}", gbl.ecl_offset, newOffset);

        gbl.ecl_offset = newOffset;
    }
}
