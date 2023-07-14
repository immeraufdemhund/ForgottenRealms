using System;
using System.Reflection;
using ForgottenRealms.Engine.Logging;

namespace ForgottenRealms.Engine.Classes;

public class DataIO
{
    public static ushort GetObjectUShort(object obj, byte[] data, int location)
    {
        var type = obj.GetType();

        // Iterate through all the fields of the class.
        foreach (var fInfo in type.GetFields())
        {
            var doAttr = (DataOffsetAttribute)Attribute.GetCustomAttribute(fInfo, typeof(DataOffsetAttribute));
            if (doAttr != null && doAttr.Offset == location)
            {
                Logger.Debug("GetObjectUShort {0}.{1}", obj, fInfo.Name);
                return GetObjectUshortValue(obj, fInfo, doAttr);
            }
        }

        var val = Sys.ArrayToUshort(data, location);
        Logger.Debug("GetObjectUShort {0} at {1,4:X} val: {2:X}", obj, location, val);
        return val;
    }

    public static void SetObjectUShort(object obj, byte[] data, int location, ushort value)
    {
        var type = obj.GetType();

        // Iterate through all the fields of the class.
        foreach (var fInfo in type.GetFields())
        {
            var doAttr = (DataOffsetAttribute)Attribute.GetCustomAttribute(fInfo, typeof(DataOffsetAttribute));
            if (doAttr != null && doAttr.Offset == location)
            {
                Logger.Debug("SetObjectUShort {0}.{1}", obj, fInfo.Name);
                SetObjectUshortValue(obj, fInfo, doAttr, value);
                return;
            }
        }

        Sys.ShortToArray((short)value, data, location);
        Logger.Debug("SetObjectUShort {0} at {1,4:X} val: {2:X}", obj, location, value);
    }

    private static ushort GetObjectUshortValue(object obj, FieldInfo fInfo, DataOffsetAttribute attr)
    {
        var o = fInfo.GetValue(obj);
        switch (attr.Type)
        {
            case DataType.SByte:
                return (ushort)(sbyte)o;
            case DataType.Byte:
                return (ushort)(byte)o;
            case DataType.SWord:
                return (ushort)(short)o;
            case DataType.Word:
                return (ushort)o;
            case DataType.Bool:
                return (bool)o ? (ushort)1 : (ushort)0;
            default:
                throw new NotImplementedException();
            //return 0;
        }
    }

    private static void SetObjectUshortValue(object obj, FieldInfo fInfo, DataOffsetAttribute attr, ushort value)
    {
        //object o = fInfo.GetValue(obj);
        switch (attr.Type)
        {
            case DataType.SByte:
                fInfo.SetValue(obj, (sbyte)value);
                break;
            case DataType.SWord:
                fInfo.SetValue(obj, (short)value);
                break;
            case DataType.Word:
                fInfo.SetValue(obj, value);
                break;
            case DataType.Byte:
                fInfo.SetValue(obj, (byte)value);
                break;
            case DataType.Bool:
                fInfo.SetValue(obj, value != 0);
                break;


            default:
                throw new NotImplementedException();
        }
    }

    public static void ReadObject(object obj, byte[] data, int offset)
    {
        var type = obj.GetType();

        // Iterate through all the fields of the class.
        foreach (var fInfo in type.GetFields())
        {
            // Iterate through all the Attributes for each Field.
            foreach (var attr in
                     Attribute.GetCustomAttributes(fInfo))
            {
                // Check for the DataOffset attribute.
                if (attr.GetType() == typeof(DataOffsetAttribute))
                {
                    var doAttr = (DataOffsetAttribute)attr;

                    readData(obj, fInfo, doAttr, data, offset);
                }
            }
        }
    }


    public static void WriteObject(object obj, byte[] data)
    {
        var type = obj.GetType();

        // Iterate through all the fields of the class.
        foreach (var fInfo in type.GetFields())
        {
            // Iterate through all the Attributes for each Field.
            foreach (var attr in
                     Attribute.GetCustomAttributes(fInfo))
            {
                // Check for the DataOffset attribute.
                if (attr.GetType() == typeof(DataOffsetAttribute))
                {
                    var doAttr = (DataOffsetAttribute)attr;

                    writeData(obj, fInfo, doAttr, data);
                }
            }
        }
    }

    private static void writeData(object obj, FieldInfo fInfo, DataOffsetAttribute attr, byte[] data)
    {
        var o = fInfo.GetValue(obj);
        var idata = o as IDataIO;

        if (idata != null)
        {
            idata.Write(data, attr.Offset);
        }
        else
        {
            switch (attr.Type)
            {
                case DataType.SByte:
                    data[attr.Offset] = (byte)(sbyte)o;
                    break;

                case DataType.IByte:
                    data[attr.Offset] = (byte)(int)o;
                    break;

                case DataType.Byte:
                    data[attr.Offset] = (byte)o;
                    break;

                case DataType.SWord:
                {
                    var s = (short)o;
                    data[attr.Offset] = (byte)(s & 0xff);
                    data[attr.Offset + 1] = (byte)((s >> 8) & 0xff);
                }
                    break;

                case DataType.Word:
                {
                    var s = (ushort)o;
                    data[attr.Offset] = (byte)(s & 0xff);
                    data[attr.Offset + 1] = (byte)((s >> 8) & 0xff);
                }
                    break;

                case DataType.Int:
                {
                    var i = (int)o;
                    var offset = attr.Offset;
                    data[offset++] = (byte)(i & 0xff);
                    data[offset++] = (byte)((i >> 8) & 0xff);
                    data[offset++] = (byte)((i >> 16) & 0xff);
                    data[offset++] = (byte)((i >> 24) & 0xff);
                }
                    break;

                case DataType.Bool:
                    data[attr.Offset] = (bool)o ? (byte)1 : (byte)0;
                    break;

                case DataType.ByteArray:
                {
                    var values = (byte[])o;

                    Array.Copy(values, 0, data, attr.Offset, attr.Size);
                }
                    break;

                case DataType.ShortArray:
                {
                    var values = (short[])o;
                    var offset = attr.Offset;
                    for (var i = 0; i < attr.Size; i++)
                    {
                        var s = values[i];
                        data[offset++] = (byte)(s & 0xff);
                        data[offset++] = (byte)((s >> 8) & 0xff);
                    }
                }
                    break;

                case DataType.WordArray:
                {
                    var values = (ushort[])o;
                    var offset = attr.Offset;
                    for (var i = 0; i < attr.Size; i++)
                    {
                        var s = values[i];
                        data[offset++] = (byte)(s & 0xff);
                        data[offset++] = (byte)((s >> 8) & 0xff);
                    }
                }
                    break;

                case DataType.PString:
                {
                    var b = (byte)attr.Size;
                    var s = (string)o;
                    if (s.Length < b)
                    {
                        b = (byte)s.Length;
                    }

                    data[attr.Offset] = b;
                    for (var i = 0; i < b; i++)
                    {
                        data[attr.Offset + i + 1] = (byte)s[i];
                    }
                }
                    break;

                case DataType.Cust1Array:
                {
                    var i = attr.Offset;
                    var sv = (StatValue[])o;
                    foreach (var v in sv)
                    {
                        data[i++] = (byte)v.cur;
                        data[i++] = (byte)v.full;
                    }
                }
                    break;

                default:
                    throw new NotImplementedException();
            }
        }
    }

    private static void readData(object obj, FieldInfo fInfo, DataOffsetAttribute attr, byte[] data, int offset)
    {
        offset += attr.Offset;
        var idata = fInfo.GetValue(obj) as IDataIO;

        if (idata != null)
        {
            idata.Read(data, offset);
        }
        else
        {
            switch (attr.Type)
            {
                case DataType.SByte:
                    fInfo.SetValue(obj, (sbyte)data[offset]);
                    break;

                case DataType.IByte:
                    fInfo.SetValue(obj, data[offset]);
                    break;

                case DataType.Byte:
                    fInfo.SetValue(obj, data[offset]);
                    break;

                case DataType.SWord:
                {
                    var s = (short)(data[offset] + (data[offset + 1] << 8));
                    fInfo.SetValue(obj, s);
                }
                    break;

                case DataType.Word:
                {
                    var w = (ushort)(data[offset] + (data[offset + 1] << 8));
                    fInfo.SetValue(obj, w);
                }
                    break;

                case DataType.Int:
                {
                    var i = data[offset] + (data[offset + 1] << 8) + (data[offset + 2] << 16) +
                            (data[offset + 3] << 24);
                    fInfo.SetValue(obj, i);
                }
                    break;

                case DataType.Bool:
                    fInfo.SetValue(obj, data[offset] != 0);
                    break;

                case DataType.ByteArray:
                {
                    var a = new byte[attr.Size];

                    Array.Copy(data, offset, a, 0, attr.Size);
                    fInfo.SetValue(obj, a);
                }
                    break;

                case DataType.ShortArray:
                {
                    var a = new short[attr.Size];
                    for (var i = 0; i < attr.Size; i++)
                    {
                        a[i] = (short)(data[offset + i * 2] + (data[offset + 1 + i * 2] << 8));
                    }

                    fInfo.SetValue(obj, a);
                }
                    break;

                case DataType.WordArray:
                {
                    var a = new ushort[attr.Size];
                    for (var i = 0; i < attr.Size; i++)
                    {
                        a[i] = (ushort)(data[offset + i * 2] + (data[offset + 1 + i * 2] << 8));
                    }

                    fInfo.SetValue(obj, a);
                }
                    break;

                case DataType.PString:
                {
                    fInfo.SetValue(obj, Sys.ArrayToString(data, offset, attr.Size));
                }
                    break;

                case DataType.Cust1Array:
                {
                    var sv = new StatValue[attr.Size];
                    for (var i = 0; i < attr.Size; i++)
                    {
                        sv[i].cur = data[offset++];
                        sv[i].full = data[offset++];
                    }

                    fInfo.SetValue(obj, sv);
                }
                    break;

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
