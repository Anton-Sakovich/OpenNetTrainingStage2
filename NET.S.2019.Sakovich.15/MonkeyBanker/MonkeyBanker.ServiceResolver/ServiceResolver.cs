// This #define determines to which data access technique
// MonkeyBanker.Data interfaces will be bound to.
//
// Currently there are two options:
//
//     USE_ADONET    for MonkeyBanker.Data.AdoNet
//     USE_EF6       for MonkeyBanker.Data.EF6
#define USE_ADONET

using System;
using System.Collections.Generic;
#if USE_ADONET
using System.Configuration;
using System.Data.Common;
using System.Data.SQLite;
#endif
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonkeyBanker.Data;
#if USE_ADONET
using MonkeyBanker.Data.AdoNet;
using MonkeyBanker.ServiceResolver.SQLite;
#else
using MonkeyBanker.Data.EF6;
#endif
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
            kernel.Bind<Repository>().ToSelf()
                .InSingletonScope();

            kernel.Bind<ICrudable<Person>>()
                .ToMethod(context => context.Kernel.Get<Repository>().People.Crud);

            kernel.Bind<ICrudable<Account>>()
                .ToMethod(context => context.Kernel.Get<Repository>().Accounts.Crud);

#if USE_ADONET
            kernel.Bind<IDbEntryPoint>().To<SQLiteEntryPoint>()
                .InSingletonScope();

            kernel.Bind<IRepositoryInitializer>().To<AdoNetRepositoryInitializer>()
                .WithConstructorArgument(typeof(IRepositoryInitializer), (object)null)
                .WithConstructorArgument(typeof(string), (context) => ConfigurationManager.ConnectionStrings["TestDatabase"].ConnectionString);

            kernel.Bind<IIdFactory<Account>>().To<IncrementAccountIdFactory>()
                .InSingletonScope()
                .WithConstructorArgument(
                    typeof(IEnumerable<int>),
                    (context) => 
                        new AccountsIdEnumerable(kernel.Get<IDbEntryPoint>(), ConfigurationManager.ConnectionStrings["TestDatabase"].ConnectionString)
                );
#endif

#if USE_EF6
            kernel.Bind<MonkeyBankerContext>().ToSelf()
                .WithConstructorArgument("TestDatabaseEF6");

            kernel.Bind<ICrudable<Person>>().To<PeopleEF6Crudable>();

            kernel.Bind<ICrudable<Account>>().To<AccountsEF6Crudable>()
                .WithConstructorArgument(typeof(bool), true);

            kernel.Bind<IIdFactory<Account>>().To<IncrementAccountIdFactory>()
                .InSingletonScope()
                .WithConstructorArgument(typeof(IEnumerable<int>), (context) => kernel.Get<MonkeyBankerContext>().Accounts.Select(acc => acc.ID));
#endif

            kernel.Bind<DepositManager>().To<FairTradeDepositManager>()
                .InSingletonScope();

            kernel.Bind<WithdrawalManager>().To<FairTradeWithdrawalManager>()
                .InSingletonScope();

            return kernel;
        }
    }
}
