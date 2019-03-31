using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolynomialTask
{
    /// <summary>
    /// Represents a polynomial from a real argument with integer coefficients.
    /// </summary>
    public sealed class Polynomial : ICloneable
    {
        // A sorted array of powers whose coefficients are not equal to zero.
        // This array is not supposed to be changed from outside the class,
        // however, this field is not read only because Polynomial class
        // itself can modify this field from outside constructor when performing
        // algebraic operations.
        int[] _Powers;
        // An array of coefficients corresponding to _Powers. The same story with
        // access as with _Powers.
        int[] _Coefficients;

        // The cached hash value.
        int _HashCache;
        // A flag indicating whether hash has been already computed.
        bool _HashComputed = false;

        /// <summary>
        /// Creates a new Polynomial with non-zero coefficients coeffsList
        /// corresponding to powers powList.
        /// </summary>
        /// <param name="powList">An array of powers with non-zero coefficients.</param>
        /// <param name="coeffsList">An array of non-zero coefficients.</param>
        public Polynomial(int[] powList, int[] coeffsList)
        {
            if (powList == null)
                throw new ArgumentNullException(nameof(powList), "An array of powers cannot be null.");

            if (coeffsList == null)
                throw new ArgumentNullException(nameof(coeffsList), "An array of coefficients cannot be null.");

            if (powList.Length != coeffsList.Length)
                throw new ArgumentException("The length of arrays for powers and coefficients must be the same.");

            // It is important that arrays are copied because otherwise _Powers and _Coefficients
            // might point to publicly accessible arrays. LINQ ToArray() will do the trick.

            // If zero coefficients are explicitly specified, take only those powers
            // whose coefficients are non-zero.
            _Powers = powList.Where((pow, index) => coeffsList[index] != 0).ToArray();
            // Take only non-zero coefficients.
            _Coefficients = coeffsList.Where(coeff => coeff != 0).ToArray();

            // Sort _Powers, performing corresponding permuatations with _Coefficients as well
            // so that _Powers and _Coefficients are consistent.
            PairwiseSort(_Powers, _Coefficients);
        }

        // Initializes an empty Polynomial to be filled later internally
        private Polynomial()
        {

        }

        // Sorts master, performing the same permutations with slave. Assumes that the arrays
        // have already been null-checked.
        void PairwiseSort(int[] master, int[] slave)
        {
            // A common temporary variable.
            int Temp = 0;

            // Insertion sort
            for (int Outer = 1; Outer <  master.Length; Outer++)
            {
                for(int Inner = Outer; Inner > 0; Inner--)
                {
                    if(master[Inner - 1] > master[Inner])
                    {
                        Swap(master, Inner - 1, Inner, ref Temp);
                        Swap(slave, Inner - 1, Inner, ref Temp);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        void Swap(int[] array, int i, int j, ref int Temp)
        {
            Temp = array[i];
            array[i] = array[j];
            array[j] = Temp;
        }

        /// <summary>
        /// Creates a copy of the calling Polynomial.
        /// </summary>
        /// <returns>A copy of the calling Polynomial.</returns>
        public object Clone()
        {
            Polynomial PolyClone = new Polynomial();

            PolyClone._Powers = (int[])_Powers.Clone();
            PolyClone._Coefficients = (int[])_Coefficients.Clone();
            PolyClone._HashCache = _HashCache;
            PolyClone._HashComputed = _HashComputed;

            return PolyClone;
        }

        /// <summary>
        /// Returns a copy of the Polynomial's powers array.
        /// </summary>
        public int[] Powers
        {
            get => (int[])_Powers.Clone();
        }

        /// <summary>
        /// Returns a copy of the Polynomial's coefficients array.
        /// </summary>
        public int[] Coefficients
        {
            get => (int[])_Coefficients.Clone();
        }

        /// <summary>
        /// Returns the power of the Polynomial.
        /// </summary>
        public int MaxPower
        {
            get => _Powers.Length > 0 ? _Powers.Last() : 0;
        }

        static void ValidateArgument(Polynomial poly)
        {
            if (poly == null)
                throw new ArgumentNullException(nameof(poly));
        }

        /// <summary>
        /// Adds two Polynomials.
        /// </summary>
        /// <param name="poly1">The first summand.</param>
        /// <param name="poly2">The second summand.</param>
        /// <returns>A new Polynomial which is the sum of poly1 and poly2.</returns>
        public static Polynomial Add(Polynomial poly1, Polynomial poly2)
        {
            ValidateArgument(poly1);
            ValidateArgument(poly2);

            // A sorted array of the resulting Polynomial's coefficients
            int[] NewCoefficientsTemp = new int[poly1._Coefficients.Length + poly2._Coefficients.Length];
            // A sorted array of the resulting Polynomial's powers
            int[] NewPowersTemp = new int[poly1._Powers.Length + poly2._Powers.Length];

            // Index inside the first Polynomial's powers
            int j1 = 0;
            // Index inside the second Polynomial's powers
            int j2 = 0;
            // Index inside the resulting Polynomial's powers
            int j = 0;
            // A temporary variable for the sum of two coefficients corresponding to same powers
            int sum;

            // A modified sorted arrays merge routine adjusted to summation of coefficients corresponding
            // to same powers
            for (; j1 < poly1._Coefficients.Length && j2 < poly2._Coefficients.Length;)
            {
                if(poly1._Powers[j1] < poly2._Powers[j2])
                {
                    // If the current power in poly1 is less than that in poly 2,
                    // just add it to the final arrays
                    NewPowersTemp[j] = poly1._Powers[j1];
                    NewCoefficientsTemp[j++] = poly1._Coefficients[j1++];
                }
                else if(poly1._Powers[j1] > poly2._Powers[j2])
                {
                    // If the current power in poly2 is less than that in poly 1,
                    // just add it to the final arrays
                    NewPowersTemp[j] = poly2._Powers[j2];
                    NewCoefficientsTemp[j++] = poly2._Coefficients[j2++];
                }
                else
                {
                    // If the current powers in poly1 and poly2 are the same, either add
                    // this common power with the sum of the corresponding coefficients
                    // to the resulting arrays or, if sum = 0, skip this power and both
                    // coefficients
                    if ((sum = poly1._Coefficients[j1] + poly2._Coefficients[j2]) != 0)
                    {
                        NewPowersTemp[j] = poly1._Powers[j1];
                        NewCoefficientsTemp[j++] = sum;
                    }

                    j1++;
                    j2++;
                }
            }

            // Add possible tails to the resulting arrays

            for (; j1 < poly1._Coefficients.Length;)
            {
                NewPowersTemp[j] = poly1._Powers[j1];
                NewCoefficientsTemp[j++] = poly1._Coefficients[j1++];
            }

            for (; j2 < poly2._Coefficients.Length;)
            {
                NewPowersTemp[j] = poly2._Powers[j2];
                NewCoefficientsTemp[j++] = poly2._Coefficients[j2++];
            }

            // Now, j is the length of relevant parts inside NewPowersTemp and NewCoefficientsTemp arrays

            // Create an empty Polynomial and fill it with data having been collected
            Polynomial ResultingPoly = new Polynomial();

            ResultingPoly._Powers = new int[j];
            Array.Copy(NewPowersTemp, ResultingPoly._Powers, j);

            ResultingPoly._Coefficients = new int[j];
            Array.Copy(NewCoefficientsTemp, ResultingPoly._Coefficients, j);

            return ResultingPoly;
        }

        /// <summary>
        /// Creates a Polynomial which is obtained from the given one by multiplying by -1.
        /// </summary>
        /// <param name="poly">The new Polynomial.</param>
        /// <returns></returns>
        public static Polynomial UnaryMinus(Polynomial poly)
        {
            ValidateArgument(poly);

            Polynomial ResultingPoly = (Polynomial)poly.Clone();

            for(int i = 0; i < ResultingPoly._Coefficients.Length; i++)
            {
                ResultingPoly._Coefficients[i] = -ResultingPoly._Coefficients[i];
            }

            return ResultingPoly;
        }

        /// <summary>
        /// Subtracts the second Polynomial from the first one.
        /// </summary>
        /// <param name="poly1">Minuend Polynomial</param>
        /// <param name="poly2">Subtrahend Polynomial</param>
        /// <returns></returns>
        public static Polynomial Subtract(Polynomial poly1, Polynomial poly2)
        {
            ValidateArgument(poly1);
            ValidateArgument(poly2);

            return Add(poly1, UnaryMinus(poly2));
        }

        public static Polynomial Times(Polynomial poly1, Polynomial poly2)
        {
            ValidateArgument(poly1);
            ValidateArgument(poly2);

            // If any of the operands is zero, return its copy
            if (poly1._Coefficients.Length == 0)
                return (Polynomial)poly1.Clone();

            if (poly2._Coefficients.Length == 0)
                return (Polynomial)poly2.Clone();

            /*
             * The times algorithm creates as many copies of poly2 as there are
             * coefficients in poly1. Then each copy of poly2 is multiplied by a corresponding
             * poly1's coeffcient and the corresponding powers are added.
             * 
             * Example for powers:
             * poly1 = (0, 2, 3)
             * poly2 = (1, 2, 4)
             * 
             * Copies of poly2:
             * (1, 2, 4) --> added with 0 --> (1, 2, 4)
             * (1, 2, 4) --> added with 2 --> (3, 4, 6)
             * (1, 2, 4) --> added with 3 --> (4, 5, 7)
             * 
             * Modified copies are then added together.
             */

            // Create copies
            Polynomial[] CopiesOfPoly2 = new Polynomial[poly1._Coefficients.Length];
            for(int i = 0; i < CopiesOfPoly2.Length; i++)
            {
                CopiesOfPoly2[i] = (Polynomial)poly2.Clone();
            }

            // Multiply each copy by a corresponding coefficient
            for (int p1 = 0; p1 < poly1._Coefficients.Length; p1++)
            {
                for(int p2 = 0; p2 < poly2._Coefficients.Length; p2++)
                {
                    CopiesOfPoly2[p1]._Coefficients[p2] *= poly1._Coefficients[p1];
                    CopiesOfPoly2[p1]._Powers[p2] += poly1._Powers[p1];
                }
            }

            // Add modified copies
            Polynomial ResultingPoly = CopiesOfPoly2[0];
            for(int i = 1; i < CopiesOfPoly2.Length; i++)
            {
                ResultingPoly = Add(ResultingPoly, CopiesOfPoly2[i]);
            }

            return ResultingPoly;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            return (obj is Polynomial other) && Equals(other);
        }

        // This internal method assumes that other != null
        bool Equals(Polynomial other)
        {
            if (_Powers.Length != other._Powers.Length)
                return false;

            if (_Coefficients.Length != other._Coefficients.Length)
                return false;

            for(int i = 0; i < _Powers.Length; i++)
            {
                if (_Powers[i] != other._Powers[i])
                    return false;

                if (_Coefficients[i] != other._Coefficients[i])
                    return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            if (_HashComputed)
                return _HashCache;

            int HashValue = CircularShift(_Coefficients[0], _Powers[0]);
            for(int i = 1; i < _Coefficients.Length; i++)
            {
                unchecked
                {
                    HashValue = (HashValue * 397) ^ CircularShift(_Coefficients[i], _Powers[i]);
                }
            }

            _HashCache = HashValue;
            _HashComputed = true;

            return HashValue;
        }

        int CircularShift(int val, int steps)
        {
            return (val << steps) | (val >> (32 - steps)) & ~(-1 << steps);
        }

        public override string ToString()
        {
            StringBuilder bldr = new StringBuilder();

            int i;
            for (i = 0; i < (_Coefficients.Length - 1); i++)
            {
                bldr.Append(_Coefficients[i].ToString());
                if(_Powers[i] > 0)
                {
                    bldr.Append(" * x");
                }
                if(_Powers[i] > 1)
                {
                    bldr.Append("^");
                    bldr.Append(_Powers[i].ToString());
                }
                bldr.Append(" + ");
            }

            bldr.Append(_Coefficients[i].ToString());
            if (_Powers[i] > 0)
            {
                bldr.Append(" * x");
            }
            if (_Powers[i] > 1)
            {
                bldr.Append("^");
                bldr.Append(_Powers[i].ToString());
            }

            return bldr.ToString();
        }

        public static bool operator ==(Polynomial poly1, object other)
        {
            return ReferenceEquals(poly1, null) ? ReferenceEquals(other, null) : poly1.Equals(other);
        }

        public static bool operator !=(Polynomial poly1, object other)
        {
            return !(poly1 == other);
        }

        public static Polynomial operator +(Polynomial poly1, Polynomial poly2)
        {
            return Add(poly1, poly2);
        }

        public static Polynomial operator -(Polynomial poly1, Polynomial poly2)
        {
            return Subtract(poly1, poly2);
        }

        public static Polynomial operator -(Polynomial poly)
        {
            return UnaryMinus(poly);
        }

        public static Polynomial operator *(Polynomial poly1, Polynomial poly2)
        {
            return Times(poly1, poly2);
        }
    }
}
