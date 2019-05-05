using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonkeyBanker.Data;
using MonkeyBanker.Entities;
using MonkeyBanker.Data.AdoNet;

namespace MonkeyBanker
{
    class Program
    {
        static void Main(string[] args)
        {
            ICrudable<Person> peopleRepo = new PeopleAdoNetCrudable(
                SQLiteFactory.Instance,
                @"Data Source = W:\ph.nerd.anton\EpamTraining\OpenNetTrainingStage2\NET.S.2019.Sakovich.15\MonkeyBanker\MonkeyBanker.db");

            foreach (Person person in peopleRepo.Read())
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
