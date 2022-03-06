using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Annotachan.ViewModels {
    public class MessageEventArgs : EventArgs {
        public MessageEventArgs(string title, string message) : base() {
            this.Title = title;
            this.Message = message;
        }
        public string Title {
            get;
        }
        public string Message {
            get;
        }
    }
}
