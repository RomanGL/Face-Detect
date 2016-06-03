using FaceDetectApp.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceDetectApp.Tests
{
    [TestClass]
    public class FaceDetectProcessTests
    {
        [ClassInitialize]
        public static void FaceDetectProcessTestsInitialize(TestContext testContext)
        {
            _bootstrapper = new TestBootstrapper();
            _bootstrapper.Run();
        }

        [TestMethod]
        public void ChoosePhotoTest()
        {
            var context = _bootstrapper.Resolve<IProcessContext>(TestBootstrapper.FACE_DETECT_CONTEXT_NAME);
            bool success = context.SelectPhoto();

            Assert.IsTrue(success, 
                $"{nameof(FaceDetectProcessContext)} неверно обрабатывает получение пути к фотографии.");
            Assert.IsTrue(context.PhotoSelected, 
                $"{nameof(FaceDetectProcessContext)} имеет неверное значение о наличии фотографии.");
            Assert.IsFalse(context.HasResult,
                $"{nameof(FaceDetectProcessContext)} указывает на наличие результатов обработки фотографии.");
            Assert.IsNull(context.Result,
                $"{nameof(FaceDetectProcessContext)} содержит результаты обработки фотографии, хотя их быть не должно.");
            Assert.AreEqual(TEST_PHOTO_PATH, context.PhotoPath,
                $"{nameof(FaceDetectProcessContext)} изменил путь к тестовой фотографии.");
        }

        [TestMethod]
        public async Task ProcessPhotoTest()
        {
            var context = _bootstrapper.Resolve<IProcessContext>(TestBootstrapper.FACE_DETECT_CONTEXT_NAME);
            bool photoSuccess = context.SelectPhoto();

            Assert.IsTrue(photoSuccess,
                $"{nameof(FaceDetectProcessContext)} неверно обрабатывает получение пути к фотографии.");

            bool processSuccess = await context.ProcessPhoto();
            Assert.IsTrue(processSuccess,
                $"{nameof(FaceDetectProcessContext)} не смог обработать фотографию.");
            Assert.IsFalse(context.IsWorking,
                $"{nameof(FaceDetectProcessContext)} не отметил завершение процесса обработки.");
            Assert.IsTrue(context.HasResult,
                $"{nameof(FaceDetectProcessContext)} не указывает на наличие результатов.");
            Assert.IsNotNull(context.Result,
                $"{nameof(FaceDetectProcessContext)} не сохранил результаты.");
        }

        private static TestBootstrapper _bootstrapper;
        private const string TEST_PHOTO_PATH = "Assets\\TestPhoto.jpg";
    }
}
