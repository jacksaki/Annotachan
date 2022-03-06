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
    public class SettingsViewModel : MenuItemViewModelBase {
        public SettingsViewModel(MainWindowViewModel parent) : base(parent) {
        }
    }
}
