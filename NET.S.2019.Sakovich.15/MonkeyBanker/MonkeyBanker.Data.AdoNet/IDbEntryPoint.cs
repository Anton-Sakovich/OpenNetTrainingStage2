using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyBanker.Data.AdoNet
{
    public interface IDbEntryPoint
    {
        IDbConnectionWrapper CreateConnection();

        IDbCommand CreateCommand();

        IDbDataParameter CreateParameter();
    }
}
