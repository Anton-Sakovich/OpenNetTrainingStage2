using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace BankingTask.Tests
{
    [TestFixture]
    public class MoneyTransferTests
    {
        [Test]
        public void ApplyTest()
        {
            Account TestAccount = BuildAccount(1000M);

            MoneyTransfer WithdrawalTransfer = new MoneyTransfer();

            WithdrawalTransfer.Apply(TestAccount, -250M);

            Assert.That(TestAccount.Money.Balance, Is.EqualTo(750));

            WithdrawalTransfer.Apply(TestAccount, 500M);

            Assert.That(TestAccount.Money.Balance, Is.EqualTo(1250M));
        }

        [Test]
        public void Apply_ToClosedAccount_ExceptionThrown()
        {
            Account TestAccount = BuildAccount(1000M);
            TestAccount.IsOpened = false;

            MoneyTransfer MoneyTransfer1 = new MoneyTransfer();

            Assert.That(() => MoneyTransfer1.Apply(TestAccount, 250M), Throws.TypeOf<AccountClosedException>());
        }

        static Account BuildAccount(decimal initialMoney)
        {
            return new Account(0, new Person("Max", "Caulfield"), new Deposit(initialMoney), true);
        }
    }
}
