using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingTask
{
    /// <summary>
    /// An exception thrown on attempt to withdraw more money than a Deposit has.
    /// </summary>
    public class NotEnoughMoneyException : ApplicationException
    {

    }
}
