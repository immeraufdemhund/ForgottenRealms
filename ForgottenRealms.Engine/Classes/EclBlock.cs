using System;

namespace ForgottenRealms.Engine.Classes;

/// <summary>
/// Summary description for EclBlock.
/// </summary>
public class EclBlock
{
    public const ushort ecl_struct_size = 0x1e00;

    private byte[] data;

    public EclBlock()
    {
        data = new byte[ecl_struct_size];
    }

    public EclBlock(byte[] _data, int offset)
    {
        data = new byte[ecl_struct_size];

        Array.Copy(_data, data, ecl_struct_size);
    }

    public void Clear() => Array.Clear(data, 0, ecl_struct_size);

    public byte this[int index]
    {
        get
        {
            // simulate the 16 bit memory space.
            var loc = index & 0xFFFF;
            var value = data[loc];
            //System.Console.WriteLine("     EclBlock[get] loc: {0,4:X} value {1,2:X}", loc, value);

            return value;
        }
        set
        {
            var loc = index & 0xFFFF;
            //System.Console.WriteLine("     EclBlock[set] loc: {0,4:X} value: {1,4:X}", loc, value);

            data[loc] = value;
        }
    }

    public void SetData(byte[] dataArray, int dataOffset, int dataLength) =>
        Array.Copy(dataArray, dataOffset, data, 0, dataLength);

    public byte[] ToByteArray() => (byte[])data.Clone();
}
