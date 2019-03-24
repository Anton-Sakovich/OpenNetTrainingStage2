using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InsertNumberTask.MSTests
{
    [TestClass]
    public class InsertNumberTaskTests
    {
        [TestMethod]
        public void TestExamplesFromTheTask()
        {
            InsertNumberTask TaskClass = new InsertNumberTask();

            Assert.AreEqual(15, TaskClass.InsertNumber(15, 15, 0, 0));
            Assert.AreEqual(9, TaskClass.InsertNumber(8, 15, 0, 0));
            Assert.AreEqual(120, TaskClass.InsertNumber(8, 15, 3, 8));
        }

        [TestMethod]
        public void InsertFourBits()
        {
            InsertNumberTask TaskClass = new InsertNumberTask();

            int MergeInto = unchecked((int)0xAAAAAAAA); // 1010 1010 1010 1010 1010 1010 1010 1010
            int MergeFrom = 0x3;                        // 0000 0000 0000 0000 0000 0000 0000 0011

            Assert.AreEqual(unchecked((int)0xAAAAAAA3), TaskClass.InsertNumber(MergeInto, MergeFrom, 0, 3), "Halfbyte 0 failed.");
            Assert.AreEqual(unchecked((int)0xAAAAAA3A), TaskClass.InsertNumber(MergeInto, MergeFrom, 4, 7), "Halfbyte 1 failed.");
            Assert.AreEqual(unchecked((int)0xAAAAA3AA), TaskClass.InsertNumber(MergeInto, MergeFrom, 8, 11), "Halfbyte 2 failed.");
            Assert.AreEqual(unchecked((int)0xAAAA3AAA), TaskClass.InsertNumber(MergeInto, MergeFrom, 12, 15), "Halfbyte 3 failed.");
            Assert.AreEqual(unchecked((int)0xAAA3AAAA), TaskClass.InsertNumber(MergeInto, MergeFrom, 16, 19), "Halfbyte 4 failed.");
            Assert.AreEqual(unchecked((int)0xAA3AAAAA), TaskClass.InsertNumber(MergeInto, MergeFrom, 20, 23), "Halfbyte 5 failed.");
            Assert.AreEqual(unchecked((int)0xA3AAAAAA), TaskClass.InsertNumber(MergeInto, MergeFrom, 24, 27), "Halfbyte 6 failed.");
            Assert.AreEqual(unchecked((int)0x3AAAAAAA), TaskClass.InsertNumber(MergeInto, MergeFrom, 28, 31), "Halfbyte 7 failed.");
        }

        [TestMethod]
        public void InsertSingleBit()
        {
            InsertNumberTask TaskClass = new InsertNumberTask();

            int MergeInto = unchecked((int)0xAAAAAAAA); // 1010 1010 1010 1010 1010 1010 1010 1010
            int MergeFrom = 0x1;                        // 0000 0000 0000 0000 0000 0000 0000 0001

            // 1010 1010 1010 1010 1010 1010 1010 1110
            Assert.AreEqual(unchecked((int)0xAAAAAAAE), TaskClass.InsertNumber(MergeInto, MergeFrom, 2, 2), "Halfbyte 0 failed.");
            // 1010 1010 1010 1010 1010 1010 1110 1010
            Assert.AreEqual(unchecked((int)0xAAAAAAEA), TaskClass.InsertNumber(MergeInto, MergeFrom, 6, 6), "Halfbyte 1 failed.");
            Assert.AreEqual(unchecked((int)0xAAAAAEAA), TaskClass.InsertNumber(MergeInto, MergeFrom, 10, 10), "Halfbyte 2 failed.");
            Assert.AreEqual(unchecked((int)0xAAAAEAAA), TaskClass.InsertNumber(MergeInto, MergeFrom, 14, 14), "Halfbyte 3 failed.");
            Assert.AreEqual(unchecked((int)0xAAAEAAAA), TaskClass.InsertNumber(MergeInto, MergeFrom, 18, 18), "Halfbyte 4 failed.");
            Assert.AreEqual(unchecked((int)0xAAEAAAAA), TaskClass.InsertNumber(MergeInto, MergeFrom, 22, 22), "Halfbyte 5 failed.");
            Assert.AreEqual(unchecked((int)0xAEAAAAAA), TaskClass.InsertNumber(MergeInto, MergeFrom, 26, 26), "Halfbyte 6 failed.");
            Assert.AreEqual(unchecked((int)0xEAAAAAAA), TaskClass.InsertNumber(MergeInto, MergeFrom, 30, 30), "Halfbyte 7 failed.");
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
