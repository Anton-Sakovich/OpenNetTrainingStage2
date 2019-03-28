using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace GCDTask
{
    public abstract class GCDBase
    {
        // A simple attached Stopwatch makes this class not thread safe,
        // don't have time to make it thread safe.
        public Stopwatch SWatch;

        protected abstract int GetGCDBase(int x, int y);

        public int GCD(int x)
        {
            return x < 0 ? -x : x;
        }

        public int GCD(int x, int y)
        {
            if (x == 0 || x == y)
                return GCD(y);
            else
            {
                x = GCD(x);
                y = GCD(y);

                int Result;

                SWatch?.Start();
                Result = GetGCDBase(x, y);
                SWatch?.Stop();

                return Result;
            }
        }

        public int GCD(params int[] nums)
        {
            if (nums == null)
                throw new ArgumentNullException(nameof(nums), "Input array of arguments cannot be null.");

            if (nums.Length == 0)
                throw new ArgumentException("There must be at least one input number specified.");


            int Length = nums.Length;
            int i;
            SWatch?.Start();
            while(Length > 1)
            {
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
