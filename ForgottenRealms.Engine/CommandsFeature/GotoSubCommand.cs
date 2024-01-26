using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class GotoSubCommand : IGameCommand
{
    private readonly ovr008 _ovr008;
    public GotoSubCommand(ovr008 ovr008)
    {
        _ovr008 = ovr008;
    }

    public void Execute()
    {
        _ovr008.vm_LoadCmdSets(1);
        var newOffset = gbl.cmd_opps[1].Word;

        VmLog.WriteLine("CMD_Gosub: was: 0x{0:X} now: 0x{1:X}", gbl.ecl_offset, newOffset);

        gbl.vmCallStack.Push(gbl.ecl_offset);
        gbl.ecl_offset = newOffset;
    }
}
