using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.IO;

namespace BooksTask.Tests
{
    [TestFixture]
    public class BookListTests
    {
        [Test]
        public void SaveToFileTest()
        {
            BookList BookList1 = new BookList(BooksSample.Books);

            string PathToBookList1 = "F:\\Temp\\BookTest1.bls";

            BookList1.SaveToFile(PathToBookList1);

            Assert.That(File.Exists(PathToBookList1), Is.True);

            byte[] ActualBytes = File.ReadAllBytes(PathToBookList1);
            byte[] ExpectedBytes = BooksSample.BooksBytes;

            Assert.That(ActualBytes, Is.EqualTo(ExpectedBytes));
        }

        [Test]
        public void LoadFromFileTest()
        {
            string PathToBookList1 = "F:\\Temp\\BookTest1.bls";

            File.WriteAllBytes(PathToBookList1, BooksSample.BooksBytes);

            BookList BookList1 = BookList.LoadFromFile(PathToBookList1);

            Assert.That(BookList1.Books, Is.EqualTo(BooksSample.Books));
        }

        [Test]
        public void SortBooksByTag_Price_Test()
        {
            BookList BookList1 = new BookList(BooksSample.Books);

            BookList1.SortBooksByTag(Book.PriceTag);

            Assert.That(BookList1.Books, Is.EqualTo(BooksSample.BooksSortedByPrice));
        }

        [Test]
        public void FindBookByTag_BookExists_TagOK_Test()
        {
            BookList BookList1 = new BookList(BooksSample.Books);

            Book Expected = BooksSample.Books[0];
            Book Actual = BookList1.FindBookByTag(Book.IsbnTag, "123");
            Assert.AreEqual(Actual, Expected);

            Expected = BooksSample.Books[1];
            Actual = BookList1.FindBookByTag(Book.AuthorTag, "Author2");
            Assert.AreEqual(Actual, Expected);

            Expected = BooksSample.Books[0];
            Actual = BookList1.FindBookByTag(Book.TitleTag, "Title1");
            Assert.AreEqual(Actual, Expected);

            Expected = BooksSample.Books[1];
            Actual = BookList1.FindBookByTag(Book.PublisherTag, "Publisher2");
            Assert.AreEqual(Actual, Expected);

            Expected = BooksSample.Books[0];
            Actual = BookList1.FindBookByTag(Book.YearPublishedTag, (uint)1995);
            Assert.AreEqual(Actual, Expected);

            Expected = BooksSample.Books[1];
            Actual = BookList1.FindBookByTag(Book.PagesTag, (uint)304);
            Assert.AreEqual(Actual, Expected);

            Expected = BooksSample.Books[0];
            Actual = BookList1.FindBookByTag(Book.PriceTag, (uint)102);
            Assert.AreEqual(Actual, Expected);
        }

        [Test]
        public void FindBookByTag_BookNotFound_ExceptionThrown()
        {
            BookList BookList1 = new BookList(BooksSample.Books);

            Assert.That(() => BookList1.FindBookByTag(Book.IsbnTag, "Foo"), Throws.TypeOf<BookNotFoundException>());
        }

        private void OnChangingBookBase(Action<BookList, EventHandler<BookChangedEventArgs>> subscriber, Action<BookList, Book> trigger, bool subscribe)
        {
            BookList expectedBookList = new BookList(BooksSample.Books);
            Book expectedBook = BooksSample.LifeIsStrangeBook;

            BookList actualBookList = null;
            Book actualBook = null;

            if (subscribe)
            {
                subscriber(expectedBookList, (sender, e) => { actualBookList = (BookList)sender; actualBook = e.ChangedBook; });
            }

            trigger(expectedBookList, expectedBook);

            if (subscribe)
            {
                Assert.That(actualBookList, Is.EqualTo(expectedBookList));
                Assert.That(actualBook, Is.EqualTo(expectedBook));
            }
            else
            {
                Assert.That(actualBookList, Is.Null);
                Assert.That(actualBook, Is.Null);
            }
        }

        [Test]
        public void OnAddingBook_HasHandlers_Test()
        {
            OnChangingBookBase(
                (blist, handler) => blist.AddingBook += handler,
                (blist, book) => blist.AddBook(book),
                true
            );
        }

        [Test]
        public void OnAddingBook_NoHandlers_Test()
        {
            OnChangingBookBase(
                (blist, handler) => blist.AddingBook += handler,
                (blist, book) => blist.AddBook(book),
                false
            );
        }

        [Test]
        public void OnBookAdded_HasHandlers_Test()
        {
            OnChangingBookBase(
                (blist, handler) => blist.BookAdded += handler,
                (blist, book) => blist.AddBook(book),
                true
            );
        }

        [Test]
        public void OnBookAdded_NoHandlers_Test()
        {
            OnChangingBookBase(
                (blist, handler) => blist.BookAdded += handler,
                (blist, book) => blist.AddBook(book),
                false
            );
        }

        [Test]
        public void OnRemovingBook_HasHandlers_Test()
        {
            OnChangingBookBase(
                (blist, handler) => blist.RemovingBook += handler,
                (blist, book) => { blist.AddBook(book);  blist.RemoveBook(book); },
                true
            );
        }

        [Test]
        public void OnRemovingBook_NoHandlers_Test()
        {
            OnChangingBookBase(
                (blist, handler) => blist.RemovingBook += handler,
                (blist, book) => { blist.AddBook(book); blist.RemoveBook(book); },
                false
            );
        }

        [Test]
        public void OnBookRemoved_HasHandlers_Test()
        {
            OnChangingBookBase(
                (blist, handler) => blist.BookRemoved += handler,
                (blist, book) => { blist.AddBook(book); blist.RemoveBook(book); },
                true
            );
        }

        [Test]
        public void OnBookRemoved_NoHandlers_Test()
        {
            OnChangingBookBase(
                (blist, handler) => blist.BookRemoved += handler,
                (blist, book) => { blist.AddBook(book); blist.RemoveBook(book); },
                false
            );
        }
    }
}
