using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class InputStringCommand : IGameCommand
{
    private readonly DisplayDriver _displayDriver;
    private readonly ovr008 _ovr008;
    public InputStringCommand(DisplayDriver displayDriver, ovr008 ovr008)
    {
        _displayDriver = displayDriver;
        _ovr008 = ovr008;
    }

    public void Execute()
    {
        _ovr008.vm_LoadCmdSets(2);

        var loc = gbl.cmd_opps[2].Word;

        var str = _displayDriver.getUserInputString(0x28, 0, 10, string.Empty);

        if (str.Length == 0)
        {
            str = " ";
        }

        _ovr008.vm_WriteStringToMemory(str, loc);
    }
}
