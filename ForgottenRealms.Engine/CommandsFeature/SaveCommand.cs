using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class SaveCommand : IGameCommand
{
    private readonly ovr008 _ovr008;
    public SaveCommand(ovr008 ovr008)
    {
        _ovr008 = ovr008;
    }

    public void Execute()
    {
        _ovr008.vm_LoadCmdSets(2);

        var loc = gbl.cmd_opps[2].Word;

        if (gbl.cmd_opps[1].Code < 0x80)
        {
            var val = _ovr008.vm_GetCmdValue(1);

            VmLog.WriteLine("CMD_Save: Value {0} Loc: {1}", val, new MemLoc(loc));
            _ovr008.vm_SetMemoryValue(val, loc);
        }
        else
        {
            VmLog.WriteLine("CMD_Save: String '{0}' Loc: {1}", gbl.unk_1D972[1], new MemLoc(loc));
            _ovr008.vm_WriteStringToMemory(gbl.unk_1D972[1], loc);
        }
    }
}
