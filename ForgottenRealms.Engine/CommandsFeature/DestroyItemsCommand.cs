using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class DestroyItemsCommand : IGameCommand
{
    private readonly ovr008 _ovr008;
    private readonly ovr025 _ovr025;
    public DestroyItemsCommand(ovr008 ovr008, ovr025 ovr025)
    {
        _ovr008 = ovr008;
        _ovr025 = ovr025;
    }

    public void Execute()
    {
        _ovr008.vm_LoadCmdSets(1);
        var item_type = (ItemType)_ovr008.vm_GetCmdValue(1);

        VmLog.WriteLine("CMD_DestroyItems: type: {0}", item_type);

        foreach (var player in gbl.TeamList)
        {
            player.items.RemoveAll(item => item.type == item_type);

            _ovr025.reclac_player_values(player);
        }
    }
}
