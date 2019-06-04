using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyBanker.Data.AdoNet
{
    public class AccountsIdEnumerable : IEnumerable<int>
    {
        protected IDbEntryPoint DataEntryPoint;

        protected string ConnectionString;

        public AccountsIdEnumerable(IDbEntryPoint factory, string connectionString)
        {
            this.DataEntryPoint = factory;

            this.ConnectionString = connectionString;
        }

        public IEnumerator<int> GetEnumerator()
        {
            using (IDbConnectionWrapper connection = this.DataEntryPoint.CreateConnection())
            {
                connection.ConnectionString = this.ConnectionString;

                using (IDbCommand command = this.DataEntryPoint.CreateCommand())
                {
                    command.CommandText = "SELECT ID FROM Accounts;";
                    command.Connection = connection.ConnectionBase;

                    connection.Open();

                    using (IDataReader reader = command.ExecuteReader())
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
