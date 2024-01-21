﻿using System.Collections.Generic;
using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class LoadMonsterCommand : IGameCommand
{
    public void Execute()
    {
        Player current_player_bkup = gbl.SelectedPlayer;
        ovr008.vm_LoadCmdSets(3);

        if (gbl.numLoadedMonsters < 63)
        {
            int mod_id = ovr008.vm_GetCmdValue(1) & 0xFF;

            Player mobMasterCopy = ovr017.load_mob(mod_id);

            Player newMob = mobMasterCopy.ShallowClone();

            int num_copies = ovr008.vm_GetCmdValue(2) & 0xFF;

            if (num_copies <= 0)
            {
                num_copies = 1;
            }

            int blockId = ovr008.vm_GetCmdValue(3) & 0xFF;
            ovr034.chead_cbody_comspr_icon(gbl.monster_icon_id, blockId, "CPIC");

            newMob.icon_id = gbl.monster_icon_id;

            gbl.TeamList.Add(newMob);

            gbl.numLoadedMonsters++;
            int copy_count = 1;

            while (copy_count < num_copies &&
                   gbl.numLoadedMonsters < 63)
            {
                newMob = mobMasterCopy.ShallowClone();

                newMob.icon_id = gbl.monster_icon_id;

                newMob.affects = new List<Affect>();
                newMob.items = new List<Item>();

                foreach (Item item in mobMasterCopy.items)
                {
                    newMob.items.Add(item.ShallowClone());
                }

                foreach (Affect affect in mobMasterCopy.affects)
                {
                    newMob.affects.Add(affect.ShallowClone());
                }

                copy_count++;
                gbl.numLoadedMonsters++;
                gbl.TeamList.Add(newMob);
            }

            gbl.monster_icon_id++;
            gbl.monstersLoaded = true;
            gbl.SelectedPlayer = current_player_bkup;
        }
    }
}