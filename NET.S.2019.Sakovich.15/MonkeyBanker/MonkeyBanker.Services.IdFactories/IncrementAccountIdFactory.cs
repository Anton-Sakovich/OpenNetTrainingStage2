using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonkeyBanker.Entities;

namespace MonkeyBanker.Services.IdFactories
{
    public class IncrementAccountIdFactory : IIdFactory<Account>
    {
        private readonly IQueryable<Account> entities;

        public IncrementAccountIdFactory(IQueryable<Account> entities)
        {
            this.entities = entities;
        }

        public void GenerateId(Account acc)
        {
            acc.ID = this.entities.Select(a => a.ID).Max() + 1;
        }
    }
}
