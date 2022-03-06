using Annotachan.Models;
using Livet;
using Livet.Commands;
using Livet.EventListeners;
using Livet.Messaging;
using Livet.Messaging.IO;
using Livet.Messaging.Windows;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Annotachan.ViewModels {
    public class MainWindowViewModel : ViewModel {
        public MainWindowViewModel() : base() {
            this.DialogCoordinator = MahApps.Metro.Controls.Dialogs.DialogCoordinator.Instance;

            var fv = System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);
            this.AppTitle = $"{fv.ProductName} Ver {fv.ProductVersion}";
        }
        public MahApps.Metro.Controls.Dialogs.IDialogCoordinator DialogCoordinator {
            get;
            set;
        }
        public string AppTitle {
            get;
        }

        public void Initialize() {
            InitMenus();
        }

        private void InitMenus() {
            this.MenuItems = new ObservableCollection<MenuItemViewModelBase>();
            this.MenuItems.CollectionChanged += (sender, e) => {
                RaisePropertyChanged(nameof(MenuItems));
            };
            this.MenuOptionItems = new ObservableCollection<MenuItemViewModelBase>();
            this.MenuOptionItems.CollectionChanged += (sender, e) => {
                RaisePropertyChanged(nameof(MenuOptionItems));
            };
            var home = new HomeViewModel(this) {
                Icon = new PackIcon { Kind = MaterialDesignThemes.Wpf.PackIconKind.Home },
                Label = "Home",
                IsVisible = true,
                ToolTip = "Welcome Home",
            };
            home.Message += Menu_Message;
            home.ErrorOccurred += Menu_ErrorOccurred;
            this.MenuItems.Add(home);

            var settings = new SettingsViewModel(this) {
                Icon = new PackIcon() { Kind = PackIconKind.Settings },
                IsVisible = true,
                Label = "Settings",
                ToolTip = "Template Settings"
            };
            settings.ErrorOccurred += Menu_ErrorOccurred;
            settings.Message += Menu_Message;
            this.MenuOptionItems.Add(settings);

        }

        private void Menu_Message(object sender, MessageEventArgs e) {
            DialogCoordinator.ShowMessageAsync(this, e.Title, e.Message, MahApps.Metro.Controls.Dialogs.MessageDialogStyle.Affirmative);
        }

        private void Menu_ErrorOccurred(object sender, ErrorOccurredEventArgs e) {
            DialogCoordinator.ShowMessageAsync(this, "エラー", e.Message, MahApps.Metro.Controls.Dialogs.MessageDialogStyle.Affirmative);
        }

        public ObservableCollection<MenuItemViewModelBase> MenuItems {
            get;
            private set;
        }
        public ObservableCollection<MenuItemViewModelBase> MenuOptionItems {
            get;
            private set;
        }



        private int _SelectedIndex;
        public int SelectedIndex {
            get {
                return _SelectedIndex;
            }
            set {
                if (_SelectedIndex == value) {
                    return;
                }
                _SelectedIndex = value;
                RaisePropertyChanged();
            }
        }

    }
}