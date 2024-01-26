using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class CompareCommand : IGameCommand
{
    private readonly ovr008 _ovr008;
    public CompareCommand(ovr008 ovr008)
    {
        _ovr008 = ovr008;
    }

    public void Execute()
    {
        _ovr008.vm_LoadCmdSets(2);

        if (gbl.cmd_opps[1].Code >= 0x80 ||
            gbl.cmd_opps[2].Code >= 0x80)
        {
            VmLog.WriteLine("CMD_Compare: Strings '{0}' '{1}'", gbl.unk_1D972[2], gbl.unk_1D972[1]);

            _ovr008.compare_strings(gbl.unk_1D972[2], gbl.unk_1D972[1]);
        }
        else
        {
            var value_a = _ovr008.vm_GetCmdValue(1);
            var value_b = _ovr008.vm_GetCmdValue(2);

            VmLog.WriteLine("CMD_Compare: Values: {0} {1}", value_b, value_a);
            _ovr008.compare_variables(value_b, value_a);
        }
    }
}
