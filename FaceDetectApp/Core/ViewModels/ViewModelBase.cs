using FirstFloor.ModernUI.Windows.Navigation;
using Prism.Mvvm;

namespace FaceDetectApp.Core.ViewModels
{
    public abstract class ViewModelBase : BindableBase, IViewModel
    {
        public virtual void OnNavigatedFrom(NavigationEventArgs e)
        {
        }

        public virtual void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        public virtual void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
        }
    }
}
