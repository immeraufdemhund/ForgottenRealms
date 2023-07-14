using System;
using System.IO;

namespace ForgottenRealms.Engine.Logging;

public static class Config
{
    static string basePath;
    static string logPath;
    static string savePath;

    public static void Setup()
    {
        basePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ForgottenRealms");

        if (Directory.Exists(basePath) == false)
        {
            Directory.CreateDirectory(basePath);
        }

        logPath = Path.Combine(basePath, "Logs");
        if (Directory.Exists(logPath) == false)
        {
            Directory.CreateDirectory(logPath);
        }

        savePath = Path.Combine(basePath, "Save");
        if (Directory.Exists(savePath) == false)
        {
            Directory.CreateDirectory(savePath);
        }

        Logger.Setup(logPath);
    }

    public static string GetLogPath() { return logPath; }
    public static string GetSavePath() { return savePath; }
    public static string GetBasePath() { return basePath; }
}
