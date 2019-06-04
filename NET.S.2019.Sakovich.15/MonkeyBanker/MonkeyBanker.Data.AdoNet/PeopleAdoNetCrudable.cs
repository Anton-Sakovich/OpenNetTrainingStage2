using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonkeyBanker.Entities;

namespace MonkeyBanker.Data.AdoNet
{
    public class PeopleAdoNetCrudable : AdoNetCrudable<Person>
    {
        public PeopleAdoNetCrudable(IDbEntryPoint factory, string connectionString)
            : base(factory, connectionString)
        {
        }

        protected override IDbCommand SelectCommand(int id)
        {
            IDbCommand command = this.factory.CreateCommand();

            command.CommandText = "SELECT * FROM People " +
                "WHERE ID=@ID;";

            IDbDataParameter idParameter = this.factory.CreateParameter();
            idParameter.ParameterName = "@ID";
            idParameter.Value = id;
            command.Parameters.Add(idParameter);

            return command;
        }

        protected override IDbCommand SelectCommand()
        {
            IDbCommand command = this.factory.CreateCommand();

            command.CommandText = "SELECT * FROM People;";

            return command;
        }

        protected override IDbCommand InsertCommand(Person entity)
        {
            IDbCommand command = this.factory.CreateCommand();

            command.CommandText = "INSERT INTO People (GivenName, FamilyName) " +
                "VALUES (@GivenName, @FamilyName);";

            IDbDataParameter givenName = this.factory.CreateParameter();
            givenName.ParameterName = "@GivenName";
            givenName.Value = entity.GivenName;
            command.Parameters.Add(givenName);

            IDbDataParameter familyName = this.factory.CreateParameter();
            familyName.ParameterName = "@FamilyName";
            familyName.Value = entity.FamilyName;
            command.Parameters.Add(familyName);

            return command;
        }

        protected override IDbCommand DeleteCommand(int id)
        {
            IDbCommand command = this.factory.CreateCommand();

            command.CommandText = "DELETE FROM People WHERE ID=@ID;";

            IDbDataParameter idParameter = this.factory.CreateParameter();
            idParameter.ParameterName = "@ID";
            idParameter.Value = id;
            command.Parameters.Add(idParameter);

            return command;
        }

        protected override IDbCommand UpdateCommand(Person entity)
        {
            IDbCommand command = this.factory.CreateCommand();

            command.CommandText = "UPDATE People " +
                "SET GivenName=@GivenName, FamilyName=@FamilyName " +
                "WHERE ID=@ID;";

            IDbDataParameter id = this.factory.CreateParameter();
            id.ParameterName = "@ID";
            id.Value = entity.ID;
            command.Parameters.Add(id);

            IDbDataParameter givenName = this.factory.CreateParameter();
            givenName.ParameterName = "@GivenName";
            givenName.Value = entity.GivenName;
            command.Parameters.Add(givenName);

            IDbDataParameter familyName = this.factory.CreateParameter();
            familyName.ParameterName = "@FamilyName";
            familyName.Value = entity.FamilyName;
            command.Parameters.Add(familyName);

            return command;
        }

        protected override Person GetEntity(IDataReader reader)
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
