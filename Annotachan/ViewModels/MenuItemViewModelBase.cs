using Annotachan.Models;
using Livet;
using Livet.Commands;
using Livet.EventListeners;
using Livet.Messaging;
using Livet.Messaging.IO;
using Livet.Messaging.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Annotachan.ViewModels {
    public class MenuItemViewModelBase : ViewModel, MahApps.Metro.Controls.IHamburgerMenuItemBase {
        public delegate void ErrorOccurredEventHandler(object sender, ErrorOccurredEventArgs e);
        public event ErrorOccurredEventHandler ErrorOccurred = delegate { };
        public delegate void MessageEventHandler(object sender, MessageEventArgs e);
        public event MessageEventHandler Message = delegate { };
        protected void OnErrorOccurred(ErrorOccurredEventArgs e) {
            ErrorOccurred(this, e);
        }
        protected void OnMessage(MessageEventArgs e) {
            Message(this, e);
        }
        public MenuItemViewModelBase(MainWindowViewModel parent) : base() {
            this.MainViewModel = parent;
        }
        public MainWindowViewModel MainViewModel {
            get;
        }

        #region Icon変更通知プロパティ
        private object _Icon;

        public object Icon {
            get {
                return _Icon;
            }
            set {
                if (_Icon == value) {
                    return;
                }
                _Icon = value;
                RaisePropertyChanged(nameof(Icon));
            }
        }
        #endregion


        #region Label変更通知プロパティ
        private object _Label;

        public object Label {
            get {
                return _Label;
            }
            set {
                if (_Label == value) {
                    return;
                }
                _Label = value;
                RaisePropertyChanged(nameof(Label));
            }
        }
        #endregion


        #region ToolTip変更通知プロパティ
        private object _ToolTip;

        public object ToolTip {
            get {
                return _ToolTip;
            }
            set {
                if (_ToolTip == value) {
                    return;
                }
                _ToolTip = value;
                RaisePropertyChanged(nameof(ToolTip));
            }
        }
        #endregion


        #region IsVisible変更通知プロパティ
        private bool _IsVisible;

        public bool IsVisible {
            get {
                return _IsVisible;
            }
            set {
                if (_IsVisible == value) {
                    return;
                }
                _IsVisible = value;
                RaisePropertyChanged(nameof(IsVisible));
            }
        }
        #endregion


    }
}
