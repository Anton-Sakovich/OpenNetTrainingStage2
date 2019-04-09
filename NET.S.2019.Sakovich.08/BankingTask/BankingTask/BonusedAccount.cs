using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingTask
{
    public class BonusedAccount : Account
    {
        public NonNegativeInteger Bonuses { get; set; } = (NonNegativeInteger)0;
        public Grades Grade { get; set; } = Grades.Base;

        public BonusedAccount(int id, Person holder, Deposit money = null, bool opened = true)
            : base(id, holder, money, opened)
        {

        }

        public enum Grades
        {
            Base, Bronze, Silver, Gold, Platimum
        }
    }
}
