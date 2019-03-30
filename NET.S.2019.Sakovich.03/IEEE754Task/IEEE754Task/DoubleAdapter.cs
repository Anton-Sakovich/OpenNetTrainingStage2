using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEEE754Task
{
    public class DoubleAdapter : FPointAdapter<double>
    {
        public DoubleAdapter() : base(new DoubleAnatomy()) { }

        protected override double FlipSign(double num) => -num;

        protected override ulong ProjectIntegerPart(double num) => (ulong)num;

        protected override double LeftShift(double num, int steps)
        {
            for (int i = 0; i < steps; i++)
                num /= 2D;

            return num;
        }

        protected override double RightShift(double num, int steps)
        {
            for (int i = 0; i < steps; i++)
                num *= 2D;

            return num;
        }

        // https://stackoverflow.com/questions/4739795/how-can-i-test-for-negative-zero
        protected override bool IsNegativeZero(double num)
        {
            return num == 0.0 && double.IsNegativeInfinity(1.0 / num);
        }
    }
}
