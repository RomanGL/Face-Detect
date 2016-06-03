using FirstFloor.ModernUI.Windows.Navigation;

namespace FaceDetectApp.Core.ViewModels
{
    public interface IViewModel
    {
        void OnNavigatedFrom(NavigationEventArgs e);
        void OnNavigatedTo(NavigationEventArgs e);
        void OnNavigatingFrom(NavigatingCancelEventArgs e);
    }
}
