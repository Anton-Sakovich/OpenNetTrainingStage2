using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonkeyBanker.Entities;

namespace MonkeyBanker.Services.FairTrade
{
    public class FairMoneyTransfer : MoneyTransferer
    {
        private readonly List<Func<Account, decimal, bool>> concreteDepositActions;

        private readonly List<Func<Account, decimal, bool>> concreteWithdrawActions;

        public FairMoneyTransfer()
        {
            concreteDepositActions = new List<Func<Account, decimal, bool>>
            {
                this.DepositBaseAccount,
                this.DepositGoldAccount,
                this.DepositPlatinumAccount
            };

            concreteWithdrawActions = new List<Func<Account, decimal, bool>>
            {
                this.WithdrawBaseAccount,
                this.DepositGoldAccount,
                this.DepositPlatinumAccount
            };
        }

        protected override List<Func<Account, decimal, bool>> DepositActions
        {
            get
            {
                return concreteDepositActions;
            }
        }

        protected override List<Func<Account, decimal, bool>> WithdrawActions
        {
            get
            {
                return concreteWithdrawActions;
            }
        }

        private bool DepositBaseAccount(Account acc, decimal sum)
        {
            if (acc.Type == AccountType.Base)
            {
                acc.Balance += 10;
                return true;
            }

            return false;
        }

        private bool WithdrawBaseAccount(Account acc, decimal sum)
        {
            if (acc.Type == AccountType.Base)
            {
                acc.Balance -= 10;
                return true;
            }

            return false;
        }

        private bool DepositGoldAccount(Account acc, decimal sum)
        {
            if (acc.Type == AccountType.Gold)
            {
                acc.Balance += 10;
                return true;
            }

            return false;
        }

        private bool WithdrawGoldAccount(Account acc, decimal sum)
        {
            if (acc.Type == AccountType.Gold)
            {
                acc.Balance += 10;
                return true;
            }

            return false;
        }

        private bool DepositPlatinumAccount(Account acc, decimal sum)
        {
            if (acc.Type == AccountType.Platinum)
            {
                acc.Balance += 10;
                return true;
            }

            return false;
        }

        private bool WithdrawPlatinumAccount(Account acc, decimal sum)
        {
            if (acc.Type == AccountType.Platinum)
            {
                acc.Balance += 10;
                return true;
            }

            return false;
        }
    }
}
