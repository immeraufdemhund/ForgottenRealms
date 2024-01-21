using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ForgottenRealms.Engine;
using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WriteableBitmap _displayAreaSource;

        public MainWindow()
        {
            InitializeComponent();
            _displayAreaSource = new WriteableBitmap(320, 200, 96, 96, PixelFormats.Bgr24, null);
            displayArea.Source = _displayAreaSource;
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            Display.UpdateCallback = UpdateDisplayCallback;
        }

        private void UpdateDisplayCallback()
        {
            // set image on display thread
            if (!displayArea.Dispatcher.CheckAccess())
            {
                displayArea.Dispatcher.Invoke(UpdateDisplayCallback);
                return;
            }

            _displayAreaSource.Lock();
            try
            {
                _displayAreaSource.WritePixels(new Int32Rect(0, 0, 320, 200), Display.VideoRam, 320 * 3, 0);
            }
            finally
            {
                _displayAreaSource.Unlock();
            }
        }

        private void MainWindow_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
            {
                Display.ForceUpdate();
            }

            KeyboardDriver.AddKey(IbmKeyboard.KeyToIBMKey(e.Key));
        }

        private void MainWindow_OnClosing(object? sender, CancelEventArgs e)
        {
            KeyboardService.print_and_exit();
        }
    }
}
