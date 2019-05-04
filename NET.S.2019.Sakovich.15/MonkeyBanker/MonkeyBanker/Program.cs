using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonkeyBanker.Data;
using MonkeyBanker.Entities;
using MonkeyBanker.Data.AdoNet.SQLite;

namespace MonkeyBanker
{
    class Program
    {
        static void Main(string[] args)
        {
            ICrudable<Person> peopleRepo = new PeopleSQLiteCrudEngine("Data Source = E:/Temp/MonkeyBanker.db");

            foreach (Person person in peopleRepo.ReadAll())
            {
                Console.WriteLine("{0} {1}", person.GivenName, person.FamilyName);
            }

            Person max = peopleRepo.Read(1);
            max.GivenName = "Maxine";
            peopleRepo.Update(max);

            Console.ReadKey();
        }
    }
}
