using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Annotachan.Models {
    public class WrappingStream : Stream {
        private Stream _streamBase;

        public WrappingStream(Stream streamBase) {
            if (streamBase == null) {
                throw new ArgumentNullException("streamBase");
            }
            _streamBase = streamBase; //渡したStreamを内部ストリームとして保持
        }

        public override bool CanRead => _streamBase.CanRead;

        public override bool CanSeek => _streamBase.CanSeek;

        public override bool CanWrite => _streamBase.CanWrite;

        public override long Length => _streamBase.Length;

        public override long Position {
            get => _streamBase.Position;
            set {
                _streamBase.Position = value;
            }
        }

        public override void Flush() {
            _streamBase.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count) {
            return _streamBase.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin) {
            return _streamBase.Seek(offset, origin);
        }

        public override void SetLength(long value) {
            _streamBase.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count) {
            _streamBase.Write(buffer, offset, count);
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                _streamBase.Dispose();
                _streamBase = null;  //disposeしたら内部ストリームをnullにして参照を外す
            }
            base.Dispose(disposing);
        }
        private void ThrowIfDisposed() {
            if (_streamBase == null) {
                throw new ObjectDisposedException(GetType().Name);
            }
        }
    }
}
