using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyBanker.Data.AdoNet
{
    public class AdoNetEntitiesSet<TEntity> : IEntitiesSet<TEntity>
    {
        public AdoNetEntitiesSet(ICrudable<TEntity> crud)
        {
            this.Crud = crud;
        }

        public ICrudable<TEntity> Crud { get; }
    }
}
