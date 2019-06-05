using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyBanker.Data
{
    public interface IEntitiesSet<TEntity>
    {
        ICrudable<TEntity> Crud { get; }
    }
}
