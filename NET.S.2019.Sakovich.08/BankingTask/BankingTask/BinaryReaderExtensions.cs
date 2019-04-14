using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingTask
{
    public static class BinaryReaderExtensions
    {
        public static Person ReadPerson(this BinaryReader reader)
        {
            Person newPerson = null;

            TryRead(() => newPerson = new Person(reader.ReadString(), reader.ReadString()), nameof(Person));

            return newPerson;
        }

        public static Deposit ReadDeposit(this BinaryReader reader)
        {
            Deposit newDeposit = null;

            TryRead(() => newDeposit = new Deposit(reader.ReadDecimal()), nameof(Deposit));

            return newDeposit;
        }

        public static Account ReadAccount(this BinaryReader reader)
        {
            Account newAccount = null;

            TryRead(() => newAccount = new Account(reader.ReadInt32(), reader.ReadPerson(), reader.ReadDeposit(), reader.ReadBoolean()), nameof(Account));

            return newAccount;
        }

        public static BonusedAccount ReadBonusedAccount(this BinaryReader reader)
        {
            BonusedAccount newAccount = null;

            TryRead(
                () => newAccount = new BonusedAccount(reader.ReadAccount(), reader.ReadInt32(), (BonusedAccount.Grades)reader.ReadByte()),
                nameof(BonusedAccount));

            return newAccount;
        }

        private static void TryRead(Action readAction, string type)
        {
            try
            {
                readAction();
            }
            catch (EndOfStreamException exc)
            {
                throw new EndOfStreamException("The end of stream was reached before a full " + type + " could be read.", exc);
            }
            catch (IOException exc)
            {
                throw new IOException("An IO exception was thrown when reading a " + type + ".", exc);
            }
            catch (ObjectDisposedException exc)
            {
                throw new ObjectDisposedException("An underlying BinaryReader has been disposed of.", exc);
            }
        }
    }
}
