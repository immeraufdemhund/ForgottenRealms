using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class FindSpecialCommand : IGameCommand
{
    public void Execute()
    {
        for (var i = 0; i < 6; i++)
        {
            gbl.compare_flags[i] = false;
        }

        ovr008.vm_LoadCmdSets(1);
        var affect_type = (Affects)ovr008.vm_GetCmdValue(1);

        if (gbl.SelectedPlayer.HasAffect(affect_type) == true)
        {
            gbl.compare_flags[0] = true;
        }
        else
        {
            gbl.compare_flags[1] = true;
        }
    }
}
