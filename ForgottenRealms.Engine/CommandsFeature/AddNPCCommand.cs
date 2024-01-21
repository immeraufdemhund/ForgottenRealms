﻿using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class AddNPCCommand : IGameCommand
{
    public void Execute()
    {
        ovr008.vm_LoadCmdSets(2);
        int npc_id = (byte)ovr008.vm_GetCmdValue(1);

        ovr017.load_npc(npc_id);

        var morale = (byte)ovr008.vm_GetCmdValue(2);

        gbl.SelectedPlayer.control_morale = (byte)((morale >> 1) + Control.NPC_Base);

        ovr025.reclac_player_values(gbl.SelectedPlayer);
        ovr025.PartySummary(gbl.SelectedPlayer);
    }
}
