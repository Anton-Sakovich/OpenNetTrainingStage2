using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingTask
{
    public class Person
    {
        public string GivenName { get; set; }
        public string FamilyName { get; set; }

        public Person(string gname, string fname)
        {
            GivenName = gname;
            FamilyName = fname;
        }
    }
}
