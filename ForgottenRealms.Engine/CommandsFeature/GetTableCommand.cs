using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class GetTableCommand : IGameCommand
{
    public void Execute()
    {
        ovr008.vm_LoadCmdSets(3);

        var var_2 = gbl.cmd_opps[1].Word;
        var var_9 = (byte)ovr008.vm_GetCmdValue(2);

        var result_loc = gbl.cmd_opps[3].Word;

        var var_6 = (ushort)(var_9 + var_2);

        var var_8 = ovr008.vm_GetMemoryValue(var_6);
        ovr008.vm_SetMemoryValue(var_8, result_loc);
    }
}
