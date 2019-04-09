using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingTask
{
    public class Account
    {
        public int ID;
        public Person Holder { get; set; }
        public Deposit Money { get; set; }
        public bool IsOpened { get; set; }

        public Account(int id, Person holder, Deposit money = null, bool opened = true)
        {
            ID = id;
            Holder = holder;
            Money = money ?? new Deposit();
            IsOpened = opened;
        }
    }
}
