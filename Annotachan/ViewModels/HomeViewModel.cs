using Livet.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAPICodePack;
using Microsoft.WindowsAPICodePack.Dialogs;
using Livet;
using Annotachan.Models;
namespace Annotachan.ViewModels {
    public class HomeViewModel : MenuItemViewModelBase {
        public HomeViewModel(MainWindowViewModel parent) : base(parent) {
            this.Images = new ObservableSynchronizedCollection<AnnoImage>();
            this.Images.CollectionChanged += (sender, e) => {
                if (e.OldItems != null) {
                    foreach (AnnoImage img in e.OldItems) {
                        img.PropertyChanged -= Img_PropertyChanged;
                    }
                }
                if (e.NewItems != null) {
                    foreach (AnnoImage img in e.NewItems) {
                        img.PropertyChanged += Img_PropertyChanged;
                    }
                }
            };
            this.ImageDirectory = @"C:\Users\jacksaki\Desktop\新しいフォルダー";
            this.SaveDirectory = @"C:\Users\jacksaki\Desktop\新しいフォルダー\新しいフォルダー";
        }

        public Action SaveImageAction {
            get;
            set;
        }
        public Action ClearRectAction {
            get;
            set;
        }

        private void Img_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            RaisePropertyChanged(nameof(Images));
        }

        private Livet.Commands.ViewModelCommand _ShowImageDirectoryDialogCommand;

        public Livet.Commands.ViewModelCommand ShowImageDirectoryDialogCommand {
            get {
                if (_ShowImageDirectoryDialogCommand == null) {
                    _ShowImageDirectoryDialogCommand = new Livet.Commands.ViewModelCommand(ShowImageDirectoryDialog);
                }
                return _ShowImageDirectoryDialogCommand;
            }
        }

        public void ShowImageDirectoryDialog() {
            var dlg = new CommonOpenFileDialog();
            dlg.IsFolderPicker = true;
            if (dlg.ShowDialog() == CommonFileDialogResult.Ok) {
                this.ImageDirectory = dlg.FileName;
            }
        }

        private ObservableSynchronizedCollection<AnnoImage> _Images;

        public ObservableSynchronizedCollection<AnnoImage> Images {
            get {
                return _Images;
            }
            private set { 
                if (_Images == value) {
                    return;
                }
                _Images = value;
                RaisePropertyChanged();
            }
        }

        private AnnoImage _SelectedImage;

        public AnnoImage SelectedImage {
            get {
                return _SelectedImage;
            }
            set { 
                if (_SelectedImage == value) {
                    return;
                }
                if (_SelectedImage != null) {
                    _SelectedImage.PropertyChanged -= _SelectedImage_PropertyChanged;
                }
                _SelectedImage = value;
                if (_SelectedImage != null) {
                    _SelectedImage.PropertyChanged += _SelectedImage_PropertyChanged;
                }
                DownScaleCommand.RaiseCanExecuteChanged();
                UpScaleCommand.RaiseCanExecuteChanged();
                IncreaseAngleCommand.RaiseCanExecuteChanged();
                DecreaseAngleCommand.RaiseCanExecuteChanged();
                SaveImageCommand.RaiseCanExecuteChanged();
                ClearRectCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged();
            }
        }

        private void _SelectedImage_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            if(e.PropertyName.Equals(nameof(this.SelectedImage.Scale))){
                ClearRectCommand.Execute();
            }
        }

        private string _ImageDirectory;

        public string ImageDirectory {
            get {
                return _ImageDirectory;
            }
            set { 
                if (_ImageDirectory == value) {
                    return;
                }
                _ImageDirectory = value;
                InitImagesCommand.Execute();
                RaisePropertyChanged();
            }
        }


        private string _SaveDirectory;

        public string SaveDirectory {
            get {
                return _SaveDirectory;
            }
            set { 
                if (_SaveDirectory == value) {
                    return;
                }
                _SaveDirectory = value;
                RaisePropertyChanged();
            }
        }


        private ViewModelCommand _SelectSaveDirectoryCommand;

        public ViewModelCommand SelectSaveDirectoryCommand {
            get {
                if (_SelectSaveDirectoryCommand == null) {
                    _SelectSaveDirectoryCommand = new ViewModelCommand(SelectSaveDirectory);
                }
                return _SelectSaveDirectoryCommand;
            }
        }

        public void SelectSaveDirectory() {
            var dlg = new CommonOpenFileDialog();
            dlg.IsFolderPicker = true;
            if (dlg.ShowDialog() == CommonFileDialogResult.Ok) {
                this.SaveDirectory = dlg.FileName;
            }
        }

        private ViewModelCommand _InitImagesCommand;

        public ViewModelCommand InitImagesCommand {
            get {
                if (_InitImagesCommand == null) {
                    _InitImagesCommand = new ViewModelCommand(InitImages, CanInitImages);
                }
                return _InitImagesCommand;
            }
        }

        public bool CanInitImages() {
            if (this.ImageDirectory == null) {
                return false;
            }
            return System.IO.Directory.Exists(this.ImageDirectory);
        }

        public void InitImages() {
            foreach (var file in System.IO.Directory.GetFiles(this.ImageDirectory, "*.*", System.IO.SearchOption.TopDirectoryOnly)) {
                if (!AppConfig.GetInstance().ImageFilters.Any(x => System.IO.Path.GetExtension(file).Equals(x, StringComparison.OrdinalIgnoreCase))) {
                    continue;
                }
                this.Images.Add(new AnnoImage(file));
            }
        }

        private ViewModelCommand _DownScaleCommand;

        public ViewModelCommand DownScaleCommand {
            get {
                if (_DownScaleCommand == null) {
                    _DownScaleCommand = new ViewModelCommand(DownScale, CanDownScale);
                }
                return _DownScaleCommand;
            }
        }

        public bool CanDownScale() {
            return this.SelectedImage != null;
        }

        public void DownScale() {
            this.SelectedImage.Scale /= 1.25d;
        }


        private ViewModelCommand _UpScaleCommand;

        public ViewModelCommand UpScaleCommand {
            get {
                if (_UpScaleCommand == null) {
                    _UpScaleCommand = new ViewModelCommand(UpScale, CanUpScale);
                }
                return _UpScaleCommand;
            }
        }

        public bool CanUpScale() {
            return this.SelectedImage != null;
        }

        public void UpScale() {
            this.SelectedImage.Scale *= 1.25d;
        }

        private ViewModelCommand _IncreaseAngleCommand;

        public ViewModelCommand IncreaseAngleCommand {
            get {
                if (_IncreaseAngleCommand == null) {
                    _IncreaseAngleCommand = new ViewModelCommand(IncreaseAngle, CanIncreaseAngle);
                }
                return _IncreaseAngleCommand;
            }
        }

        public bool CanIncreaseAngle() {
            return this.SelectedImage != null;
        }

        public void IncreaseAngle() {
            if (this.SelectedImage.Angle < 360d) {
                this.SelectedImage.Angle += 1d;
            } else {
                this.SelectedImage.Angle = 360d - (this.SelectedImage.Angle + 1d);
            }
        }


        private ViewModelCommand _DecreaseAngleCommand;

        public ViewModelCommand DecreaseAngleCommand {
            get {
                if (_DecreaseAngleCommand == null) {
                    _DecreaseAngleCommand = new ViewModelCommand(DecreaseAngle, CanDecreaseAngle);
                }
                return _DecreaseAngleCommand;
            }
        }

        public bool CanDecreaseAngle() {
            return this.SelectedImage != null;
        }

        public void DecreaseAngle() {
            if (this.SelectedImage.Angle >= 1d) {
                this.SelectedImage.Angle -= 1d;
            } else {
                this.SelectedImage.Angle = 360d - (this.SelectedImage.Angle - 1d);
            }
        }


        private ViewModelCommand _SaveImageCommand;

        public ViewModelCommand SaveImageCommand {
            get {
                if (_SaveImageCommand == null) {
                    _SaveImageCommand = new ViewModelCommand(SaveImage, CanSaveImage);
                }
                return _SaveImageCommand;
            }
        }

        public bool CanSaveImage() {
            return this.SelectedImage != null;
        }

        public void SaveImage() {
            this.SaveImageAction?.Invoke();
        }


        private ViewModelCommand _ClearRectCommand;

        public ViewModelCommand ClearRectCommand {
            get {
                if (_ClearRectCommand == null) {
                    _ClearRectCommand = new ViewModelCommand(ClearRect, CanClearRect);
                }
                return _ClearRectCommand;
            }
        }

        public bool CanClearRect() {
            return this.SelectedImage != null;
        }

        public void ClearRect() {
            this.ClearRectAction?.Invoke();
        }


    }
}
