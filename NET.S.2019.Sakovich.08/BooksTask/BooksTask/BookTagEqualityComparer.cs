using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksTask
{
    public class BookTagEqualityComparer<T> : IEqualityComparer<Book> where T : IEquatable<T>
    {
        public readonly IBookTagSelector<T> Selector;

        public BookTagEqualityComparer(IBookTagSelector<T> selector)
        {
            Selector = selector;
        }

        public int GetHashCode(Book book)
        {
            if (ReferenceEquals(book, null))
            {
                throw new ArgumentNullException(nameof(book));
            }

            return Selector.SelectTag(book).GetHashCode();
        }

        public bool Equals(Book book1, Book book2)
        {
            if (ReferenceEquals(book1, null))
            {
                throw new ArgumentNullException(nameof(book1));
            }

            if (ReferenceEquals(book2, null))
            {
                throw new ArgumentNullException(nameof(book2));
            }

            return (Selector.SelectTag(book1)).Equals(Selector.SelectTag(book2));
        }
    }
}
