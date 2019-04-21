using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace GenBinSearchTask.Tests
{
    [TestFixture]
    public class GenBinSearchTests
    {
        public static IEnumerable<TestCaseData> TestData
        {
            get
            {
                yield return new TestCaseData(
                    new int[] { 2 },
                    new int[] { 1, 2, 3 },
                    new int[] { ~0, 0, ~0 });
                yield return new TestCaseData(
                    new int[] { 2, 5 },
                    new int[] { 1, 2, 3, 4, 5, 6 },
                    new int[] { ~0, 0, ~1, ~1, 1, ~1 });
                yield return new TestCaseData(
                    new int[] { 2, 5, 8 },
                    new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 },
                    new int[] { ~0, 0, ~1, ~1, 1, ~2, ~2, 2, ~2 });
                yield return new TestCaseData(
                    new int[] { 2, 5, 8, 11 },
                    new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 },
                    new int[] { ~0, 0, ~1, ~1, 1, ~2, ~2, 2, ~3, ~3, 3, ~3 });
                yield return new TestCaseData(
                    new int[] { 2, 5, 8, 11, 14 },
                    new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 },
                    new int[] { ~0, 0, ~1, ~1, 1, ~2, ~2, 2, ~3, ~3, 3, ~4, ~4, 4, ~4 });
            }
        }

        [TestCaseSource(nameof(TestData))]
        public void BinarySearch_NonEmptyList_CorrectResult_Test(int[] data, int[] values, int[] expected)
        {
            int[] actual = new int[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                actual[i] = GenBinSearch.BinarySearch(data, values[i], (x, y) => x - y);
            }

            for (int i = 0; i < values.Length; i++)
            {
                Assert.That(actual[i], Is.EqualTo(expected[i]), "Failed for {0} in {1}.", values[i], ArrayToString(data));
            }
        }

        [Test]
        public void BinarySearch_EmptyList_ArgumentException_Test()
        {
            Assert.That(() => GenBinSearch.BinarySearch(new int[] { }, 0, (x, y) => x - y), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void BinarySearch_NullData_ArgumentNullException_Test()
        {
            Assert.That(() => GenBinSearch.BinarySearch(null, 0, (x, y) => x - y), Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void BinarySearch_NullComparingDelegate_ArgumentNullException_Test()
        {
            Assert.That(() => GenBinSearch.BinarySearch(new int[] { 1 }, 0, null), Throws.TypeOf<ArgumentNullException>());
        }

        private static string ArrayToString<T>(T[] array)
        {
            return string.Concat(array.Select(n => n.ToString() + " ").ToArray());
        }
    }
}
