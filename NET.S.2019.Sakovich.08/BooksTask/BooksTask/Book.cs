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
            return $"{this.GetType().Name}<{this.Title}, {this.YearPublished}>";
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

            if ((CompareResult = this.Title.CompareTo(other.Title)) != 0)
            {
                return CompareResult;
            } else if ((CompareResult = this.Author.CompareTo(other.Author)) != 0)
            {
                return CompareResult;
            } else if ((CompareResult = this.Publisher.CompareTo(other.Publisher)) != 0)
            {
                return CompareResult;
            }
            else if (this.YearPublished != other.YearPublished)
            {
                return (((int)this.YearPublished - (int)other.YearPublished) >> 31) & 1;
            }
            else if (this.Pages != other.Pages)
            {
                return (((int)this.Pages - (int)other.Pages) >> 31) & 1;
            }
            else
            {
                return (((int)this.Price - (int)other.Price) >> 31) & 1;
            }
        }
    }
}
