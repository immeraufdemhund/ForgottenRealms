using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class HorizontalMenuCommand : IGameCommand
{
    private readonly ovr008 _ovr008;
    private readonly ovr027 _ovr027;
    public HorizontalMenuCommand(ovr008 ovr008, ovr027 ovr027)
    {
        _ovr008 = ovr008;
        _ovr027 = ovr027;
    }

    public void Execute()
    {
        bool useOverlay;
        bool var_3B;

        _ovr008.vm_LoadCmdSets(2);

        var loc = gbl.cmd_opps[1].Word;
        var string_count = (byte)_ovr008.vm_GetCmdValue(2);

        gbl.ecl_offset--;

        _ovr008.vm_LoadCmdSets(string_count);

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

        var text = string.Empty;
        for (var i = 1; i < string_count; i++)
        {
            text += "~" + gbl.unk_1D972[i] + " ";
        }

        text += "~" + gbl.unk_1D972[string_count];

        var menu_selected = (byte)_ovr008.sub_317AA(useOverlay, var_3B, colors, text, "");

        _ovr008.vm_SetMemoryValue(menu_selected, loc);

        _ovr027.ClearPromptAreaNoUpdate();
    }
}
