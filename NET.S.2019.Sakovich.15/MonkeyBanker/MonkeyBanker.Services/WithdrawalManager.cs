using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonkeyBanker.Entities;

namespace MonkeyBanker.Services
{
    public class WithdrawalManager : BalanceManager
    {
        protected override void ApplyUpdate(Account acc, decimal sum)
        {
            acc.Balance -= sum;
        }

        protected override void ValidateUpdate(Account acc, decimal sum)
        {
            base.ValidateUpdate(acc, sum);

            if (acc.Balance < sum)
            {
                throw new BalanceUpdateException();
            }
        }
    }
}
