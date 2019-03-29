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
            if(max < min)
            {
                if (max == 0)
                    return min;
                min = min % max;
            }

            while(true)
            {
                if (min == 0)
                    return max;
                max = max % min;

                if (max == 0)
                    return min;
                min = min % max;
            }
        }
    }
}
