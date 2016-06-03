using FaceDetectApp.Core.ViewModels;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using System.Windows.Controls;

namespace FaceDetectApp.Common
{
    public abstract class View : UserControl, IContent
    {
        public virtual void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
        }

        public virtual void OnNavigatedFrom(NavigationEventArgs e)
        {
            CurrentViewModel?.OnNavigatedFrom(e);
        }

        public virtual void OnNavigatedTo(NavigationEventArgs e)
        {
            CurrentViewModel?.OnNavigatedTo(e);
        }

        public virtual void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            CurrentViewModel?.OnNavigatingFrom(e);
        }

        private IViewModel CurrentViewModel { get { return DataContext as IViewModel; } }
    }
}
