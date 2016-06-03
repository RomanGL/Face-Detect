using FaceDetectApp.Core;
using FaceDetectApp.Core.Services.Interfaces;
using FaceDetectApp.Core.ViewModels;
using FaceDetectApp.Tests.TestServices;
using FaceDetectApp.Views;
using Microsoft.ProjectOxford.Face.Contract;
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
            ChoosePhotoTest();
            var context = _bootstrapper.Resolve<IProcessContext>(TestBootstrapper.FACE_DETECT_CONTEXT_NAME);

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

        [TestMethod]
        public async Task ResultViewModelTest()
        {
            await ProcessPhotoTest();
                        
            var viewModel = _bootstrapper.Resolve<ResultViewModel>();
            Assert.IsNotNull(viewModel.Context,
                $"Зависимость к контексту процесса в {nameof(ResultViewModel)} не разрешена.");

            bool drawCalled = false;
            viewModel.DrawAction = (faces, path) => drawCalled = true;
            viewModel.OnNavigatedTo(null);

            Assert.IsTrue(drawCalled,
                $"{nameof(ResultViewModel)} не вызвал процесс отрисовки данных.");
            Assert.IsNotNull(viewModel.PhotoPath,
                $"{nameof(ResultViewModel)} не возвратил путь к фотографии для отображения.");
            Assert.IsTrue(viewModel.Attributes.Count != 0,
                $"{nameof(ResultViewModel)} не подготовил атрибуты для отображения.");
        }

        [TestMethod]
        public void ProcessViewModelTest()
        {
            ChoosePhotoTest();

            var navigationService = (TestNavigationService)_bootstrapper.Resolve<INavigationService>();
            var viewModel = _bootstrapper.Resolve<ProcessViewModel>();

            Assert.IsNotNull(viewModel.Context,
                $"Зависимость к контексту процесса в {nameof(ProcessViewModel)} не разрешена.");

            bool resultViewCalled = false;
            navigationService.OnNavigate = (v) => resultViewCalled = v == "ResultView";
            viewModel.OnNavigatedTo(null);
            
            Assert.IsTrue(resultViewCalled,
                $"{nameof(ProcessViewModel)} не вызвал процесс навигации к странице результатов.");
        }

        [TestMethod]
        public void ChoosePhotoViewModelTest()
        {            
            var viewModel = _bootstrapper.Resolve<ChoosePhotoViewModel>();

            Assert.IsNotNull(viewModel.Context,
                $"Зависимость к контексту процесса в {nameof(ChoosePhotoViewModel)} не разрешена.");

            viewModel.ChoosePhotoCommand.Execute();
            Assert.IsNotNull(viewModel.PhotoPath,
                $"{nameof(ChoosePhotoViewModel)} не установил путь к фотографии.");

            bool processViewCalled = false;            
            var navigationService = (TestNavigationService)_bootstrapper.Resolve<INavigationService>();
            navigationService.OnNavigate = (v) => processViewCalled = v == "ProcessView";

            viewModel.ProcessPhotoCommand.Execute();
            Assert.IsTrue(processViewCalled,
                $"{nameof(ChoosePhotoViewModel)} не вызвал процесс навигации к странице обработки.");
        }

        private static TestBootstrapper _bootstrapper;
        private const string TEST_PHOTO_PATH = "Assets\\TestPhoto.jpg";
    }
}
