using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace BankingTask.Tests
{
    public class GradedMoneyTransferTests
    {
        [Test]
        public virtual void ApplyToBonusedAccountTest()
        {
            BonusedAccount TestAccount = BuildBonusedAccount(1000M);
            TestAccount.AddBonuses(100);
            TestAccount.Grade = BonusedAccount.Grades.Gold;

            GradedMoneyTransfer WithdrawalTransfer = new GradedMoneyTransfer();

            WithdrawalTransfer.Apply(TestAccount, -250M);

            Assert.That(TestAccount.Money.Balance, Is.EqualTo(750));
            Assert.That(TestAccount.Bonuses, Is.EqualTo(93));

            WithdrawalTransfer.Apply(TestAccount, 500M);

            Assert.That(TestAccount.Money.Balance, Is.EqualTo(1250M));
            Assert.That(TestAccount.Bonuses, Is.EqualTo(113));
        }

        [Test]
        public virtual void ApplyToAccountTest()
        {
            BonusedAccount BAccount = BuildBonusedAccount(1000M);
            BAccount.AddBonuses(100);
            BAccount.Grade = BonusedAccount.Grades.Gold;

            Account AAccount = BAccount;

            GradedMoneyTransfer WithdrawalTransfer = new GradedMoneyTransfer();

            WithdrawalTransfer.Apply(AAccount, -250M);

            Assert.That(BAccount.Money.Balance, Is.EqualTo(750));
            Assert.That(BAccount.Bonuses, Is.EqualTo(100));

            WithdrawalTransfer.Apply(AAccount, 500M);

            Assert.That(BAccount.Money.Balance, Is.EqualTo(1250M));
            Assert.That(BAccount.Bonuses, Is.EqualTo(100));
        }

        [Test]
        public void Apply_ToClosedAccount_ExceptionThrown()
        {
            BonusedAccount TestAccount = BuildBonusedAccount(1000M);
            TestAccount.IsOpened = false;

            GradedMoneyTransfer MoneyTransfer1 = new GradedMoneyTransfer();

            Assert.That(() => MoneyTransfer1.Apply(TestAccount, 250M), Throws.TypeOf<AccountClosedException>());
        }

        static BonusedAccount BuildBonusedAccount(decimal initBalance)
        {
            return new BonusedAccount(0, new Person("Kate", "Marsh"), new Deposit(initBalance), true);
        }
    }
}
