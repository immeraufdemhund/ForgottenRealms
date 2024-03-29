﻿using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class PrintCommand : IGameCommand
{
    private readonly DisplayDriver _displayDriver;
    private readonly ovr008 _ovr008;
    public void Execute()
    {
        _ovr008.vm_LoadCmdSets(1);

        VmLog.WriteLine("CMD_Print: '{0}'",
            gbl.cmd_opps[1].Code < 0x80 ? _ovr008.vm_GetCmdValue(1).ToString() : gbl.unk_1D972[1]);

        gbl.bottomTextHasBeenCleared = false;
        gbl.DelayBetweenCharacters = true;

        if (gbl.cmd_opps[1].Code < 0x80)
        {
            gbl.unk_1D972[1] = _ovr008.vm_GetCmdValue(1).ToString();
        }

        if (gbl.command == 0x11)
        {
            _displayDriver.press_any_key(gbl.unk_1D972[1], false, 10, TextRegion.NormalBottom);
        }
        else
        {
            gbl.textYCol = 0x11;
            gbl.textXCol = 1;

            _displayDriver.press_any_key(gbl.unk_1D972[1], true, 10, TextRegion.NormalBottom);
        }

        gbl.DelayBetweenCharacters = false;
    }
}
