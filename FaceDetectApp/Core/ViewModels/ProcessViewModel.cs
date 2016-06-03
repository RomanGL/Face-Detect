using FirstFloor.ModernUI.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using FaceDetectApp.Core.Services.Interfaces;
using Microsoft.Practices.Unity;
using FirstFloor.ModernUI.Windows.Navigation;
using System.Windows.Input;

namespace FaceDetectApp.Core.ViewModels
{
    public sealed class ProcessViewModel : ViewModelBase
    {
        public ProcessViewModel(IMessagesService messagesService, INavigationService navigationService)
        {
            _messagesService = messagesService;
            _navigationService = navigationService;
        }

        [Dependency("FaceDetectContext")]
        public IProcessContext Context { get; set; }

        public override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!Context.PhotoSelected)
            {
                _messagesService.ShowMessage("Сначала требуется выбрать фотографию.");
                _navigationService.Navigate("ChoosePhotoView");
                return;
            }
            else if (Context.HasResult)
            {
                _navigationService.Navigate("ResultView");
                return;
            }

            if (!Context.IsWorking)
                Process();
        }

        private async void Process()
        {
            bool success = await Context.ProcessPhoto();
            if (success)
            {
                _navigationService.Navigate("ResultView");
            }
            else
            {
                _messagesService.ShowMessage("Не удалось обработать фотографию.\nПроверьте подключение к интернету и повторите попытку.");
                _navigationService.Navigate("ChoosePhotoView");
            }
        }
        
        private readonly IMessagesService _messagesService;
        private readonly INavigationService _navigationService;
    }
}
