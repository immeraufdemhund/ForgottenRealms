using ForgottenRealms.Engine.Classes;
using ForgottenRealms.Engine.Classes.Combat;
using ForgottenRealms.Engine.Classes.DaxFiles;
using ForgottenRealms.Engine.Logging;

namespace ForgottenRealms.Engine;

public class ovr034
{
    private byte[] unk_16E30 = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }; // seg600:0B20
    private byte[] unk_16E40 = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }; // seg600:0B30
    private byte[] unk_16E50 = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 0, 14, 15 }; // seg600:0B40

    private readonly seg040 _seg040;
    private readonly seg051 _seg051;
    private readonly KeyboardService _keyboardService;
    private readonly DaxBlockReader _daxBlockReader;
    private readonly MainGameEngine _mainGameEngine;

    public ovr034(seg040 seg040, seg051 seg051, KeyboardService keyboardService, DaxBlockReader daxBlockReader, MainGameEngine mainGameEngine)
    {
        _seg040 = seg040;
        _seg051 = seg051;
        _keyboardService = keyboardService;
        _daxBlockReader = daxBlockReader;
        _mainGameEngine = mainGameEngine;
    }

    internal void Load24x24Set(int cellCount, int destCellOffset, int block_id, string fileName)
    {
        if (destCellOffset > 0x30)
        {
            Logger.Log("Start range error in Load24x24Set. {0}", destCellOffset);
            _mainGameEngine.EngineStop();
        }

        DaxBlock tmp_block = _daxBlockReader.LoadDax(0, 0, block_id, fileName);

        int dateLength = cellCount * tmp_block.bpp;
        int destByteOffset = destCellOffset * tmp_block.bpp;

        if (gbl.dax24x24Set != null)
        {
            System.Array.Copy(tmp_block.data, 0, gbl.dax24x24Set.data, destByteOffset, dateLength);
        }

        _keyboardService.clear_keyboard();
    }


    internal void DrawIsoTile(int tileIndex, int rowY, int colX) /* sub_760F7 */
    {
        if (tileIndex > 0x7f)
        {
            _seg040.OverlayUnbounded(gbl.dword_1C8FC, tileIndex, tileIndex & 0x7F, rowY, colX);
        }
        else
        {
            _seg040.OverlayUnbounded(gbl.dax24x24Set, 0, tileIndex, rowY, colX);
        }
    }


    internal void ReleaseCombatIcon(int index) // free_icon
    {
        gbl.combat_icons[index].Release();
    }

    internal void chead_cbody_comspr_icon(byte combat_icon_index, int block_id, string fileText)
    {
        string file_text = fileText;

        string sub = _seg051.Copy(5, 0, file_text);
        if (sub == "CHEAD" ||
            sub == "CBODY")
        {
            if (char.ToUpper(file_text[file_text.Length - 1]) == 'T')
            {
                block_id += 0x40;
            }

            file_text = _seg051.Copy(file_text.Length - 1, 0, file_text);

            gbl.combat_icons[combat_icon_index].LoadIcons(0, 1, file_text, block_id, block_id + 0x80);
        }
        else if (file_text == "COMSPR" || file_text == "ICON")
        {
            gbl.combat_icons[combat_icon_index].LoadIcons(0, 1, file_text, block_id, block_id + 0x80);

            if (file_text == "ICON")
            {
                gbl.combat_icons[combat_icon_index].Recolor(false, unk_16E50, unk_16E30);
            }
        }
        else
        {
            file_text += gbl.game_area.ToString();

            gbl.combat_icons[combat_icon_index].LoadIcons(0, 1, file_text, block_id, block_id + 0x80);
            gbl.combat_icons[combat_icon_index].Recolor(false, unk_16E40, unk_16E30);
        }

        _keyboardService.clear_keyboard();
    }


    internal void draw_combat_icon(int iconIndex, Icon iconState, int direction, int tileY, int tileX) /* sub_76504 */
    {
        DaxBlock icon = gbl.combat_icons[iconIndex].GetIcon(iconState, direction);

        if (icon != null)
        {
            _seg040.draw_combat_picture(icon, (tileY * 3) + 1, (tileX * 3) + 1, 0);
        }
    }
}
