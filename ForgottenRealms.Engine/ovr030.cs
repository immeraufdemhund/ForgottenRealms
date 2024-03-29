using ForgottenRealms.Engine.Classes;
using ForgottenRealms.Engine.Classes.DaxFiles;
using ForgottenRealms.Engine.Logging;

namespace ForgottenRealms.Engine;

public class ovr030
{
    private byte[] fadeOldColors = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
    private byte[] fadeNewColors = { 12, 12, 12, 12, 4, 5, 6, 7, 12, 12, 10, 12, 12, 12, 14, 12 };
    private byte[] transparentOldColors = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
    private byte[] transparentNewColors = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 0, 14, 15 };

    private readonly ovr027 _ovr027;
    private readonly seg037 _seg037;
    private readonly seg040 _seg040;
    private readonly DisplayDriver _displayDriver;
    private readonly KeyboardService _keyboardService;
    private readonly DrawPictureAction _drawPictureAction;
    private readonly DaxFileDecoder _daxFileDecoder;
    private readonly DaxBlockReader _daxBlockReader;
    private readonly MainGameEngine _mainGameEngine;

    public ovr030(ovr027 ovr027, seg037 seg037, seg040 seg040, DisplayDriver displayDriver, KeyboardService keyboardService, DrawPictureAction drawPictureAction, DaxFileDecoder daxFileDecoder, DaxBlockReader daxBlockReader, MainGameEngine mainGameEngine)
    {
        _ovr027 = ovr027;
        _seg037 = seg037;
        _seg040 = seg040;
        _displayDriver = displayDriver;
        _keyboardService = keyboardService;
        _drawPictureAction = drawPictureAction;
        _daxFileDecoder = daxFileDecoder;
        _daxBlockReader = daxBlockReader;
        _mainGameEngine = mainGameEngine;
    }

    internal void DrawMaybeOverlayed(DaxBlock dax_block, bool useOverlay, int rowY, int colX)// sub_7000A
    {
        if (dax_block != null)
        {
            if (gbl.area_ptr.picture_fade > 0 || useOverlay == true)
            {
                if (gbl.area_ptr.picture_fade > 0)
                {
                    dax_block.Recolor(true, fadeNewColors, fadeOldColors);
                }

                _seg040.OverlayBounded(dax_block, 0, 0, rowY - 1, colX - 1);
                _seg040.DrawOverlay();
            }
            else
            {
                _drawPictureAction.DrawPicture(dax_block, rowY, colX, 0);
            }
        }
    }


    internal void load_pic_final(ref DaxArray daxArray, byte masked, byte block_id, string file_name)
    {
        if (file_name != gbl.lastDaxFile ||
            block_id != gbl.lastDaxBlockId)
        {
            if (block_id != 0xff)
            {
                if (gbl.AnimationsOn)
                {
                    _ovr027.ClearPromptAreaNoUpdate();
                    _displayDriver.displayString("Loading...Please Wait", 0, 10, 0x18, 0);
                }

                DaxArrayFreeDaxBlocks(daxArray);

                gbl.lastDaxFile = file_name;
                gbl.lastDaxBlockId = block_id;

                bool is_pic_or_final = (file_name == "PIC" || file_name == "FINAL");

                short uncompressed_size;
                byte[] uncompressed_data;

                _daxFileDecoder.LoadDecodeDax(out uncompressed_data, out uncompressed_size, block_id, file_name + gbl.game_area.ToString() + ".dax");

                if (uncompressed_size == 0)
                {
                    _displayDriver.DisplayAndPause("PIC not found", 14);
                }
                else
                {
                    int src_offset = 0;

                    daxArray.numFrames = uncompressed_data[src_offset];
                    src_offset++;
                    daxArray.curFrame = 1;

                    byte frames_count = 0; // kind of pointless...

                    if (gbl.AnimationsOn == false && is_pic_or_final == true)
                    {
                        daxArray.numFrames = 1;
                    }

                    byte[] first_frame_ega_layout = null;

                    for (int frame = 0; frame < daxArray.numFrames; frame++)
                    {
                        daxArray.frames[frame].delay = Sys.ArrayToInt(uncompressed_data, src_offset);
                        src_offset += 4;

                        short height = Sys.ArrayToShort(uncompressed_data, src_offset);
                        src_offset += 2;

                        short width = Sys.ArrayToShort(uncompressed_data, src_offset);
                        src_offset += 2;

                        frames_count++;

                        daxArray.frames[frame].picture = new DaxBlock(masked, 1, width, height);

                        DaxBlock dax_block = daxArray.frames[frame].picture;

                        dax_block.x_pos = Sys.ArrayToShort(uncompressed_data, src_offset);
                        src_offset += 2;

                        dax_block.y_pos = Sys.ArrayToShort(uncompressed_data, src_offset);
                        src_offset += 3;

                        System.Array.Copy(uncompressed_data, src_offset, dax_block.field_9, 0, 8);
                        src_offset += 8;

                        int ega_encoded_size = (daxArray.frames[frame].picture.bpp / 2) - 1;

                        if (is_pic_or_final == true)
                        {
                            if (frame == 0)
                            {
                                first_frame_ega_layout = new byte[ega_encoded_size + 1];

                                System.Array.Copy(uncompressed_data, src_offset, first_frame_ega_layout, 0, ega_encoded_size + 1);
                            }
                            else
                            {
                                for (int i = 0; i < ega_encoded_size; i++)
                                {
                                    byte b = first_frame_ega_layout[i];
                                    uncompressed_data[src_offset + i] ^= b;
                                }
                            }
                        }

                        daxArray.frames[frame].picture.DaxToPicture(0, masked, src_offset, uncompressed_data);

                        if ((masked & 1) > 0)
                        {
                            daxArray.frames[frame].picture.Recolor(false, transparentNewColors, transparentOldColors);
                        }

                        src_offset += ega_encoded_size + 1;
                    }

                    daxArray.numFrames = frames_count; // also pointless

                    uncompressed_data = null;
                    _keyboardService.clear_keyboard();

                    if (gbl.AnimationsOn)
                    {
                        _ovr027.ClearPromptAreaNoUpdate();
                    }
                }
            }
        }
    }


    internal void DaxArrayFreeDaxBlocks(DaxArray animation)
    {
        for (int index = 0; index < animation.numFrames; index++)
        {
            animation.frames[index].picture = null;
            animation.frames[index].delay = 0;
        }

        animation.numFrames = 0;
        animation.curFrame = 0;

        gbl.lastDaxFile = string.Empty;
        gbl.lastDaxBlockId = 0x0FF;
    }


    internal void head_body(byte body_id, byte head_id)
    {
        string text = gbl.game_area.ToString();

        if (head_id != 0xff &&
            (gbl.current_head_id == 0xff || gbl.current_head_id != head_id))
        {
            gbl.headX_dax = _daxBlockReader.LoadDax(0, 0, head_id, "HEAD" + text);

            if (gbl.headX_dax == null)
            {
                _displayDriver.DisplayAndPause("head not found", 14);
            }

            gbl.current_head_id = head_id;
        }

        if (body_id != 0xff &&
            (gbl.current_body_id == 0xff || gbl.current_body_id != body_id))
        {
            gbl.bodyX_dax = _daxBlockReader.LoadDax(0, 0, body_id, "BODY" + text);
            if (gbl.bodyX_dax == null)
            {
                _displayDriver.DisplayAndPause("body not found", 14);
            }

            gbl.current_body_id = body_id;
        }

        _keyboardService.clear_keyboard();
    }


    internal void draw_head_and_body(bool draw_body, byte rowY, byte colX) /* sub_706DC */
    {
        if (draw_body == true)
        {
            DrawMaybeOverlayed(gbl.headX_dax, false, rowY, colX);
            DrawMaybeOverlayed(gbl.bodyX_dax, false, rowY + 5, colX);
        }
        else
        {
            DrawMaybeOverlayed(gbl.headX_dax, false, rowY, colX);
        }
    }


    internal void Show3DSprite(DaxArray arg_0, int sprite_index)
    {
        if (sprite_index < 1 || sprite_index > 3)
        {
            Logger.Log("Illegal range in Show3DSprite. {0}", sprite_index);
            _mainGameEngine.EngineStop();
        }

        if (arg_0.frames[sprite_index - 1].picture != null)
        {
            DaxBlock block = arg_0.frames[sprite_index - 1].picture;
            _seg040.OverlayBounded(arg_0.frames[sprite_index - 1].picture, 1, 0, block.y_pos + 3 - 1, block.x_pos + 3 - 1);
            _seg040.DrawOverlay();
        }
    }


    internal void load_bigpic(byte block_id) /* bigpic */
    {
        DaxArrayFreeDaxBlocks(gbl.byte_1D556);

        if (gbl.bigpic_block_id != block_id)
        {
            gbl.bigpic_dax = _daxBlockReader.LoadDax(0, 0, block_id, "bigpic" + gbl.game_area.ToString());
            gbl.bigpic_block_id = block_id;
        }
    }


    internal void draw_bigpic() /* sub_7087A */
    {
        _seg037.DrawFrame_WildernessMap();
        _drawPictureAction.DrawPicture(gbl.bigpic_dax, 1, 1, 0);
    }
}
