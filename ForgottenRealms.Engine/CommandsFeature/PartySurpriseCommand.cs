using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class PartySurpriseCommand : IGameCommand
{
    public void Execute()
    {
        ovr008.vm_LoadCmdSets(2);

        byte val_a = 0;
        byte val_b = 0;

        foreach (var player in gbl.TeamList)
        {
            if (player._class == ClassId.ranger ||
                player._class == ClassId.mc_c_r)
            {
                val_a = 1;
            }
        }

        var loc_a = gbl.cmd_opps[1].Word;
        var loc_b = gbl.cmd_opps[2].Word;

        ovr008.vm_SetMemoryValue(val_a, loc_a);
        ovr008.vm_SetMemoryValue(val_b, loc_b);
    }
}
