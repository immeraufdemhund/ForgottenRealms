using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class RandomCommand : IGameCommand
{
    private readonly ovr008 _ovr008;
    private readonly seg051 _seg051;
    public RandomCommand(ovr008 ovr008, seg051 seg051)
    {
        _ovr008 = ovr008;
        _seg051 = seg051;
    }

    public void Execute()
    {
        _ovr008.vm_LoadCmdSets(2);

        var rand_max = (byte)_ovr008.vm_GetCmdValue(1);

        if (rand_max < 0xff)
        {
            rand_max++;
        }

        var loc = gbl.cmd_opps[2].Word;

        var val = _seg051.Random(rand_max);

        VmLog.WriteLine("CMD_Random: Max: {0} Loc: {1} Val: {2}", rand_max, new MemLoc(loc), val);

        _ovr008.vm_SetMemoryValue(val, loc);
    }
}
