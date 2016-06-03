namespace FaceDetectApp.Core.Services.Interfaces
{
    public interface IBlurService
    {
        void Register(IBlurSupport blurSupport);
        void UnRegister(IBlurSupport blurSupport);

        void Blur();
        void UnBlur();
    }
}
