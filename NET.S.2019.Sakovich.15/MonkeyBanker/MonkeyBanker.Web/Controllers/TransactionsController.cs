using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MonkeyBanker.Data;
using MonkeyBanker.Entities;
using MonkeyBanker.Services;
using MonkeyBanker.Web.Models.Transactions;

namespace MonkeyBanker.Web.Controllers
{
    public class TransactionsController : Controller
    {
        protected DepositManager DepositManager;

        protected WithdrawalManager WithdrawalManager;

        protected ICrudable<Account> Accounts;

        public TransactionsController(DepositManager depositManager, WithdrawalManager withdrawalManager, ICrudable<Account> accounts)
        {
            this.DepositManager = depositManager;

            this.WithdrawalManager = withdrawalManager;

            this.Accounts = accounts;
        }

        [HttpPost]
        public ActionResult Deposit(int id, decimal sum)
        {
            Account accountToUpdate = this.Accounts.Read(id);

            this.DepositManager.UpdateBalance(accountToUpdate, sum);

            this.Accounts.Update(accountToUpdate);

            DepositViewModel model = new DepositViewModel()
            {
                IsSuccess = true,
                Message = "Success",
                Account = accountToUpdate
            };

            return this.PartialView(model);
        }
    }
}