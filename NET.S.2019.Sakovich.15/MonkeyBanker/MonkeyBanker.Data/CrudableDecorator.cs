using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyBanker.Data
{
    public class CrudableDecorator<T> : ICrudable<T>
    {
        protected readonly ICrudable<T> InnerCrudable;

        public CrudableDecorator(ICrudable<T> innerCrudable)
        {
            this.InnerCrudable = innerCrudable;
        }

        public virtual int Create(T entity)
        {
            return this.InnerCrudable.Create(entity);
        }

        public virtual int Delete(int id)
        {
            return this.InnerCrudable.Delete(id);
        }

        public virtual T Read(int id)
        {
            return this.InnerCrudable.Read(id);
        }

        public virtual IEnumerable<T> Read()
        {
            return this.InnerCrudable.Read();
        }

        public virtual int Update(T entity)
        {
            return this.InnerCrudable.Update(entity);
        }
    }
}
