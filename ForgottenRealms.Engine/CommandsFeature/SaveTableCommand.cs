using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class SaveTableCommand : IGameCommand
{
    public void Execute()
    {
        ovr008.vm_LoadCmdSets(3);

        ushort var_6 = ovr008.vm_GetCmdValue(1);

        ushort result_loc = gbl.cmd_opps[2].Word;
        result_loc += ovr008.vm_GetCmdValue(3);

        ovr008.vm_SetMemoryValue(var_6, result_loc);
    }
}