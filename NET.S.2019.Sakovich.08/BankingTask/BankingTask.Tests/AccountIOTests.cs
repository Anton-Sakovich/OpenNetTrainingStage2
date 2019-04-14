using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace BankingTask.Tests
{
    [TestFixture]
    public class AccountIOTests
    {
        public static readonly BonusedAccount TestAccount =
            new BonusedAccount(783276575, new Person("Max", "Caulfield"), new Deposit(1000M), true, 1250, BonusedAccount.Grades.Platimum);

        public static readonly byte[] TestAccountBytes = new byte[]
        {
            31, 218, 175, 46,
            3, 77, 97, 120,
            9, 67, 97, 117, 108, 102, 105, 101, 108, 100,
            232, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            1,
            226, 4, 0, 0,
            (byte)BonusedAccount.Grades.Platimum
        };

        [Test]
        public static void BonusedAccouned_Write_Test()
        {
            MemoryStream mstream = new MemoryStream();

            new BinaryWriter(mstream).Write(TestAccount);

            Assert.That(mstream.ToArray(), Is.EqualTo(TestAccountBytes));
        }

        [Test]
        public static void BonusedAccouned_Read_Test()
        {
            MemoryStream mstream = new MemoryStream(TestAccountBytes);

            BonusedAccount ReadAccount = new BinaryReader(mstream).ReadBonusedAccount();

            Assert.That(ReadAccount.ID, Is.EqualTo(ReadAccount.ID));
            Assert.That(ReadAccount.Holder.GivenName, Is.EqualTo(ReadAccount.Holder.GivenName));
            Assert.That(ReadAccount.Holder.FamilyName, Is.EqualTo(ReadAccount.Holder.FamilyName));
            Assert.That(ReadAccount.Money.Balance, Is.EqualTo(ReadAccount.Money.Balance));
            Assert.That(ReadAccount.Bonuses, Is.EqualTo(ReadAccount.Bonuses));
            Assert.That(ReadAccount.Grade, Is.EqualTo(ReadAccount.Grade));
        }
    }
}
