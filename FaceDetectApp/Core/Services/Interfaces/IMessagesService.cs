using FaceDetectApp.Common;

namespace FaceDetectApp.Core.Services.Interfaces
{
    public interface IMessagesService
    {
        void ShowMessage(string text);
        void ShowMessage(string text, string title);
        FDMessageBoxResult ShowMessage(string text, FDMessageBoxButton button);
        FDMessageBoxResult ShowMessage(string text, string title, FDMessageBoxButton button);
    }
}
