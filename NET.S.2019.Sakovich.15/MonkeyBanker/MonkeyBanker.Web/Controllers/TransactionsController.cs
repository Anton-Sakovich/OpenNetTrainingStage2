using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MonkeyBanker.Entities;
using MonkeyBanker.Services;
using MonkeyBanker.Web.Models.Transactions;

namespace MonkeyBanker.Web.Controllers
{
    public class TransactionsController : Controller
    {
        protected DepositManager DepositManager;

        protected WithdrawalManager WithdrawalManager;

        public TransactionsController(DepositManager depositManager, WithdrawalManager withdrawalManager)
        {
            this.DepositManager = depositManager;

            this.WithdrawalManager = withdrawalManager;
        }

        [HttpPost]
        public ActionResult Deposit(decimal? sum)
        {
            if (sum == null)
            {
                return this.PartialView(new DepositViewModel() { Message = "Error" });
            }

            return this.PartialView(new DepositViewModel() { Message = $"The sum is {sum}" });
        }
    }
}