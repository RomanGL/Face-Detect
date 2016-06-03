using FaceDetectApp.Core;
using FaceDetectApp.Core.Services.Interfaces;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace FaceDetectApp
{
    public partial class App : Application
    {
        public App()
        {
            DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _botstrapper = new AppBootstrapper();
            _botstrapper.Run();

            base.OnStartup(e);
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            bool canStyled = true;
            if (_botstrapper != null)
            {
                try
                {
                    var msgService = _botstrapper.Container.TryResolve<IMessagesService>();
                    if (msgService != null)
                        msgService.ShowMessage(String.Format(ErrorTextMask, e.Exception.ToString()));
                }
                catch (Exception)
                {
                    canStyled = false;
                }
            }

            if (!canStyled)
            {
                MessageBox.Show(String.Format(ErrorTextMask, e.Exception.ToString()), ErrorTitle, MessageBoxButton.OK, MessageBoxImage.Error);
            }

            e.Handled = true;
            Shutdown();
        }

        private AppBootstrapper _botstrapper;

        private const string ErrorTextMask = "Произошла непредвиденная ошибка, приложение будет закрыто. Мы очень сожалеем о случившемся.\n\n{0}";
        private const string ErrorTitle = "Face Detect";
    }
}
