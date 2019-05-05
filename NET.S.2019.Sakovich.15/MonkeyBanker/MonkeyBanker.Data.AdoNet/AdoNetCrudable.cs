using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyBanker.Data.AdoNet
{
    public abstract class AdoNetCrudable<T> : ICrudable<T>
        where T : class
    {
        protected readonly DbProviderFactory factory;

        protected readonly string connectionString;

        protected AdoNetCrudable(DbProviderFactory factory, string connectionString)
        {
            this.factory = factory;
            this.connectionString = connectionString;
        }

        public int Create(T entity)
        {
            return this.ExecuteNonQuery(InsertCommand(entity));
        }

        public IEnumerable<T> Read()
        {
            return this.ExecuteSelectAll(this.SelectCommand());
        }

        public T Read(int id)
        {
            return this.ExecuteSelect(this.SelectCommand(id));
        }

        public int Update(T entity)
        {
            return this.ExecuteNonQuery(this.UpdateCommand(entity));
        }

        public int Delete(int id)
        {
            return this.ExecuteNonQuery(this.DeleteCommand(id));
        }

        protected abstract DbCommand InsertCommand(T entity);

        protected abstract DbCommand SelectCommand();

        protected abstract DbCommand SelectCommand(int id);

        protected abstract DbCommand UpdateCommand(T entity);

        protected abstract DbCommand DeleteCommand(int id);

        protected abstract T GetEntity(DbDataReader reader);

        private int ExecuteNonQuery(DbCommand command)
        {
            int rowsAffected;

            using (DbConnection connection = this.factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;

                using (command)
                {
                    command.Connection = connection;

                    connection.Open();

                    rowsAffected = command.ExecuteNonQuery();
                }
            }

            return rowsAffected;
        }

        private T ExecuteSelect(DbCommand command)
        {
            T readEntity = null;

            using (DbConnection connection = this.factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;

                using (command)
                {
                    command.Connection = connection;

                    connection.Open();

                    using (DbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            readEntity = GetEntity(reader);
                        }
                    }
                }
            }

            return readEntity;
        }

        private List<T> ExecuteSelectAll(DbCommand command)
        {
            List<T> entities = new List<T>();

            using (DbConnection connection = this.factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;

                using (command)
                {
                    command.Connection = connection;

                    connection.Open();

                    using (DbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            entities.Add(GetEntity(reader));
                        }
                    }
                }
            }

            return entities;
        }
    }
}
