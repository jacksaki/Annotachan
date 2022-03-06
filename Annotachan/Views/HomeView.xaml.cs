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
using Annotachan.ViewModels;
namespace Annotachan.Views {
    /// <summary>
    /// EditTemplateView.xaml の相互作用ロジック
    /// </summary>
    public partial class HomeView : UserControl {
        public HomeView() {
            InitializeComponent();
            this.DataContextChanged += (sender, e) => {
                this.ViewModel.SaveImageAction = SaveImage;
                this.ViewModel.ClearRectAction = ClearRect;
            };
        }
        public HomeViewModel ViewModel {
            get {
                return this.DataContext as HomeViewModel;
            }
        }
        public void SaveImage() {
            SaveCanvasImage();
            SaveRectImage();
        }

        private void SaveCanvasImage() {
            var bounds = VisualTreeHelper.GetDescendantBounds(canvas);
            var renderTargetBitmap = new RenderTargetBitmap((int)(bounds.Width),
                                                            (int)(bounds.Height),
                                                            96,
                                                            96,
                                                            PixelFormats.Pbgra32);
            var drawingVisual = new DrawingVisual();
            using (var drawingContext = drawingVisual.RenderOpen()) {
                var visualBrush = new VisualBrush(canvas);
                drawingContext.DrawRectangle(visualBrush, null, new Rect(new Point(), bounds.Size));
            }
            renderTargetBitmap.Render(drawingVisual);

            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
            var result = new BitmapImage();
            using (var fileStream = new System.IO.FileStream(this.ViewModel.SelectedImage.CanvasSavePath, System.IO.FileMode.Create)) {
                encoder.Save(fileStream);
            }
        }
        private void SaveRectImage() {

        }
        private Int32Rect RectToIntRect(Rect rect) {
            return new Int32Rect((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);
        }
        private BitmapSource CroppedBitmapFromRect(BitmapSource source, Rect rect) {
            var dv = new DrawingVisual();

            using (DrawingContext dc = dv.RenderOpen()) {
                dc.DrawImage(new CroppedBitmap(source, RectToIntRect(rect)), rect);
            }

            dv.Offset = new Vector(-dv.ContentBounds.X, -dv.ContentBounds.Y);

            var bmp = new RenderTargetBitmap(
                (int)Math.Ceiling(dv.ContentBounds.Width),
                (int)Math.Ceiling(dv.ContentBounds.Height),
                96, 96, PixelFormats.Pbgra32);

            bmp.Render(dv);
            return bmp;
        }

        public void ClearRect() {
            if (this.Rect != null) {
                this.canvas.Children.Remove(this.Rect);
                this.Rect = null;
            }
        }
        public Rectangle Rect {
            get;
            private set;
        }
        public Point StartPos {
            get;
            private set;
        }
        private bool _drawing = false;
        private void canvas_MouseDown(object sender, MouseButtonEventArgs e) {
            if (this.Rect != null) {
                return;
            }
            _drawing = true;
            this.StartPos = e.GetPosition(this.canvas);
            this.Rect = new Rectangle {
                Stroke = Brushes.Green,
                StrokeThickness = 1
            };
            Canvas.SetLeft(this.Rect, StartPos.X);
            Canvas.SetTop(this.Rect, StartPos.Y);
            this.canvas.Children.Add(this.Rect);
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e) {
            if (!_drawing) {
                return;
            }
            if (e.LeftButton == MouseButtonState.Released || this.Rect == null) {
                return;
            }

            var mousePoint = e.GetPosition(canvas);
            var x = Math.Min(mousePoint.X, this.StartPos.X);
            var y = Math.Min(mousePoint.Y, this.StartPos.Y);
            var width = Math.Max(mousePoint.X, this.StartPos.X) - x;
            var height = Math.Max(mousePoint.Y, this.StartPos.Y) - y;

            this.Rect.Width = width;
            this.Rect.Height = height;
            Canvas.SetLeft(this.Rect, x);
            Canvas.SetTop(this.Rect, y);
        }

        private void canvas_MouseUp(object sender, MouseButtonEventArgs e) {
            _drawing = false;
        }
    }
}
