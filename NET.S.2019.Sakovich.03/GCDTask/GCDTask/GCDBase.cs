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
            if (x == 0 || x == y)
                return y > 0 ? y : -y;
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

            // For an array of input integers, their GCD is computed
            // via binary ascending, i.e.
            // GCD(a, b, c, d, e) = GCD(GCD(GCD(a, b), GCD(c, d)), e)

            // It is implementted by taking successive pairs and computing
            // their GCD on each step.
            // For example, for {a, b, c, d, e} the first step is
            // a -> GCD(a, b)
            // b -> GCD(c, d)
            // c -> e
            // resuling in
            // {GCD(a, b), GCD(c, d), e, d (don't care), e (don't care)}

            // The length of the subarray with data
            int Length = nums.Length;
            // An index inside the subarray
            int i;

            SWatch?.Start();
            while(Length > 1)
            {
                // Reset index on each step
                i = 0;

                // Loop the first half of the old array.
                for (; i < (Length >> 1); i++)
                {
                    nums[i] = GetGCDBase(nums[i << 1], nums[(i << 1) + 1]);
                }

                // If in the old array there is still an unvisited element,
                // then take it and change the length appropriately
                if ((i << 1) < Length)
                {
                    nums[i] = nums[i << 1];
                    Length = (Length >> 1) + 1;
                }
                else
                {
                    Length = Length >> 1;
                }
            }
            SWatch?.Stop();

            return nums[0];
        }
    }
}
