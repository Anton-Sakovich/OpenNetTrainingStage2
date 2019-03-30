using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEEE754Task
{
    abstract class FloatAdapter<T> where T : IComparable<T>
    {
        public readonly IFloatAnatomy<T> Anatomy;

        public FloatAdapter(IFloatAnatomy<T> anat)
        {
            Anatomy = anat;
        }

        protected abstract T LeftShift(T num, int steps);

        protected abstract T RightShift(T num, int steps);

        protected abstract bool IsNegative(T num);

        protected abstract bool IsNormal(T num);

        protected abstract ulong ProjectIntegerPart(T num);

        protected abstract T FlipSign(T num);

        protected abstract T One { get; }

        public bool Destruct(T num, out ulong exponent, out ulong mantissa)
        {
            bool IsNegativeCached = IsNegative(num);

            if (IsNegativeCached)
                FlipSign(num);

            if(!IsNormal(num))
            {
                exponent = 0;

                mantissa = ProjectIntegerPart(RightShift(num, Anatomy.ExponentShift + Anatomy.MantissaLength));

                return IsNegativeCached;
            }

            ulong MantissaMask = 1UL << Anatomy.MantissaLength;
            exponent = (ulong)Anatomy.ExponentShift + (ulong)Anatomy.MantissaLength;
            T LowerBound = RightShift(One, Anatomy.MantissaLength);
            T UpperBound = RightShift(One, Anatomy.MantissaLength + 1);

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

            mantissa = ProjectIntegerPart(num);
            return IsNegativeCached;
        }
    }
}
