using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ForgottenRealms.Engine;
using ForgottenRealms.Engine.Classes;
using Microsoft.Extensions.Logging;

namespace ForgottenRealms
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ILogger<MainWindow> _logger;
        private readonly KeyboardDriver _keyboardDriver;
        private readonly WriteableBitmap _displayAreaSource;

        public MainWindow(ILogger<MainWindow> logger, KeyboardDriver keyboardDriver)
        {
            _logger = logger;
            _keyboardDriver = keyboardDriver;
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
                return;
            }

            _keyboardDriver.AddKey(IbmKeyboard.KeyToIBMKey(e.Key));
        }
    }
}
