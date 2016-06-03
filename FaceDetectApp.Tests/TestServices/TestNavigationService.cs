using FaceDetectApp.Core.Services.Interfaces;
using System;

namespace FaceDetectApp.Tests.TestServices
{
    internal sealed class TestNavigationService : INavigationService
    {
        public Action<string> OnNavigate { get; set; }

        public void Navigate(string viewName)
        {
            if (OnNavigate != null)
                OnNavigate(viewName);
        }
    }
}
