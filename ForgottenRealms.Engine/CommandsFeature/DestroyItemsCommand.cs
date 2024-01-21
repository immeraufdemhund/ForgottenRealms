using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class DestroyItemsCommand : IGameCommand
{
    public void Execute()
    {
        ovr008.vm_LoadCmdSets(1);
        ItemType item_type = (ItemType)ovr008.vm_GetCmdValue(1);

        VmLog.WriteLine("CMD_DestroyItems: type: {0}", item_type);

        foreach (Player player in gbl.TeamList)
        {
            player.items.RemoveAll(item => item.type == item_type);

            ovr025.reclac_player_values(player);
        }
    }
}