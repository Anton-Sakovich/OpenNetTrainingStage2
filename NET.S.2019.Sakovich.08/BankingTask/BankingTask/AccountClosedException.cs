using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingTask
{
    /// <summary>
    /// An exception thrown by money transfer operations when an account passed to them is closed.
    /// </summary>
    public class AccountClosedException : ApplicationException
    {
    }
}
