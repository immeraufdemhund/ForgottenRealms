using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class ClearMonstersCommand : IGameCommand
{
    public void Execute()
    {
        gbl.ecl_offset++;
        gbl.numLoadedMonsters = 0;
        gbl.monstersLoaded = false;
        gbl.monster_icon_id = 8;

        VmLog.WriteLine("CMD_ClearMonsters:");

        gbl.pooled_money.ClearAll();
        gbl.items_pointer.Clear();
    }
}
