using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class OnGotoGoSubCommand : IGameCommand
{
    public void Execute()
    {
        ovr008.vm_LoadCmdSets(2);
        var var_1 = (byte)ovr008.vm_GetCmdValue(1);
        var var_2 = (byte)ovr008.vm_GetCmdValue(2);
        gbl.ecl_offset--;
        ovr008.vm_LoadCmdSets(var_2);

        if (var_1 < var_2)
        {
            var newloc = gbl.cmd_opps[var_1 + 1].Word;
            VmLog.WriteLine("CMD_OnGotoGoSub: {4} A: {0} B: {1} Was: 0x{2:X} Now: 0x{3:X}",
                var_1, var_2, gbl.ecl_offset, newloc,
                gbl.command == 0x25 ? "Goto" : "Gosub");

            if (gbl.command == 0x25)
            {
                // Goto
                gbl.ecl_offset = newloc;
            }
            else
            {
                // Gosub
                gbl.vmCallStack.Push(gbl.ecl_offset);
                gbl.ecl_offset = newloc;
            }
        }
        else
        {
            VmLog.WriteLine("CMD_OnGotoGoSub: {0} A: {1} B: {2}",
                gbl.command == 0x25 ? "Goto" : "Gosub", var_1, var_2);
        }
    }
}
