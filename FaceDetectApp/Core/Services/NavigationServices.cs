using FaceDetectApp.Core.Services.Interfaces;
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows.Navigation;
using System;
using System.Windows;
using FaceDetectApp.Common;

namespace FaceDetectApp.Core.Services
{
    public sealed class NavigationServices : INavigationService
    {
        public NavigationServices(ModernWindow window)
        {
            _window = window;
            _navigator = window.LinkNavigator;
        }

        public void Navigate(string viewName)
        {
            if (_windowFrame == null)
                FindFrame();

            _navigator.Navigate(new Uri($"/Views/{viewName}.xaml", UriKind.Relative), 
                _windowFrame);
        }

        private void FindFrame()
        {
            _windowFrame = _window.GetFirstOrDefaultDescendantOfType<ModernFrame>();
        }

        private ModernFrame _windowFrame;
        private readonly ModernWindow _window;        
        private readonly ILinkNavigator _navigator;
    }
}
