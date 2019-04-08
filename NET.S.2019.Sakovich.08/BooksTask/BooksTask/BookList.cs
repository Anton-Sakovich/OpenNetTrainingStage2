using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BooksTask
{
    public class BookList
    {
        public readonly IList<Book> Books;

        public BookList(IEnumerable<Book> booksSource)
        {
            Books = booksSource.ToList();
        }

        BookList(List<Book> booksSource)
        {
            Books = booksSource;
        }

        public void SaveToFile(string fname)
        {
            using (BookWriter Writer = new BookWriter(new BinaryWriter(new FileStream(fname, FileMode.Create))))
            {
                foreach(Book book in Books)
                {
                    Writer.WriteBook(book);
                }
            }
        }

        public static BookList LoadFromFile(string fname)
        {
            List<Book> BooksSoure = new List<Book>();

            using (FileStream FStream = new FileStream(fname, FileMode.Open, FileAccess.Read))
            {
                using (BookReader Reader = new BookReader(new BinaryReader(FStream)))
                {
                    while (FStream.Position < FStream.Length)
                    {
                        BooksSoure.Add(Reader.ReadBook());
                    }
                }
            }

            return new BookList(BooksSoure);
        }

        public void AddBook(Book newBook)
        {
            if(Books.Contains(newBook))
            {
                throw new AddDuplicateBookException();
            }

            Books.Add(newBook);
        }

        public void RemoveBook(Book removeBook)
        {
            if(!Books.Contains(removeBook))
            {
                throw new BookNotFoundException();
            }

            Books.Remove(removeBook);
        }

        public void SortBooksByTag(Book.Tag tag)
        {
            IComparer<Book> SelectedComparer = Book.GetBookByTagComparer(tag);

            // Insertion sort
            Book Temp;

            for (int Outer = 1; Outer < Books.Count; Outer++)
            {
                for (int Inner = Outer; Inner > 0; Inner--)
                {
                    if (SelectedComparer.Compare(Books[Inner - 1], Books[Inner]) > 0)
                    {
                        Temp = Books[Inner - 1];
                        Books[Inner - 1] = Books[Inner];
                        Books[Inner] = Temp;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        public Book FindBookByTag(Book.Tag tag, string value)
        {
            IBookTagSelector<string> Selector = Book.GetStringTagSelector(tag);

            if(Selector == null)
            {
                throw new ArgumentException("The tag specified corresponds to a non-string tag value.");
            }

            foreach(Book book in Books)
            {
                if(Selector.SelectTag(book) == value)
                {
                    return book;
                }
            }

            throw new BookNotFoundException();
        }

        public Book FindBookByTag(Book.Tag tag, uint value)
        {
            IBookTagSelector<uint> Selector = Book.GetUIntTagSelector(tag);

            if (Selector == null)
            {
                throw new ArgumentException("The tag specified corresponds to a non-string tag value.");
            }

            foreach (Book book in Books)
            {
                if (Selector.SelectTag(book) == value)
                {
                    return book;
                }
            }

            throw new BookNotFoundException();
        }
    }
}
