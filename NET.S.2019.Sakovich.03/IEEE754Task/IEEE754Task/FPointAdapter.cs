using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEEE754Task
{
    public class FPointAdapter
    {
        /// <summary>
        /// An FPointAnatomy setting the layout of floating points.
        /// </summary>
        public readonly FPointAnatomy Anatomy;

        /// <summary>
        /// Creates a new FPointAdapter for the specified FPointAnatomy.
        /// </summary>
        /// <param name="anat">FPointAnatomy setting the layout of floating points.</param>
        public FPointAdapter(FPointAnatomy anat)
        {
            Anatomy = anat;
        }

        /// <summary>
        /// Extracts a floating point's mantissa and exponent into separate integers.
        /// </summary>
        /// <param name="num">The floating point number.</param>
        /// <param name="exponent">The floating point number's exponent.</param>
        /// <param name="mantissa">The floating point number's mantissa.</param>
        /// <returns>True if the floating point number is negative and false otherwise.</returns>
        public bool Destruct(double num, out ulong exponent, out ulong mantissa)
        {
            // First, destruct those numbers for which algerbaic operations are poorly
            // defined.
            if(double.IsNaN(num))
            {
                exponent = ~(ulong.MaxValue << Anatomy.ExponentLength);
                mantissa = 1UL << (Anatomy.MantissaLength - 1);
                return true;
            }
            if (double.IsPositiveInfinity(num))
            {
                exponent = ~(ulong.MaxValue << Anatomy.ExponentLength);
                mantissa = 0;
                return false;
            }
            if (double.IsNegativeInfinity(num))
            {
                exponent = ~(ulong.MaxValue << Anatomy.ExponentLength);
                mantissa = 0;
                return true;
            }

            // First, check the num's sign. If num == 0.0, special care is to be taken
            // to catch negative zero.
            bool IsNegativeCached = (num < 0.0) | double.IsNegativeInfinity(1.0 / num);

            // If num is negative, make it possitive not to mess with the leading bit
            // when num is converted to ulong
            if (IsNegativeCached)
            {
                num = -num;
            }

            // If num is subnormal, then we know in advance how many times we need to move
            // the floating point to the right. We also don't need a mask because for subnormals,
            // there is no leading 1.
            if (num < Anatomy.MinNormalNumber)
            {
                exponent = 0;

                mantissa = (ulong)ShiftFloatingPoint(num, Anatomy.ExponentBias + Anatomy.MantissaLength - 1);

                return IsNegativeCached;
            }

            // If num is normal, move floating point to the right until its logical exponent becomes
            // bias + mantissa_length. When a number is in normalized form, its logical exponent is
            // bias + mantissa_length when
            // 2 ^ (bias + mantissa_length) <= the number < 2 ^ (bias + mantissa_length + 1)
            double LowerBound = ShiftFloatingPoint(1.0, Anatomy.MantissaLength);
            double UpperBound = LowerBound * 2.0;

            // A mask for cutting mantissa from resulting integers (mantissa_length 1s)
            ulong MantissaMask = ~(ulong.MaxValue << Anatomy.MantissaLength);

            // Assume that
            // 2 ^ (bias + mantissa_length) <= the number < 2 ^ (bias + mantissa_length + 1)
            // is already satisfied and the exponent is therefore bias + mantissa_length
            exponent = (ulong)Anatomy.ExponentBias + (ulong)Anatomy.MantissaLength;

            // If it is not so, move the floating point and correct the exponent.
            if (num < LowerBound)
            {
                while(num < LowerBound)
                {
                    num *= 2.0;
                    exponent--;
                }
            }            
            while(num >= UpperBound)
            {
                num /= 2.0;
                exponent++;
            }

            // Now the exponent is as we need it to be and we can take integer part
            mantissa = (ulong)num & MantissaMask;

            return IsNegativeCached;
        }

        static double ShiftFloatingPoint(double num, int n)
        {
            for(int i = 0; i < n; i++)
            {
                num *= 2.0;
            }

            return num;
        }

        /// <summary>
        /// Returns an ulong representation of how the given floating point number is stored in memory.
        /// </summary>
        /// <param name="num">A floating point number.</param>
        /// <returns>Ulong representation of the number's bits.</returns>
        public ulong Bitmap(double num)
        {
            bool sign = Destruct(num, out ulong exponent, out ulong mantissa);

            return ComposeBitmap(sign, exponent, mantissa);
        }

        private ulong ComposeBitmap(bool sign, ulong exponent, ulong mantissa)
        {
            return ((sign ? 1UL : 0UL) << (Anatomy.FullLength - 1)) | (exponent << Anatomy.MantissaLength) | mantissa;
        }

        /// <summary>
        /// Returns a string representation of how the given floating point number is stored in memory.
        /// </summary>
        /// <param name="num">A floating point number.</param>
        /// <returns>String representation of the number's bits.</returns>
        public string BinaryString(double num)
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

        private string ToBinaryString(ulong num)
        {
            string[] BitGroups = new string[Anatomy.FullLength / 4];

            for (int i = Anatomy.FullLength / 4 - 1; i >= 0; i--)
            {
                BitGroups[i] = HexStrings[num & 0xF];
                num >>= 4;
            }

            return String.Concat(BitGroups);
        }
    }
}
