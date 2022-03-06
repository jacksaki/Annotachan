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
    public class ExceptionWindowViewModel : ViewModel {
        /* コマンド、プロパティの定義にはそれぞれ 
         * 
         *  lvcom   : ViewModelCommand
         *  lvcomn  : ViewModelCommand(CanExecute無)
         *  llcom   : ListenerCommand(パラメータ有のコマンド)
         *  llcomn  : ListenerCommand(パラメータ有のコマンド・CanExecute無)
         *  lprop   : 変更通知プロパティ(.NET4.5ではlpropn)
         *  
         * を使用してください。
         * 
         * Modelが十分にリッチであるならコマンドにこだわる必要はありません。
         * View側のコードビハインドを使用しないMVVMパターンの実装を行う場合でも、ViewModelにメソッドを定義し、
         * LivetCallMethodActionなどから直接メソッドを呼び出してください。
         * 
         * ViewModelのコマンドを呼び出せるLivetのすべてのビヘイビア・トリガー・アクションは
         * 同様に直接ViewModelのメソッドを呼び出し可能です。
         */

        /* ViewModelからViewを操作したい場合は、View側のコードビハインド無で処理を行いたい場合は
         * Messengerプロパティからメッセージ(各種InteractionMessage)を発信する事を検討してください。
         */

        /* Modelからの変更通知などの各種イベントを受け取る場合は、PropertyChangedEventListenerや
         * CollectionChangedEventListenerを使うと便利です。各種ListenerはViewModelに定義されている
         * CompositeDisposableプロパティ(LivetCompositeDisposable型)に格納しておく事でイベント解放を容易に行えます。
         * 
         * ReactiveExtensionsなどを併用する場合は、ReactiveExtensionsのCompositeDisposableを
         * ViewModelのCompositeDisposableプロパティに格納しておくのを推奨します。
         * 
         * LivetのWindowテンプレートではViewのウィンドウが閉じる際にDataContextDisposeActionが動作するようになっており、
         * ViewModelのDisposeが呼ばれCompositeDisposableプロパティに格納されたすべてのIDisposable型のインスタンスが解放されます。
         * 
         * ViewModelを使いまわしたい時などは、ViewからDataContextDisposeActionを取り除くか、発動のタイミングをずらす事で対応可能です。
         */

        /* UIDispatcherを操作する場合は、DispatcherHelperのメソッドを操作してください。
         * UIDispatcher自体はApp.xaml.csでインスタンスを確保してあります。
         * 
         * LivetのViewModelではプロパティ変更通知(RaisePropertyChanged)やDispatcherCollectionを使ったコレクション変更通知は
         * 自動的にUIDispatcher上での通知に変換されます。変更通知に際してUIDispatcherを操作する必要はありません。
         */

        public void Initialize() {
        }
        #region Exception変更通知プロパティ
        private Exception _Exception;

        public Exception Exception {
            get {
                return _Exception;
            }
            set {
                if (_Exception == value) {
                    return;
                }
                _Exception = value;
                RaisePropertyChanged(nameof(Exception));
                RaisePropertyChanged(nameof(ErrorText));
            }
        }
        #endregion



        #region ErrorText変更通知プロパティ

        public string ErrorText {
            get {
                if (this.Exception is AggregateException) {
                    return string.Join("\r\n\r\n", ((AggregateException)this.Exception).InnerExceptions.Select(x => $"{x.Message}\r\n\r\n{x.StackTrace}"));
                } else {
                    return $"{this.Exception.Message}\r\n\r\n{this.Exception.StackTrace}";
                }
            }
        }
        #endregion


        #region ContinueCommand
        private ViewModelCommand _ContinueCommand;

        public ViewModelCommand ContinueCommand {
            get {
                if (_ContinueCommand == null) {
                    _ContinueCommand = new ViewModelCommand(Continue, CanContinue);
                }
                return _ContinueCommand;
            }
        }

        public bool CanContinue() {
            return this.EnableContinue;
        }

        public void Continue() {
            this.DialogResult = true;
        }
        #endregion



        #region QuitCommand
        private ViewModelCommand _QuitCommand;

        public ViewModelCommand QuitCommand {
            get {
                if (_QuitCommand == null) {
                    _QuitCommand = new ViewModelCommand(Quit);
                }
                return _QuitCommand;
            }
        }

        public void Quit() {
            this.DialogResult = false;
        }
        #endregion


        #region DialogResult変更通知プロパティ
        private bool? _DialogResult;

        public bool? DialogResult {
            get {
                return _DialogResult;
            }
            private set {
                if (_DialogResult == value) {
                    return;
                }
                _DialogResult = value;
                RaisePropertyChanged(nameof(DialogResult));
            }
        }
        #endregion



        #region EnableContinue変更通知プロパティ
        private bool _EnableContinue;

        public bool EnableContinue {
            get {
                return _EnableContinue;
            }
            set {
                if (_EnableContinue == value) {
                    return;
                }
                _EnableContinue = value;
                RaisePropertyChanged(nameof(EnableContinue));
            }
        }
        #endregion


    }
}
