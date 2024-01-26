using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class RobCommand : IGameCommand
{
    private readonly ovr008 _ovr008;
    public RobCommand(ovr008 ovr008)
    {
        _ovr008 = ovr008;
    }

    public void Execute()
    {
        _ovr008.vm_LoadCmdSets(3);
        var allParty = (byte)_ovr008.vm_GetCmdValue(1);
        var var_2 = (byte)_ovr008.vm_GetCmdValue(2);

        var percentage = (100 - var_2) / 100.0;
        int robChance = (byte)_ovr008.vm_GetCmdValue(3);

        if (allParty == 0)
        {
            _ovr008.RobMoney(gbl.SelectedPlayer, percentage);
            _ovr008.RobItems(gbl.SelectedPlayer, robChance);
        }
        else
        {
            foreach (var player in gbl.TeamList)
            {
                _ovr008.RobMoney(player, percentage);
                _ovr008.RobItems(player, robChance);
            }
        }
    }
}
