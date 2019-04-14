using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace BooksTask.Tests
{
    public class FancyBookFormatterTests
    {
        [Test]
        public void Format_FormatsFancyFormatString_Test()
        {
            Assert.That(
                string.Format(new FancyBookFormatter(), "{0:Fancy}", BookTests.TheArtOfTimeRewindingBook),
                Is.EqualTo("Book(Title = \"The Art of Time Rewinding\", Author = \"Max Caulfield\""));
        }

        [Test]
        public void Format_FallbacksToIFormattable_Test()
        {
            Assert.That(
                string.Format(new FancyBookFormatter(), "{0:\\tt (\\pr)}", BookTests.TheArtOfTimeRewindingBook),
                Is.EqualTo("The Art of Time Rewinding (205)"));
        }
    }
}
