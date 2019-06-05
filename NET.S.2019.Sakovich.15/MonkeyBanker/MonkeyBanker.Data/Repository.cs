using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonkeyBanker.Entities;

namespace MonkeyBanker.Data
{
    public class Repository
    {
        public Repository(IRepositoryInitializer initializer)
        {
            initializer.Initialize(this);
        }

        public IEntitiesSet<Person> People { get; set; }

        public IEntitiesSet<Account> Accounts { get; set; }
    }
}
