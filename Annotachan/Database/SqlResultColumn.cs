using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Annotachan {
    public class SqlResultColumn {
        internal SqlResultColumn(IDataReader dr, int index, SqlResultColumnCollection parent) {
            this.ColumnName = dr.GetName(index);
            this.ColumnIndex = index;
            this.DataType = dr.GetFieldType(index);
            this.Parent = parent;
        }
        public SqlResultColumnCollection Parent {
            get;
        }
        public string ColumnName {
            get;
        }
        public int ColumnIndex {
            get;
        }
        public Type DataType {
            get;
        }
    }
}
