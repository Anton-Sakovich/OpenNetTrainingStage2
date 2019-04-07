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

            BookList1.SortBooksByTag(Book.Tag.Price);

            Assert.That(BookList1.Books, Is.EqualTo(BooksSample.BooksSortedByPrice));
        }

        [Test]
        public void FindBookByTag_BookExists_TagOK_Test()
        {
            BookList BookList1 = new BookList(BooksSample.Books);

            Book Expected = BooksSample.Books[0];
            Book Actual = BookList1.FindBookByTag(Book.Tag.Isdbn, "123");
            Assert.AreEqual(Actual, Expected);

            Expected = BooksSample.Books[1];
            Actual = BookList1.FindBookByTag(Book.Tag.Author, "Author2");
            Assert.AreEqual(Actual, Expected);

            Expected = BooksSample.Books[0];
            Actual = BookList1.FindBookByTag(Book.Tag.Title, "Title1");
            Assert.AreEqual(Actual, Expected);

            Expected = BooksSample.Books[1];
            Actual = BookList1.FindBookByTag(Book.Tag.Publisher, "Publisher2");
            Assert.AreEqual(Actual, Expected);

            Expected = BooksSample.Books[0];
            Actual = BookList1.FindBookByTag(Book.Tag.YearPublished, 1995);
            Assert.AreEqual(Actual, Expected);

            Expected = BooksSample.Books[1];
            Actual = BookList1.FindBookByTag(Book.Tag.Pages, 304);
            Assert.AreEqual(Actual, Expected);

            Expected = BooksSample.Books[0];
            Actual = BookList1.FindBookByTag(Book.Tag.Price, 102);
            Assert.AreEqual(Actual, Expected);
        }

        [Test]
        public void FindBookByTag_TagValueMismatch_ExceptionThrown()
        {
            BookList BookList1 = new BookList(BooksSample.Books);

            Assert.That(() => BookList1.FindBookByTag(Book.Tag.Isdbn, 123), Throws.TypeOf<ArgumentException>());
            Assert.That(() => BookList1.FindBookByTag(Book.Tag.Price, "Good damn it, it is a string!"), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void FindBookByTag_BookNotFound_ExceptionThrown()
        {
            BookList BookList1 = new BookList(BooksSample.Books);

            Assert.That(() => BookList1.FindBookByTag(Book.Tag.Isdbn, "Foo"), Throws.TypeOf<BookNotFoundException>());
        }
    }
}
