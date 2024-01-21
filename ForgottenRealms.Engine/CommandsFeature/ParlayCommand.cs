using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class ParlayCommand : IGameCommand
{
    public void Execute()
    {
        ovr008.vm_LoadCmdSets(6);

        byte[] values = new byte[5];
        for (int i = 0; i < 5; i++)
        {
            values[i] = (byte)ovr008.vm_GetCmdValue(i + 1);
        }

        int menu_selected = ovr008.sub_317AA(false, false, gbl.defaultMenuColors, "~HAUGHTY ~SLY ~NICE ~MEEK ~ABUSIVE", " ");

        ushort location = gbl.cmd_opps[6].Word;

        byte value = values[menu_selected];

        ovr008.vm_SetMemoryValue(value, location);
    }
}