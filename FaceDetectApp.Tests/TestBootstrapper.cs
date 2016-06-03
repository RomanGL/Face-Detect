using FaceDetectApp.Core;
using FaceDetectApp.Core.Services.Interfaces;
using FaceDetectApp.Tests.TestServices;
using Microsoft.Practices.Unity;
using Microsoft.ProjectOxford.Face;

namespace FaceDetectApp.Tests
{
    internal sealed class TestBootstrapper : DITestBootstrapper
    {
        public override void Run(bool runWithDefaultConfiguration)
        {
            Container = CreateContainer();
            ConfigureContainer();
        }

        protected override void ConfigureContainer()
        {
            Container.RegisterType<IBlurService, TestBlurService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IMessagesService, TestMessagesService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<INavigationService, TestNavigationService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IFaceServiceClient, TestFaceServiceClient>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IProcessContext, TestFaceDetectProcessContext>(FACE_DETECT_CONTEXT_NAME, 
                new ContainerControlledLifetimeManager());
        }

        public const string FACE_DETECT_CONTEXT_NAME = "FaceDetectContext";
    }
}
