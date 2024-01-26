using System.Collections.Generic;
using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class VerticalMenuCommand : IGameCommand
{
    private readonly DisplayDriver _displayDriver;
    private readonly ovr008 _ovr008;
    private readonly seg037 _seg037;

    public VerticalMenuCommand(DisplayDriver displayDriver, ovr008 ovr008, seg037 seg037)
    {
        _displayDriver = displayDriver;
        _ovr008 = ovr008;
        _seg037 = seg037;
    }

    public void Execute()
    {
        gbl.bottomTextHasBeenCleared = false;

        _ovr008.vm_LoadCmdSets(3);
        var mem_loc = gbl.cmd_opps[1].Word;

        var delay_text = gbl.unk_1D972[1];

        var menuCount = (byte)_ovr008.vm_GetCmdValue(3);
        gbl.ecl_offset--;
        _ovr008.vm_LoadCmdSets(menuCount);

        List<MenuItem> menuList = new();

        gbl.textXCol = 1;
        gbl.textYCol = 0x11;

        _displayDriver.press_any_key(delay_text, true, 10, 22, 38, 17, 1);

        for (var i = 0; i < menuCount; i++)
        {
            menuList.Add(new MenuItem(gbl.unk_1D972[i + 1]));
        }

        var index = _ovr008.VertMenuSelect(0, true, false, menuList, 0x16, 0x26, gbl.textYCol + 1, 1);

        _ovr008.vm_SetMemoryValue((ushort)index, mem_loc);

        menuList.Clear();
        _seg037.draw8x8_clear_area(TextRegion.NormalBottom);
    }
}
