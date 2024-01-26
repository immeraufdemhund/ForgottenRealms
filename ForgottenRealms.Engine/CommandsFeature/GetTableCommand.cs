using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class GetTableCommand : IGameCommand
{
    private readonly ovr008 _ovr008;
    public GetTableCommand(ovr008 ovr008)
    {
        _ovr008 = ovr008;
    }

    public void Execute()
    {
        _ovr008.vm_LoadCmdSets(3);

        var var_2 = gbl.cmd_opps[1].Word;
        var var_9 = (byte)_ovr008.vm_GetCmdValue(2);

        var result_loc = gbl.cmd_opps[3].Word;

        var var_6 = (ushort)(var_9 + var_2);

        var var_8 = _ovr008.vm_GetMemoryValue(var_6);
        _ovr008.vm_SetMemoryValue(var_8, result_loc);
    }
}
