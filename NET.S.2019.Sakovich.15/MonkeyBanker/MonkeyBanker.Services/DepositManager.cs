using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonkeyBanker.Entities;

namespace MonkeyBanker.Services
{
    public class DepositManager : BalanceManager
    {
        protected override void ApplyUpdate(Account acc, decimal sum)
        {
            acc.Balance += sum;
        }
    }
}
