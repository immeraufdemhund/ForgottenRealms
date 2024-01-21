using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class InputStringCommand : IGameCommand
{
    public void Execute()
    {
        ovr008.vm_LoadCmdSets(2);

        ushort loc = gbl.cmd_opps[2].Word;

        string str = DisplayDriver.getUserInputString(0x28, 0, 10, string.Empty);

        if (str.Length == 0)
        {
            str = " ";
        }

        ovr008.vm_WriteStringToMemory(str, loc);
    }
}