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
            IComparer<Book> SelectedComparer = null;

            switch (tag)
            {
                case Book.Tag.Author:
                    SelectedComparer = new Book.AuthorComparer();
                    break;
                case Book.Tag.Isdbn:
                    SelectedComparer = new Book.IsbnComparer();
                    break;
                case Book.Tag.Pages:
                    SelectedComparer = new Book.PagesComparer();
                    break;
                case Book.Tag.Price:
                    SelectedComparer = new Book.PriceComparer();
                    break;
                case Book.Tag.Publisher:
                    SelectedComparer = new Book.PublisherComparer();
                    break;
                case Book.Tag.Title:
                    SelectedComparer = new Book.TitleComparer();
                    break;
                case Book.Tag.YearPublished:
                    SelectedComparer = new Book.YearPublishedComparer();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(); // In case we modify Tag enum but forget to modify this
            }

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
            ITagChecker<string> SelectedChecker = null;

            switch (tag)
            {
                case Book.Tag.Author:
                    SelectedChecker = new AuthorChecker();
                    break;
                case Book.Tag.Isdbn:
                    SelectedChecker = new IsbnChecker();
                    break;
                case Book.Tag.Publisher:
                    SelectedChecker = new PublisherChecker();
                    break;
                case Book.Tag.Title:
                    SelectedChecker = new TitleChecker();
                    break;
                default:
                    throw new ArgumentException();
            }

            foreach(Book book in Books)
            {
                if(SelectedChecker.CheckTag(book, value))
                {
                    return book;
                }
            }

            throw new BookNotFoundException();
        }

        public Book FindBookByTag(Book.Tag tag, uint value)
        {
            ITagChecker<uint> SelectedChecker = null;

            switch (tag)
            {
                case Book.Tag.Pages:
                    SelectedChecker = new PagesChecker();
                    break;
                case Book.Tag.Price:
                    SelectedChecker = new PriceChecker();
                    break;
                case Book.Tag.YearPublished:
                    SelectedChecker = new YearPublishedChecker();
                    break;
                default:
                    throw new ArgumentException();
            }

            foreach (Book book in Books)
            {
                if (SelectedChecker.CheckTag(book, value))
                {
                    return book;
                }
            }

            throw new BookNotFoundException();
        }

        interface ITagChecker<T>
        {
            bool CheckTag(Book book, T val);
        }

        class IsbnChecker : ITagChecker<string>
        {
            public bool CheckTag(Book book, string val)
            {
                return book.Isbn == val;
            }
        }

        class AuthorChecker : ITagChecker<string>
        {
            public bool CheckTag(Book book, string val)
            {
                return book.Author == val;
            }
        }

        class TitleChecker : ITagChecker<string>
        {
            public bool CheckTag(Book book, string val)
            {
                return book.Title == val;
            }
        }

        class PublisherChecker : ITagChecker<string>
        {
            public bool CheckTag(Book book, string val)
            {
                return book.Publisher == val;
            }
        }

        class YearPublishedChecker : ITagChecker<uint>
        {
            public bool CheckTag(Book book, uint val)
            {
                return book.YearPublished == val;
            }
        }

        class PagesChecker : ITagChecker<uint>
        {
            public bool CheckTag(Book book, uint val)
            {
                return book.Pages == val;
            }
        }

        class PriceChecker : ITagChecker<uint>
        {
            public bool CheckTag(Book book, uint val)
            {
                return book.Price == val;
            }
        }
    }
}
