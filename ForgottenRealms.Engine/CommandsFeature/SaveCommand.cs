using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class SaveCommand : IGameCommand
{
    public void Execute()
    {
        ovr008.vm_LoadCmdSets(2);

        ushort loc = gbl.cmd_opps[2].Word;

        if (gbl.cmd_opps[1].Code < 0x80)
        {
            ushort val = ovr008.vm_GetCmdValue(1);

            VmLog.WriteLine("CMD_Save: Value {0} Loc: {1}", val, new MemLoc(loc));
            ovr008.vm_SetMemoryValue(val, loc);
        }
        else
        {
            VmLog.WriteLine("CMD_Save: String '{0}' Loc: {1}", gbl.unk_1D972[1], new MemLoc(loc));
            ovr008.vm_WriteStringToMemory(gbl.unk_1D972[1], loc);
        }
    }
}