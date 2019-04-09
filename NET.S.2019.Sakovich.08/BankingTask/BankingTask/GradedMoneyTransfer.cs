using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingTask
{
    public class GradedMoneyTransfer : MoneyTransfer
    {
        public virtual void Apply(BonusedAccount acc, decimal sum)
        {
            if(sum > 0)
            {
                acc.Money.DepositMoney(sum);

                switch (acc.Grade)
                {
                    case BonusedAccount.Grades.Bronze:
                        acc.Bonuses.Add((int)sum / 100 * 2);
                        break;
                    case BonusedAccount.Grades.Silver:
                        acc.Bonuses.Add((int)sum / 100 * 3);
                        break;
                    case BonusedAccount.Grades.Gold:
                        acc.Bonuses.Add((int)sum / 100 * 4);
                        break;
                    case BonusedAccount.Grades.Platimum:
                        acc.Bonuses.Add((int)sum / 100 * 5);
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
                            acc.Bonuses.Take((int)sum / 100 * 2);
                            break;
                        case BonusedAccount.Grades.Silver:
                            acc.Bonuses.Take((int)sum / 100 * 3);
                            break;
                        case BonusedAccount.Grades.Gold:
                            acc.Bonuses.Take((int)sum / 100 * 4);
                            break;
                        case BonusedAccount.Grades.Platimum:
                            acc.Bonuses.Take((int)sum / 100 * 5);
                            break;
                        default:
                            throw new NotSupportedException();
                    }
                }
            }
        }
    }
}
