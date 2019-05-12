using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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

        [DefaultValue(0.0)]
        public decimal Balance { get; set; }

        [DefaultValue(0.0)]
        public int Bonuses { get; set; }

        [DefaultValue(AccountType.Base)]
        public AccountType Type { get; set; }

        [DefaultValue(true)]
        public bool IsActive { get; set; }
    }
}
