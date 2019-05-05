using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonkeyBanker.Data;
using MonkeyBanker.Data.AdoNet;
using MonkeyBanker.Entities;
using MonkeyBanker.Services;
using MonkeyBanker.Services.FairTrade;
using Ninject;

namespace MonkeyBanker.ServiceResolver
{
    public static class ServiceResolver
    {
        public static IKernel CnfigureKernel(this IKernel kernel)
        {
            kernel.Bind<DbProviderFactory>().ToMethod((context) => SQLiteFactory.Instance);

            kernel.Bind<ICrudable<Person>>().To<PeopleAdoNetCrudable>()
                .InSingletonScope()
                .WithConstructorArgument(typeof(string), (context) => ConfigurationManager.ConnectionStrings["TestDatabase"].ConnectionString);

            kernel.Bind<ICrudable<Account>>().To<AccountsAdoNetCrudable>()
                .InSingletonScope()
                .WithConstructorArgument(typeof(string), (context) => ConfigurationManager.ConnectionStrings["TestDatabase"].ConnectionString);

            kernel.Bind<DepositManager>().To<FairTradeDepositManager>()
                .InSingletonScope();

            kernel.Bind<WithdrawalManager>().To<FairTradeWithdrawalManager>()
                .InSingletonScope();

            return kernel;
        }
    }
}
