using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Annotachan.ViewModels {
    public class ErrorOccurredEventArgs : EventArgs {
        public ErrorOccurredEventArgs(string message) {
            this.Message = message;
        }
        public ErrorOccurredEventArgs(string message, Exception ex) : this(message) {
            this.Exception = ex;
        }
        public string Message {
            get;
        }
        public Exception Exception {
            get;
        }
    }
}
