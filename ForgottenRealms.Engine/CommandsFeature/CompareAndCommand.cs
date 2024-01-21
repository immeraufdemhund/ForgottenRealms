using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class CompareAndCommand : IGameCommand
{
    public void Execute()
    {
        for (int i = 0; i < 6; i++)
        {
            gbl.compare_flags[i] = false;
        }

        ovr008.vm_LoadCmdSets(4);

        ushort var_8 = ovr008.vm_GetCmdValue(1);
        ushort var_6 = ovr008.vm_GetCmdValue(2);
        ushort var_4 = ovr008.vm_GetCmdValue(3);
        ushort var_2 = ovr008.vm_GetCmdValue(4);

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