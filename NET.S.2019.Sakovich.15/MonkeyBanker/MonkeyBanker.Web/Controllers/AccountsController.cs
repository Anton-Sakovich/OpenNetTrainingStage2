using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MonkeyBanker.Data;
using MonkeyBanker.Entities;
using MonkeyBanker.Services;
using MonkeyBanker.Web.Models;
using MonkeyBanker.Web.Models.Accounts;

namespace MonkeyBanker.Web.Controllers
{
    public class AccountsController : CrudController<Account>
    {
        protected ICrudable<Person> crudToPeople;

        protected IIdFactory<Account> idFactory;

        public AccountsController(ICrudable<Account> crudToAccounts, ICrudable<Person> crudToPeople, IIdFactory<Account> idFactory)
            : base(crudToAccounts)
        {
            this.crudToPeople = crudToPeople;

            this.idFactory = idFactory;

            bool needReadPeopleManually = true;

            if (this.crud is IRelatedCrudable<Account> relatedCrud)
            {
                needReadPeopleManually = !relatedCrud.IsEager;
            }

            if (needReadPeopleManually)
            {
                this.crud = new ManualRelatedCrudableAccounts(this.crud, this.crudToPeople, true);
            }
        }

        protected override CrudFormViewModel<Account> LoadFormViewModel()
        {
            return new AccountsFormViewModel(base.LoadFormViewModel());
        }

        protected override CrudFormViewModel<Account> LoadFormViewModel(int id)
        {
            return new AccountsFormViewModel(base.LoadFormViewModel(id));
        }

        protected override void InitializeFormViewModelOnCreate(CrudFormViewModel<Account> model)
        {
            base.InitializeFormViewModelOnCreate(model);

            this.idFactory.GenerateId(model.Entity);

            SelectListItem[] holdersSelectListItems = this.crudToPeople.Read()
                .Select(p => new SelectListItem() { Text = p.GivenName + " " + p.FamilyName, Value = p.ID.ToString() })
                .ToArray();

            SelectList holders = new SelectList(holdersSelectListItems, "Value", "Text", holdersSelectListItems[0]);

            ((AccountsFormViewModel)model).Holders = holders;
        }

        protected override void InitializeFormViewModelOnEdit(CrudFormViewModel<Account> model)
        {
            base.InitializeFormViewModelOnEdit(model);

            SelectListItem[] holdersSelectListItems = this.crudToPeople.Read()
                .Select(p => new SelectListItem() { Text = p.GivenName + " " + p.FamilyName, Value = p.ID.ToString() })
                .ToArray();

            SelectList holders = new SelectList(holdersSelectListItems, "Value", "Text", holdersSelectListItems[0]);

            ((AccountsFormViewModel)model).Holders = holders;
        }
    }
}