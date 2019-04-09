using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingTask
{
    /// <summary>
    /// A class representing a simple operation on a bank account's Deposit (withdrawing money and depositing money).
    /// </summary>
    public class MoneyTransfer
    {
        /// <summary>
        /// Applies the operation on the specified account in the specified amount.
        /// </summary>
        /// <param name="acc">An account to apply the operation on.</param>
        /// <param name="sum">A cost of operation.</param>
        /// <exception cref="AccountClosedException">Thrown when the specified account is closed.</exception>
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
