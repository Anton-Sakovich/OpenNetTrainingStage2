using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonkeyBanker.Entities;

namespace MonkeyBanker.Data.AdoNet
{
    public class AccountsAdoNetCrudable : AdoNetCrudable<Account>
    {
        public AccountsAdoNetCrudable(DbProviderFactory factory, string connectionString)
            : base(factory, connectionString)
        {
        }

        protected override DbCommand SelectCommand(int id)
        {
            DbCommand command = this.factory.CreateCommand();

            command.CommandText = "SELECT * FROM Accounts " +
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

            command.CommandText = "SELECT * FROM Accounts;";

            return command;
        }

        protected override DbCommand InsertCommand(Account entity)
        {
            DbCommand command = this.factory.CreateCommand();

            command.CommandText = "INSERT INTO Accounts (PersonID, Balance, Bonuses, Type, IsActive) " +
                "VALUES (@PersonID, @Balance, @Bonuses, @Type, @IsActive);";

            DbParameter personIDParameter = this.factory.CreateParameter();
            personIDParameter.ParameterName = "@PersonID";
            personIDParameter.Value = entity.PersonID;
            command.Parameters.Add(personIDParameter);

            DbParameter balanceParameter = this.factory.CreateParameter();
            balanceParameter.ParameterName = "@Balance";
            balanceParameter.Value = entity.Balance;
            command.Parameters.Add(balanceParameter);

            DbParameter bonusesParameter = this.factory.CreateParameter();
            bonusesParameter.ParameterName = "@Bonuses";
            bonusesParameter.Value = entity.Bonuses;
            command.Parameters.Add(bonusesParameter);

            DbParameter typeParameter = this.factory.CreateParameter();
            typeParameter.ParameterName = "@Type";
            typeParameter.Value = entity.Type;
            command.Parameters.Add(typeParameter);

            DbParameter isActiveParameter = this.factory.CreateParameter();
            isActiveParameter.ParameterName = "@IsActive";
            isActiveParameter.Value = entity.IsActive ? 0 : 1;
            command.Parameters.Add(isActiveParameter);

            return command;
        }

        protected override DbCommand DeleteCommand(int id)
        {
            DbCommand command = this.factory.CreateCommand();

            command.CommandText = "DELETE FROM Accounts WHERE ID=@ID;";

            DbParameter idParameter = this.factory.CreateParameter();
            idParameter.ParameterName = "@ID";
            idParameter.Value = id;
            command.Parameters.Add(idParameter);

            return command;
        }

        protected override DbCommand UpdateCommand(Account entity)
        {
            DbCommand command = this.factory.CreateCommand();

            command.CommandText = "UPDATE Accounts " +
                "SET PersonID=@PersonID, Balance=@Balance, Bonuses=@Bonuses, Type=@Type, IsActive=@IsActive " +
                "WHERE ID=@ID;";

            DbParameter idParameter = this.factory.CreateParameter();
            idParameter.ParameterName = "@ID";
            idParameter.Value = entity.ID;
            command.Parameters.Add(idParameter);

            DbParameter personIDParameter = this.factory.CreateParameter();
            personIDParameter.ParameterName = "@PersonID";
            personIDParameter.Value = entity.PersonID;
            command.Parameters.Add(personIDParameter);

            DbParameter balanceParameter = this.factory.CreateParameter();
            balanceParameter.ParameterName = "@Balance";
            balanceParameter.Value = entity.Balance;
            command.Parameters.Add(balanceParameter);

            DbParameter bonusesParameter = this.factory.CreateParameter();
            bonusesParameter.ParameterName = "@Bonuses";
            bonusesParameter.Value = entity.Bonuses;
            command.Parameters.Add(bonusesParameter);

            DbParameter typeParameter = this.factory.CreateParameter();
            typeParameter.ParameterName = "@Type";
            typeParameter.Value = entity.Type;
            command.Parameters.Add(typeParameter);

            DbParameter isActiveParameter = this.factory.CreateParameter();
            isActiveParameter.ParameterName = "@IsActive";
            isActiveParameter.Value = entity.IsActive ? 0 : 1;
            command.Parameters.Add(isActiveParameter);

            return command;
        }

        protected override Account GetEntity(DbDataReader reader)
        {
            return new Account()
            {
                ID = reader.GetInt32(0),
                PersonID = reader.GetInt32(1),
                Balance = reader.GetInt32(2),
                Bonuses = reader.GetInt32(3),
                Type = (AccountType)reader.GetInt32(4),
                IsActive = reader.GetInt32(5) == 1
            };
        }
    }
}
