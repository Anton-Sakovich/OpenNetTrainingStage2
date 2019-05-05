using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonkeyBanker.Entities;

namespace MonkeyBanker.Data.AdoNet
{
    public class PeopleAdoNetCrudable : AdoNetCrudable<Person>
    {
        public PeopleAdoNetCrudable(DbProviderFactory factory, string connectionString)
            : base(factory, connectionString)
        {
        }

        protected override DbCommand SelectCommand(int id)
        {
            DbCommand command = this.factory.CreateCommand();

            command.CommandText = "SELECT * FROM People " +
                "WHERE ID=@ID;";

            DbParameter idParameter = this.factory.CreateParameter();
            idParameter.ParameterName = "@ID";
            idParameter.Value = id;
            command.Parameters.Add(idParameter);

            return command;
        }

        protected override DbCommand SelectCommand()
        {
            DbCommand command = this.factory.CreateCommand();

            command.CommandText = "SELECT * FROM People;";

            return command;
        }

        protected override DbCommand InsertCommand(Person entity)
        {
            DbCommand command = this.factory.CreateCommand();

            command.CommandText = "INSERT INTO People (GivenName, FamilyName) " +
                "VALUES (@GivenName, @FamilyName);";

            DbParameter givenName = this.factory.CreateParameter();
            givenName.ParameterName = "@GivenName";
            givenName.Value = entity.GivenName;
            command.Parameters.Add(givenName);

            DbParameter familyName = this.factory.CreateParameter();
            familyName.ParameterName = "@FamilyName";
            familyName.Value = entity.FamilyName;
            command.Parameters.Add(familyName);

            return command;
        }

        protected override DbCommand DeleteCommand(int id)
        {
            DbCommand command = this.factory.CreateCommand();

            command.CommandText = "DELETE FROM People WHERE ID=@ID;";

            DbParameter idParameter = this.factory.CreateParameter();
            idParameter.ParameterName = "@ID";
            idParameter.Value = id;
            command.Parameters.Add(idParameter);

            return command;
        }

        protected override DbCommand UpdateCommand(Person entity)
        {
            DbCommand command = this.factory.CreateCommand();

            command.CommandText = "UPDATE People " +
                "SET GivenName=@GivenName, FamilyName=@FamilyName " +
                "WHERE ID=@ID;";

            DbParameter id = this.factory.CreateParameter();
            id.ParameterName = "@ID";
            id.Value = entity.ID;
            command.Parameters.Add(id);

            DbParameter givenName = this.factory.CreateParameter();
            givenName.ParameterName = "@GivenName";
            givenName.Value = entity.GivenName;
            command.Parameters.Add(givenName);

            DbParameter familyName = this.factory.CreateParameter();
            familyName.ParameterName = "@FamilyName";
            familyName.Value = entity.FamilyName;
            command.Parameters.Add(familyName);

            return command;
        }

        protected override Person GetEntity(DbDataReader reader)
        {
            return new Person()
            {
                ID = reader.GetInt32(0),
                GivenName = reader.GetString(1),
                FamilyName = reader.GetString(2)
            };
        }
    }
}
