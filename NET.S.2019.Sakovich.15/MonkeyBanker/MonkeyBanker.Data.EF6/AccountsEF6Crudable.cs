using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonkeyBanker.Entities;

namespace MonkeyBanker.Data.EF6
{
    public class AccountsEF6Crudable : IRelatedCrudable<Account>, IDisposable
    {
        private readonly MonkeyBankerContext context;

        public AccountsEF6Crudable(MonkeyBankerContext context, bool isEager = false)
        {
            this.context = context;
            this.IsEager = isEager;
        }

        public bool IsEager { get; }

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
            if (this.IsEager)
            {
                return this.context.Accounts.Include(acc => acc.Holder).FirstOrDefault(acc => acc.ID == id);
            }
            else
            {
                return this.context.Accounts.FirstOrDefault(acc => acc.ID == id);
            }
        }

        public IEnumerable<Account> Read()
        {
            if (this.IsEager)
            {
                return this.context.Accounts.Include(acc => acc.Holder).ToList();
            }
            else
            {
                return this.context.Accounts.ToList();
            }
        }

        public int Update(Account entity)
        {
            this.context.Accounts.Attach(entity);
            this.context.Entry<Account>(entity).State = System.Data.Entity.EntityState.Modified;
            return this.context.SaveChanges();
        }
    }
}
