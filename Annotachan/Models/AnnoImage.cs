using Livet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.IO;
namespace Annotachan.Models {
    public class AnnoImage : NotificationObject {
        public AnnoImage(string path) {
            this.Path = path;
            this.Image = new BitmapImage();
            using (var stream = new WrappingStream(System.IO.File.OpenRead(path))) {
                this.Image.BeginInit();
                this.Image.CacheOption = BitmapCacheOption.OnLoad;
                this.Image.StreamSource = stream;
                this.Image.EndInit();
                this.Image.Freeze();
            }
        }
        public BitmapImage Image {
            get;
        }

        private string _Path;

        public string Path {
            get {
                return _Path;
            }
            set { 
                if (_Path == value)
                    return;
                _Path = value;
                RaisePropertyChanged();
            }
        }
        public string RectSavePath {
            get {
                return System.IO.Path.ChangeExtension(this.Path, ".rect" + System.IO.Path.GetExtension(this.Path));
            }
        }
        public string CanvasSavePath {
            get {
                return System.IO.Path.ChangeExtension(this.Path, ".canvas" + System.IO.Path.GetExtension(this.Path));
            }
        }

        /// <summary>
        /// +----------------------+
        /// |                      |
        /// |                      |
        /// +----------------------+
        /// 斜辺の長さを取得します
        /// </summary>
        public double Length {
            get {
                return System.Math.Sqrt(System.Math.Pow(this.Image.Height, 2) + System.Math.Pow(this.Image.Width, 2)) * this.Scale;
            }
        }
        /// <summary>
        /// Canvasの幅を取得します。
        ///  Canvasの幅はLengthと一致します
        /// </summary>
        public double CanvasWidth {
            get {
                return this.Length;
            }
        }

        /// <summary>
        /// Canvasの高さを取得します。
        ///  Canvasの高さはLengthと一致します
        /// </summary>
        public double CanvasHeight {
            get {
                return this.Length;
            }
        }
        /// <summary>
        /// 拡大/縮小後の画像の幅を取得します。
        ///  ※元画像の幅 / スケール
        /// </summary>
        public double ImageWidth {
            get {
                return this.Image.Width * this.Scale; 
            }
        }

        /// <summary>
        /// 拡大/縮小後の画像の高さを取得します。
        ///  ※元画像の高さ / スケール
        /// </summary>
        public double ImageHeight {
            get {
                return this.Image.Height * this.Scale;
            }
        }

        private double _Angle;
        /// <summary>
        /// 回転角度を取得または設定します
        /// </summary>
        public double Angle {
            get {
                return _Angle;
            }
            set { 
                if (_Angle == value) {
                    return;
                }
                _Angle = value;
                RaisePropertyChanged();
            }
        }
        /// <summary>
        /// 画像の左上のX座標を取得します。
        /// </summary>
        public double X {
            get {
                return this.CanvasWidth / 2d - this.ImageWidth / 2d;
            }
        }
        /// <summary>
        /// 画像の左上のY座標を取得します。
        /// 　画像の中心は高さ/2
        /// </summary>
        public double Y {
            get {
                return this.CanvasHeight/2d - this.ImageHeight / 2d;
            }
        }


        private double _Scale = 1d;

        public double Scale {
            get {
                return _Scale;
            }
            set { 
                if (_Scale == value) {
                    return;
                }
                _Scale = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(ImageHeight));
                RaisePropertyChanged(nameof(ImageWidth));
                RaisePropertyChanged(nameof(Length));
                RaisePropertyChanged(nameof(CanvasHeight));
                RaisePropertyChanged(nameof(CanvasWidth));
                RaisePropertyChanged(nameof(X));
                RaisePropertyChanged(nameof(Y));
                RaisePropertyChanged(nameof(CenterX));
                RaisePropertyChanged(nameof(CenterY));
            }
        }

        public double CenterX {
            get {
                return this.ImageWidth / 2d;
            }
        }

        public double CenterY {
            get {
                return this.ImageHeight / 2d;
            }
        }

    }
}
