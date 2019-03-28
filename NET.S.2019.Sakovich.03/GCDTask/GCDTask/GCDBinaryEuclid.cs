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
            int Multiplier = 1;

            while ((x | y) > 0)
            {
                switch (((x << 1) & 2) | y & 1)
                {
                    case 0:
                        Multiplier <<= 1;
                        x >>= 1;
                        y >>= 1;
                        break;
                    case 1:
                        y >>= 1;
                        break;
                    case 2:
                        x >>= 1;
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

            return (x > 0 ? x : y) << Multiplier;
        }
    }
}
