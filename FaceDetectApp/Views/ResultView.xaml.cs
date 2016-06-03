using FaceDetectApp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FirstFloor.ModernUI.Windows.Navigation;
using FaceDetectApp.Core.ViewModels;
using Microsoft.ProjectOxford.Face.Contract;
using System.Globalization;

namespace FaceDetectApp.Views
{
    /// <summary>
    /// Interaction logic for ResultView.xaml
    /// </summary>
    public partial class ResultView : View
    {
        public ResultView()
        {
            InitializeComponent();
        }

        public override void OnNavigatedTo(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
            ((ResultViewModel)DataContext).DrawAction = DrawFaces;
            base.OnNavigatedTo(e);
        }

        private void DrawFaces(Face[] faces, string photoPath)
        {
            var bitmapSource = new BitmapImage(new Uri(photoPath));
            var faceRects = faces.Select(f => f.FaceRectangle).ToList();

            DrawingVisual visual = new DrawingVisual();
            DrawingContext drawingContext = visual.RenderOpen();
            drawingContext.DrawImage(bitmapSource,
                new Rect(0, 0, bitmapSource.Width, bitmapSource.Height));
            double dpi = bitmapSource.DpiX;
            double resizeFactor = 96 / dpi;

            for (int i = 0; i < faceRects.Count; i++)
            {
                var faceRect = faceRects[i];
                drawingContext.DrawRectangle(
                    Brushes.Transparent,
                    new Pen(Brushes.Red, 4),
                    new Rect(faceRect.Left * resizeFactor,
                        faceRect.Top * resizeFactor,
                        faceRect.Width * resizeFactor,
                        faceRect.Height * resizeFactor));

                var text = new FormattedText((i + 1).ToString(), CultureInfo.CurrentUICulture, FlowDirection.LeftToRight,
                    new Typeface("Segoe UI"), 32 * resizeFactor, Brushes.Red);

                drawingContext.DrawText(text, new Point((faceRect.Left + 10) * resizeFactor, faceRect.Top * resizeFactor));
            }

            drawingContext.Close();
            RenderTargetBitmap faceWithRectBitmap = new RenderTargetBitmap(
                (int)(bitmapSource.PixelWidth * resizeFactor),
                (int)(bitmapSource.PixelHeight * resizeFactor),
                96, 96, PixelFormats.Pbgra32);

            faceWithRectBitmap.Render(visual);
            PhotoBlock.Source = faceWithRectBitmap;
        }
    }
}
