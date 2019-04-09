using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingTask
{
    /// <summary>
    /// A bank account to which a Grade and Bonuses are assigned.
    /// </summary>
    public class BonusedAccount : Account
    {
        /// <summary>
        /// The value of Bonuses the BonusedAccount has.
        /// </summary>
        public int Bonuses { get; private set; } = 0;

        /// <summary>
        /// The Grade of the BonusedAccount.
        /// </summary>
        public Grades Grade { get; set; } = Grades.Base;

        /// <summary>
        /// Creates a new BonusedAccount with zero bonuses and Base Grade.
        /// </summary>
        /// <param name="id">Account identification number.</param>
        /// <param name="holder">Account's holder.</param>
        /// <param name="money">A money deposit attached to the account.</param>
        /// <param name="opened">A flag indicating whether the account is opened for operations.</param>
        public BonusedAccount(int id, Person holder, Deposit money = null, bool opened = true)
            : base(id, holder, money, opened)
        {

        }

        /// <summary>
        /// The possible values of a BonusedAccount's Grade.
        /// </summary>
        public enum Grades
        {
            Base, Bronze, Silver, Gold, Platimum
        }

        /// <summary>
        /// Adds the specified amount of bonuses to the BonusedAccount.
        /// </summary>
        /// <param name="b">The amount of bonuses to add.</param>
        /// <exception cref="ArgumentException">Thrown when the amount of bonuses is negative.</exception>
        public void AddBonuses(int b)
        {
            if(b < 0)
            {
                throw new ArgumentException();
            }

            Bonuses += b;
        }

        /// <summary>
        /// Takes the specified amount of bonuses from the BonusedAccount.
        /// </summary>
        /// <param name="b">he amount of bonuses to take.</param>
        /// <exception cref="ArgumentException">Thrown when the amount of bonuses is negative.</exception>
        public void TakeBonuses(int b)
        {
            if (b < 0)
            {
                throw new ArgumentException();
            }

            Bonuses = Math.Max(0, Bonuses - b);
        }
    }
}
