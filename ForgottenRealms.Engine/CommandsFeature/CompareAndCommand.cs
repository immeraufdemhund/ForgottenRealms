using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class CompareAndCommand : IGameCommand
{
    private readonly ovr008 _ovr008;
    public CompareAndCommand(ovr008 ovr008)
    {
        _ovr008 = ovr008;
    }

    public void Execute()
    {
        for (var i = 0; i < 6; i++)
        {
            gbl.compare_flags[i] = false;
        }

        _ovr008.vm_LoadCmdSets(4);

        var var_8 = _ovr008.vm_GetCmdValue(1);
        var var_6 = _ovr008.vm_GetCmdValue(2);
        var var_4 = _ovr008.vm_GetCmdValue(3);
        var var_2 = _ovr008.vm_GetCmdValue(4);

        if (var_8 == var_6 &&
            var_4 == var_2)
        {
            gbl.compare_flags[0] = true;
        }
        else
        {
            gbl.compare_flags[1] = true;
        }
    }
}
