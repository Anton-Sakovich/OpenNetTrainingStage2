using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingTask
{
    public class Deposit
    {
        private decimal _Balance = 0M;

        public decimal Balance { get => _Balance; }

        public Deposit(decimal initBalance)
        {
            if (initBalance < 0)
            {
                throw new ArgumentException();
            }

            _Balance = initBalance;
        }

        public Deposit()
        {

        }

        public void DepositMoney(decimal sum)
        {
            _Balance += sum;
        }

        public void WithdrawMoney(decimal sum)
        {
            if(!TryWithdrawMoney(sum))
            {
                throw new NotEnoughMoneyException();
            }
        }

        public bool TryWithdrawMoney(decimal sum)
        {
            if(_Balance >= sum)
            {
                _Balance -= sum;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
