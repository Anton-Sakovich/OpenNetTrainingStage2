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

            int Mask = (-1 << lowBitPos) ^ (-2 << highBitPos);
            int ShiftedIn = numberIn << lowBitPos;
            int Difference = numberSource ^ ShiftedIn;
            int MaskedDifference = Difference & Mask;
            return numberSource ^ MaskedDifference;
        }
    }
}
