using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonkeyBanker.Entities;

namespace MonkeyBanker.Services
{
    public abstract class MoneyTransferer
    {
        public void Deposit(Account acc, decimal sum)
        {
            ValidateAccount(acc);
            ValidateSum(sum);

            if (DepositActions == null)
            {
                DepositFallback(acc, sum);
                return;
            }

            bool accTypeMatched = false;

            foreach (Func<Account, decimal, bool> depositAction in this.DepositActions)
            {
                if (accTypeMatched = depositAction(acc, sum))
                {
                    break;
                }
            }

            if (!accTypeMatched)
            {
                DepositFallback(acc, sum);
            }
        }

        public void Withdraw(Account acc, decimal sum)
        {
            ValidateAccount(acc);
            ValidateSum(sum);

            if (WithdrawActions == null)
            {
                WithdrawFallback(acc, sum);
                return;
            }

            bool accTypeMatched = false;

            foreach (Func<Account, decimal, bool> withdrawAction in this.WithdrawActions)
            {
                if (accTypeMatched = withdrawAction(acc, sum))
                {
                    break;
                }
            }

            if (!accTypeMatched)
            {
                WithdrawFallback(acc, sum);
            }
        }

        protected abstract List<Func<Account, decimal, bool>> DepositActions { get; }

        protected abstract List<Func<Account, decimal, bool>> WithdrawActions { get; }

        protected virtual void ValidateAccount(Account acc)
        {
        }

        protected virtual void ValidateSum(decimal sum)
        {
            if (sum < 0)
            {
                throw new ArgumentException("Deposit sum must be non-negative.", nameof(sum));
            }
        }

        protected virtual void DepositFallback(Account acc, decimal sum)
        {
            acc.Balance += sum;
        }

        protected virtual void WithdrawFallback(Account acc, decimal sum)
        {
            acc.Balance -= sum;
        }
    }
}
