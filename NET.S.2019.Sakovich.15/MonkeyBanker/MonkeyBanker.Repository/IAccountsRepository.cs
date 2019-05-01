using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonkeyBanker.Entities;

namespace MonkeyBanker.Repository
{
    public interface IAccountsRepository
    {
        IEnumerable<Account> GetAllAccounts();
    }
}
