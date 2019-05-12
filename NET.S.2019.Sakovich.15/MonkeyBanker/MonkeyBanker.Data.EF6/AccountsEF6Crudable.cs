using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonkeyBanker.Entities;

namespace MonkeyBanker.Data.EF6
{
    public class AccountsEF6Crudable : ICrudable<Account>, IDisposable
    {
        private readonly MonkeyBankerContext context;

        public AccountsEF6Crudable(MonkeyBankerContext context)
        {
            this.context = context;
        }

        public int Create(Account entity)
        {
            this.context.Accounts.Add(entity);
            return this.context.SaveChanges();
        }

        public int Delete(int id)
        {
            return this.context.Database.ExecuteSqlCommand("DELETE FROM Accounts WHERE ID=@p0;", id);
        }

        public void Dispose()
        {
            this.context.Dispose();
        }

        public Account Read(int id)
        {
            return this.context.Accounts.FirstOrDefault(acc => acc.ID == id);
        }

        public IEnumerable<Account> Read()
        {
            return this.context.Accounts.ToList();
        }

        public int Update(Account entity)
        {
            this.context.Accounts.Attach(entity);
            this.context.Entry<Account>(entity).State = System.Data.Entity.EntityState.Modified;
            return this.context.SaveChanges();
        }
    }
}
