using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class CompareCommand : IGameCommand
{
    public void Execute()
    {
        ovr008.vm_LoadCmdSets(2);

        if (gbl.cmd_opps[1].Code >= 0x80 ||
            gbl.cmd_opps[2].Code >= 0x80)
        {
            VmLog.WriteLine("CMD_Compare: Strings '{0}' '{1}'", gbl.unk_1D972[2], gbl.unk_1D972[1]);

            ovr008.compare_strings(gbl.unk_1D972[2], gbl.unk_1D972[1]);
        }
        else
        {
            ushort value_a = ovr008.vm_GetCmdValue(1);
            ushort value_b = ovr008.vm_GetCmdValue(2);

            VmLog.WriteLine("CMD_Compare: Values: {0} {1}", value_b, value_a);
            ovr008.compare_variables(value_b, value_a);
        }
    }
}
