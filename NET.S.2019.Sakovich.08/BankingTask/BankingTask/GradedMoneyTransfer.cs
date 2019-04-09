using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingTask
{
    /// <summary>
    /// A class representing an operation on a bank account's Deposit (withdrawing money and depositing money) which
    /// also affects the account's Bonuses if applicable.
    /// </summary>
    public class GradedMoneyTransfer : MoneyTransfer
    {
        /// <summary>
        /// Applies the operation on the specified account in the specified amount.
        /// </summary>
        /// <param name="acc">An account to apply the operation on.</param>
        /// <param name="sum">A cost of operation.</param>
        /// <exception cref="AccountClosedException">Thrown when the specified account is closed.</exception>
        public virtual void Apply(BonusedAccount acc, decimal sum)
        {
            Validate(acc, sum);

            if (sum > 0)
            {
                acc.Money.DepositMoney(sum);

                switch (acc.Grade)
                {
                    case BonusedAccount.Grades.Bronze:
                        acc.AddBonuses((int)((double)sum / 100 * 2));
                        break;
                    case BonusedAccount.Grades.Silver:
                        acc.AddBonuses((int)((double)sum / 100 * 3));
                        break;
                    case BonusedAccount.Grades.Gold:
                        acc.AddBonuses((int)((double)sum / 100 * 4));
                        break;
                    case BonusedAccount.Grades.Platimum:
                        acc.AddBonuses((int)((double)sum / 100 * 5));
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }
            else if (sum < 0)
            {
                if(acc.Money.TryWithdrawMoney(-sum))
                {
                    switch (acc.Grade)
                    {
                        case BonusedAccount.Grades.Bronze:
                            acc.TakeBonuses((int)(-(double)sum / 100 * 5));
                            break;
                        case BonusedAccount.Grades.Silver:
                            acc.TakeBonuses((int)(-(double)sum / 100 * 4));
                            break;
                        case BonusedAccount.Grades.Gold:
                            acc.TakeBonuses((int)(-(double)sum / 100 * 3));
                            break;
                        case BonusedAccount.Grades.Platimum:
                            acc.TakeBonuses((int)(-(double)sum / 100 * 2));
                            break;
                        default:
                            throw new NotSupportedException();
                    }
                }
            }
        }
    }
}
