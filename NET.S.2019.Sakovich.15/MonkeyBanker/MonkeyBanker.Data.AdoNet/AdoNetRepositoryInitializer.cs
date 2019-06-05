using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonkeyBanker.Entities;

namespace MonkeyBanker.Data.AdoNet
{
    public class AdoNetRepositoryInitializer : IRepositoryInitializer
    {
        private readonly IDbEntryPoint factory;

        private readonly string connectionString;

        private readonly IRepositoryInitializer postInitializer;

        public AdoNetRepositoryInitializer(IDbEntryPoint factory, string connectionString,
            IRepositoryInitializer postInitializer)
        {
            this.factory = factory;

            this.connectionString = connectionString;

            this.postInitializer = postInitializer;
        }

        public void Initialize(Repository repository)
        {
            repository.People = new AdoNetEntitiesSet<Person>(
                new PeopleAdoNetCrudable(this.factory, this.connectionString));

            repository.Accounts = new AdoNetEntitiesSet<Account>(
                new AccountsAdoNetCrudable(this.factory, this.connectionString));

            this.postInitializer?.Initialize(repository);
        }
    }
}
