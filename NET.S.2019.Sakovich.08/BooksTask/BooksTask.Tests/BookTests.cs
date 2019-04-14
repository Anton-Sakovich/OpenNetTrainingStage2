using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace BooksTask.Tests
{
    [TestFixture]
    public class BookTests
    {
        public static readonly Book TheArtOfTimeRewindingBook = new Book("1234", "Max Caulfield", "The Art of Time Rewinding", "Blackwell Academy", 2013, 123, 205);

        [Test]
        public void ToString_ValidTokens_Test()
        {
            Assert.That(TheArtOfTimeRewindingBook.ToString(), Is.EqualTo("Book(The Art of Time Rewinding by Max Caulfield)"));
            Assert.That(TheArtOfTimeRewindingBook.ToString(@"\\\tt"), Is.EqualTo("\\The Art of Time Rewinding"));
            Assert.That(TheArtOfTimeRewindingBook.ToString(@"\tt"), Is.EqualTo("The Art of Time Rewinding"));
            Assert.That(TheArtOfTimeRewindingBook.ToString(@"\tt\\"), Is.EqualTo("The Art of Time Rewinding\\"));
            Assert.That(TheArtOfTimeRewindingBook.ToString(@"\\"), Is.EqualTo("\\"));
        }

        [Test]
        public void ToString_InvalidTokens_Test()
        {
            Assert.That(() => TheArtOfTimeRewindingBook.ToString(@"\t\\"), Throws.TypeOf<FormatException>());
        }

        [Test]
        public void BookFormattableTest()
        {
            Assert.That(String.Format(@"{0:\tt (\yp)}", TheArtOfTimeRewindingBook), Is.EqualTo("The Art of Time Rewinding (2013)"));
        }
    }
}
