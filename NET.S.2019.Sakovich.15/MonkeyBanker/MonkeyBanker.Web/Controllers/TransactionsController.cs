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
        public ActionResult Deposit(int? id, decimal? sum, string act)
        {
            Account accountToUpdate = null;

            DepositViewModel model = new DepositViewModel()
            {
                IsSuccess = false
            };

            if (id == null || sum == null)
            {
                model.Message = "Invalid input";
                return this.PartialView(model);
            }

            try
            {
                accountToUpdate = this.Accounts.Read(id.Value);
            }
            catch (Exception)
            {
                model.Message = "Account loading error.";
                return this.PartialView(model);
            }

            if (accountToUpdate == null)
            {
                model.Message = "Account not found.";
                return this.PartialView(model);
            }

            BalanceManager balanceManager = null;

            switch (act)
            {
                case "D":
                    balanceManager = this.DepositManager;
                    break;
                case "W":
                    balanceManager = this.WithdrawalManager;
                    break;
            }

            if (balanceManager == null)
            {
                model.Message = "Deposit action error.";
                return this.PartialView(model);
            }

            try
            {
                balanceManager.UpdateBalance(accountToUpdate, sum.Value);
            }
            catch (Exception)
            {
                model.Message = "UpdateBalance error.";
                return this.PartialView(model);
            }

            try
            {
                this.Accounts.Update(accountToUpdate);
            }
            catch (Exception)
            {
                model.Message = "UpdateAccount error.";
                return this.PartialView(model);
            }

            model.Account = accountToUpdate;
            model.Message = "Success";
            model.IsSuccess = true;

            return this.PartialView(model);
        }
    }
}