using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonkeyBanker.Entities;

namespace MonkeyBanker.Data.EF6
{
    class PeopleEF6Crudable : ICrudable<Person>, IDisposable
    {
        private readonly MonkeyBankerContext context;

        public PeopleEF6Crudable(MonkeyBankerContext context)
        {
            this.context = context;
        }

        public int Create(Person entity)
        {
            this.context.People.Add(entity);
            return this.context.SaveChanges();
        }

        public int Delete(int id)
        {
            return this.context.Database.ExecuteSqlCommand("DELETE FROM People WHERE ID=@p0;", id);
        }

        public void Dispose()
        {
            this.context.Dispose();
        }

        public Person Read(int id)
        {
            return this.context.People.FirstOrDefault(p => p.ID == id);
        }

        public IEnumerable<Person> Read()
        {
            return this.context.People.ToList();
        }

        public int Update(Person entity)
        {
            this.context.People.Attach(entity);
            this.context.Entry<Person>(entity).State = System.Data.Entity.EntityState.Modified;
            return this.context.SaveChanges();
        }
    }
}
