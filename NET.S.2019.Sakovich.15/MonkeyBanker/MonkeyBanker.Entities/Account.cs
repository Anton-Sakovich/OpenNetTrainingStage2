using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyBanker.Entities
{
    public class Account
    {
        public int ID { get; set; }

        public int PersonID { get; set; }

        public Person Holder { get; set; }

        public decimal Balance { get; set; }

        public int Bonuses { get; set; }

        public AccountType Type { get; set; }

        public bool IsActive { get; set; }
    }
}
