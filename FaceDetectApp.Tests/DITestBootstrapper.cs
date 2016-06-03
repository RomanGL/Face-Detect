using FaceDetectApp.Core;
using Microsoft.Practices.Unity;

namespace FaceDetectApp.Tests
{
    internal class DITestBootstrapper : AppBootstrapper
    {
        public T Resolve<T>(string name = null)
        {
            if (name == null)
                return Container.Resolve<T>();
            else
                return Container.Resolve<T>(name);
        }

        public object ResolveViewModelFromView<T>()
        {
            return GetViewModel(GetViewModelType(typeof(T)));
        }

        protected override void InitializeShell()
        {
        }
    }
}
