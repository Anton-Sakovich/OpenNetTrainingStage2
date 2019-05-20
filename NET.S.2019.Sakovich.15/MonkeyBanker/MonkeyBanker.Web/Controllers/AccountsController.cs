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
    public class AccountsController : Controller
    {
        protected ICrudable<Account> crudToAccounts;

        protected ICrudable<Person> crudToPeople;

        protected IIdFactory<Account> idFactory;

        public AccountsController(ICrudable<Account> crudToAccounts, ICrudable<Person> crudToPeople, IIdFactory<Account> idFactory)
        {
            this.crudToAccounts = crudToAccounts;

            this.crudToPeople = crudToPeople;

            this.idFactory = idFactory;

            bool needReadPeopleManually = true;

            if (this.crudToAccounts is IRelatedCrudable<Account> relatedCrud)
            {
                needReadPeopleManually = !relatedCrud.IsEager;
            }

            if (needReadPeopleManually)
            {
                this.crudToAccounts = new ManualEagerCrudableAccounts(this.crudToAccounts, this.crudToPeople, true);
            }
        }

        public ActionResult Index()
        {
            CrudIndexViewModel<Account> model = new CrudIndexViewModel<Account>
            {
                Entitites = this.crudToAccounts.Read()
            };

            return View(model);
        }

        public ActionResult Create()
        {
            Account newEntity = new Account();

            SelectListItem[] holdersSelectListItems = this.crudToPeople.Read()
                .Select(p => new SelectListItem() { Text = p.GivenName + p.FamilyName, Value = p.ID.ToString() })
                .ToArray();

            SelectList holders = new SelectList(holdersSelectListItems, "Value", "Text", holdersSelectListItems[0]);

            AccountFormViewModel model = new AccountFormViewModel()
            {
                Account = newEntity,
                Holders = holders,
                ButtonTitle = "Create"
            };

            return this.View(model);
        }

        [HttpPost]
        public ActionResult Create(AccountFormViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            this.idFactory.GenerateId(model.Account);

            if (this.crudToAccounts.Create(model.Account) == 1)
            {
                return this.RedirectToAction("Index");
            }
            else
            {
                // Handling concurrency problems in future
                return this.RedirectToAction("Index");
            }
        }

        public ActionResult Edit(int id)
        {
            Account editingEntity = this.crudToAccounts.Read(id);

            SelectListItem[] holdersSelectListItems = this.crudToPeople.Read()
                .Select(p => new SelectListItem() { Text = p.GivenName + p.FamilyName, Value = p.ID.ToString() })
                .ToArray();

            SelectList holders = new SelectList(holdersSelectListItems, "Value", "Text", holdersSelectListItems[0]);

            AccountFormViewModel model = new AccountFormViewModel()
            {
                Account = editingEntity,
                Holders = holders,
                ButtonTitle = "Apply"
            };

            return this.View(model);
        }

        [HttpPost]
        public ActionResult Edit(AccountFormViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            if (this.crudToAccounts.Update(model.Account) == 1)
            {
                return this.RedirectToAction("Index");
            }
            else
            {
                // Handling concurrency problems in future
                return this.RedirectToAction("Index");
            }
        }

        public ActionResult Delete(int id)
        {
            Account model = this.crudToAccounts.Read(id);

            return this.View(model);
        }

        [HttpPost]
        public ActionResult Delete(Account entity)
        {
            if (this.crudToAccounts.Delete(entity.ID) == 1)
            {
                return this.RedirectToAction("Index");
            }
            else
            {
                // Handling concurrency problems in future
                return this.RedirectToAction("Index");
            }
        }
    }
}