using FaceDetectApp.Core.Services.Interfaces;
using FirstFloor.ModernUI.Windows.Controls;
using Microsoft.Practices.Unity;
using System.Windows;
using System.Windows.Media.Animation;

namespace FaceDetectApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow, IBlurSupport
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            Unloaded += MainWindow_Unloaded;
        }

        [Dependency]
        public IBlurService BlurService { get; set; }

        public void Blur()
        {
            Dispatcher.InvokeAsync(() =>
            {
                if (_blurStoryboard == null)
                    _blurStoryboard = (Storyboard)Resources["BlurStoryboard"];

                _blurStoryboard.Begin();
            });
        }

        public void UnBlur()
        {
            Dispatcher.InvokeAsync(() =>
            {
                if (_unblurStoryboard == null)
                    _unblurStoryboard = (Storyboard)Resources["UnblurStoryboard"];

                _unblurStoryboard.Begin();
            });
        }

        private void MainWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            Unloaded -= MainWindow_Unloaded;
            BlurService.UnRegister(this);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= MainWindow_Loaded;
            BlurService.Register(this);
        }

        private Storyboard _blurStoryboard;
        private Storyboard _unblurStoryboard;
    }
}
