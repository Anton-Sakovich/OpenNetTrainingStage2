using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonkeyBanker.Entities;

namespace MonkeyBanker.Data
{
    public class ManualEagerCrudableAccounts : CrudableDecorator<Account>, IRelatedCrudable<Account>
    {
        private readonly ICrudable<Person> crudablePeople;

        public ManualEagerCrudableAccounts(ICrudable<Account> crudableAccounts, ICrudable<Person> crudablePeople, bool isEager)
            : base(crudableAccounts)
        {
            this.crudablePeople = crudablePeople;
            this.IsEager = isEager;
        }

        public bool IsEager { get; }

        public override Account Read(int id)
        {
            Account account = base.Read(id);

            if (this.IsEager)
            {
                account.Holder = this.crudablePeople.Read(account.PersonID);
            }

            return account;
        }

        public override IEnumerable<Account> Read()
        {
            if (this.IsEager)
            {
                foreach (Account account in base.Read())
                {
                    account.Holder = this.crudablePeople.Read(account.PersonID);
                    yield return account;
                }
            }
            else
            {
                foreach (Account account in base.Read())
                {
                    yield return account;
                }
            }
        }
    }
}
