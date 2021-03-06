﻿using System;
using System.Collections.Generic;
using NUnit.Framework;
using FilterDigitTask;

namespace FilterDigit.Tests
{
    [TestFixture]
    public class DigitFilterTests
    {
        static IEnumerable<TestCaseData> NormalTestCases
        {
            get
            {
                yield return new TestCaseData(new int[] { 1, 2, 3, 22, 12, 21, 32, 101, 115, 223, 0, 1 }, 1, false)
                    .Returns(new int[] { 1, 12, 21, 101, 115, 1 })
                    .SetDescription("Filtering non empty array on one (present, retain duplicates).");
                yield return new TestCaseData(new int[] { 1, 2, 3, 22, 12, 21, 32, 101, 115, 223, 0, 1 }, 1, true)
                    .Returns(new int[] { 1, 12, 21, 101, 115 })
                    .SetDescription("Filtering non empty array on one (present, delete duplicates).");
                yield return new TestCaseData(new int[] { 2, 3, 22, 32, 223, 0 }, 1, false)
                    .Returns(new int[] { })
                    .SetDescription("Filtering non empty array on one (absent, retain duplicates).");
                yield return new TestCaseData(new int[] { 2, 3, 22, 32, 223, 0 }, 1, true)
                    .Returns(new int[] { })
                    .SetDescription("Filtering non empty array on one (absent, delete duplicates).");
                yield return new TestCaseData(new int[] { 1, 0, 11, 10, 235, 105, 0 }, 0, false)
                    .Returns(new int[] { 0, 10, 105, 0 })
                    .SetDescription("Filtering non empty array on zero (present, retain duplicates).");
                yield return new TestCaseData(new int[] { 1, 0, 11, 10, 235, 105, 0 }, 0, true)
                    .Returns(new int[] { 0, 10, 105 })
                    .SetDescription("Filtering non empty array on zero (present, delete duplicates).");
                yield return new TestCaseData(new int[] { 1, 11, 235 }, 0, false)
                    .Returns(new int[] { })
                    .SetDescription("Filtering non empty array on zero (absent, retain duplicates).");
                yield return new TestCaseData(new int[] { 1, 11, 235 }, 0, true)
                    .Returns(new int[] { })
                    .SetDescription("Filtering non empty array on zero (absent, delete duplicates).");
            }
        }

        [TestCaseSource(nameof(NormalTestCases))]
        public int[] NonEmptyArraysTests(int[] nums, int digit, bool deleteDuplicates)
        {
            return new DigitFilter().Filter(nums, digit, deleteDuplicates);
        }

        [Test]
        public void EmptyArray_ResultsInEmptyArray_ForAnyDigit()
        {
            DigitFilter Filter1 = new DigitFilter();

            for (int i = 0; i < 10; i++)
                Assert.That(Filter1.Filter(new int[] { }, i), Is.EquivalentTo(new int[] { }));
        }

        [Test]
        public void DigitsOutOfRange_OutOfRangeExceptionThrown()
        {
            DigitFilter Filter1 = new DigitFilter();

            int[] NotDigits = new int[] { -1, 11 };
            int[][] TestNums = new int[2][];
            TestNums[0] = new int[] { };
            TestNums[1] = new int[] { 1, 2 };

            foreach (int nd in NotDigits)
                foreach (int[] nums in TestNums)
                    Assert.That(() => Filter1.Filter(nums, nd), Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void NullArray_ArgumentNullExceptionThrown()
        {
            DigitFilter Filter1 = new DigitFilter();

            Assert.That(() => Filter1.Filter(null, 1), Throws.TypeOf<ArgumentNullException>());
        }
    }
}
