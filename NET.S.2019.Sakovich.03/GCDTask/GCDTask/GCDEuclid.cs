using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCDTask
{
    public class GCDEuclid : GCDBase
    {
        protected override int GetGCDBase(int min, int max)
        {
            /*
             * The idea of the algorithm is that when there is max and min (max >= min),
             * then max % min is smaller than both of them. Therefore, we always know
             * which number is greater and which one is smaller.
             */

            if (max < min)
            {
                if (max == 0)
                    return min;
                min = min % max;
            }

            while(true)
            {
                // Here always max >= min
                if (min == 0)
                    return max;
                max = max % min;

                // Here always max < min
                if (max == 0)
                    return min;
                min = min % max;
            }
        }
    }
}
