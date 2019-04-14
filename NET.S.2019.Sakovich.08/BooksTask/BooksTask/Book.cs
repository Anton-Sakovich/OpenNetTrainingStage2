using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksTask
{
    public class Book : IEquatable<Book>, IComparable<Book>
    {
        public static readonly IBookTag<string> TitleTag = new TitleBookTag();

        public static readonly IBookTag<string> AuthorTag = new AuthorBookTag();

        public static readonly IBookTag<string> PublisherTag = new PublisherBookTag();

        public static readonly IBookTag<uint> YearPublishedTag = new YearPublishedBookTag();

        public static readonly IBookTag<uint> PagesTag = new PagesBookTag();

        public static readonly IBookTag<string> IsbnTag = new IsbnBookTag();

        public static readonly IBookTag<uint> PriceTag = new PriceBookTag();

        public Book(string isbn, string author, string title, string publisher, uint yearPublished, uint pages, uint price)
        {
            this.Isbn = isbn ?? throw new ArgumentNullException(nameof(isbn));
            this.Author = author ?? throw new ArgumentNullException(nameof(author));
            this.Title = title ?? throw new ArgumentNullException(nameof(title));
            this.Publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
            this.YearPublished = yearPublished;
            this.Pages = pages;
            this.Price = price;
        }

        public string Isbn { get; set; }

        public string Author { get; set; }

        public string Title { get; set; }

        public string Publisher { get; set; }

        public uint YearPublished { get; set; }

        public uint Pages { get; set; }

        public uint Price { get; set; }

        public static bool operator ==(Book book1, Book book2)
        {
            return ReferenceEquals(book1, null) ? ReferenceEquals(book2, null) : book1.Equals(book2);
        }

        public static bool operator !=(Book book1, Book book2)
        {
            return ReferenceEquals(book1, null) ? !ReferenceEquals(book2, null) : !book1.Equals(book2);
        }

        public override bool Equals(object obj)
        {
            var book = obj as Book;
            return !ReferenceEquals(book, null) &&
                   this.Isbn == book.Isbn &&
                   this.Author == book.Author &&
                   this.Title == book.Title &&
                   this.Publisher == book.Publisher &&
                   this.YearPublished == book.YearPublished &&
                   this.Pages == book.Pages &&
                   this.Price == book.Price;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = 1926891238;
                hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(this.Isbn);
                hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(this.Author);
                hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(this.Title);
                hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(this.Publisher);
                hashCode = (hashCode * -1521134295) + this.YearPublished.GetHashCode();
                hashCode = (hashCode * -1521134295) + this.Pages.GetHashCode();
                hashCode = (hashCode * -1521134295) + this.Price.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"{this.GetType().Name}<{this.Title} by {this.Author}, {this.YearPublished}>";
        }

        public bool Equals(Book other)
        {
            if (ReferenceEquals(other, null))
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
            if (ReferenceEquals(other, null))
            {
                throw new InvalidOperationException("A Book instance has been compared with null.");
            }

            int compareResult;

            if ((compareResult = this.Title.CompareTo(other.Title)) != 0)
            {
                return compareResult;
            }
            else if ((compareResult = this.Author.CompareTo(other.Author)) != 0)
            {
                return compareResult;
            }
            else if ((compareResult = this.Publisher.CompareTo(other.Publisher)) != 0)
            {
                return compareResult;
            }
            else if (this.YearPublished != other.YearPublished)
            {
                return (((int)this.YearPublished - (int)other.YearPublished) >> 31) | 1;
            }
            else if (this.Pages != other.Pages)
            {
                return (((int)this.YearPublished - (int)other.YearPublished) >> 31) | 1;
            }
            else if ((compareResult = this.Isbn.CompareTo(other.Isbn)) != 0)
            {
                return compareResult;
            }
            else if (this.Price != other.Price)
            {
                return (((int)this.YearPublished - (int)other.YearPublished) >> 31) | 1;
            }

            return 0;
        }

        private class IsbnBookTag : IBookTag<string>
        {
            public string GetTag(Book book)
            {
                return book.Isbn;
            }
        }

        private class AuthorBookTag : IBookTag<string>
        {
            public string GetTag(Book book)
            {
                return book.Author;
            }
        }

        private class TitleBookTag : IBookTag<string>
        {
            public string GetTag(Book book)
            {
                return book.Title;
            }
        }

        private class PublisherBookTag : IBookTag<string>
        {
            public string GetTag(Book book)
            {
                return book.Publisher;
            }
        }

        private class YearPublishedBookTag : IBookTag<uint>
        {
            public uint GetTag(Book book)
            {
                return book.YearPublished;
            }
        }

        private class PagesBookTag : IBookTag<uint>
        {
            public uint GetTag(Book book)
            {
                return book.Pages;
            }
        }

        private class PriceBookTag : IBookTag<uint>
        {
            public uint GetTag(Book book)
            {
                return book.Price;
            }
        }
    }
}
