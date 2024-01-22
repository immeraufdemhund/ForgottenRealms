using System;
using System.IO;
namespace ForgottenRealms.Engine.Logging;

[Obsolete]
public class Logger
{
    private static DebugWriter debug;

    public static void Setup(string path)
    {
        debug = new DebugWriter(Path.Combine(path, "Debugging.txt"));
    }

    public static void Log(string fmt, params object[] args)
    {
        System.Console.WriteLine(fmt, args);
        debug.WriteLine(fmt, args);
    }

    public static void Close()
    {
        debug.Close();
    }

    public static void Debug(string fmt, params object[] args)
    {
        System.Console.WriteLine(fmt, args);
        debug.WriteLine(fmt, args);
    }

    public static void DebugWrite(string fmt, params object[] args)
    {
        System.Console.Write(fmt, args);
        debug.Write(fmt, args);
    }
}
