using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NthRootTask
{
    public class NewtonNthRoot
    {
        public readonly NthRootOptions Options;

        public NewtonNthRoot(NthRootOptions opts)
        {
            Options = opts;
        }

        public bool AreEqual(double y2, double y1)
        {
            return Math.Abs(y2 - y1) < (Math.Abs(y2) * Options.RelativeErrorGoal + Options.AbsoluteErrorGoal);
        }

        public double FindRoot(double y, int n)
        {
            if (y < 0.0)
                throw new ArgumentOutOfRangeException(nameof(y), "Value of y must be non-negative.");

            if(n < 1)
                throw new ArgumentOutOfRangeException(nameof(n), "Value of n must be greater than zero.");

            if (n == 1)
                return y;

            if (AreEqual(y, 0))
                return y;

            double x = y;
            double temp;
            int StepsCount = 0;

            while (!AreEqual(IntegerPower(x, n), y))
            {
                temp = IntegerPower(x, n - 1);
                x = x - (x * temp - y) / (temp * n);

                if (++StepsCount == Options.MaxStepsCount)
                    throw new MaxStepsReachedException();
            }

            return x;
        }

        double IntegerPower(double x, int n)
        {
            double y = x;
            for(int i = 0; i < (n - 1); i++)
            {
                y *= x;
            }
            return y;
        }
    }
}
