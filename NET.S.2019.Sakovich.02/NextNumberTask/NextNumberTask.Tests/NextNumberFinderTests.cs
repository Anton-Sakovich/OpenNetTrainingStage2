using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextNumberTask;
using NUnit.Framework;

namespace NextNumberTask.Tests
{
    [TestFixture]
    class NextNumberFinderTests
    {
        [TestCase(12, ExpectedResult = 21)]
        [TestCase(513, ExpectedResult = 531)]
        [TestCase(2017, ExpectedResult = 2071)]
        [TestCase(414, ExpectedResult = 441)]
        [TestCase(1176980216, ExpectedResult = 1176980261)]
        [TestCase(1350974179, ExpectedResult = 1350974197)]
        [TestCase(1655781228, ExpectedResult = 1655781282)]
        [TestCase(657142064, ExpectedResult = 657142406)]
        [TestCase(1205969247, ExpectedResult = 1205969274)]
        [TestCase(346069745, ExpectedResult = 346069754)]
        [TestCase(608118613, ExpectedResult = 608118631)]
        [TestCase(1525662710, ExpectedResult = 1525667012)]
        [TestCase(678060409, ExpectedResult = 678060490)]
        [TestCase(76862175, ExpectedResult = 76862517)]
        [TestCase(1278780767, ExpectedResult = 1278780776)]
        [TestCase(853585376, ExpectedResult = 853585637)]
        [TestCase(1318516019, ExpectedResult = 1318516091)]
        [TestCase(164702473, ExpectedResult = 164702734)]
        [TestCase(1136783364, ExpectedResult = 1136783436)]
        [TestCase(136384118, ExpectedResult = 136384181)]
        public int? RandomTests(int number) => NextNumberFinder.NextBiggerThan(number);

        [TestCase(0)]
        [TestCase(-1)]
        public void InputNumber_NotPositive_ExceptionThrown(int number)
        {
            Assert.That(() => NextNumberFinder.NextBiggerThan(number), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void NextNumber_ExistsButOverflow_NullReturned()
        {
            Assert.That(NextNumberFinder.NextBiggerThan(1999999999), Is.Null);
        }

        [Test]
        public void TimeMeasurements_Stopwatch()
        {
            IStopwatch Timer1 = new StopwatchTimer();

            int? NextNumberFound = NextNumberFinder.NextBiggerThan(123, Timer1);

            Assert.That(Timer1.IsRunning, Is.False, "Timer is not running after the method finished.");
            Assert.That(NextNumberFound, Is.EqualTo(132), "Timer does not break the method's logic.");
            Assert.That(Timer1.Elapsed.Ticks, Is.GreaterThan(0), "Timer shows a non-negative number of ticks elapsed.");

            Assert.Pass("{0} ticks elapsed ({1} milliseconds).", Timer1.Elapsed.Ticks, Timer1.Elapsed.Milliseconds);
        }
    }
}
