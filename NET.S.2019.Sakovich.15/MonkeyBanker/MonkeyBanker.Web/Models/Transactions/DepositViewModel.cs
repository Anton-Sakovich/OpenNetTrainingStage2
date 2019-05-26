using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MonkeyBanker.Entities;

namespace MonkeyBanker.Web.Models.Transactions
{
    public class DepositViewModel
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public Account Account { get; set; }

        public string IsSuccessString()
        {
            return this.IsSuccess ? "true" : "false";
        }
    }
}