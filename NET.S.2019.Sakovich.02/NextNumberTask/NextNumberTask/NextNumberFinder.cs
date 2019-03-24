using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextNumberTask
{
    public class NextNumberFinder
    {
        // MaxDigitsCount stores the maximum number of possible decimal digits
        // for array pre-allocation. For Int32 it is 10 (2^31 - 1 = 2147483647)
        const int MaxDigitsCount = 10;

        // The main method
        public static int? NextBiggerThan(int number)
        {
            if (number <= 0)
            {
                throw new ArgumentException("Input number must be positive.");
            }

            // Destruct(int) returns digits of the given number in reversed order
            int[] NumberDigits = Destruct(number);

            // Swap(int[]) returns true if there is a number greater than
            // the given one and swaps digits of the array to obtain it.
            // Swap(int[]) returns false if there is no such a number and 
            // there is nothing to swap, leaving the array untouched
            if (Swap(NumberDigits))
            {
                // Construct(int[]) composes the number from its digits given in reversed order.
                // If the digits provided correspond to a number greater than 2^31 - 1,
                // an OverflowException will be thrown.
                int? Result;
                try
                {
                    Result = Construct(NumberDigits);
                }
                catch (OverflowException)
                {
                    // The bigger number does exist but is out of Int32
                    Result = null;
                }

                return Result;
            }
            else
            {
                return null;
            }
        }

        // Returns digits of the given number in reversed order
        static int[] Destruct(int number)
        {
            // A pre-allocated storage
            int[] DigitsBag = new int[MaxDigitsCount];

            // After execution of this loop, j will be equal to
            // the actual number of digits inside DigitsBag
            int Current = number;
            int j = 0;
            do
            {
                DigitsBag[j++] = Current % 10;
                Current = Current / 10;
            } while (Current != 0);

            // Copy actual number of digits into an array of the
            // appropriate length
            int[] Digits = new int[j];
            Array.Copy(DigitsBag, Digits, j);

            return Digits;
        }

        // Makes an integer from its decimal digits given in reversed order
        static int Construct(int[] digits)
        {
            int Result = digits[0];
            int Multiplier = 1;

            for (int j = 1; j < digits.Length; j++)
            {
                checked
                {
                    Multiplier *= 10;
                    Result += digits[j] * Multiplier;
                }
            }

            return Result;
        }

        // See NextBiggerThan(int) for details
        static bool Swap(int[] digits)
        {
            // Position of the first element out of sequence
            int j;

            // Looking for the pisition j of the first element which
            // violates ascending order of digits.
            for (j = 1; j < digits.Length; j++)
            {
                if (digits[j - 1] > digits[j])
                {
                    break;
                }
            }

            // If j = the length of the array, then it means that
            // the loop didn't break and the digits are completely
            // sorted
            if (j == digits.Length)
            {
                return false;
            }

            // Swap the smallest element before the j-th one
            // which is larger than the j-th one with the j-th.
            // Because all elements before the j-th one are sorted,
            // just swap the first one which is larger then the j-th.
            int temp;
            for (int k = 0; k < j; k++)
            {
                if (digits[k] > digits[j])
                {
                    temp = digits[k];
                    digits[k] = digits[j];
                    digits[j] = temp;
                    break;
                }
            }

            // Resort the sequence from ascending to descending order
            for (int k = 0; k < (j / 2); k++)
            {
                temp = digits[k];
                digits[k] = digits[j - k - 1];
                digits[j - k - 1] = temp;
            }

            return true;
        }
    }
}
