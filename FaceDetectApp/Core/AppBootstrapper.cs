using Prism.Mvvm;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Unity;
using FaceDetectApp.Core.Services.Interfaces;
using FaceDetectApp.Core.Services;
using Microsoft.ProjectOxford.Face;

namespace FaceDetectApp.Core
{
    public class AppBootstrapper : UnityBootstrapper
    {
        public Window MainWindow { get { return Shell as Window; } }

        protected override void ConfigureViewModelLocator()
        {
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(GetViewModelType);
            ViewModelLocationProvider.SetDefaultViewModelFactory(GetViewModel);
        }

        protected override DependencyObject CreateShell()
        {
            var window = Container.Resolve<MainWindow>();
            Container.RegisterInstance<INavigationService>(new NavigationServices(window), new ContainerControlledLifetimeManager());
            return window;
        }

        protected override void InitializeShell()
        {
            App.Current.MainWindow = MainWindow;
            MainWindow.Show();
        }

        protected override void ConfigureContainer()
        {            
            Container.RegisterType<IBlurService, BlurService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IMessagesService, MessagesService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IProcessContext, FaceDetectProcessContext>("FaceDetectContext", new ContainerControlledLifetimeManager());

            Container.RegisterInstance<IFaceServiceClient>(new FaceServiceClient("f0a74171fb214447a78dc48d0525c0ee"));

            base.ConfigureContainer();
        }

        protected object GetViewModel(Type viewModelType)
        {
            return Container.Resolve(viewModelType, viewModelType.Name);
        }

        protected Type GetViewModelType(Type viewType)
        {
            string viewModelTypeName = null;
            if (viewType.Name.EndsWith("View"))
                viewModelTypeName = String.Format(CultureInfo.InvariantCulture, VIEW_MODEL_FORMAT, viewType.Name);
            else
                viewModelTypeName = String.Format(CultureInfo.InvariantCulture, VIEW_MODEL_CONTROLS_FORMAT, viewType.Name);

            var t = Type.GetType(viewModelTypeName);
            return t;
        }

        private const string VIEW_MODEL_FORMAT = "FaceDetectApp.Core.ViewModels.{0}Model, FaceDetectApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
        private const string VIEW_MODEL_CONTROLS_FORMAT = "FaceDetectApp.Core.ViewModels.{0}ViewModel, FaceDetectApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
    }
}
