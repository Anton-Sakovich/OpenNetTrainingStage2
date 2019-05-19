using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonkeyBanker.Entities;

namespace MonkeyBanker.Services
{
    public interface IIdFactory<T>
    {
        void GenerateId(T acc);
    }
}
