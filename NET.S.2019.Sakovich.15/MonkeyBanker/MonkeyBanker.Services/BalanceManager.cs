using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonkeyBanker.Entities;

namespace MonkeyBanker.Services
{
    public abstract class BalanceManager
    {
        public void UpdateBalance(Account acc, decimal sum)
        {
            ValidateUpdate(acc, sum);

            ApplyUpdate(acc, sum);
        }

        protected abstract void ApplyUpdate(Account acc, decimal sum);

        protected virtual void ValidateUpdate(Account acc, decimal sum)
        {
            if (sum < 0)
            {
                throw new BalanceUpdateException();
            }
        }
    }
}
