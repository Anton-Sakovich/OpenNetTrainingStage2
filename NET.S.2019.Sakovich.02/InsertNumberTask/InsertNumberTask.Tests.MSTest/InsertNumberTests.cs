using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InsertNumberTask.Tests.Common;

namespace InsertNumberTask.Tests.MSTests
{
    [TestClass]
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

        [TestMethod]
        public void TestExamplesFromTheTask()
        {
            CheckTestCases(InsertNumberTestCase.ExamplesFromTheTask);
        }

        [TestMethod]
        public void InsertFourBits()
        {
            CheckTestCases(InsertNumberTestCase.FourBitsTestCases);
        }

        [TestMethod]
        public void InsertSingleBit()
        {
            CheckTestCases(InsertNumberTestCase.SingleBitTestCases);
        }

        [TestMethod]
        public void BitPositions_OutOfRange_OutOfRangeExceptionThrown()
        {
            InsertNumberTask TaskClass = new InsertNumberTask();

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => TaskClass.InsertNumber(1, 1, -1, 20), "Lower bit posiion is less than 0.");
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => TaskClass.InsertNumber(1, 1, 0, 32), "Higher bit posiion is more than 31.");
        }

        [TestMethod]
        public void BitPositions_WrongOrder_ArgumentExceptionThrown()
        {
            InsertNumberTask TaskClass = new InsertNumberTask();

            Assert.ThrowsException<ArgumentException>(() => TaskClass.InsertNumber(1, 1, 20, 1), "Lower bit position is greater than higher bit position.");
        }
    }
}

