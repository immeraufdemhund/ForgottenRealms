using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class HorizontalMenuCommand : IGameCommand
{
    public void Execute()
    {
        bool useOverlay;
        bool var_3B;

        ovr008.vm_LoadCmdSets(2);

        ushort loc = gbl.cmd_opps[1].Word;
        byte string_count = (byte)ovr008.vm_GetCmdValue(2);

        gbl.ecl_offset--;

        ovr008.vm_LoadCmdSets(string_count);

        MenuColorSet colors;
        if (string_count == 1)
        {
            var_3B = true;
            colors = new MenuColorSet(15, 15, 13);

            if (gbl.unk_1D972[1] == "PRESS BUTTON OR RETURN TO CONTINUE.")
            {
                gbl.unk_1D972[1] = "PRESS <ENTER>/<RETURN> TO CONTINUE";
            }
        }
        else
        {
            colors = new MenuColorSet(1, 15, 15);
            var_3B = false;
            colors = gbl.defaultMenuColors;
        }

        if (gbl.spriteChanged == false ||
            gbl.byte_1EE8D == false)
        {
            useOverlay = false;
        }
        else
        {
            useOverlay = true;
        }

        string text = string.Empty;
        for (int i = 1; i < string_count; i++)
        {
            text += "~" + gbl.unk_1D972[i] + " ";
        }

        text += "~" + gbl.unk_1D972[string_count];

        byte menu_selected = (byte)ovr008.sub_317AA(useOverlay, var_3B, colors, text, "");

        ovr008.vm_SetMemoryValue(menu_selected, loc);

        ovr027.ClearPromptAreaNoUpdate();
    }
}