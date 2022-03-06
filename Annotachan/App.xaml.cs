using Annotachan.ViewModels;
using Annotachan.Views;
using Livet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;

namespace Annotachan {
    public partial class App : Application {
        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        private static extern bool IsWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        private static extern bool IsWindowVisible(IntPtr hWnd);
        [DllImport("user32.dll")]
        private static extern IntPtr GetLastActivePopup(IntPtr hWnd);
        [DllImport("user32.dll")]
        private static extern int ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        internal enum SW {
            HIDE = 0,
            SHOWNORMAL = 1,
            SHOWMINIMIZED = 2,
            SHOWMAXIMIZED = 3,
            SHOWNOACTIVATE = 4,
            SHOW = 5,
            MINIMIZE = 6,
            SHOWMINNOACTIVE = 7,
            SHOWNA = 8,
            RESTORE = 9,
            SHOWDEFAULT = 10,
        }
        private void InitCulture() {
            var newCulture = new CultureInfo(CultureInfo.CurrentCulture.Name);
            //newCulture.DateTimeFormat.FullDateTimePattern = AppConfig.GetInstance().DateStringFormat;

            CultureInfo.DefaultThreadCurrentCulture = newCulture;
            CultureInfo.DefaultThreadCurrentUICulture = newCulture;
            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(System.Windows.Markup.XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
        }

        private void Application_Startup(object sender, StartupEventArgs e) {
            DispatcherHelper.UIDispatcher = Dispatcher;
            Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            InitCulture();
            var p = GetOtherProcess();
            if (p != null) {
                MessageBox.Show("既に起動しています。", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                ActivateProcess(p);
                Current.Shutdown(-1);
            }
            Application.Current.Exit += Current_Exit;
            Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            var mainWindow = new Views.MainWindow();
            mainWindow.DataContext = new ViewModels.MainWindowViewModel();
            this.MainWindow = mainWindow;
            mainWindow.Show();
        }

        private void Current_Exit(object sender, ExitEventArgs e) {
        }

        private void ActivateProcess(System.Diagnostics.Process p) {
            var hWnd = p.Handle;
            if (IsWindow(hWnd) && IsWindowVisible(hWnd)) {
                ShowWindow(hWnd, (int)SW.SHOWNORMAL);
                SetForegroundWindow(hWnd);
            }

        }
        private System.Diagnostics.Process GetOtherProcess() {
            var processes = System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileName(System.Reflection.Assembly.GetExecutingAssembly().Location));
            return processes.Where(x => x.Id != System.Diagnostics.Process.GetCurrentProcess().Id).FirstOrDefault();
        }
        private void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e) {
            if (e.Exception is AggregateException) {
                foreach (var ex in ((AggregateException)e.Exception).InnerExceptions) {
                    Logger.Fatal("エラー", ex);
                }
            } else {
                Logger.Fatal("エラー", e.Exception);
            }
            using (var vm = new ViewModels.ExceptionWindowViewModel()) {
                vm.EnableContinue = true;
                vm.Exception = e.Exception;
                var window = new Views.ExceptionWindow();
                window.DataContext = vm;
                if (window.ShowDialog() == true) {
                    e.Handled = true;
                    return;
                } else {
                    Current.Shutdown(-1);
                    System.Environment.Exit(1);
                }
            }
        }

        //集約エラーハンドラ
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
            Logger.Fatal("エラー", e.ExceptionObject as Exception);
            using (var vm = new ViewModels.ExceptionWindowViewModel()) {
                vm.EnableContinue = false;
                vm.Exception = e.ExceptionObject as Exception;
                var window = new Views.ExceptionWindow();
                window.DataContext = vm;
                window.ShowDialog();
                Current.Shutdown(-1);
                System.Environment.Exit(1);
            }
        }
    }
}
