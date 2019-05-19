using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyBanker.Data
{
    public interface IRelatedCrudable<T> : ICrudable<T>
    {
        bool IsEager { get; }
    }
}
