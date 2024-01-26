using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class ParlayCommand : IGameCommand
{
    private readonly ovr008 _ovr008;
    public ParlayCommand(ovr008 ovr008)
    {
        _ovr008 = ovr008;
    }

    public void Execute()
    {
        _ovr008.vm_LoadCmdSets(6);

        var values = new byte[5];
        for (var i = 0; i < 5; i++)
        {
            values[i] = (byte)_ovr008.vm_GetCmdValue(i + 1);
        }

        var menu_selected = _ovr008.sub_317AA(false, false, gbl.defaultMenuColors, "~HAUGHTY ~SLY ~NICE ~MEEK ~ABUSIVE", " ");

        var location = gbl.cmd_opps[6].Word;

        var value = values[menu_selected];

        _ovr008.vm_SetMemoryValue(value, location);
    }
}
