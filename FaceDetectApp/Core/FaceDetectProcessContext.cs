using FaceDetectApp.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Face;
using System.IO;

namespace FaceDetectApp.Core
{
    public class FaceDetectProcessContext : IProcessContext
    {
        public FaceDetectProcessContext(IMessagesService messagesService, IBlurService blurService,
            IFaceServiceClient faceServiceClient)
        {
            _messagesService = messagesService;
            _blurService = blurService;
            _faceServiceClient = faceServiceClient;
        }

        public bool HasResult { get; private set; }
        public bool PhotoSelected { get; private set; }
        public bool IsWorking { get; private set; }
        public string PhotoPath { get; private set; }
        public object Result { get; private set; }

        public async Task<bool> ProcessPhoto()
        {
            lock (_lockObject)
            {
                if (!PhotoSelected || IsWorking)
                    return false;
                IsWorking = true;
            }

            bool success = false;

            try
            {
                var fileStream = File.OpenRead(PhotoPath);

                var requiedFaceAttributes = new FaceAttributeType[]
                {
                    FaceAttributeType.Age,
                    FaceAttributeType.Gender,
                    FaceAttributeType.Smile,
                    FaceAttributeType.FacialHair,
                    FaceAttributeType.HeadPose,
                    FaceAttributeType.Glasses
                };

                var faces = await _faceServiceClient.DetectAsync(fileStream,
                    returnFaceLandmarks: true,
                    returnFaceAttributes: requiedFaceAttributes);

                Result = faces;
                HasResult = true;
                success = true;
            }
            catch (Exception)
            {
                success = false;
            }

            lock (_lockObject)
            {
                IsWorking = false;
                return success;
            }
        }

        public void Reset()
        {
            HasResult = false;
            PhotoSelected = false;
            Result = null;
            PhotoPath = null;
        }

        public bool SelectPhoto()
        {
            _blurService.Blur();
            bool success;

            string path = GetPhotoPath();
            if (path != null)
            {
                PhotoPath = path;
                PhotoSelected = true;
                HasResult = false;
                Result = null;
                success = true;
            }
            else
            {
                success = false;
            }

            _blurService.UnBlur();
            return success;
        }

        protected virtual string GetPhotoPath()
        {
            var openDlg = new Microsoft.Win32.OpenFileDialog();
            openDlg.Filter = "JPEG Image(*.jpg)|*.jpg";

            if (openDlg.ShowDialog() == true)
                return openDlg.FileName;
            return null;
        }

        private readonly IMessagesService _messagesService;
        private readonly IBlurService _blurService;
        private readonly IFaceServiceClient _faceServiceClient;
        private readonly object _lockObject = new object();
    }
}
