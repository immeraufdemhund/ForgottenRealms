using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class InputNumberCommand : IGameCommand
{
    private readonly DisplayDriver _displayDriver;
    private readonly ovr008 _ovr008;
    public InputNumberCommand(DisplayDriver displayDriver, ovr008 ovr008)
    {
        _displayDriver = displayDriver;
        _ovr008 = ovr008;
    }

    public void Execute()
    {
        _ovr008.vm_LoadCmdSets(2);

        var loc = gbl.cmd_opps[2].Word;

        var var_4 = _displayDriver.getUserInputShort(0, 0x0a, string.Empty);

        _ovr008.vm_SetMemoryValue(var_4, loc);
    }
}
