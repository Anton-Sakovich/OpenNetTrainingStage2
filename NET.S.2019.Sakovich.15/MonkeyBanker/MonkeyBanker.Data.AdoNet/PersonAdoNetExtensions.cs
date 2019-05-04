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
    public static class PersonAdoNetExtensions
    {
        public static Person GetPerson(this IDataReader reader)
        {
            return new Person()
            {
                ID = reader.GetInt32(0),
                GivenName = reader.GetString(1),
                FamilyName = reader.GetString(2)
            };
        }

        public static Person ToPerson(this DataRow row)
        {
            return new Person()
            {
                ID = (int)row["ID"],
                GivenName = (string)row["GivenName"],
                FamilyName = (string)row["FamilyName"]
            };
        }

        public static DataRow InsertPerson(this DataTable table, Person entity)
        {
            DataRow newRow = table.NewRow();

            newRow["GivenName"] = entity.GivenName;
            newRow["FamilyName"] = entity.FamilyName;

            table.Rows.Add(newRow);

            return newRow;
        }

        public static DataRow UpdatePerson(this DataTable table, Person entity)
        {
            DataRow updatedRow = table.Rows.Find(entity.ID);

            updatedRow["GivenName"] = entity.GivenName;
            updatedRow["FamilyName"] = entity.FamilyName;

            return updatedRow;
        }

        public static DataRow DeletePerson(this DataTable table, Person entity)
        {
            DataRow deletedRow = table.Rows.Find(entity.ID);

            deletedRow.Delete();

            return deletedRow;
        }

        public static IDbDataAdapter AdaptsPeople(this IDbDataAdapter adapter, DbProviderFactory factory)
        {
            adapter.SelectCommand = factory.CreateCommand().SelectsPeople(factory);

            adapter.UpdateCommand = factory.CreateCommand().UpdatesPeople(factory);

            adapter.DeleteCommand = factory.CreateCommand().DeletesPeople(factory);

            adapter.InsertCommand = factory.CreateCommand().InsertsPeople(factory);

            return adapter;
        }

        public static IDbCommand SelectsPeople(this IDbCommand command, DbProviderFactory factory)
        {
            command.CommandText = "SELECT * FROM People;";

            return command;
        }

        public static IDbCommand SelectsPerson(this IDbCommand command, DbProviderFactory factory)
        {
            IDbDataParameter idParameter = factory.CreateParameter();
            idParameter.ParameterName = "ID";
            idParameter.SourceColumn = "ID";
            idParameter.SourceVersion = DataRowVersion.Original;

            command.CommandText = "SELECT * FROM People " +
                "WHERE ID=:ID;";
            command.Parameters.Add(idParameter);

            return command;
        }

        public static IDbCommand UpdatesPeople(this IDbCommand command, DbProviderFactory factory)
        {
            IDbDataParameter givenNameParameter = factory.CreateParameter();
            givenNameParameter.ParameterName = ":GivenName";
            givenNameParameter.SourceColumn = "GivenName";
            givenNameParameter.SourceVersion = DataRowVersion.Current;

            IDbDataParameter familyNameParameter = factory.CreateParameter();
            familyNameParameter.ParameterName = ":FamilyName";
            familyNameParameter.SourceColumn = "FamilyName";
            familyNameParameter.SourceVersion = DataRowVersion.Current;

            IDbDataParameter idParameterOnUpdate = factory.CreateParameter();
            idParameterOnUpdate.ParameterName = ":ID";
            idParameterOnUpdate.SourceColumn = "ID";
            idParameterOnUpdate.SourceVersion = DataRowVersion.Original;

            command.CommandText = "UPDATE People " +
                "SET GivenName=:GivenName, FamilyName=:FamilyName " +
                "WHERE ID=:ID;";
            command.Parameters.Add(idParameterOnUpdate);
            command.Parameters.Add(givenNameParameter);
            command.Parameters.Add(familyNameParameter);

            return command;
        }

        public static IDbCommand InsertsPeople(this IDbCommand command, DbProviderFactory factory)
        {
            IDbDataParameter givenNameParameter = factory.CreateParameter();
            givenNameParameter.ParameterName = "GivenName";
            givenNameParameter.SourceColumn = "GivenName";
            givenNameParameter.SourceVersion = DataRowVersion.Current;

            IDbDataParameter familyNameParameter = factory.CreateParameter();
            familyNameParameter.ParameterName = "FamilyName";
            familyNameParameter.SourceColumn = "FamilyName";
            familyNameParameter.SourceVersion = DataRowVersion.Current;

            command.CommandText = "INSERT INTO People " +
                "(GivenName, FamilyName) VALUES " +
                "(:GivenName, :FamilyName);";
            command.Parameters.Add(givenNameParameter);
            command.Parameters.Add(familyNameParameter);

            return command;
        }

        public static IDbCommand DeletesPeople(this IDbCommand command, DbProviderFactory factory)
        {
            IDbDataParameter idParameterOnDelete = factory.CreateParameter();
            idParameterOnDelete.ParameterName = "ID";
            idParameterOnDelete.SourceColumn = "ID";
            idParameterOnDelete.SourceVersion = DataRowVersion.Current;

            command.CommandText = "DELETE FROM People " +
                "WHERE ID=:ID;";
            command.Parameters.Add(idParameterOnDelete);

            return command;
        }
    }
}
