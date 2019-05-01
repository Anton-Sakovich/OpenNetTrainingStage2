using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonkeyBanker.Entities;

namespace MonkeyBanker.Services.FairTrade
{
    public class FairTradeWithdrawalManager : WithdrawalManager
    {
        protected override void ApplyUpdate(Account acc, decimal sum)
        {
            base.ApplyUpdate(acc, sum);

            ApplyUpdateBase(acc, sum);

            if (acc.Bonuses < 0)
            {
                acc.Bonuses = 0;
            }
        }

        private void ApplyUpdateBase(Account acc, decimal sum)
        {
            if (acc.Type == AccountType.Base)
            {
                acc.Bonuses -= 3 * (int)(sum / 100);
            }
            else
            {
                ApplyUpdateGold(acc, sum);
            }
        }

        private void ApplyUpdateGold(Account acc, decimal sum)
        {
            if (acc.Type == AccountType.Gold)
            {
                acc.Bonuses -= 2 * (int)(sum / 100);
            }
            else
            {
                ApplyUpdatePlatinum(acc, sum);
            }
        }

        private void ApplyUpdatePlatinum(Account acc, decimal sum)
        {
            if (acc.Type == AccountType.Platinum)
            {
                acc.Bonuses -= (int)(sum / 100);
            }
        }
    }
}
