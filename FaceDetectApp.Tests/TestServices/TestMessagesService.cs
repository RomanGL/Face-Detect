using FaceDetectApp.Common;
using FaceDetectApp.Core.Services.Interfaces;

namespace FaceDetectApp.Tests.TestServices
{
    internal sealed class TestMessagesService : IMessagesService
    {
        public void ShowMessage(string text) { }

        public FDMessageBoxResult ShowMessage(string text, FDMessageBoxButton button)
        {
            return FDMessageBoxResult.OK;
        }

        public void ShowMessage(string text, string title) { }

        public FDMessageBoxResult ShowMessage(string text, string title, FDMessageBoxButton button)
        {
            return FDMessageBoxResult.OK;
        }
    }
}
