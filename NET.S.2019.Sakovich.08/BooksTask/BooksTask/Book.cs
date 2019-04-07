using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksTask
{
    public class Book
    {
        public string Isbn;
        public string Author;
        public string Title;
        public string Publisher;
        public uint YearPublished;
        public uint Pages;
        public uint Price;

        public override bool Equals(object obj)
        {
            if(obj == null || obj.GetType() != this.GetType())
            {
                return false;
            }

            Book Other = (Book)obj;

            return
                this.Isbn == Other.Isbn &&
                this.Author == Other.Author &&
                this.Title == Other.Title &&
                this.Publisher == Other.Publisher &&
                this.YearPublished == Other.YearPublished &&
                this.Pages == Other.Pages &&
                this.Price == Other.Price;
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
    }
}
