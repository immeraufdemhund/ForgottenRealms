using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class DelayCommand : IGameCommand
{
    public void Execute()
    {
        gbl.ecl_offset++;
        DisplayDriver.GameDelay();
    }
}
