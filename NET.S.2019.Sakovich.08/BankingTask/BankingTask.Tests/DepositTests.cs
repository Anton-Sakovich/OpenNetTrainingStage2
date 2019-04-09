using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace BankingTask.Tests
{
    [TestFixture]
    public class DepositTests
    {
        [Test]
        public void Deposit_Initialization_Test()
        {
            Deposit TestedDeposit = new Deposit();

            CheckDepositBalance(TestedDeposit, 0M);

            Deposit TestedDeposit1 = new Deposit(250M);

            CheckDepositBalance(TestedDeposit1, 250M);

            Assert.That(() => new Deposit(-250M), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void Deposit_NormalMoneyEvolution_Test()
        {
            Deposit TestedDeposit = new Deposit();

            CheckDepositBalance(TestedDeposit, 0M);

            TestedDeposit.DepositMoney(1000M);

            CheckDepositBalance(TestedDeposit, 1000M);

            TestedDeposit.DepositMoney(250M);

            CheckDepositBalance(TestedDeposit, 1250M);

            TestedDeposit.WithdrawMoney(750M);

            CheckDepositBalance(TestedDeposit, 500M);
        }

        [Test]
        public void WithdrawalAboveBalanceTest()
        {
            Deposit TestedDeposit = new Deposit();
            TestedDeposit.DepositMoney(1000M);

            Assert.That(TestedDeposit.TryWithdrawMoney(1250M), Is.False);
            CheckDepositBalance(TestedDeposit, 1000M);

            Assert.That(() => TestedDeposit.WithdrawMoney(1250M), Throws.TypeOf<NotEnoughMoneyException>());
        }

        static void CheckDepositBalance(Deposit depo, decimal balance)
        {
            Assert.That(depo.Balance, Is.EqualTo(balance));
        }
    }
}
