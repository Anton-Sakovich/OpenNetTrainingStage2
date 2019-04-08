using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksTask
{
    public class BookTagComaprer<T> : IComparer<Book> where T : IComparable<T>
    {
        public readonly IBookTagSelector<T> Selector;

        public BookTagComaprer(IBookTagSelector<T> selector)
        {
            Selector = selector;
        }

        public int Compare(Book book1, Book book2)
        {
            if(ReferenceEquals(book1, null))
            {
                throw new ArgumentNullException(nameof(book1));
            }

            if (ReferenceEquals(book2, null))
            {
                throw new ArgumentNullException(nameof(book2));
            }

            return Selector.SelectTag(book1).CompareTo(Selector.SelectTag(book2));
        }
    }
}
