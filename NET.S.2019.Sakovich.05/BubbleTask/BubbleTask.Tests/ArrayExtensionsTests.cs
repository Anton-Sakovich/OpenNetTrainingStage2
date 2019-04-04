using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace BubbleTask.Tests
{
    [TestFixture]
    public class ArrayExtensionsTest
    {
        static IEnumerable<TestCaseData> RowsMaxTestCases
        {
            get
            {
                int[][] Actual = new int[7][];

                Actual[0] = new int[] { 10, 12 }; // 0 : 12
                Actual[1] = new int[] { 5, 8 }; // 1 : 8
                Actual[2] = new int[] { 1, 2 }; // 2 : 2
                Actual[3] = new int[] { 9, 2 }; // 3: 9
                Actual[4] = new int[] { 1 }; // 4: 1
                Actual[5] = new int[] { 45, 43 }; // 5: 45
                Actual[6] = new int[] { 96, 57, 75 }; // 6: 96

                int[][] ExpectedAsc = new int[7][];

                ExpectedAsc[0] = Actual[4];
                ExpectedAsc[1] = Actual[2];
                ExpectedAsc[2] = Actual[1];
                ExpectedAsc[3] = Actual[3];
                ExpectedAsc[4] = Actual[0];
                ExpectedAsc[5] = Actual[5];
                ExpectedAsc[6] = Actual[6];

                int[][] ExpectedDesc = new int[7][];

                ExpectedDesc[0] = Actual[6];
                ExpectedDesc[1] = Actual[5];
                ExpectedDesc[2] = Actual[0];
                ExpectedDesc[3] = Actual[3];
                ExpectedDesc[4] = Actual[1];
                ExpectedDesc[5] = Actual[2];
                ExpectedDesc[6] = Actual[4];

                yield return new TestCaseData(Actual, ExpectedAsc, ExpectedDesc);
            }
        }

        static IEnumerable<TestCaseData> RowsMinTestCases
        {
            get
            {
                int[][] Actual = new int[7][];

                Actual[0] = new int[] { 10, 12 }; // 10
                Actual[1] = new int[] { 5, 8 }; // 5
                Actual[2] = new int[] { 1, 2 }; // 1
                Actual[3] = new int[] { 9, 2 }; // 2
                Actual[4] = new int[] { 1 }; // 1
                Actual[5] = new int[] { 45, 43 }; // 43
                Actual[6] = new int[] { 96, 57, 75 }; // 57

                int[][] ExpectedAsc = new int[7][];

                ExpectedAsc[0] = Actual[2];
                ExpectedAsc[1] = Actual[4];
                ExpectedAsc[2] = Actual[3];
                ExpectedAsc[3] = Actual[1];
                ExpectedAsc[4] = Actual[0];
                ExpectedAsc[5] = Actual[5];
                ExpectedAsc[6] = Actual[6];

                int[][] ExpectedDesc = new int[7][];

                ExpectedDesc[0] = Actual[6];
                ExpectedDesc[1] = Actual[5];
                ExpectedDesc[2] = Actual[0];
                ExpectedDesc[3] = Actual[1];
                ExpectedDesc[4] = Actual[3];
                ExpectedDesc[5] = Actual[2];
                ExpectedDesc[6] = Actual[4];

                yield return new TestCaseData(Actual, ExpectedAsc, ExpectedDesc);
            }
        }

        static IEnumerable<TestCaseData> RowsTotalTestCases
        {
            get
            {
                int[][] Actual = new int[7][];

                Actual[0] = new int[] { 5, -2 }; // 3
                Actual[1] = new int[] { 5, 8 }; // 13 
                Actual[2] = new int[] { 1, 2 }; // 3 
                Actual[3] = new int[] { 9, 2 }; // 11 
                Actual[4] = new int[] { 1 }; // 1 
                Actual[5] = new int[] { 45, 43 }; // 88 
                Actual[6] = new int[] { 96, 57, 75 }; // 228 

                int[][] ExpectedAsc = new int[7][];

                ExpectedAsc[0] = Actual[4];
                ExpectedAsc[1] = Actual[0];
                ExpectedAsc[2] = Actual[2];
                ExpectedAsc[3] = Actual[3];
                ExpectedAsc[4] = Actual[1];
                ExpectedAsc[5] = Actual[5];
                ExpectedAsc[6] = Actual[6];

                int[][] ExpectedDesc = new int[7][];

                ExpectedDesc[0] = Actual[6];
                ExpectedDesc[1] = Actual[5];
                ExpectedDesc[2] = Actual[1];
                ExpectedDesc[3] = Actual[3];
                ExpectedDesc[4] = Actual[0];
                ExpectedDesc[5] = Actual[2];
                ExpectedDesc[6] = Actual[4];

                yield return new TestCaseData(Actual, ExpectedAsc, ExpectedDesc);
            }
        }

        static string FormatArray(int[] array)
        {
            return string.Concat(array.Select(n => n.ToString() + " ").ToArray());
        }

        [Test]
        public void PermuteTests()
        {
            Random RandGen = new Random(DateTime.Now.Millisecond);

            for (int j = 0; j < 1000; j++)
            {
                int[] actual = new int[10];

                for (int i = 0; i < actual.Length; i++)
                {
                    actual[i] = i;
                }

                int[] perm = actual.OrderBy(x => RandGen.Next()).ToArray();

                int[] expected = (int[])perm.Clone();

                actual.Permute(perm);

                Assert.That(actual, Is.EqualTo(expected), "expected = {0}, actual = {1}", FormatArray(expected), FormatArray(actual));
            }
        }

        [TestCase(new int[] { -2, 5, 6, 14, 0, -7 }, ExpectedResult = 14)]
        [TestCase(new int[] { 2, -2 }, ExpectedResult = 2)]
        [TestCase(new int[] { 4, 5, 2, -3, 53, 26, 32, -563 }, ExpectedResult = 53)]
        public int AmateurMaxTests(int[] array)
        {
            return array.AmateurMax();
        }

        [TestCase(new int[] { -2, 5, 6, 14, 0, -7 }, ExpectedResult = -7)]
        [TestCase(new int[] { 2, -2 }, ExpectedResult = -2)]
        [TestCase(new int[] { 4, 5, 2, -3, 53, 26, 32, -563 }, ExpectedResult = -563)]
        public int AmateurMinTests(int[] array)
        {
            return array.AmateurMin();
        }

        [TestCase(new int[] { -2, 5, 6, 14, 0, -7 }, ExpectedResult = 16)]
        [TestCase(new int[] { 2, -2 }, ExpectedResult = 0)]
        [TestCase(new int[] { 4, 5, 2, -3, 53, 26, 32, -563 }, ExpectedResult = -444)]
        public int AmateurTotalTests(int[] array)
        {
            return array.AmateurTotal();
        }

        [TestCaseSource(nameof(RowsMaxTestCases))]
        public void BubbleSortByMaxTests(int[][] actualAsc, int[][] expectedAsc, int[][] expectedDesc)
        {
            int[][] actualDesc = (int[][])actualAsc.Clone();

            actualAsc.BubbleSortBy(arr => arr.AmateurMax());
            actualDesc.BubbleSortBy(arr => arr.AmateurMax(), true);

            Assert.That(actualAsc, Is.EqualTo(expectedAsc));
            Assert.That(actualDesc, Is.EqualTo(expectedDesc));
        }

        [TestCaseSource(nameof(RowsMinTestCases))]
        public void BubbleSortByMinTests(int[][] actualAsc, int[][] expectedAsc, int[][] expectedDesc)
        {
            int[][] actualDesc = (int[][])actualAsc.Clone();

            actualAsc.BubbleSortBy(arr => arr.AmateurMin());
            actualDesc.BubbleSortBy(arr => arr.AmateurMin(), true);

            Assert.That(actualAsc, Is.EqualTo(expectedAsc));
            Assert.That(actualDesc, Is.EqualTo(expectedDesc));
        }

        [TestCaseSource(nameof(RowsTotalTestCases))]
        public void BubbleSortByTotalTests(int[][] actualAsc, int[][] expectedAsc, int[][] expectedDesc)
        {
            int[][] actualDesc = (int[][])actualAsc.Clone();

            actualAsc.BubbleSortBy(arr => arr.AmateurTotal());
            actualDesc.BubbleSortBy(arr => arr.AmateurTotal(), true);

            Assert.That(actualAsc, Is.EqualTo(expectedAsc));
            Assert.That(actualDesc, Is.EqualTo(expectedDesc));
        }
    }
}
