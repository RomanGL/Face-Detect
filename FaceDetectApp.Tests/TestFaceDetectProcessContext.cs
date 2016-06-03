using FaceDetectApp.Core;
using FaceDetectApp.Core.Services.Interfaces;
using Microsoft.ProjectOxford.Face;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceDetectApp.Tests
{
    internal sealed class TestFaceDetectProcessContext : FaceDetectProcessContext
    {
        public TestFaceDetectProcessContext(IMessagesService messagesService, IBlurService blurService,
            IFaceServiceClient faceServiceClient)
            : base(messagesService, blurService, faceServiceClient)
        {
        }

        protected override string GetPhotoPath()
        {
            return "Assets\\TestPhoto.jpg";
        }
    }
}
