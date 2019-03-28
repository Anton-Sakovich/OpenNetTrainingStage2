using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace GCDTask.Tests
{
    [TestFixture]
    public abstract class GCDTestsBase
    {
        private readonly GCDBase TestedGCDObject;

        public GCDTestsBase(GCDBase tested)
        {
            TestedGCDObject = tested;
        }

        protected static IEnumerable<TestCaseData> SingleArgumentTestData
        {
            get
            {
                for (int i = -2; i < 3; i++)
                {
                    yield return new TestCaseData(i).Returns(Math.Abs(i));
                }
            }
        }

        protected static IEnumerable<TestCaseData> TwoArgumentsTestData
        {
            get
            {
                yield return new TestCaseData(608532563, 1947714034).Returns(1);
                yield return new TestCaseData(-1091149873, 536472850).Returns(1);
                yield return new TestCaseData(-732934307, -2061903435).Returns(1);
                yield return new TestCaseData(-213491738, 1258852680).Returns(2);
                yield return new TestCaseData(-1657764645, 414895599).Returns(3);
                yield return new TestCaseData(-1218437966, -1370144133).Returns(1);
                yield return new TestCaseData(-716156275, -782512179).Returns(1);
                yield return new TestCaseData(1606940719, 1056656569).Returns(1);
                yield return new TestCaseData(-558485030, 37451211).Returns(1);
                yield return new TestCaseData(1466598320, -1321506538).Returns(22);
                yield return new TestCaseData(2001046363, 1548210297).Returns(1);
                yield return new TestCaseData(-1872178909, -1568648410).Returns(1);
                yield return new TestCaseData(-1558373064, 909043658).Returns(2);
                yield return new TestCaseData(594555220, 1979550627).Returns(1);
                yield return new TestCaseData(-989598031, 1032618701).Returns(1);
                yield return new TestCaseData(1974258326, 180174560).Returns(2);
                yield return new TestCaseData(-906584661, -954523118).Returns(1);
                yield return new TestCaseData(-1336711799, 2041482701).Returns(1);
                yield return new TestCaseData(324879168, -699396176).Returns(16);
                yield return new TestCaseData(-1146440863, 438095205).Returns(1);
                yield return new TestCaseData(-1624815107, -1504287475).Returns(1);
                yield return new TestCaseData(-1638664104, 1715216058).Returns(6);
                yield return new TestCaseData(1484288167, 1695878275).Returns(1);
                yield return new TestCaseData(-762360973, -436453421).Returns(1);
                yield return new TestCaseData(-204493701, -985184829).Returns(3);
                yield return new TestCaseData(220885116, -2114960129).Returns(1);
                yield return new TestCaseData(-415876274, -2030311158).Returns(2);
                yield return new TestCaseData(360483101, 691722443).Returns(1);
                yield return new TestCaseData(-44432163, 1289817468).Returns(9);
                yield return new TestCaseData(354187598, 1198644450).Returns(2);
                yield return new TestCaseData(-452238887, 1464677545).Returns(1);
                yield return new TestCaseData(-1387409355, -497013647).Returns(19);
                yield return new TestCaseData(-1795391515, 2062612905).Returns(5);
                yield return new TestCaseData(729051066, 825757119).Returns(9);
                yield return new TestCaseData(1205990039, -1071334708).Returns(1);
                yield return new TestCaseData(579204534, 36977756).Returns(2);
                yield return new TestCaseData(-1511100538, -1023193450).Returns(2);
                yield return new TestCaseData(991706733, 603561839).Returns(1);
                yield return new TestCaseData(27337174, 1914852204).Returns(2);
                yield return new TestCaseData(371709579, -829060874).Returns(1);
            }
        }

        [TestCaseSource(nameof(SingleArgumentTestData))]
        public int SingleArgumentTests(int x)
        {
            return TestedGCDObject.GCD(x);
        }

        [TestCase(5, 2, ExpectedResult = 1)]
        [TestCase(21, 16, ExpectedResult = 1)]
        [TestCase(63, 81, ExpectedResult = 9)]
        [TestCase(-5, 2, ExpectedResult = 1)]
        [TestCase(21, -16, ExpectedResult = 1)]
        [TestCase(-63, -81, ExpectedResult = 9)]
        public int Lightweight_TwoArguments_NonSpecial_Tests(int x, int y)
        {
            return TestedGCDObject.GCD(x, y);
        }

        [TestCaseSource(nameof(TwoArgumentsTestData))]
        public int Heavy_TwoArguments_NonSpecial_Testss(int x, int y)
        {
            return TestedGCDObject.GCD(x, y);
        }

        [Test]
        public void GCD_NullArray_ArgumentNullExceptionThrown()
        {
            Assert.Throws<ArgumentNullException>(() => new GCDEuclid().GCD(null));
        }

        [Test]
        public void GCD_EmptyArray_ArgumentExceptionThrown()
        {
            Assert.Throws<ArgumentException>(() => new GCDEuclid().GCD());
        }
    }
}
