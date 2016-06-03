using FaceDetectApp.Core.Services.Interfaces;
using Microsoft.Practices.Unity;
using Prism.Commands;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceDetectApp.Core.ViewModels
{
    [ImplementPropertyChanged]
    public sealed class ChoosePhotoViewModel : ViewModelBase
    {
        public ChoosePhotoViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            ChoosePhotoCommand = new DelegateCommand(OnChoosePhotoCommand);
            ProcessPhotoCommand = new DelegateCommand(OnProcessPhoto, () => Context.PhotoSelected);
        }

        [Dependency("FaceDetectContext"), DoNotNotify]
        public IProcessContext Context { get; set; }

        [DoNotNotify]
        public DelegateCommand ChoosePhotoCommand { get; private set; }

        [DoNotNotify]
        public DelegateCommand ProcessPhotoCommand { get; private set; }

        public string PhotoPath { get; private set; }

        private void OnChoosePhotoCommand()
        {
            bool success = Context.SelectPhoto();
            if (success)
                PhotoPath = Context.PhotoPath;

            ProcessPhotoCommand.RaiseCanExecuteChanged();
        }

        private void OnProcessPhoto()
        {
            _navigationService.Navigate("ProcessView");
        }

        private readonly INavigationService _navigationService;
    }
}
