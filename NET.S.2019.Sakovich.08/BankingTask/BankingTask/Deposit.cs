using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingTask
{
    /// <summary>
    /// A money container representing a sum of money and operations on it.
    /// </summary>
    public class Deposit
    {
        private decimal _Balance = 0M;

        /// <summary>
        /// The sum of money available.
        /// </summary>
        public decimal Balance { get => _Balance; }

        /// <summary>
        /// Creates a new money container with the specified inital balance.
        /// </summary>
        /// <param name="initBalance">Inital balance.</param>
        /// <exception cref="ArgumentException">Thrown when the initial sum is negative.</exception>
        public Deposit(decimal initBalance)
        {
            if (initBalance < 0)
            {
                throw new ArgumentException();
            }

            _Balance = initBalance;
        }

        /// <summary>
        /// Creates a new money container with zero balance.
        /// </summary>
        public Deposit()
        {

        }

        /// <summary>
        /// Adds a specified sum of money to the container.
        /// </summary>
        /// <param name="sum">A sum of money to add.</param>
        public void DepositMoney(decimal sum)
        {
            _Balance += sum;
        }

        /// <summary>
        /// Takes a specified sum of money from the container.
        /// </summary>
        /// <param name="sum">A sum of money to take.</param>
        /// <exception cref="NotEnoughMoneyException">Thrown when the balance is less than the sum of money required.</exception>
        public void WithdrawMoney(decimal sum)
        {
            if(!TryWithdrawMoney(sum))
            {
                throw new NotEnoughMoneyException();
            }
        }

        /// <summary>
        /// Tries to take a specified sum of money from the container.
        /// </summary>
        /// <param name="sum">A sum of money to take.</param>
        /// <returns>True on success and false on failure.</returns>
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
