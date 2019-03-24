using System;
using System.Collections.Generic;
using InsertNumberTask.Tests.Common;
using NUnit.Framework;

namespace InsertNumberTask.Tests.NUnit
{
    [TestFixture]
    public class InsertNumberTests
    {
        private void CheckTestCases(IEnumerable<InsertNumberTestCase> cases)
        {
            InsertNumberTask TaskClass = new InsertNumberTask();

            foreach (InsertNumberTestCase input in cases)
            {
                Assert.AreEqual(input.Result, TaskClass.InsertNumber(input.MergeInto, input.MergeFrom, input.LowBitPos, input.HighBitPos), input.Message);
            }
        }

        [Test]
        public void TestExamplesFromTheTask()
        {
            CheckTestCases(InsertNumberTestCase.ExamplesFromTheTask);
        }

        [Test]
        public void InsertFourBits()
        {
            CheckTestCases(InsertNumberTestCase.FourBitsTestCases);
        }

        [Test]
        public void InsertSingleBit()
        {
            CheckTestCases(InsertNumberTestCase.SingleBitTestCases);
        }

        [Test]
        public void BitPositions_OutOfRange_OutOfRangeExceptionThrown()
        {
            InsertNumberTask TaskClass = new InsertNumberTask();

            Assert.Throws<ArgumentOutOfRangeException>(() => TaskClass.InsertNumber(1, 1, -1, 20), "Lower bit posiion is less than 0.");
            Assert.Throws<ArgumentOutOfRangeException>(() => TaskClass.InsertNumber(1, 1, 0, 32), "Higher bit posiion is more than 31.");
        }

        [Test]
        public void BitPositions_WrongOrder_ArgumentExceptionThrown()
        {
            InsertNumberTask TaskClass = new InsertNumberTask();

            Assert.Throws<ArgumentException>(() => TaskClass.InsertNumber(1, 1, 20, 1), "Lower bit position is greater than higher bit position.");
        }
    }
}
