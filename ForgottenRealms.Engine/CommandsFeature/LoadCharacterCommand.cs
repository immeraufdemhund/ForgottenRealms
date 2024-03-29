﻿using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class LoadCharacterCommand : IGameCommand
{
    private readonly ovr008 _ovr008;
    private readonly ovr018 _ovr018;
    private readonly ovr025 _ovr025;
    public LoadCharacterCommand(ovr008 ovr008, ovr018 ovr018, ovr025 ovr025)
    {
        _ovr008 = ovr008;
        _ovr018 = ovr018;
        _ovr025 = ovr025;
    }

    public void Execute()
    {
        _ovr008.vm_LoadCmdSets(1);

        int player_index = (byte)_ovr008.vm_GetCmdValue(1);
        VmLog.WriteLine("CMD_LoadCharacter: 0x{0:X}", player_index);

        gbl.restore_player_ptr = true;

        var high_bit_set = (player_index & 0x80) != 0;
        player_index = player_index & 0x7f;

        var player = player_index > 0 && player_index < gbl.TeamList.Count ? gbl.TeamList[player_index] : null;

        if (player != null)
        {
            gbl.SelectedPlayer = player;
            gbl.player_not_found = false;
        }
        else
        {
            gbl.player_not_found = true;
        }

        if (high_bit_set == true &&
            gbl.redrawPartySummary1 == true &&
            gbl.redrawPartySummary2 == true)
        {
            if (gbl.LastSelectedPlayer == player)
            {
                gbl.restore_player_ptr = false;
            }

            gbl.SelectedPlayer = _ovr018.FreeCurrentPlayer(gbl.SelectedPlayer, true, false);

            _ovr025.PartySummary(gbl.SelectedPlayer);
            gbl.redrawPartySummary1 = false;
            gbl.redrawPartySummary2 = false;
        }
    }
}
