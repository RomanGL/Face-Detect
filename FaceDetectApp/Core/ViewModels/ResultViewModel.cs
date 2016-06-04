using FaceDetectApp.Core.Services.Interfaces;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstFloor.ModernUI.Windows.Navigation;
using Microsoft.Practices.Unity;
using Microsoft.ProjectOxford.Face.Contract;
using System.Collections.ObjectModel;

namespace FaceDetectApp.Core.ViewModels
{
    [ImplementPropertyChanged]
    public sealed class ResultViewModel : ViewModelBase
    {
        public ResultViewModel(IMessagesService messagesService, INavigationService navigationService)
        {
            _messagesService = messagesService;
            _navigationService = navigationService;
        }

        [Dependency("FaceDetectContext"), DoNotNotify]
        public IProcessContext Context { get; set; }

        [DoNotNotify]
        public Action<Face[], string> DrawAction { get; set; }

        public string PhotoPath { get; private set; }

        public ObservableCollection<string> Attributes { get; private set; }        

        public override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!Context.PhotoSelected)
            {
                _messagesService.ShowMessage("Сначала требуется выбрать фотографию.");
                _navigationService.Navigate("ChoosePhotoView");
                return;
            }

            PhotoPath = Context.PhotoPath;
            if (Context.IsWorking || !Context.HasResult)
            {
                _navigationService.Navigate("ProcessView");
                return;
            }
            else if (Context.HasResult)
            {
                ProcessResult();
            }
        }

        private void ProcessResult()
        {
            Attributes = new ObservableCollection<string>();
            var faces = (Face[])Context.Result;
            if (faces.Length == 0)
            {
                _messagesService.ShowMessage("Это очень милая фотография, но найти лиц на ней мы не смогли.");
                _navigationService.Navigate("ChoosePhotoView");
                return;
            }

            for (int i = 0; i < faces.Length; i++)
            {
                var face = faces[i];
                var sb = new StringBuilder($"Лицо {i + 1}\n");
                sb.AppendLine(GetGender(face.FaceAttributes.Gender));
                sb.AppendLine($"Возраст {face.FaceAttributes.Age} лет");
                sb.Append(GetGlassesString(face.FaceAttributes.Glasses));

                Attributes.Add(sb.ToString());
            }

            DrawAction(faces, PhotoPath);
        }

        private string GetGlassesString(Glasses g)
        {
            switch (g)
            {
                case Glasses.Sunglasses:
                    return "Солнцезащитные очки";
                case Glasses.ReadingGlasses:
                    return "Обычные очки";
                case Glasses.SwimmingGoggles:
                    return "Очки для плавания";
                default:
                    return "Нет очков";
            }
        }

        private string GetGender(string originalGender)
        {
            switch (originalGender)
            {
                case "male":
                    return "Мужчина";
                case "female":
                    return "Женщина";
                default:
                    return "Неизвестный пол";
            }
        }
        
        private readonly IMessagesService _messagesService;
        private readonly INavigationService _navigationService;
    }
}
