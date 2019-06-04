using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyBanker.Data.AdoNet
{
    public abstract class AdoNetCrudable<T> : ICrudable<T>
        where T : class
    {
        protected readonly IDbEntryPoint factory;

        protected readonly string connectionString;

        protected AdoNetCrudable(IDbEntryPoint factory, string connectionString)
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

        protected abstract IDbCommand InsertCommand(T entity);

        protected abstract IDbCommand SelectCommand();

        protected abstract IDbCommand SelectCommand(int id);

        protected abstract IDbCommand UpdateCommand(T entity);

        protected abstract IDbCommand DeleteCommand(int id);

        protected abstract T GetEntity(IDataReader reader);

        private int ExecuteNonQuery(IDbCommand command)
        {
            int rowsAffected;

            using (IDbConnectionWrapper connection = this.factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;

                using (command)
                {
                    command.Connection = connection.ConnectionBase;

                    connection.Open();

                    rowsAffected = command.ExecuteNonQuery();
                }
            }

            return rowsAffected;
        }

        private T ExecuteSelect(IDbCommand command)
        {
            T readEntity = null;

            using (IDbConnectionWrapper connection = this.factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;

                using (command)
                {
                    command.Connection = connection.ConnectionBase;

                    connection.Open();

                    using (IDataReader reader = command.ExecuteReader())
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

        private List<T> ExecuteSelectAll(IDbCommand command)
        {
            List<T> entities = new List<T>();

            using (IDbConnectionWrapper connection = this.factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;

                using (command)
                {
                    command.Connection = connection.ConnectionBase;

                    connection.Open();

                    using (IDataReader reader = command.ExecuteReader())
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
