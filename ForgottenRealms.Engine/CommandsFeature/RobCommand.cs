using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class RobCommand : IGameCommand
{
    public void Execute()
    {
        ovr008.vm_LoadCmdSets(3);
        var allParty = (byte)ovr008.vm_GetCmdValue(1);
        var var_2 = (byte)ovr008.vm_GetCmdValue(2);

        var percentage = (100 - var_2) / 100.0;
        int robChance = (byte)ovr008.vm_GetCmdValue(3);

        if (allParty == 0)
        {
            ovr008.RobMoney(gbl.SelectedPlayer, percentage);
            ovr008.RobItems(gbl.SelectedPlayer, robChance);
        }
        else
        {
            foreach (var player in gbl.TeamList)
            {
                ovr008.RobMoney(player, percentage);
                ovr008.RobItems(player, robChance);
            }
        }
    }
}
