using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class PrintReturnCommand : IGameCommand
{
    public void Execute()
    {
        gbl.ecl_offset++;

        VmLog.WriteLine("CMD_PrintReturn:");

        gbl.textXCol = 1;
        gbl.textYCol++;
    }
}
