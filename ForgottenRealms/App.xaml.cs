using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using ForgottenRealms.Engine;
using ForgottenRealms.Engine.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace ForgottenRealms;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private void App_OnStartup(object sender, StartupEventArgs e)
    {
        ConfigureDependencyInjection();
        _logger = _provider.Services.GetRequiredService<ILogger<App>>();
        _logger.LogDebug("Setting up Config");
        var config = _provider.Services.GetRequiredService<Config>();
        config.Setup();
        Logger.Setup(Config.GetLogPath());
        _logger.LogDebug("Starting DnD Engine");
        _mainGameEngine = _provider.Services.GetRequiredService<MainGameEngine>();
        cancellationTokenSource = _provider.Services.GetRequiredService<CancellationTokenSource>();
        StartEngine();
        var mainWindow = _provider.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

    private void ConfigureDependencyInjection()
    {
        var settings = new HostApplicationBuilderSettings
        {
            ApplicationName = "ForgottenRealms",
            ContentRootPath = Directory.GetCurrentDirectory(),
            #if DEBUG
            EnvironmentName = "Development",
            #else
            EnvironmentName = "Production",
            #endif
        };
        var builder = Host.CreateEmptyApplicationBuilder(settings);
        builder.Logging
            .AddSimpleConsole(o =>
            {
                o.ColorBehavior = LoggerColorBehavior.Enabled;
            })
            .AddApplicationInsights()
            .AddFilter("*", LogLevel.Trace);

        builder.Services
            .AddTransient<MainWindow>()
            .AddTransient<System.Media.SoundPlayer>()
            .AddSingleton<ISoundDevice, WpfSoundDevice>()
            .AddSingleton<CancellationTokenSource>()
            .RegisterEngineFeature()
            .BuildServiceProvider();
        _provider = builder.Build();
    }

    private CancellationTokenSource? cancellationTokenSource;
    private IHost _provider;
    private ILogger? _logger;
    private MainGameEngine? _mainGameEngine;

    private async void StartEngine()
    {
        _logger!.LogDebug("Starting Engine");
        try
        {
            await Task.Run(EngineThread, cancellationTokenSource!.Token);
            _mainGameEngine!.EngineStop();
            _logger!.LogDebug("After thread");
        }
        catch (TaskCanceledException)
        {
            _logger!.LogInformation("Engine Thread Cancelled");
            _mainGameEngine!.EngineStop();
            EngineStopped();
        }

        _logger!.LogInformation("Engine stopping");
    }

    private void EngineThread()
    {
        _logger!.LogDebug("Engine Thread Started");
        _mainGameEngine!.PROGRAM();
        EngineStopped();
        _logger!.LogInformation("Engine Thread Stopped");
    }

    private void EngineStopped()
    {
        Current.Dispatcher.Invoke(() =>
        {
            Current.Shutdown();
        });
    }
    private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        _logger?.LogCritical(e.Exception, "Unhandled Exception");

        var logFile = Path.Combine(Config.GetLogPath(), "Crash Log.txt");

        using (TextWriter tw = new StreamWriter(logFile, true))
        {
            tw.WriteLine("");
            tw.WriteLine(DateTime.Now.ToString("O"));
            tw.Write("Unhandled exception: ");
            tw.WriteLine(e.Exception);
        }
        cancellationTokenSource?.Cancel();
        _provider?.Dispose();

        MessageBox.Show($"Unexpected Error, please send '{logFile}' to immeraufdemhund@gmail.com", "Unexpected Error");
        Environment.Exit(1);
    }

    private void App_OnExit(object sender, ExitEventArgs e)
    {
        _logger?.LogInformation("Application exiting");
        cancellationTokenSource?.Cancel();
        _provider?.Dispose();
    }
}
