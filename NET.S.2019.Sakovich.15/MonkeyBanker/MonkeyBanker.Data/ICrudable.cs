using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyBanker.Data
{
    public interface ICrudable<T>
    {
        int Create(T entity);

        T Read(int id);

        int Update(T entity);

        int Delete(int id);

        IEnumerable<T> ReadAll();
    }
}
