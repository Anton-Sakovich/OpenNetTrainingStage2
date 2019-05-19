using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MonkeyBanker.Data;
using MonkeyBanker.Entities;

namespace MonkeyBanker.Web.Controllers
{
    public class AccountsController : CrudController<Account>
    {
        protected ICrudable<Person> crudToPeople;

        public AccountsController(ICrudable<Account> crud, ICrudable<Person> crudToPeople)
            : base(crud)
        {
            this.crudToPeople = crudToPeople;

            bool needReadPeopleManually = true;

            if (this.crud is IRelatedCrudable<Account> relatedCrud)
            {
                needReadPeopleManually = !relatedCrud.IsEager;
            }

            if (needReadPeopleManually)
            {
                this.crud = new ManualEagerCrudableAccounts(this.crud, this.crudToPeople, true);
            }
        }
    }
}