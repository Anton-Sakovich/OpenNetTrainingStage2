using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyBanker.Entities
{
    public class Person : IIdentifiable<int>
    {
        public int ID { get; set; }

        [DisplayName("Given name")]
        public string GivenName { get; set; }

        [DisplayName("Family name")]
        public string FamilyName { get; set; }
    }
}
