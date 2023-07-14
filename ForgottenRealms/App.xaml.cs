using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using ForgottenRealms.Engine;
using ForgottenRealms.Engine.Logging;

namespace ForgottenRealms
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            Logger.SetExitFunc(seg043.print_and_exit);
            StartEngine();
            var mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private async void StartEngine()
        {
            await Task.Run(EngineThread);
        }

        private void EngineThread()
        {
            seg001.__SystemInit(EngineStopped);
            seg001.PROGRAM();
            EngineStopped();
        }

        private void EngineStopped()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Application.Current.Shutdown();
            });
        }
        private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            string logFile = Path.Combine(Logger.GetPath(), "Crash Log.txt");

            using (TextWriter tw = new StreamWriter(logFile, true))
            {
                tw.WriteLine("");
                // tw.WriteLine("{0} {1}", Application.ProductName, Application.ProductVersion);
                tw.WriteLine(DateTime.Now.ToString("O"));
                tw.Write("Unhandled exception: ");
                tw.WriteLine(e.Exception);
            }

            MessageBox.Show($"Unexpected Error, please send '{logFile}' to immeraufdemhund@gmail.com", "Unexpected Error");
            Environment.Exit(1);
        }
    }
}
