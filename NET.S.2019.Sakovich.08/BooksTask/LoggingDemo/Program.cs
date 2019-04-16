using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksTask;
using NLog;

namespace LoggingDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger lgr = NLog.LogManager.GetLogger("Foo");

            BookList blst1 = new BookList(Enumerable.Empty<Book>());

            blst1.AddingBook += (sender, e) => lgr.Trace("A book {0} is being added to {1}.", e.ChangedBook, sender);

            blst1.AddBook(new Book("123", "Kate Marsh", "View from the Top", "Blackwell Dorms", 2013, 100, 222));

            Console.ReadKey();
        }
    }
}
