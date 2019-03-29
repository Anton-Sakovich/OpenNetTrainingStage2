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
        // are guaranted to be non-negative. Do not check them inside an
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
            int Result;

            SWatch?.Start();
            Result = GetGCDBase(x > 0 ? x : -x, y > 0 ? y : -y);
            SWatch?.Stop();

            return Result;
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

            int gcd = GetGCDBase(nums[0], nums[1]);
            for(int i = 2; i < nums.Length; i++)
            {
                gcd = GetGCDBase(gcd, nums[i] > 0 ? nums[i] : -nums[i]);
            }

            SWatch?.Stop();

            return gcd;
        }
    }
}
