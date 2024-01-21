﻿using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class InputNumberCommand : IGameCommand
{
    public void Execute()
    {
        ovr008.vm_LoadCmdSets(2);

        ushort loc = gbl.cmd_opps[2].Word;

        ushort var_4 = DisplayDriver.getUserInputShort(0, 0x0a, string.Empty);

        ovr008.vm_SetMemoryValue(var_4, loc);
    }
}