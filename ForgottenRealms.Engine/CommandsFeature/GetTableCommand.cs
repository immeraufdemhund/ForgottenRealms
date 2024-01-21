using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class GetTableCommand : IGameCommand
{
    public void Execute()
    {
        ovr008.vm_LoadCmdSets(3);

        ushort var_2 = gbl.cmd_opps[1].Word;
        byte var_9 = (byte)ovr008.vm_GetCmdValue(2);

        ushort result_loc = gbl.cmd_opps[3].Word;

        ushort var_6 = (ushort)(var_9 + var_2);

        ushort var_8 = ovr008.vm_GetMemoryValue(var_6);
        ovr008.vm_SetMemoryValue(var_8, result_loc);
    }
}