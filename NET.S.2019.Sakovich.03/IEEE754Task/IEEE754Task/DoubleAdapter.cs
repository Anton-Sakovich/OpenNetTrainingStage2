using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEEE754Task
{
    public class DoubleAdapter
    {
        /// <summary>
        /// Mantissa length in binary representation of double.
        /// </summary>
        public const int MantissaLength = 52;

        /// <summary>
        /// Total length of binary representation of double.
        /// </summary>
        public const int FullLength = 64;

        /// <summary>
        /// Minimal positive double which can be represented in normalized form.
        /// </summary>
        public static readonly double MinNormalNumber;

        /// <summary>
        /// IEEE754 exponent bias for doubles.
        /// </summary>
        public static readonly int ExponentBias;

        static DoubleAdapter()
        {
            // Exponent bias is defined as 2 ^ (k - 1) - 1, where k is mantissa length
            ExponentBias = (1 << 10) - 1;

            // Minimal normal number corresponds to exponent = (1 - bias) = (1 - 1023)
            MinNormalNumber = 2.0;
            for (int i = 0; i < ExponentBias; i++)
                MinNormalNumber /= 2.0;
        }

        public bool Destruct(double num, out ulong exponent, out ulong mantissa)
        {
            // First, destruct those numbers for which algerbaic operations are poorly
            // defined.
            if(double.IsNaN(num))
            {
                return DestrcutNaN(out exponent, out mantissa);
            }
            if (double.IsPositiveInfinity(num))
            {
                return DestrcutPositiveInfinity(out exponent, out mantissa);
            }
            if (double.IsNegativeInfinity(num))
            {
                return DestrcutNegativeInfinity(out exponent, out mantissa);
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
            // the floating point to the right.
            if (num < MinNormalNumber)
            {
                exponent = 0;

                mantissa = (ulong)ShiftFloatingPoint(num, ExponentBias + MantissaLength - 1);

                return IsNegativeCached;
            }

            // If num is normal, move floating point to the right until its exponent becomes
            // 52 (the number of bits in mantissa). When a number is in normalized form, its
            // exponent is 52 when 2^52 <= the number < 2^53
            double LowerBound = ShiftFloatingPoint(1.0, MantissaLength); // 2^52
            double UpperBound = LowerBound * 2.0; // 2^53

            // A mask for cutting mantissa from resulting integers (52 1s)
            ulong MantissaMask = ~(ulong.MaxValue << MantissaLength);

            // Assume that 2^52 <= num < 2^53 is already satisfied and its exponent is therefore
            // 1023 + 52
            exponent = (ulong)ExponentBias + (ulong)MantissaLength;

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

            // Now 2^52 <= num < 2^53 is satisfied and we can take integer part
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

        static bool DestrcutNaN(out ulong exponent, out ulong mantissa)
        {
            exponent = 0x7FF;
            mantissa = 1UL << (MantissaLength - 1);
            return true;
        }

        static bool DestrcutPositiveInfinity(out ulong exponent, out ulong mantissa)
        {
            exponent = 0x7FF;
            mantissa = 0;
            return false;
        }

        static bool DestrcutNegativeInfinity(out ulong exponent, out ulong mantissa)
        {
            exponent = 0x7FF;
            mantissa = 0;
            return true;
        }

        /// <summary>
        /// Returns an ulong representation of how the given double is stored in memory.
        /// </summary>
        /// <param name="num">Double precision floating point number.</param>
        /// <returns>Ulong representation of the number's bits.</returns>
        public ulong Bitmap(double num)
        {
            bool sign = Destruct(num, out ulong exponent, out ulong mantissa);

            return ComposeBitmap(sign, exponent, mantissa);
        }

        private ulong ComposeBitmap(bool sign, ulong exponent, ulong mantissa)
        {
            return ((sign ? 1UL : 0UL) << (FullLength - 1)) | (exponent << MantissaLength) | mantissa;
        }

        /// <summary>
        /// Returns a string representation of how the given double is stored in memory.
        /// </summary>
        /// <param name="num">Double precision floating point number.</param>
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
