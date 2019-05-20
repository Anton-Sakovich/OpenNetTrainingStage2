using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MonkeyBanker.Entities;

namespace MonkeyBanker.Web.Models.Accounts
{
    public class AccountFormViewModel
    {
        public Account Account { get; set; }

        public SelectList Holders { get; set; }

        public string ButtonTitle { get; set; }
    }
}