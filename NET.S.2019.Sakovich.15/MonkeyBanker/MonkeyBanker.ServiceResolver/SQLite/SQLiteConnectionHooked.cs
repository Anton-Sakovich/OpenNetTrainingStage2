using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonkeyBanker.Data.AdoNet;

namespace MonkeyBanker.ServiceResolver.SQLite
{
    public class SQLiteConnectionHooked : IDbConnectionWrapper
    {
        public SQLiteConnectionHooked(SQLiteConnection connectionBase)
        {
            this.ConnectionBase = connectionBase;
        }

        public SQLiteConnection ConnectionBase { get; }

        IDbConnection IDbConnectionWrapper.ConnectionBase
        {
            get
            {
                return this.ConnectionBase;
            }
        }

        public string ConnectionString
        {
            get
            {
                return this.ConnectionBase.ConnectionString;
            }
            set
            {
                this.ConnectionBase.ConnectionString = value;
            }
        }

        public int ConnectionTimeout
        {
            get
            {
                return this.ConnectionBase.ConnectionTimeout;
            }
        }

        public string Database
        {
            get
            {
                return this.ConnectionBase.Database;
            }
        }

        public ConnectionState State
        {
            get
            {
                return this.ConnectionBase.State;
            }
        }

        public IDbTransaction BeginTransaction()
        {
            return this.ConnectionBase.BeginTransaction();
        }

        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            return this.ConnectionBase.BeginTransaction(il);
        }

        public void ChangeDatabase(string databaseName)
        {
            this.ConnectionBase.ChangeDatabase(databaseName);
        }

        public void Close()
        {
            this.ConnectionBase.Close();
        }

        public IDbCommand CreateCommand()
        {
            return this.ConnectionBase.CreateCommand();
        }

        public void Dispose()
        {
            this.ConnectionBase.Dispose();
        }

        public void Open()
        {
            this.ConnectionBase.Open();

            SQLiteCommand command = new SQLiteCommand(
                "PRAGMA foreign_keys = 1;", this.ConnectionBase);

            command.ExecuteNonQuery();
        }

        public static implicit operator DbConnection(SQLiteConnectionHooked hookedConnection)
        {
            return hookedConnection.ConnectionBase;
        }
    }
}
