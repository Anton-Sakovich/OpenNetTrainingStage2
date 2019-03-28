using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCDTask
{
    public class GCDBinaryEuclid : GCDBase
    {
        protected override int GetGCDBase(int x, int y)
        {
            int Shift = 0;

            while ((x > 0) && (y > 0))
            {
                switch (((x & 1) << 1) | y & 1)
                {
                    case 0:
                        Shift++;
                        x >>= 1;
                        y >>= 1;
                        break;
                    case 1:
                        x >>= 1;
                        break;
                    case 2:
                        y >>= 1;
                        break;
                    case 3:
                        if(x > y)
                        {
                            x = (x - y) >> 1;
                        }
                        else
                        {
                            y = (y - x) >> 1;
                        }
                        break;
                }
            }

            return (x > 0 ? x : y) << Shift;
        }
    }
}
