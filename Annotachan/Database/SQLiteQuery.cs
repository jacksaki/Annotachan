using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Annotachan {
    public class SQLiteQuery : DbQueryBase {
        private static string _connectionString;
        public SQLiteQuery(string connectionString) : base(connectionString) {
        }
        public SQLiteQuery() : base(_connectionString) {
        }

        public static void SetConnectionString(string value) {
            using (var conn = new SQLiteConnection(value)) {
                conn.Open();
                _connectionString = value;
            }
        }
        protected override IDbConnection GenerateConnection(string connectionString) {
            return new SQLiteConnection(connectionString);
        }
        protected override IDbDataParameter CreateParameter(string name, object value) {
            return new SQLiteParameter(name, value);
        }

    }
}