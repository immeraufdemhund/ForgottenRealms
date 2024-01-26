using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class FindSpecialCommand : IGameCommand
{
    private readonly ovr008 _ovr008;
    public FindSpecialCommand(ovr008 ovr008)
    {
        _ovr008 = ovr008;
    }

    public void Execute()
    {
        for (var i = 0; i < 6; i++)
        {
            gbl.compare_flags[i] = false;
        }

        _ovr008.vm_LoadCmdSets(1);
        var affect_type = (Affects)_ovr008.vm_GetCmdValue(1);

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
