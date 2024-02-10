using ForgottenRealms.Engine.Classes;
using ForgottenRealms.Engine.Classes.DaxFiles;

namespace ForgottenRealms.Engine;

public class ovr038
{
    private readonly seg040 _seg040;
    private readonly KeyboardService _keyboardService;
    private readonly DrawPictureAction _drawPictureAction;
    private readonly DaxBlockReader _daxBlockReader;
    private readonly DaxBlock[] _symbol8X8Set;

    public ovr038(seg040 seg040, KeyboardService keyboardService, DrawPictureAction drawPictureAction, DaxBlockReader daxBlockReader)
    {
        _seg040 = seg040;
        _keyboardService = keyboardService;
        _drawPictureAction = drawPictureAction;
        _daxBlockReader = daxBlockReader;
        _symbol8X8Set = new DaxBlock[5];
    }

    public void LoadTitleScreenSymbols()
    {
        Load8x8D(4, 0xca);
        Load8x8D(0, 0xcb);
    }

    public void LoadWallDefinitionTiles(int block_id, int blockCount, int block, int idx)
    {
        if (blockCount > 1)
        {
            var blockId = (block_id * 10) + block + 1;
            Load8x8D(idx, blockId);
        }
        else
        {
            Load8x8D(idx, block_id);
        }
    }

    private void Load8x8D(int symbolSet, int block_id)
    {
        if (symbolSet >= 0 && symbolSet < 5)
        {
            _symbol8X8Set[symbolSet] = _daxBlockReader.LoadDax(13, 1, block_id, $"8x8d{gbl.game_area}");
            _keyboardService.clear_keyboard();
        }
    }

    internal void Put8x8Symbol(byte arg_0, bool use_overlay, int symbol_id, int rowY, int colX)
    {
        byte symbol_set = 0; /*HACK to make compiler happy*/

        if (symbol_id >= 1 && symbol_id <= 0x2d)
        {
            symbol_set = 0;
        }
        else if (symbol_id >= 0x2E && symbol_id <= 0x73)
        {
            symbol_set = 1;
        }
        else if (symbol_id >= 0x74 && symbol_id <= 0x0B9)
        {
            symbol_set = 2;
        }
        else if (symbol_id >= 0x0BA && symbol_id <= 0x0FF)
        {
            symbol_set = 3;
        }
        else if (symbol_id >= 0x100 && symbol_id <= 0x127)
        {
            symbol_set = 4;
        }
        else if (symbol_id == 0 || (symbol_id >= 0x128 && symbol_id <= 0x7FFF))
        {
            throw new System.ApplicationException("Bad symbol number in Put8x8Symbol." + symbol_id);
        }

        if (_symbol8X8Set[symbol_set] == null) return;

        symbol_id -= gbl.symbol_set_fix[symbol_set];

        if (use_overlay)
        {
            _seg040.OverlayUnbounded(_symbol8X8Set[symbol_set], arg_0, symbol_id, rowY, colX);
        }
        else
        {
            DaxBlock var_6 = _symbol8X8Set[symbol_set];

            int offset = symbol_id * var_6.bpp;
            System.Array.Copy(var_6.data, offset, gbl.cursor_bkup.data, 0, var_6.bpp);

            _drawPictureAction.DrawPicture(gbl.cursor_bkup, rowY, colX, 0);
        }
    }
}
