using System;

namespace ForgottenRealms.Engine;

internal class MemLoc
{
    private ushort loc;
    internal MemLoc(ushort _loc)
    {
        loc = _loc;
    }

    public override string ToString()
    {
        return String.Format("0x{0:X}", loc);
    }
}
