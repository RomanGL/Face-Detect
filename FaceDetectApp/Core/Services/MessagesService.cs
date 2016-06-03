using FaceDetectApp.Common;
using FaceDetectApp.Core.Services.Interfaces;
using FirstFloor.ModernUI.Windows.Controls;
using System.Windows;

namespace FaceDetectApp.Core.Services
{
    public sealed class MessagesService : IMessagesService
    {
        public MessagesService(IBlurService blurService)
        {
            _blurService = blurService;
        }

        public void ShowMessage(string text)
        {
            ShowMessage(text, DefaultMessageDialogTitle, FDMessageBoxButton.OK);
        }

        public FDMessageBoxResult ShowMessage(string text, FDMessageBoxButton button)
        {
            return ShowMessage(text, DefaultMessageDialogTitle, button);
        }

        public void ShowMessage(string text, string title)
        {
            ShowMessage(text, title, FDMessageBoxButton.OK);
        }

        public FDMessageBoxResult ShowMessage(string text, string title, FDMessageBoxButton button)
        {
            _blurService.Blur();
            var result = ModernDialog.ShowMessage(text, title, (MessageBoxButton)(int)button);
            _blurService.UnBlur();
            return (FDMessageBoxResult)(int)result;
        }

        private readonly IBlurService _blurService;
        private const string DefaultMessageDialogTitle = "Face Detect";
    }
}
