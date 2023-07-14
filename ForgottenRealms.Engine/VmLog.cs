using ForgottenRealms.Engine.Classes;
using ForgottenRealms.Engine.Logging;

namespace ForgottenRealms.Engine;

internal static class VmLog
{
    internal static void Write(string fmt, params object[] args)
    {
        if (gbl.printCommands == true)
        {
            Logger.DebugWrite(fmt, args);
        }
    }

    internal static void WriteLine(string fmt, params object[] args)
    {
        if (gbl.printCommands == true)
        {
            Logger.Debug(fmt, args);
        }
    }
}
