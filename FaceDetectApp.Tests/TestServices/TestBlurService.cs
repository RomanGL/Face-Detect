using FaceDetectApp.Core.Services.Interfaces;

namespace FaceDetectApp.Tests.TestServices
{
    internal sealed class TestBlurService : IBlurService
    {
        public void Blur() { }

        public void Register(IBlurSupport blurSupport) { }

        public void UnBlur() { }

        public void UnRegister(IBlurSupport blurSupport) { }
    }
}
