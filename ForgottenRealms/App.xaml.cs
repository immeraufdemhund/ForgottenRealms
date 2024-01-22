using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using ForgottenRealms.Engine;
using ForgottenRealms.Engine.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ForgottenRealms
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            ConfigureDependencyInjection();
            _logger = _provider.GetRequiredService<ILogger<App>>();
            _logger.LogDebug("Setting up Config");
            var config = _provider.GetRequiredService<Config>();
            config.Setup();
            Logger.Setup(Config.GetLogPath());
            _logger.LogDebug("Starting DnD Engine");
            StartEngine();
            var mainWindow = _provider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureDependencyInjection()
        {
            var serviceCollection = new ServiceCollection();
            _provider = serviceCollection.AddLogging(logging =>
                {
                    logging.AddSimpleConsole();
                    logging.AddApplicationInsights();
                    logging.AddFilter("*", LogLevel.Trace);
                })
                .AddTransient<MainWindow>()
                .RegisterEngineFeature()
                .BuildServiceProvider();
        }

        private CancellationTokenSource cancellationTokenSource;
        private ServiceProvider _provider;
        private ILogger _logger;

        private async void StartEngine()
        {
            _logger.LogDebug("Starting Engine");
            cancellationTokenSource = new CancellationTokenSource();
            try
            {
                await Task.Run(EngineThread, cancellationTokenSource.Token);
                _logger.LogDebug("After thread");
            }
            catch (TaskCanceledException)
            {
                _logger.LogInformation("Engine Thread Cancelled");
                EngineStopped();
            }

            _logger.LogInformation("Engine stopping");
            KeyboardService.print_and_exit();
        }

        private void EngineThread()
        {
            _logger.LogDebug("Engine Thread Started");
            var engineConfig = new MainGameEngineConfig
            {
                EngineThreadStoppedCallback = cancellationTokenSource,
                ResourceLoader = Resource.ResourceManager.GetStream,
            };
            MainGameEngine.__SystemInit(engineConfig);
            MainGameEngine.PROGRAM();
            EngineStopped();
            _logger.LogInformation("Engine Thread Stopped");
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
            _logger.LogCritical(e.Exception, "Unhandled Exception");
            string logFile = Path.Combine(Config.GetLogPath(), "Crash Log.txt");

            using (TextWriter tw = new StreamWriter(logFile, true))
            {
                tw.WriteLine("");
                tw.WriteLine(DateTime.Now.ToString("O"));
                tw.Write("Unhandled exception: ");
                tw.WriteLine(e.Exception);
            }
            cancellationTokenSource.Cancel();
            _provider.Dispose();

            MessageBox.Show($"Unexpected Error, please send '{logFile}' to immeraufdemhund@gmail.com", "Unexpected Error");
            Environment.Exit(1);
        }

        private void App_OnExit(object sender, ExitEventArgs e)
        {
            _logger.LogInformation("Application exiting");
            cancellationTokenSource.Cancel();
            _provider.Dispose();
        }
    }
}
