using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingTask
{
    public class MoneyTransfer
    {
        public virtual void Apply(Account acc, decimal sum)
        {
            Validate(acc, sum);

            if (sum > 0)
            {
                acc.Money.DepositMoney(sum);
            }
            else if (sum < 0)
            {
                acc.Money.WithdrawMoney(-sum);
            }
        }

        protected virtual void Validate(Account acc, decimal sum)
        {
            if(!acc.IsOpened)
            {
                throw new AccountClosedException();
            }
        }
    }
}
