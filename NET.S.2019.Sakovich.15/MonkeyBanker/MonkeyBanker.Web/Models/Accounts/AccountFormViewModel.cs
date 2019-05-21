using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MonkeyBanker.Entities;

namespace MonkeyBanker.Web.Models.Accounts
{
    public class AccountsFormViewModel : CrudFormViewModel<Account>
    {
        public SelectList Holders { get; set; }

        public AccountsFormViewModel()
            : base()
        {
        }

        public AccountsFormViewModel(CrudFormViewModel<Account> model)
        {
            this.Entity = model.Entity;

            this.ButtonLabel = model.ButtonLabel;
        }
    }
}