using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace ForgottenRealms.Engine.Logging;

public class Config
{
    private static string basePath;
    private static string logPath;
    private static string savePath;

    private readonly ILogger _logger;
    public Config(ILogger<Config> logger)
    {
        _logger = logger;
    }
    public void Setup()
    {
        _logger.LogInformation("setting up config paths");
        basePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ForgottenRealms");
        _logger.LogDebug("Setting base path to {Path}", basePath);
        if (Directory.Exists(basePath) == false)
        {
            _logger.LogInformation("Base path was missing. Creating path {Path}", basePath);
            Directory.CreateDirectory(basePath);
        }

        logPath = Path.Combine(basePath, "Logs");
        _logger.LogDebug("Setting log path to {Path}", logPath);
        if (Directory.Exists(logPath) == false)
        {
            _logger.LogInformation("logPath was missing. Creating path {Path}", logPath);
            Directory.CreateDirectory(logPath);
        }

        savePath = Path.Combine(basePath, "Save");
        _logger.LogDebug("Setting save path to {Path}", savePath);
        if (Directory.Exists(savePath) == false)
        {
            _logger.LogInformation("save Path was missing. Creating path {Path}", savePath);
            Directory.CreateDirectory(savePath);
        }

        Logger.Setup(logPath);
    }

    public static string GetLogPath() { return logPath; }
    public static string GetSavePath() { return savePath; }
    public static string GetBasePath() { return basePath; }
}
