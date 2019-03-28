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
            if(min > max)
            {
                int temp = min;
                min = max;
                max = temp;
            }

            while(true)
            {
                max = max % min;
                if (max == 0)
                    return min;

                min = min % max;
                if (min == 0)
                    return max;
            }
        }
    }
}
