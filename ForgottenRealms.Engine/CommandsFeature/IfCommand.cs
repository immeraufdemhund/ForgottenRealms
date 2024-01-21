using System.Collections.Generic;
using ForgottenRealms.Engine.Classes;
using ForgottenRealms.Engine.Logging;

namespace ForgottenRealms.Engine.CommandsFeature;

public class IfCommand : IGameCommand
{
    private readonly Dictionary<int, CmdItem> _commandTable;
    public IfCommand(Dictionary<int, CmdItem> commandTable)
    {
        _commandTable = commandTable;
    }

    public void Execute()
    {
        gbl.ecl_offset++;

        int index = gbl.command - 0x16;
        string[] types = { "==", "!=", "<", ">", "<=", ">=" };

        VmLog.WriteLine("CMD_if: {0} {1}", types[index], gbl.compare_flags[index]);

        if (gbl.compare_flags[index] == false)
        {
            SkipNextCommand();
        }
    }

    private void SkipNextCommand()
    {
        gbl.command = gbl.ecl_ptr[gbl.ecl_offset + 0x8000];

        CmdItem cmd;
        if (_commandTable.TryGetValue(gbl.command, out cmd))
        {
            cmd.Skip();
        }
        else
        {
            Logger.Log("Skipping Unknown command id {0}", gbl.command);
            gbl.ecl_offset += 1;
        }
    }
}
