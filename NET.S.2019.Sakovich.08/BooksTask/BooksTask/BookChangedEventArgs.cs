using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksTask
{
    public class BookChangedEventArgs : EventArgs
    {
        public Book ChangedBook { get; }

        public BookChangedEventArgs(Book chBook)
        {
            ChangedBook = chBook;
        }
    }
}
