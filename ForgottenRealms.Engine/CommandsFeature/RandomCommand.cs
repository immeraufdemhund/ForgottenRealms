using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class RandomCommand : IGameCommand
{
    public void Execute()
    {
        ovr008.vm_LoadCmdSets(2);

        var rand_max = (byte)ovr008.vm_GetCmdValue(1);

        if (rand_max < 0xff)
        {
            rand_max++;
        }

        var loc = gbl.cmd_opps[2].Word;

        var val = seg051.Random(rand_max);

        VmLog.WriteLine("CMD_Random: Max: {0} Loc: {1} Val: {2}", rand_max, new MemLoc(loc), val);

        ovr008.vm_SetMemoryValue(val, loc);
    }
}
