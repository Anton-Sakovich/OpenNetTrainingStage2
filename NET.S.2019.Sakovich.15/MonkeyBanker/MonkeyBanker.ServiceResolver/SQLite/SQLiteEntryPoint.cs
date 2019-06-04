using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonkeyBanker.Data.AdoNet;

namespace MonkeyBanker.ServiceResolver.SQLite
{
    public class SQLiteEntryPoint : IDbEntryPoint
    {
        public IDbCommand CreateCommand()
        {
            return new SQLiteCommand();
        }

        public IDbConnectionWrapper CreateConnection()
        {
            return new SQLiteConnectionHooked(new SQLiteConnection());
        }

        public IDbDataParameter CreateParameter()
        {
            return new SQLiteParameter();
        }
    }
}
