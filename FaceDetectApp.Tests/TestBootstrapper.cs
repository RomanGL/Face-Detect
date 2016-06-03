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
            Container.RegisterType<IBlurService, TestBlurService>();
            Container.RegisterType<IMessagesService, TestMessagesService>();
            Container.RegisterType<INavigationService, TestNavigationService>();
            Container.RegisterType<IFaceServiceClient, TestFaceServiceClient>();
            Container.RegisterType<IProcessContext, TestFaceDetectProcessContext>(FACE_DETECT_CONTEXT_NAME);
        }

        public const string FACE_DETECT_CONTEXT_NAME = "FaceDetectContext";
    }
}
