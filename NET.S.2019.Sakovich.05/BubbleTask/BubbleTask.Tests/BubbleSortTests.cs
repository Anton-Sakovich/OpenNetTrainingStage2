using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace BubbleTask.Tests
{
    [TestFixture]
    public class BubbleSortTests
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

        void Sort_with_AnyComparer(int[][] actual, int[][] expectedAsc, int[][] expectedDesc, IComparer<int[]> comp)
        {
            BubbleSort SortEngine = new BubbleSort();

            int[][] actualCopy = (int[][])actual.Clone();

            SortEngine.Sort(actual, comp);
            SortEngine.Sort(actualCopy, comp, true);

            Assert.That(actual, Is.EqualTo(expectedAsc));
            Assert.That(actualCopy, Is.EqualTo(expectedDesc));
        }

        [TestCaseSource(nameof(RowsMaxTestCases))]
        public void Sort_with_MaxComparer(int[][] actual, int[][] expectedAsc, int[][] expectedDesc)
        {
            Sort_with_AnyComparer(actual, expectedAsc, expectedDesc, new RowsMaxComparer());
        }

        [TestCaseSource(nameof(RowsMinTestCases))]
        public void Sort_with_MixComparer(int[][] actual, int[][] expectedAsc, int[][] expectedDesc)
        {
            Sort_with_AnyComparer(actual, expectedAsc, expectedDesc, new RowsMinComparer());
        }
    }
}
