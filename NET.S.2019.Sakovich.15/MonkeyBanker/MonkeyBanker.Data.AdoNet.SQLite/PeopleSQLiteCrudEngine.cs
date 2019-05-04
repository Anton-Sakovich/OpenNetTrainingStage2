using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonkeyBanker.Entities;

namespace MonkeyBanker.Data.AdoNet.SQLite
{
    public class PeopleSQLiteCrudEngine : ICrudable<Person>
    {
        private readonly string connectionString;

        public PeopleSQLiteCrudEngine(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int Create(Person entity)
        {
            int rowsAffected;

            using (SQLiteConnection connection = new SQLiteConnection(this.connectionString))
            {
                using (IDbCommand command = new SQLiteCommand(connection).InsertsPeople(SQLiteFactory.Instance))
                {
                    ((IDbDataParameter)command.Parameters["GivenName"]).Value = entity.GivenName;
                    ((IDbDataParameter)command.Parameters["FamilyName"]).Value = entity.FamilyName;

                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }

            return rowsAffected;
        }

        public Person Read(int id)
        {
            Person readPerson = null;

            using (SQLiteConnection connection = new SQLiteConnection(this.connectionString))
            {
                using (IDbCommand command = new SQLiteCommand(connection).SelectsPerson(SQLiteFactory.Instance))
                {
                    ((IDbDataParameter)command.Parameters["ID"]).Value = id;

                    connection.Open();
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            readPerson = reader.GetPerson();
                        }
                    }
                }
            }

            return readPerson;
        }

        public int Update(Person entity)
        {
            int rowsAffected;

            using (SQLiteConnection connection = new SQLiteConnection(this.connectionString))
            {
                using (IDbCommand command = new SQLiteCommand(connection).UpdatesPeople(SQLiteFactory.Instance))
                {
                    ((IDbDataParameter)command.Parameters[":ID"]).Value = entity.ID;
                    ((IDbDataParameter)command.Parameters[":GivenName"]).Value = entity.GivenName;
                    ((IDbDataParameter)command.Parameters[":FamilyName"]).Value = entity.FamilyName;

                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }

            return rowsAffected;
        }

        public int Delete(int id)
        {
            int rowsAffected;

            using (SQLiteConnection connection = new SQLiteConnection(this.connectionString))
            {
                using (IDbCommand command = new SQLiteCommand(connection).DeletesPeople(SQLiteFactory.Instance))
                {
                    ((IDbDataParameter)command.Parameters["ID"]).Value = id;

                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }

            return rowsAffected;
        }

        public IEnumerable<Person> ReadAll()
        {
            List<Person> people = new List<Person>();

            using (SQLiteConnection connection = new SQLiteConnection(this.connectionString))
            {
                using (IDbCommand command = new SQLiteCommand(connection).SelectsPeople(SQLiteFactory.Instance))
                {
                    connection.Open();
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            people.Add(reader.GetPerson());
                        }
                    }
                }
            }

            return people;
        }

        private int ExecuteNonQuery(SQLiteCommand command)
        {
            int rowsAffected;

            using (SQLiteConnection connection = new SQLiteConnection(this.connectionString))
            {
                using (command)
                {
                    command.Connection = connection;
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }

            return rowsAffected;
        }
    }
}
