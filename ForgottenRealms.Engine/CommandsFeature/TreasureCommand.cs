﻿using ForgottenRealms.Engine.Classes;
using ForgottenRealms.Engine.Classes.DaxFiles;
using ForgottenRealms.Engine.Logging;

namespace ForgottenRealms.Engine.CommandsFeature;

public class TreasureCommand : IGameCommand
{
    private readonly DaxFileDecoder _daxFileDecoder;
    private readonly MainGameEngine _mainGameEngine;
    private readonly ovr008 _ovr008;
    private readonly ovr022 _ovr022;
    private readonly ovr024 _ovr024;
    private readonly ovr025 _ovr025;

    public TreasureCommand(DaxFileDecoder daxFileDecoder, MainGameEngine mainGameEngine, ovr008 ovr008, ovr022 ovr022, ovr024 ovr024, ovr025 ovr025)
    {
        _daxFileDecoder = daxFileDecoder;
        _mainGameEngine = mainGameEngine;
        _ovr008 = ovr008;
        _ovr022 = ovr022;
        _ovr024 = ovr024;
        _ovr025 = ovr025;
    }

    public void Execute()
    {
        byte[] data;
        short dataSize;
        ItemType item_type = 0;

        _ovr008.vm_LoadCmdSets(8);

        for (var coin = 0; coin < 7; coin++)
        {
            gbl.pooled_money.SetCoins(coin, _ovr008.vm_GetCmdValue(coin + 1));
        }

        var block_id = (byte)_ovr008.vm_GetCmdValue(8);

        if (block_id < 0x80)
        {
            var filename = string.Format("ITEM{0}.dax", gbl.game_area);
            _daxFileDecoder.LoadDecodeDax(out data, out dataSize, block_id, filename);

            if (dataSize == 0)
            {
                Logger.Log("Unable to find item file: {0}", filename);
                _mainGameEngine.EngineStop();
            }

            for (var offset = 0; offset < dataSize; offset += Item.StructSize)
            {
                gbl.items_pointer.Add(new Item(data, offset));
            }

            data = null;
        }
        else if (block_id != 0xff)
        {
            for (var count = 0; count < block_id - 0x80; count++)
            {
                int var_63 = _ovr024.roll_dice(100, 1);

                if (var_63 >= 1 && var_63 <= 60)
                {
                    int var_64 = _ovr024.roll_dice(100, 1);

                    if ((var_64 >= 1 && var_64 <= 47) ||
                        (var_64 >= 50 && var_64 <= 59))
                    {
                        if (var_64 == 45)
                        {
                            item_type = ItemType.Shield;
                        }
                        else
                        {
                            item_type = (ItemType)var_64;
                        }
                    }
                    else if (var_64 >= 60 && var_64 <= 90)
                    {
                        var_64 = _ovr024.roll_dice(10, 1);

                        if (var_64 >= 1 && var_64 <= 4)
                        {
                            item_type = ItemType.LongSword;
                        }
                        else if (var_64 >= 5 && var_64 <= 7)
                        {
                            item_type = ItemType.BroadSword;
                        }
                        else if (var_64 == 8)
                        {
                            item_type = ItemType.BastardSword;
                        }
                        else if (var_64 == 9)
                        {
                            item_type = ItemType.ShortSword;
                        }
                        else if (var_64 == 10)
                        {
                            item_type = ItemType.TwoHandedSword;
                        }
                    }
                    else if (var_64 >= 91 && var_64 <= 94)
                    {
                        item_type = ItemType.Arrow;
                    }
                    else if (var_64 >= 95 && var_64 <= 97)
                    {
                        item_type = ItemType.RingOfProt;
                    }
                    else if (var_64 >= 98 && var_64 <= 100)
                    {
                        item_type = ItemType.Bracers;
                    }
                    else
                    {
                        item_type = ItemType.Shield;
                    }
                }
                else if (var_63 >= 0x3d && var_63 <= 0x55)
                {
                    item_type = ItemType.MUScroll;
                }
                else if (var_63 >= 0x56 && var_63 <= 0x5C)
                {
                    item_type = ItemType.ClrcScroll;
                }
                else if (var_63 >= 0x5B && var_63 <= 0x62)
                {
                    int var_62 = _ovr024.roll_dice(15, 1);

                    if (var_62 >= 1 && var_62 <= 9)
                    {
                        item_type = ItemType.Potion;
                    }
                    else if (var_62 == 10)
                    {
                        item_type = ItemType.Type_84;
                    }
                    else if (var_62 >= 11 && var_62 <= 15)
                    {
                        item_type = ItemType.WandB;
                    }
                }
                else if (var_63 == 99 || var_63 == 100)
                {
                    item_type = ItemType.Shield;
                }

                gbl.items_pointer.Add(_ovr022.create_item(item_type));
            }

            gbl.items_pointer.ForEach(item => _ovr025.ItemDisplayNameBuild(false, false, 0, 0, item));
        }
    }
}
