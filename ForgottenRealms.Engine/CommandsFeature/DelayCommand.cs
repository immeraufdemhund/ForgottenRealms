using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class DelayCommand : IGameCommand
{
    private readonly DisplayDriver _displayDriver;
    public DelayCommand(DisplayDriver displayDriver)
    {
        _displayDriver = displayDriver;
    }

    public void Execute()
    {
        gbl.ecl_offset++;
        _displayDriver.GameDelay();
    }
}
