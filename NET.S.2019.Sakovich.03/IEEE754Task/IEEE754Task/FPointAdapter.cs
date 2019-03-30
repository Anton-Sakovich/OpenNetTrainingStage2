using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEEE754Task
{
    public abstract class FPointAdapter<T> where T : IComparable<T>
    {
        public readonly IFPointAnatomy<T> Anatomy;

        public FPointAdapter(IFPointAnatomy<T> anat)
        {
            Anatomy = anat;
        }

        protected abstract T LeftShift(T num, int steps);

        protected abstract T RightShift(T num, int steps);

        protected abstract ulong ProjectIntegerPart(T num);

        protected abstract T FlipSign(T num);

        public bool Destruct(T num, out ulong exponent, out ulong mantissa)
        {
            bool IsNegativeCached = num.CompareTo(Anatomy.Zero) < 0;

            if (IsNegativeCached)
                num = FlipSign(num);

            if(num.CompareTo(Anatomy.MinNormalNumber) < 0)
            {
                exponent = 0;

                mantissa = ProjectIntegerPart(RightShift(num, Anatomy.ExponentShift + Anatomy.MantissaLength - 1));

                return IsNegativeCached;
            }

            ulong MantissaMask = ~(ulong.MaxValue << Anatomy.MantissaLength);
            exponent = (ulong)Anatomy.ExponentShift + (ulong)Anatomy.MantissaLength;
            T LowerBound = RightShift(Anatomy.One, Anatomy.MantissaLength);
            T UpperBound = RightShift(Anatomy.One, Anatomy.MantissaLength + 1);

            if(num.CompareTo(LowerBound) < 0)
            {
                while(num.CompareTo(LowerBound) < 0)
                {
                    num = RightShift(num, 1);
                    exponent--;
                }
            }
            
            while(num.CompareTo(UpperBound) >= 0)
            {
                num = LeftShift(num, 1);
                exponent++;
            }

            mantissa = ProjectIntegerPart(num) & MantissaMask;
            return IsNegativeCached;
        }
    }
}
