using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsertNumberTask.Tests.Common
{
    public class InsertNumberTestCase
    {
        public int MergeInto;
        public int MergeFrom;
        public int LowBitPos;
        public int HighBitPos;
        public int Result;
        public string Message = "";

        public static IEnumerable<InsertNumberTestCase> ExamplesFromTheTask
        {
            get
            {
                yield return new InsertNumberTestCase { MergeInto = 15, MergeFrom = 15, LowBitPos = 0, HighBitPos = 0, Result = 15 };
                yield return new InsertNumberTestCase { MergeInto = 8, MergeFrom = 15, LowBitPos = 0, HighBitPos = 0, Result = 9 };
                yield return new InsertNumberTestCase { MergeInto = 8, MergeFrom = 15, LowBitPos = 3, HighBitPos = 8, Result = 120 };
            }
        }

        public static IEnumerable<InsertNumberTestCase> FourBitsTestCases
        {
            get
            {
                yield return new InsertNumberTestCase { MergeInto = unchecked((int)0xAAAAAAAA), MergeFrom = 0x3, LowBitPos = 0, HighBitPos = 3, Result = unchecked((int)0xAAAAAAA3), Message = "0 half byte failed." };
                yield return new InsertNumberTestCase { MergeInto = unchecked((int)0xAAAAAAAA), MergeFrom = 0x3, LowBitPos = 4, HighBitPos = 7, Result = unchecked((int)0xAAAAAA3A), Message = "1 half byte failed." };
                yield return new InsertNumberTestCase { MergeInto = unchecked((int)0xAAAAAAAA), MergeFrom = 0x3, LowBitPos = 8, HighBitPos = 11, Result = unchecked((int)0xAAAAA3AA), Message = "2 half byte failed." };
                yield return new InsertNumberTestCase { MergeInto = unchecked((int)0xAAAAAAAA), MergeFrom = 0x3, LowBitPos = 12, HighBitPos = 15, Result = unchecked((int)0xAAAA3AAA), Message = "3 half byte failed." };
                yield return new InsertNumberTestCase { MergeInto = unchecked((int)0xAAAAAAAA), MergeFrom = 0x3, LowBitPos = 16, HighBitPos = 19, Result = unchecked((int)0xAAA3AAAA), Message = "4 half byte failed." };
                yield return new InsertNumberTestCase { MergeInto = unchecked((int)0xAAAAAAAA), MergeFrom = 0x3, LowBitPos = 20, HighBitPos = 23, Result = unchecked((int)0xAA3AAAAA), Message = "5 half byte failed." };
                yield return new InsertNumberTestCase { MergeInto = unchecked((int)0xAAAAAAAA), MergeFrom = 0x3, LowBitPos = 24, HighBitPos = 27, Result = unchecked((int)0xA3AAAAAA), Message = "7 half byte failed." };
                yield return new InsertNumberTestCase { MergeInto = unchecked((int)0xAAAAAAAA), MergeFrom = 0x3, LowBitPos = 28, HighBitPos = 31, Result = unchecked((int)0x3AAAAAAA), Message = "7 half byte failed." };
            }
        }

        public static IEnumerable<InsertNumberTestCase> SingleBitTestCases
        {
            get
            {
                yield return new InsertNumberTestCase { MergeInto = unchecked((int)0xAAAAAAAA), MergeFrom = 0x1, LowBitPos = 2, HighBitPos = 2, Result = unchecked((int)0xAAAAAAAE), Message = "0 half byte failed." };
                yield return new InsertNumberTestCase { MergeInto = unchecked((int)0xAAAAAAAA), MergeFrom = 0x1, LowBitPos = 6, HighBitPos = 6, Result = unchecked((int)0xAAAAAAEA), Message = "1 half byte failed." };
                yield return new InsertNumberTestCase { MergeInto = unchecked((int)0xAAAAAAAA), MergeFrom = 0x1, LowBitPos = 10, HighBitPos = 10, Result = unchecked((int)0xAAAAAEAA), Message = "2 half byte failed." };
                yield return new InsertNumberTestCase { MergeInto = unchecked((int)0xAAAAAAAA), MergeFrom = 0x1, LowBitPos = 14, HighBitPos = 14, Result = unchecked((int)0xAAAAEAAA), Message = "3 half byte failed." };
                yield return new InsertNumberTestCase { MergeInto = unchecked((int)0xAAAAAAAA), MergeFrom = 0x1, LowBitPos = 18, HighBitPos = 18, Result = unchecked((int)0xAAAEAAAA), Message = "4 half byte failed." };
                yield return new InsertNumberTestCase { MergeInto = unchecked((int)0xAAAAAAAA), MergeFrom = 0x1, LowBitPos = 22, HighBitPos = 22, Result = unchecked((int)0xAAEAAAAA), Message = "5 half byte failed." };
                yield return new InsertNumberTestCase { MergeInto = unchecked((int)0xAAAAAAAA), MergeFrom = 0x1, LowBitPos = 26, HighBitPos = 26, Result = unchecked((int)0xAEAAAAAA), Message = "6 half byte failed." };
                yield return new InsertNumberTestCase { MergeInto = unchecked((int)0xAAAAAAAA), MergeFrom = 0x1, LowBitPos = 30, HighBitPos = 30, Result = unchecked((int)0xEAAAAAAA), Message = "7 half byte failed." };
            }
        }
    }
}
