using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace GCDTask
{
    /// <summary>
    /// A base class for computing GCD of a sequence of numbers.
    /// </summary>
    public abstract class GCDBase
    {
        // A simple attached Stopwatch makes this class not thread safe,
        // don't have time to make it thread safe.
        /// <summary>
        /// An instance of Stopwatch used to measure the time taken to
        /// compute GCD.
        /// </summary>
        public Stopwatch SWatch;

        // An internal method for computing GCD. Arguments to this method
        // are guaranted to be positive. Do not check them inside an
        // implementation.
        protected abstract int GetGCDBase(int x, int y);

        // This method is supposed to be high-level, don't use it as a low-level
        // one, use GetGCDBase instead.
        /// <summary>
        /// Returns a GCD of x and y.
        /// </summary>
        /// <param name="x">The first integer.</param>
        /// <param name="y">The second integer.</param>
        /// <returns>The GCD of input numbers.</returns>
        public int GCD(int x, int y)
        {
            if(x == 0)
            {
                return y > 0 ? y : -y;
            }
            else if(y == 0)
            {
                return x > 0 ? x : -x;
            }
            else
            {
                x = x > 0 ? x : -x;
                y = y > 0 ? y : -y;

                int Result;

                SWatch?.Start();
                Result = GetGCDBase(x, y);
                SWatch?.Stop();

                return Result;
            }
        }

        /// <summary>
        /// Returns a GCD of a sequence of input integers.
        /// </summary>
        /// <param name="nums">A sequence of integers. Must contain at least two integers.</param>
        /// <returns>The GCD of input integers.</returns>
        public int GCD(params int[] nums)
        {
            if (nums == null)
                throw new ArgumentNullException(nameof(nums), "Input array of arguments cannot be null.");

            if (nums.Length < 2)
                throw new ArgumentException("There must be at least two input numbers specified.");

            SWatch?.Start();

            // Move all non zeros to the beginning of the array. Length is the length
            // of the non-zero part of the resulting array.
            int Length = ShiftNonZeros(nums);

            // All are zeros
            if (Length == 0)
                return 0;

            // Only one non-zero
            if (Length == 1)
                return nums[0];

            // At least two non-zeros. Check the signs because we promised GetGCDBase
            // to pass only positive numbers
            for (int i = 0; i < Length; i++)
            {
                if (nums[i] < 0)
                    nums[i] = -nums[i];
            }

            // Compute GCD
            int gcd = GetGCDBase(nums[0], nums[1]);
            for(int i = 2; i < Length; i++)
            {
                gcd = GetGCDBase(gcd, nums[i]);
            }

            SWatch?.Stop();

            return gcd;
        }

        private int ShiftNonZeros(int[] array)
        {
            int ZeroPos = 0;

            for (ZeroPos = 0; ZeroPos < array.Length; ZeroPos++)
            {
                if (array[ZeroPos] == 0)
                    break;
            }

            for(int i = ZeroPos + 1; i < array.Length; i++)
            {
                if (array[i] != 0)
                    array[ZeroPos++] = array[i];
            }

            return ZeroPos;
        }
    }
}
