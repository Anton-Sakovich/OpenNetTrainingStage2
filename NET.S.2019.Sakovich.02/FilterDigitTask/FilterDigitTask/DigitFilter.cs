using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterDigitTask
{
    public class DigitFilter
    {
        public int[] Filter(int[] nums, int digit, bool deleteDuplicates = false)
        {
            if (digit < 0 || digit > 9)
                throw new ArgumentOutOfRangeException(nameof(digit), digit, "Filtering digit must be a valid decimal digit.");

            if (nums == null)
                throw new ArgumentNullException(nameof(nums), "Input array of digits must not be null.");

            ICollection<int> FilteredNums;
            if(deleteDuplicates)
            {
                FilteredNums = new HashSet<int>();
            }
            else
            {
                FilteredNums = new List<int>();
            }

            foreach(int num in nums)
            {
                if (HasDigit(num, digit))
                    FilteredNums.Add(num);
            }

            return FilteredNums.ToArray();
        }

        bool HasDigit(int num, int digit)
        {
            if(num < 0)
            {
                num = -num;
            }

            do
            {
                if ((num % 10) == digit)
                    return true;
                num = num / 10;
            } while (num > 0);

            return false;
        }
    }
}
