using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksTask
{
    public class BookList
    {
        public readonly IList<Book> Books;

        public BookList(IEnumerable<Book> booksSource)
        {
            Books = booksSource.ToList();
        }

        private BookList(List<Book> booksSource)
        {
            Books = booksSource;
        }

        public static BookList LoadFromFile(string fname)
        {
            List<Book> booksSoure = new List<Book>();

            using (FileStream fstream = new FileStream(fname, FileMode.Open, FileAccess.Read))
            {
                using (BookReader reader = new BookReader(new BinaryReader(fstream)))
                {
                    while (fstream.Position < fstream.Length)
                    {
                        booksSoure.Add(reader.ReadBook());
                    }
                }
            }

            return new BookList(booksSoure);
        }

        public void SaveToFile(string fname)
        {
            using (BookWriter writer = new BookWriter(new BinaryWriter(new FileStream(fname, FileMode.Create))))
            {
                foreach (Book book in Books)
                {
                    writer.WriteBook(book);
                }
            }
        }

        public void AddBook(Book newBook)
        {
            if (Books.Contains(newBook))
            {
                throw new AddDuplicateBookException();
            }

            Books.Add(newBook);
        }

        public void RemoveBook(Book removeBook)
        {
            if (!Books.Contains(removeBook))
            {
                throw new BookNotFoundException();
            }

            Books.Remove(removeBook);
        }

        public void SortBooksByTag<T>(IBookTag<T> tag)
            where T : IComparable<T>
        {
            // Insertion sort
            Book temp;

            for (int outer = 1; outer < this.Books.Count; outer++)
            {
                for (int inner = outer; inner > 0; inner--)
                {
                    if (tag.GetTag(this.Books[inner - 1]).CompareTo(tag.GetTag(this.Books[inner])) > 0)
                    {
                        temp = this.Books[inner - 1];
                        this.Books[inner - 1] = this.Books[inner];
                        this.Books[inner] = temp;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        public Book FindBookByTag<T>(IBookTag<T> tag, T value)
            where T : IEquatable<T>
        {
            foreach (Book book in this.Books)
            {
                if (tag.GetTag(book).Equals(value))
                {
                    return book;
                }
            }

            throw new BookNotFoundException();
        }
    }
}
