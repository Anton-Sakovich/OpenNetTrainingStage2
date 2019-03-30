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

        public ulong Bitmap(T num)
        {
            bool sign = Destruct(num, out ulong exponent, out ulong mantissa);

            return ComposeBitmap(sign, exponent, mantissa);
        }

        private ulong ComposeBitmap(bool sign, ulong exponent, ulong mantissa)
        {
            return ((sign ? 1UL : 0UL) << (Anatomy.FullLength - 1)) | (exponent << Anatomy.MantissaLength) | mantissa;
        }

        public string BinaryString(T num)
        {
            return ToBinaryString(Bitmap(num));
        }

        private static readonly string[] HexStrings = new string[]
            {
                "0000", "0001", "0010", "0011",
                "0100", "0101", "0110", "0111",
                "1000", "1001", "1010", "1011",
                "1100", "1101", "1110", "1111"
            };

        private static string ToBinaryString(ulong num)
        {
            string[] BitGroups = new string[16];

            for (int i = 15; i >= 0; i--)
            {
                BitGroups[i] = HexStrings[num & 0xF];
                num >>= 4;
            }

            return String.Concat(BitGroups);
        }
    }
}
