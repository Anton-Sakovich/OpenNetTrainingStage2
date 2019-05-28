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

        private readonly int init;

        public IncrementAccountIdFactory(IEnumerable<int> ids, int init = 0)
        {
            this.ids = ids ?? throw new ArgumentNullException(nameof(ids));

            this.init = init;
        }

        public void GenerateId(Account acc)
        {
            acc.ID = this.ids.Any() ? this.ids.Max() + 1 : this.init;
        }
    }
}
