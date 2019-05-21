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
        private readonly IEnumerable<int> ids;

        public IncrementAccountIdFactory(IEnumerable<int> ids)
        {
            this.ids = ids;
        }

        public void GenerateId(Account acc)
        {
            acc.ID = this.ids.Max() + 1;
        }
    }
}
