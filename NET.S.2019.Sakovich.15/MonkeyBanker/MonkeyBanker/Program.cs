using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using MonkeyBanker.Data;
using MonkeyBanker.Entities;
using MonkeyBanker.Services;
using MonkeyBanker.ServiceResolver;
using Ninject;

namespace MonkeyBanker
{
    class Program
    {
        static void Main(string[] args)
        {
            IKernel kernel = new StandardKernel().CnfigureKernel();

            ICrudable<Person> peopleRepo = kernel.Get<ICrudable<Person>>();

            foreach (Person person in peopleRepo.Read())
            {
                Console.WriteLine("{0} {1}", person.GivenName, person.FamilyName);
            }

            Person max = peopleRepo.Read(1);
            max.GivenName = "Maxine";
            peopleRepo.Update(max);

            ICrudable<Account> accsRepo = kernel.Get<ICrudable<Account>>();

            foreach (Account acc in accsRepo.Read())
            {
                Console.WriteLine("{0} {1}", acc.Balance, acc.Bonuses);
            }

            DepositManager depoManager = kernel.Get<DepositManager>();

            Account maxsAccount = accsRepo.Read(1);

            Console.WriteLine(maxsAccount.Balance);
            Console.WriteLine(maxsAccount.Bonuses);

            depoManager.UpdateBalance(maxsAccount, 100);

            Console.WriteLine(maxsAccount.Balance);
            Console.WriteLine(maxsAccount.Bonuses);

            WithdrawalManager withdrawalManager = kernel.Get<WithdrawalManager>();

            withdrawalManager.UpdateBalance(maxsAccount, 20);

            Console.WriteLine(maxsAccount.Balance);
            Console.WriteLine(maxsAccount.Bonuses);

            Console.ReadKey();
        }
    }
}
