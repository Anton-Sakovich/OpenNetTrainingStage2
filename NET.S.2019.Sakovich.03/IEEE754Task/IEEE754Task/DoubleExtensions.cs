using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEEE754Task
{
    public static class DoubleExtensions
    {
        const double LowerBound = 0x10000000000000U; // 2^52
        const double UpperBound = 0x20000000000000U; // 2^53
        const ulong MantissaMask = 0xFFFFFFFFFFFFF;
        const ulong InitialExponent = 1075; // 1023 + 52
        const int MantissaLength = 52;
        const int ExponentLength = 11;

        public static string ToIEEE754String(this double d)
        {
            bool IsNegative = d < 0;
            if (IsNegative)
                d = -d;

            ulong Exponent = InitialExponent;
            if( d < LowerBound)
            {
                while(d < LowerBound)
                {
                    d *= 2;
                    if (Exponent > 0)
                        Exponent--;
                }
            }
            else
            {
                while(d >= UpperBound)
                {
                    d /= 2;
                    if (Exponent < 2046)
                        Exponent++;
                }
            }

            ulong BitsOfDouble = (Exponent << MantissaLength) | (ulong)d & MantissaMask;
            if (IsNegative)
                BitsOfDouble |= 1UL << (MantissaLength + ExponentLength);

            return BitsOfDouble.ToBinaryString();
        }

        static readonly string[] HexStrings = new string[]
            {
                "0000", "0001", "0010", "0011",
                "0100", "0101", "0110", "0111",
                "1000", "1001", "1010", "1011",
                "1100", "1101", "1110", "1111"
            };

        public static string ToBinaryString(this ulong num)
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
