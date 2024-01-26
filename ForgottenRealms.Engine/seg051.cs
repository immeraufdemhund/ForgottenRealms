using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine;

public class seg051
{
    private System.Random random_number;

    internal string Copy(int CopyLen, int StartAt, string InString)
    {
        string OutString;

        if (CopyLen >= InString.Length - StartAt)
        {
            CopyLen = InString.Length - StartAt;
        }

        if (CopyLen > 0)
        {
            OutString = InString.Substring(StartAt, CopyLen);
        }
        else
        {
            OutString = string.Empty;
        }

        return OutString;
    }

    internal byte Random(byte arg_0)
    {
        if (arg_0 == 0)
        {
            return 0;
        }

        return (byte)(random_number.Next() % arg_0);
    }

    internal int Random(int arg_0)
    {
        if (arg_0 == 0)
        {
            return 0;
        }

        return random_number.Next() % arg_0;
    }

    internal double Random__Real()
    {
        return random_number.NextDouble();
    }


    internal void Randomize()
    {
        random_number = new System.Random(unchecked((int)System.DateTime.Now.Ticks));
    }


    internal void Reset(File arg_4)
    {
        arg_4.stream.Seek(0, System.IO.SeekOrigin.Begin);
    }

    internal void Rewrite(File arg_2)
    {
        arg_2.stream.SetLength(0);
    }


    internal void Close(File arg_0)
    {
        arg_0.stream.Close();
    }

    internal int BlockRead(int count, byte[] data, File file)
    {
        return file.stream.Read(data, 0, count);
    }


    internal void BlockWrite(int arg_4, byte[] arg_6, File arg_A)
    {
        arg_A.stream.Write(arg_6, 0, arg_4);
    }

    internal void FillChar(byte fill_byte, int buffer_size, byte[] buffer)
    {
        for (int i = 0; i < buffer_size; i++)
        {
            buffer[i] = fill_byte;
        }
    }
}
