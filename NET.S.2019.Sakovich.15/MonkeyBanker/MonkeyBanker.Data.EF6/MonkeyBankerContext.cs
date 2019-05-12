using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonkeyBanker.Entities;

namespace MonkeyBanker.Data.EF6
{
    public class MonkeyBankerContext : DbContext
    {
        public MonkeyBankerContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        public DbSet<Person> People { get; set; }

        public DbSet<Account> Accounts { get; set; }
    }
}
