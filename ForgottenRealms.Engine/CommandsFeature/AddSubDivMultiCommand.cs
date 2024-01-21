using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class AddSubDivMultiCommand : IGameCommand
{
    public void Execute()
    {
        ushort value;

        ovr008.vm_LoadCmdSets(3);

        var val_a = ovr008.vm_GetCmdValue(1);
        var val_b = ovr008.vm_GetCmdValue(2);

        var location = gbl.cmd_opps[3].Word;

        switch (gbl.command)
        {
            case 4:
                value = (ushort)(val_a + val_b);
                break;

            case 5:
                value = (ushort)(val_b - val_a);
                break;

            case 6:
                value = (ushort)(val_a / val_b);
                gbl.area2_ptr.field_67E = (short)(val_a % val_b);
                break;

            case 7:
                value = (ushort)(val_a * val_b);
                break;

            default:
                value = 0;
                throw new System.Exception("can't get here.");
        }

        string[] sym = { "", "", "", "", "A + B", "B - A", "A / B", "A * B" };
        VmLog.WriteLine("CMD_AdSubDivMulti: {0} A: {1} B: {2} Loc: {3} Res: {4}",
            sym[gbl.command], val_a, val_b, new MemLoc(location), value);

        ovr008.vm_SetMemoryValue(value, location);
    }
}
