using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEEE754Task
{
    /// <summary>
    /// Describes a layout of a floating point number in memory.
    /// </summary>
    public struct FPointAnatomy
    {
        /// <summary>
        /// Full length of binary representation of a floating point number.
        /// </summary>
        public int FullLength { get; }

        /// <summary>
        /// Mantissa length of binary representation of a floating point number.
        /// </summary>
        public int MantissaLength { get; }

        /// <summary>
        /// Creates an instance of FPointAnatomy for the specified full length and mantissa length.
        /// </summary>
        /// <param name="flen">Full length of binary representation of a floating point number.</param>
        /// <param name="mlen">Mantissa length of binary representation of a floating point number.</param>
        public FPointAnatomy(int flen, int mlen)
        {
            FullLength = flen;
            MantissaLength = mlen;
        }

        /// <summary>
        /// Exponent length of binary representation of a floating point number.
        /// </summary>
        public int ExponentLength
        {
            get
            {
                return FullLength - MantissaLength - 1;
            }
        }

        /// <summary>
        /// IEEE754 floating point exponent bias.
        /// </summary>
        public int ExponentBias
        {
            get
            {
                return (1 << (ExponentLength - 1)) - 1;
            }   
        }

        /// <summary>
        /// Minimal positive floating point number which can be represented in normalized form.
        /// </summary>
        public double MinNormalNumber
        {
            get
            {
                double Num = 2.0;
                for (int i = 0; i < ExponentBias; i++)
                    Num /= 2.0;

                return Num;
            }
        }

        /// <summary>
        /// A binary mask which filters floating point number's bits from an ulong integer.
        /// </summary>
        public ulong Mask
        {
            get
            {
                return ~(ulong.MaxValue << FullLength);
            }
        }
    }
}
