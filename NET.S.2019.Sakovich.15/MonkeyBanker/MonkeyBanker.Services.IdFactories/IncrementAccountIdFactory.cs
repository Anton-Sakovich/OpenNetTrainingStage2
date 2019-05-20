using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonkeyBanker.Entities;
using MonkeyBanker.Data;

namespace MonkeyBanker.Services.IdFactories
{
    public class IncrementAccountIdFactory : IIdFactory<Account>
    {
        private readonly ICrudable<Account> entities;

        public IncrementAccountIdFactory(ICrudable<Account> entities)
        {
            this.entities = entities;
        }

        public void GenerateId(Account acc)
        {
            acc.ID = this.entities.Read().Select(a => a.ID).Max() + 1;
        }
    }
}
