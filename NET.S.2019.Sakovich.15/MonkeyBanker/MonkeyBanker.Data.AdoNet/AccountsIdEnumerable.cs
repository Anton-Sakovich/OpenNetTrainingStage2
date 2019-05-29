using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyBanker.Data.AdoNet
{
    public class AccountsIdEnumerable : IEnumerable<int>
    {
        protected DbProviderFactory Factory;

        protected string ConnectionString;

        public AccountsIdEnumerable(DbProviderFactory factory, string connectionString)
        {
            this.Factory = factory;

            this.ConnectionString = connectionString;
        }

        public IEnumerator<int> GetEnumerator()
        {
            using (DbConnection connection = this.Factory.CreateConnection())
            {
                connection.ConnectionString = this.ConnectionString;

                using (DbCommand command = this.Factory.CreateCommand())
                {
                    command.CommandText = "SELECT ID FROM Accounts;";
                    command.Connection = connection;

                    connection.Open();

                    using (DbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return reader.GetInt32(0);
                        }
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
