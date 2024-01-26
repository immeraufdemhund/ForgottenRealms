using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class FindItemCommand : IGameCommand
{
    private readonly ovr008 _ovr008;
    public FindItemCommand(ovr008 ovr008)
    {
        _ovr008 = ovr008;
    }

    public void Execute()
    {
        _ovr008.vm_LoadCmdSets(1);

        var item_type = (ItemType)_ovr008.vm_GetCmdValue(1);

        for (var i = 0; i < 6; i++)
        {
            gbl.compare_flags[i] = false;
        }

        gbl.compare_flags[1] = true;

        foreach (var player in gbl.TeamList)
        {
            foreach (var item in player.items)
            {
                if (item_type == item.type)
                {
                    gbl.compare_flags[0] = true;
                    gbl.compare_flags[1] = false;
                    return;
                }
            }
        }
    }
}
