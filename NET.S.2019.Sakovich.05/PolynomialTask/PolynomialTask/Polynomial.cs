using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolynomialTask
{
    public sealed class Polynomial : ICloneable
    {
        int[] _Powers;
        int[] _Coefficients;

        int _HashCache;
        bool _HashComputed = false;

        public Polynomial(int[] powList, int[] coeffsList)
        {
            if (powList == null)
                throw new ArgumentNullException(nameof(powList), "An array of powers cannot be null.");

            if (coeffsList == null)
                throw new ArgumentNullException(nameof(coeffsList), "An array of coefficients cannot be null.");

            if (powList.Length != coeffsList.Length)
                throw new ArgumentException("The length of arrays for powers and coefficients must be the same.");

            _Powers = powList.Where((pow, index) => coeffsList[index] != 0).ToArray();
            _Coefficients = coeffsList.Where(coeff => coeff != 0).ToArray();

            PairwiseSort(_Powers, _Coefficients);
        }

        // For internal use by algebraic function
        private Polynomial()
        {

        }

        // Arrays are asumed to be of equal non-zero length
        void PairwiseSort(int[] master, int[] slave)
        {
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

        public object Clone()
        {
            Polynomial PolyClone = new Polynomial();

            PolyClone._Powers = (int[])_Powers.Clone();
            PolyClone._Coefficients = (int[])_Coefficients.Clone();
            PolyClone._HashCache = _HashCache;
            PolyClone._HashComputed = _HashComputed;

            return PolyClone;
        }

        public int[] Powers
        {
            get => (int[])_Powers.Clone();
        }

        public int[] Coefficients
        {
            get => (int[])_Coefficients.Clone();
        }

        public int MaxPower
        {
            get => _Powers.Length > 0 ? _Powers.Last() : 0;
        }

        static void ValidateArgument(Polynomial poly)
        {
            if (poly == null)
                throw new ArgumentNullException(nameof(poly));
        }

        public static Polynomial Add(Polynomial poly1, Polynomial poly2)
        {
            ValidateArgument(poly1);
            ValidateArgument(poly2);

            int[] NewCoefficientsTemp = new int[poly1._Coefficients.Length + poly2._Coefficients.Length];
            int[] NewPowersTemp = new int[poly1._Powers.Length + poly2._Powers.Length];

            int j1 = 0;
            int j2 = 0;
            int j = 0;
            int sum;

            for (; j1 < poly1._Coefficients.Length && j2 < poly2._Coefficients.Length;)
            {
                if(poly1._Powers[j1] < poly2._Powers[j1])
                {
                    NewPowersTemp[j] = poly1._Powers[j1];
                    NewCoefficientsTemp[j++] = poly1._Coefficients[j1++];
                }
                else if(poly1._Powers[j1] > poly2._Powers[j1])
                {
                    NewPowersTemp[j] = poly2._Powers[j2];
                    NewCoefficientsTemp[j++] = poly2._Coefficients[j2++];
                }
                else
                {
                    if((sum = poly1._Coefficients[j1] + poly2._Coefficients[j2]) != 0)
                    {
                        NewPowersTemp[j] = poly1._Powers[j1];
                        NewCoefficientsTemp[j++] = sum;
                    }

                    j1++;
                    j2++;
                }
            }

            for(; j1 < poly1._Coefficients.Length;)
            {
                NewPowersTemp[j] = poly1._Powers[j1];
                NewCoefficientsTemp[j++] = poly1._Coefficients[j1++];
            }

            for (; j2 < poly2._Coefficients.Length;)
            {
                NewPowersTemp[j] = poly2._Powers[j2];
                NewCoefficientsTemp[j++] = poly2._Coefficients[j2++];
            }

            Polynomial ResultingPoly = new Polynomial();

            ResultingPoly._Powers = new int[j];
            Array.Copy(NewPowersTemp, ResultingPoly._Powers, j);

            ResultingPoly._Coefficients = new int[j];
            Array.Copy(NewCoefficientsTemp, ResultingPoly._Coefficients, j);

            return ResultingPoly;
        }

        public static Polynomial Times(Polynomial poly1, Polynomial poly2)
        {
            ValidateArgument(poly1);
            ValidateArgument(poly2);

            if (poly1._Coefficients.Length == 0)
                return (Polynomial)poly1.Clone();

            if (poly2._Coefficients.Length == 0)
                return (Polynomial)poly2.Clone();

            Polynomial[] CopiesOfPoly2 = new Polynomial[poly1._Coefficients.Length];
            for(int i = 0; i < CopiesOfPoly2.Length; i++)
            {
                CopiesOfPoly2[i] = (Polynomial)poly2.Clone();
            }

            for(int p1 = 0; p1 < poly1._Coefficients.Length; p1++)
            {
                for(int p2 = 0; p2 < poly2._Coefficients.Length; p2++)
                {
                    CopiesOfPoly2[p1]._Coefficients[p2] *= poly1._Coefficients[p1];
                    CopiesOfPoly2[p1]._Powers[p2] += poly1._Powers[p1];
                }
            }

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
            return poly1 == null ? other == null : poly1.Equals(other);
        }

        public static bool operator !=(Polynomial poly1, object other)
        {
            return !(poly1 == other);
        }
    }
}
