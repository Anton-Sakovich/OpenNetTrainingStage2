using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingTask
{
    public static class BinaryWriterExtensions
    {
        public static void Write(this BinaryWriter writer, Person person)
        {
            writer.Write(person.GivenName);
            writer.Write(person.FamilyName);
        }

        public static void Write(this BinaryWriter writer, Deposit deposit)
        {
            writer.Write(deposit.Balance);
        }

        public static void Write(this BinaryWriter writer, Account acc)
        {
            writer.Write(acc.ID);
            writer.Write(acc.Holder);
            writer.Write(acc.Money);
            writer.Write(acc.IsOpened);
        }

        public static void Write(this BinaryWriter writer, BonusedAccount acc)
        {
            writer.Write((Account)acc);
            writer.Write(acc.Bonuses);
            writer.Write((byte)acc.Grade);
        }
    }
}
