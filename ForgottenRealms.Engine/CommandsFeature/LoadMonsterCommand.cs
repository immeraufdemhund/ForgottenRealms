using System.Collections.Generic;
using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class LoadMonsterCommand : IGameCommand
{
    private readonly ovr008 _ovr008;
    private readonly ovr017 _ovr017;
    private readonly ovr034 _ovr034;
    public LoadMonsterCommand(ovr008 ovr008, ovr017 ovr017, ovr034 ovr034)
    {
        _ovr008 = ovr008;
        _ovr017 = ovr017;
        _ovr034 = ovr034;
    }

    public void Execute()
    {
        var current_player_bkup = gbl.SelectedPlayer;
        _ovr008.vm_LoadCmdSets(3);

        if (gbl.numLoadedMonsters < 63)
        {
            var mod_id = _ovr008.vm_GetCmdValue(1) & 0xFF;

            var mobMasterCopy = _ovr017.load_mob(mod_id);

            var newMob = mobMasterCopy.ShallowClone();

            var num_copies = _ovr008.vm_GetCmdValue(2) & 0xFF;

            if (num_copies <= 0)
            {
                num_copies = 1;
            }

            var blockId = _ovr008.vm_GetCmdValue(3) & 0xFF;
            _ovr034.chead_cbody_comspr_icon(gbl.monster_icon_id, blockId, "CPIC");

            newMob.icon_id = gbl.monster_icon_id;

            gbl.TeamList.Add(newMob);

            gbl.numLoadedMonsters++;
            var copy_count = 1;

            while (copy_count < num_copies &&
                   gbl.numLoadedMonsters < 63)
            {
                newMob = mobMasterCopy.ShallowClone();

                newMob.icon_id = gbl.monster_icon_id;

                newMob.affects = new List<Affect>();
                newMob.items = new List<Item>();

                foreach (var item in mobMasterCopy.items)
                {
                    newMob.items.Add(item.ShallowClone());
                }

                foreach (var affect in mobMasterCopy.affects)
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
