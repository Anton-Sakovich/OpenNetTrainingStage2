using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyBanker.Data
{
    public interface IRepositoryInitializer
    {
        void Initialize(Repository repository);
    }
}
