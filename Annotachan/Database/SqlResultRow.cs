using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Annotachan {
    public class SqlResultRow {
        public SqlResultRow(IDataReader dr, SqlResultRowCollection parent) {
            this.Parent = parent;
            this.Values = new object[parent.Parent.Columns.Count];
            dr.GetValues(this.Values);
        }
        public object this[int columnIndex] {
            get {
                return this.Values[columnIndex];
            }
        }
        public object this[string columnName] {
            get {
                return this.Values[this.Parent.Parent.Columns.Where(x => x.ColumnName.Equals(columnName, StringComparison.OrdinalIgnoreCase)).First().ColumnIndex];
            }
        }
        public SqlResultRowCollection Parent {
            get;
        }
        public object[] Values {
            get;
        }
    }
}
