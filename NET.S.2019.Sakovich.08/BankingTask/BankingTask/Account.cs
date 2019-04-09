using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingTask
{
    /// <summary>
    /// A base class for a bank account containing minimal information required.
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Account identification number.
        /// </summary>
        public int ID;

        /// <summary>
        /// Account's holder.
        /// </summary>
        public Person Holder { get; set; }

        /// <summary>
        /// A money deposit attached to the account.
        /// </summary>
        public Deposit Money { get; set; }

        /// <summary>
        /// A flag indicating whether the account is opened for operations.
        /// </summary>
        public bool IsOpened { get; set; }

        /// <summary>
        /// Creates a new account.
        /// </summary>
        /// <param name="id">Account identification number.</param>
        /// <param name="holder">Account's holder.</param>
        /// <param name="money">A money deposit attached to the account.</param>
        /// <param name="opened">A flag indicating whether the account is opened for operations.</param>
        public Account(int id, Person holder, Deposit money = null, bool opened = true)
        {
            ID = id;
            Holder = holder;
            Money = money ?? new Deposit();
            IsOpened = opened;
        }
    }
}
