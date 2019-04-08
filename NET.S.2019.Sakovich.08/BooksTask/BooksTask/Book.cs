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

        class IsbnSelector : IBookTagSelector<string>
        {
            public string SelectTag(Book book) => book.Isbn;
        }

        public string Author;

        class AuthorSelector : IBookTagSelector<string>
        {
            public string SelectTag(Book book) => book.Author;
        }

        public string Title;

        class TitleSelector : IBookTagSelector<string>
        {
            public string SelectTag(Book book) => book.Title;
        }

        public string Publisher;

        class PublisherSelector : IBookTagSelector<string>
        {
            public string SelectTag(Book book) => book.Publisher;
        }

        public uint YearPublished;

        class YearPublishedSelector : IBookTagSelector<uint>
        {
            public uint SelectTag(Book book) => book.YearPublished;
        }

        public uint Pages;

        class PagesSelector : IBookTagSelector<uint>
        {
            public uint SelectTag(Book book) => book.Pages;
        }

        public uint Price;

        class PriceSelector : IBookTagSelector<uint>
        {
            public uint SelectTag(Book book) => book.Price;
        }

        public enum Tag
        {
            Isdbn = 6,
            Author= 1,
            Title = 0,
            Publisher = 2,
            YearPublished = 3,
            Pages = 4,
            Price = 5
        }

        static readonly IBookTagSelector<string>[] StringTagSelectors = new IBookTagSelector<string>[]
        {
            new TitleSelector(),
            new AuthorSelector(),
            new PublisherSelector(), 
            null,
            null,
            null,
            new IsbnSelector()
        };

        static readonly IBookTagSelector<uint>[] UIntTagSelectors = new IBookTagSelector<uint>[]
        {
            null,
            null,
            null,
            new YearPublishedSelector(),
            new PagesSelector(),
            new PriceSelector(),
            null
        };

        static readonly IEqualityComparer<Book>[] BookByTagEqualityComparers = new IEqualityComparer<Book>[]
        {
            new BookTagEqualityComparer<string>(StringTagSelectors[(int)Book.Tag.Title]),
            new BookTagEqualityComparer<string>(StringTagSelectors[(int)Book.Tag.Author]),
            new BookTagEqualityComparer<string>(StringTagSelectors[(int)Book.Tag.Publisher]),
            new BookTagEqualityComparer<uint>(UIntTagSelectors[(int)Book.Tag.YearPublished]),
            new BookTagEqualityComparer<uint>(UIntTagSelectors[(int)Book.Tag.Pages]),
            new BookTagEqualityComparer<uint>(UIntTagSelectors[(int)Book.Tag.Price]),
            new BookTagEqualityComparer<string>(StringTagSelectors[(int)Book.Tag.Isdbn])
        };

        static readonly IComparer<Book>[] BookByTagComparers = new IComparer<Book>[]
        {
            new BookTagComaprer<string>(StringTagSelectors[(int)Book.Tag.Title]),
            new BookTagComaprer<string>(StringTagSelectors[(int)Book.Tag.Author]),
            new BookTagComaprer<string>(StringTagSelectors[(int)Book.Tag.Publisher]),
            new BookTagComaprer<uint>(UIntTagSelectors[(int)Book.Tag.YearPublished]),
            new BookTagComaprer<uint>(UIntTagSelectors[(int)Book.Tag.Pages]),
            new BookTagComaprer<uint>(UIntTagSelectors[(int)Book.Tag.Price]),
            new BookTagComaprer<string>(StringTagSelectors[(int)Book.Tag.Isdbn])
        };

        public static IEqualityComparer<Book> GetBookByTagEqualityComparer(Book.Tag tag)
        {
            return BookByTagEqualityComparers[(int)tag];
        }

        public static IComparer<Book> GetBookByTagComparer(Book.Tag tag)
        {
            return BookByTagComparers[(int)tag];
        }

        public static IBookTagSelector<string> GetStringTagSelector(Book.Tag tag)
        {
            return StringTagSelectors[(int)tag];
        }

        public static IBookTagSelector<uint> GetUIntTagSelector(Book.Tag tag)
        {
            return UIntTagSelectors[(int)tag];
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

            foreach (IComparer<Book> comp in BookByTagComparers)
            {
                CompareResult = comp.Compare(this, other);
                if(CompareResult != 0)
                {
                    return CompareResult;
                }
            }

            return 0;
        }

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
