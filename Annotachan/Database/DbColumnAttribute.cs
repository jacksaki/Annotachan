using System;

namespace Annotachan {
    public class DbColumnAttribute : Attribute {

        public DbColumnAttribute(string columnName) {
            this.ColumnName = columnName;
        }
        public DbColumnAttribute(string columnName, string dateFormat) : this(columnName) {
            this.DateFormat = dateFormat;
        }
        public string ColumnName {
            get;
        }

        public string DateFormat {
            get;
        }
    }
}