using System;
using System.IO;

namespace ForgottenRealms.Engine.Classes;

/// <summary>
/// Summary description for Text.
/// </summary>
public class Text
{
    public ushort field_2;


    public byte field_30 = 0;

    public object field_80;

    public TextReader reader;
    public TextWriter writer;

    public Text()
    {
        reader = Console.In;
        writer = Console.Out;
    }

    public enum AssignType
    {
        Read,
        Write,
    }

    private AssignType _type;

    public void Assign(string s, AssignType type)
    {
        _type = type;

        if (s != string.Empty)
        {
            if (type == AssignType.Read)
            {
                reader = new StreamReader(s);
            }
            else
            {
                writer = new StreamWriter(s);
            }
        }

        field_2 = 0xD7B0;
    }
}
