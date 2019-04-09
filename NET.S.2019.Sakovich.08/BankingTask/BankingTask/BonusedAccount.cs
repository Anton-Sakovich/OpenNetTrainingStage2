using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingTask
{
    public class BonusedAccount : Account
    {
        public int Bonuses { get; private set; } = 0;
        public Grades Grade { get; set; } = Grades.Base;

        public BonusedAccount(int id, Person holder, Deposit money = null, bool opened = true)
            : base(id, holder, money, opened)
        {

        }

        public enum Grades
        {
            Base, Bronze, Silver, Gold, Platimum
        }

        public void AddBonuses(int b)
        {
            if(b < 0)
            {
                throw new ArgumentException();
            }

            Bonuses += b;
        }

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
