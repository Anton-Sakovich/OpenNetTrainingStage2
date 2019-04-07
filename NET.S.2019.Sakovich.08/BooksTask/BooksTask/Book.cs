using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksTask
{
    public class Book : IEquatable<Book>, IComparable<Book>
    {
        public string Isbn;
        public string Author;
        public string Title;
        public string Publisher;
        public uint YearPublished;
        public uint Pages;
        public uint Price;

        public enum Tag
        {
            Isdbn = 0x00,
            Author= 0x01,
            Title = 0x02,
            Publisher = 0x03,
            YearPublished = 0x10,
            Pages = 0x11,
            Price = 0x12
        }

        public Book(string isbn, string author, string title, string publisher, uint yearPublished, uint pages, uint price)
        {
            this.Isbn = isbn;
            this.Author = author;
            this.Title = title;
            this.Publisher = publisher;
            this.YearPublished = yearPublished;
            this.Pages = pages;
            this.Price = price;
        }

        public override bool Equals(object obj)
        {
            if(obj is Book Other)
            {
                return
                this.Isbn == Other.Isbn &&
                this.Author == Other.Author &&
                this.Title == Other.Title &&
                this.Publisher == Other.Publisher &&
                this.YearPublished == Other.YearPublished &&
                this.Pages == Other.Pages &&
                this.Price == Other.Price;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            int Hash = this.Isbn.GetHashCode();

            unchecked
            {
                Hash = (Hash * 397) ^ this.Author.GetHashCode();
                Hash = (Hash * 397) ^ this.Title.GetHashCode();
                Hash = (Hash * 397) ^ this.Publisher.GetHashCode();
                Hash = (Hash * 397) ^ this.YearPublished.GetHashCode();
                Hash = (Hash * 397) ^ this.Pages.GetHashCode();
                Hash = (Hash * 397) ^ this.Price.GetHashCode();
            }

            return Hash;
        }

        public override string ToString()
        {
            return $"{this.GetType().Name}<{this.Title} by {this.Author}, {this.YearPublished}>";
        }

        public bool Equals(Book other)
        {
            if(other == null)
            {
                return false;
            }

            return
                this.Isbn == other.Isbn &&
                this.Author == other.Author &&
                this.Title == other.Title &&
                this.Publisher == other.Publisher &&
                this.YearPublished == other.YearPublished &&
                this.Pages == other.Pages &&
                this.Price == other.Price;
        }

        public int CompareTo(Book other)
        {
            if(other == null)
            {
                throw new InvalidOperationException("A Book instance has been compared with null.");
            }

            int CompareResult;

            foreach (IComparer<Book> comp in PrimaryComparers)
            {
                CompareResult = comp.Compare(this, other);
                if(CompareResult != 0)
                {
                    return CompareResult;
                }
            }

            return 0;
        }

        public sealed class IsbnComparer : IComparer<Book>
        {
            public int Compare(Book book1, Book book2)
            {
                return book1.Isbn.CompareTo(book2.Isbn);
            }
        }

        public sealed class AuthorComparer : IComparer<Book>
        {
            public int Compare(Book book1, Book book2)
            {
                return book1.Author.CompareTo(book2.Author);
            }
        }

        public sealed class TitleComparer : IComparer<Book>
        {
            public int Compare(Book book1, Book book2)
            {
                return book1.Title.CompareTo(book2.Title);
            }
        }

        public sealed class PublisherComparer : IComparer<Book>
        {
            public int Compare(Book book1, Book book2)
            {
                return book1.Publisher.CompareTo(book2.Publisher);
            }
        }

        public sealed class YearPublishedComparer : IComparer<Book>
        {
            public int Compare(Book book1, Book book2)
            {
                if (book1.YearPublished == book2.YearPublished)
                {
                    return 0;
                }
                else
                {
                    return (((int)book1.YearPublished - (int)book2.YearPublished) >> 31) & 1;
                }
            }
        }

        public sealed class PagesComparer : IComparer<Book>
        {
            public int Compare(Book book1, Book book2)
            {
                if (book1.Pages == book2.Pages)
                {
                    return 0;
                }
                else
                {
                    return (((int)book1.Pages - (int)book2.Pages) >> 31) & 1;
                }
            }
        }

        public sealed class PriceComparer : IComparer<Book>
        {
            public int Compare(Book book1, Book book2)
            {
                if (book1.Price == book2.Price)
                {
                    return 0;
                }
                else
                {
                    return (((int)book1.Price - (int)book2.Price) >> 31) & 1;
                }
            }
        }

        static readonly IComparer<Book>[] PrimaryComparers = new IComparer<Book>[]
        {
            new TitleComparer(), new AuthorComparer(), new YearPublishedComparer(),
            new PagesComparer(), new YearPublishedComparer()
        };

        public static bool operator ==(Book book1, Book book2)
        {
            return ReferenceEquals(book1, null) ? ReferenceEquals(book2, null) : book1.Equals(book2);
        }

        public static bool operator !=(Book book1, Book book2)
        {
            return ReferenceEquals(book1, null) ? !ReferenceEquals(book2, null) : !book1.Equals(book2);
        }
    }
}
