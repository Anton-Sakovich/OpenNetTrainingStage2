using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsertNumberTask
{
    public class InsertNumberTask
    {
        public int InsertNumber(int numberSource, int numberIn, int lowBitPos, int highBitPos)
        {
            if(lowBitPos < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(lowBitPos), "The position of the lowest bit must be greater than or equal to 0.");
            }

            if(highBitPos > 31)
            {
                throw new ArgumentOutOfRangeException(nameof(highBitPos), "The position of the highest bit must be less than or equal to 31.");
            }

            if(lowBitPos > highBitPos)
            {
                throw new ArgumentException("The position of the lowest bit must be less than or equal to that of the highest bit.");
            }

            // 1. Shift numberIn so that its first (highBitPos - lowBitPos + 1) bits occupy positions from lowBitPos to highBitPos
            // numberInShifted = numberIn << lowBitPos

            // 2. Calculate the difference between numberSource and the number thus obtained
            // Difference = numberSource ^ numberInShifted

            // 3. Build a mask which has 1 at bits from lowBitPos to highBitPos and 0 everywhere else
            // Mask = (-1 << lowBitPos) ^ (-2 << highBitPos)

            // 4. Apply this mask to the Difference
            // FilteredDifference = Difference & Mask

            // 5. Apply the filtered diffeence to numberSource
            // return numberSource ^ FilteredDifference

            return numberSource ^ ((numberSource ^ (numberIn << lowBitPos)) & ((-1 << lowBitPos) ^ (-2 << highBitPos)));
        }
    }
}
