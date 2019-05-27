using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonkeyBanker.Data;
//using MonkeyBanker.Data.AdoNet;
using MonkeyBanker.Data.EF6;
using MonkeyBanker.Entities;
using MonkeyBanker.Services;
using MonkeyBanker.Services.FairTrade;
using MonkeyBanker.Services.IdFactories;
using Ninject;

namespace MonkeyBanker.ServiceResolver
{
    public static class ServiceResolver
    {
        public static IKernel CnfigureKernel(this IKernel kernel)
        {
            //kernel.Bind<DbProviderFactory>().ToMethod((context) => SQLiteFactory.Instance);

            kernel.Bind<MonkeyBankerContext>().ToSelf()
                .WithConstructorArgument("TestDatabaseEF6");

            kernel.Bind<ICrudable<Person>>().To<PeopleEF6Crudable>();

            kernel.Bind<ICrudable<Account>>().To<AccountsEF6Crudable>()
                .WithConstructorArgument(typeof(bool), true);

            //kernel.Bind<ICrudable<Person>>().To<PeopleAdoNetCrudable>()
            //    .InSingletonScope()
            //    .WithConstructorArgument(typeof(string), (context) => ConfigurationManager.ConnectionStrings["TestDatabase"].ConnectionString);

            //kernel.Bind<ICrudable<Account>>().To<AccountsAdoNetCrudable>()
            //    .InSingletonScope()
            //    .WithConstructorArgument(typeof(string), (context) => ConfigurationManager.ConnectionStrings["TestDatabase"].ConnectionString);

            kernel.Bind<DepositManager>().To<FairTradeDepositManager>()
                .InSingletonScope();

            kernel.Bind<WithdrawalManager>().To<FairTradeWithdrawalManager>()
                .InSingletonScope();

            kernel.Bind<IIdFactory<Account>>().To<IncrementAccountIdFactory>()
                .InSingletonScope()
                .WithConstructorArgument(typeof(IEnumerable<int>), (context) => kernel.Get<MonkeyBankerContext>().Accounts.Select(acc => acc.ID).DefaultIfEmpty());

            return kernel;
        }
    }
}
