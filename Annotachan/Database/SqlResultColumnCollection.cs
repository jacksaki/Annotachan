using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Annotachan {
    public class SqlResultColumnCollection : IEnumerable<SqlResultColumn> {
        private List<SqlResultColumn> _columns = null;
        public SqlResultColumnCollection(IDataReader dr, SqlResult parent) {
            this.Parent = parent;
            this.Count = dr.FieldCount;
            _columns = new List<SqlResultColumn>();
            for (var i = 0; i < this.Count; i++) {
                _columns.Add(new SqlResultColumn(dr, i, this));
            }
        }
        public int Count {
            get;
        }
        public SqlResult Parent {
            get;
        }
        public IEnumerator<SqlResultColumn> GetEnumerator() {
            return ((IEnumerable<SqlResultColumn>)this._columns).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return ((IEnumerable)this._columns).GetEnumerator();
        }
    }
}
