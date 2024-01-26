using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class SaveTableCommand : IGameCommand
{
    private readonly ovr008 _ovr008;
    public SaveTableCommand(ovr008 ovr008)
    {
        _ovr008 = ovr008;
    }

    public void Execute()
    {
        _ovr008.vm_LoadCmdSets(3);

        var var_6 = _ovr008.vm_GetCmdValue(1);

        var result_loc = gbl.cmd_opps[2].Word;
        result_loc += _ovr008.vm_GetCmdValue(3);

        _ovr008.vm_SetMemoryValue(var_6, result_loc);
    }
}
