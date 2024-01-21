﻿using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class AndOrCommand : IGameCommand
{
    public void Execute()
    {
        byte resultant;

        ovr008.vm_LoadCmdSets(3);
        var val_a = ovr008.vm_GetCmdValue(1);
        var val_b = ovr008.vm_GetCmdValue(2);

        var loc = gbl.cmd_opps[3].Word;
        string sym;
        if (gbl.command == 0x2F)
        {
            sym = "And";
            resultant = (byte)(val_a & val_b);
        }
        else
        {
            sym = "Or";
            resultant = (byte)(val_a | val_b);
        }

        VmLog.WriteLine("CMD_AndOr: {0} A: {1} B: {2} Loc: {3} Val: {4}", sym, val_a, val_b, new MemLoc(loc), resultant);

        ovr008.compare_variables(resultant, 0);
        ovr008.vm_SetMemoryValue(resultant, loc);
    }
}
