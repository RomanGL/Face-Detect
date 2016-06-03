using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FaceDetectApp.Core;
using FaceDetectApp.Views;
using FaceDetectApp.Core.Services.Interfaces;
using FaceDetectApp.Core.ViewModels;

namespace FaceDetectApp.Tests
{
    /// <summary>
    /// Представляет набор тестов контейнера внедрения зависимостей.
    /// </summary>
    [TestClass]
    public class DependencyInjectionTests
    {
        [ClassInitialize()]
        public static void DependencyInjectionTestsInitialize(TestContext testContext)
        {
            _bootstrapper = new DITestBootstrapper();
            _bootstrapper.Run();
        }

        [TestMethod]
        public void MainWindowResolveTest()
        {
            try
            {
                var window = _bootstrapper.Resolve<MainWindow>();
                Assert.IsNotNull(window.BlurService, $"Bootstrapper IOC не разрешил зависимость {nameof(IBlurService)}.");
            }
            catch (Exception)
            {
                Assert.Fail($"Bootstrapper IOC не разрешил {nameof(MainWindow)}.");
            }
        }

        [TestMethod]
        public void ResolveViewModelsByViewTest()
        {
            var choosePhotoViewModel = _bootstrapper.ResolveViewModelFromView<ChoosePhotoView>();
            var processViewModel = _bootstrapper.ResolveViewModelFromView<ProcessView>();
            var resultViewModel = _bootstrapper.ResolveViewModelFromView<ResultView>();

            Assert.IsNotNull(choosePhotoViewModel, $"Bootstrapper IOC не разрешил view model для {nameof(ChoosePhotoView)}");
            Assert.IsNotNull(processViewModel, $"Bootstrapper IOC не разрешил view model для {nameof(ProcessView)}");
            Assert.IsNotNull(resultViewModel, $"Bootstrapper IOC не разрешил view model для {nameof(ResultView)}");

            Assert.IsInstanceOfType(choosePhotoViewModel, typeof(ChoosePhotoViewModel),
                $"Bootstrapper IOC разрешил неверный тип view model для {nameof(ChoosePhotoView)}");
            Assert.IsInstanceOfType(processViewModel, typeof(ProcessViewModel),
                $"Bootstrapper IOC разрешил неверный тип view model для {nameof(ProcessView)}");
            Assert.IsInstanceOfType(resultViewModel, typeof(ResultViewModel),
                $"Bootstrapper IOC разрешил неверный тип view model для {nameof(ResultView)}");
        }

        private static DITestBootstrapper _bootstrapper;
    }
}
