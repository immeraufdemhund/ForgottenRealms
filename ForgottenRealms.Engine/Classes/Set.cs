namespace ForgottenRealms.Engine.Classes;

/// <summary>
/// Summary description for Set.
/// </summary>
public class Set
{
    private byte[] bits;
    private const uint arrayLen = 0x20;
    private const int shift = 3;
    private const int mask = 7;

    public Set()
    {
        bits = new byte[arrayLen];
    }


    public Set(ushort arg_0, byte[] arg_2)
    {
        bits = new byte[arrayLen];

        var indA = arg_0 >> 8;
        var indB = arg_0 & 0x00ff;

        for (var i = 0; i < indA; i++)
        {
            bits[i] = 0;
        }

        for (var i = 0; i < indB; i++)
        {
            bits[i + indA] = arg_2[i];
        }

        for (var i = indA + indB; i < 20; i++)
        {
            bits[i] = 0;
        }

        DumpSet();
        var z = 0;
    }

    public Set(params int[] toset)
    {
        bits = new byte[arrayLen];

        foreach (var b in toset)
        {
            SetBit(b);
        }
    }


    public void DumpSet()
    {
        var sb = new System.Text.StringBuilder();

        for (var i = 0; i < 256; i++)
        {
            if (MemberOf(i))
            {
                sb.AppendFormat("{0}, ", i);
            }
        }

        var s = sb.ToString();
    }

    public void Clear()
    {
        for (var i = 0; i < arrayLen; i++)
        {
            bits[i] = 0;
        }
    }

    public static Set operator +(Set lhs, byte rhs)
    {
        lhs.bits[rhs >> shift] |= (byte)(1 << (rhs & mask));

        return lhs;
    }

    public void SetRange(byte arg_0, byte arg_2)
    {
        //public static void Set__operator+=( byte arg_0, byte arg_2, int arg_4 )
        //Set::operator+=(Byte,Byte)
        //original left the Set object on the stack.

        for (var i = arg_2; i <= arg_0; i++)
        {
            bits[i >> shift] |= (byte)(1 << (i & mask));
        }
    }

    private void SetBit(int i) => bits[i >> shift] |= (byte)(1 << (i & mask));

    public bool MemberOf(int bit)
    {
        int lhs = bits[bit >> shift];
        var rhs = 1 << (bit & mask);

        return (lhs & rhs) != 0;
    }
}
