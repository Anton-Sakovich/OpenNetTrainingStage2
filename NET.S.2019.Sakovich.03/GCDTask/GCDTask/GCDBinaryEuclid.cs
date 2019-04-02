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

            // A this point x and y are arbitrary.

            /*
             * An important note:
             * For an even number greater than zero, right shifting it until it
             * becomes odd will never result in zero. For this reason, we have to
             * explicitly exclude initial zeros.
             */

            if (x == 0)
            {
                return y;
            }

            if (y == 0)
            {
                return x;
            }

            // A this point both x and y are greater than zero.
            // Except of that, they are arbitrary.

            // If both of them are even, make at least one of them odd.
            // While they both are even, memorize shifts.
            while ((x & 1 | y & 1) == 0)
            {
                x >>= 1;
                y >>= 1;
                Shift++;
            }

            // At this point at least one number in the pair is odd.

            // Make the other one odd.
            if ((x & 1) == 0)
            {
                while((x & 1) == 0)
                {
                    x >>= 1;
                }
            }
            else
            {
                while ((y & 1) == 0)
                {
                    y >>= 1;
                }
            }

            while (true)
            {
                // A this point both of them are odd and greater than zero (read the note above).
                if (x > y)
                {
                    x -= y;

                    // Only here and only x can become zero, which is the termination condition.
                    if (x == 0)
                    {
                        return y << Shift;
                    }

                    // Here, y is still odd and x is even greater than zero. Make x odd.
                    while ((x & 1) == 0)
                    {
                        x >>= 1;
                    }
                }
                else
                {
                    y -= x;

                    // Only here and only y can become zero, which is the termination condition.
                    if (y == 0)
                    {
                        return x << Shift;
                    }

                    // Here, x is still odd and y is even greater than zero. Make y odd.
                    while ((y & 1) == 0)
                    {
                        y >>= 1;
                    }
                }
            }
        }
    }
}
