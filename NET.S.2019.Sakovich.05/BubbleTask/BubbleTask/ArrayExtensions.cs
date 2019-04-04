using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BubbleTask
{
    public static class ArrayExtensions
    {
        /// <summary>
        /// Sorts the elements of an input array in the order definied by applying
        /// a given function to each of them.
        /// </summary>
        /// <param name="array">An array to sort.</param>
        /// <param name="func">A function to obtain values used for sorting.</param>
        /// <param name="desc">A boolean which determines whether the order is descending..</param>
        public static void BubbleSortBy(this int[][] array, Func<int[], int> func, bool desc = false)
        {
            // Check that the array is not null and its length is greater than zero.
            ValidateArray(array);

            // An array of values used to sort the input array.
            int[] Map = new int[array.Length];
            // An array of indices which are permuted by the sorting algorithm.
            int[] Indices = new int[array.Length];

            // Fill the arrays.
            for (int i = 0; i < array.Length; i++)
            {
                Map[i] = func(array[i]);
                Indices[i] = i;
            }

            // Permute indices so that Map[indices[i]] is sorted.
            if (desc)
            {
                BubbleSortDsc(Map, Indices);
            }
            else
            {
                BubbleSortAsc(Map, Indices);
            }

            // Permute the input array so that at its indices[i]-th element will go to 
            // to i-th position.
            _Permute(array, Indices);
        }

        /// <summary>
        /// Permutes the elements of an array so that its indices[i]-th element will go to
        /// i-th position.
        /// </summary>
        /// <typeparam name="T">An underlying type of the array.</typeparam>
        /// <param name="array">An input array.</param>
        /// <param name="indices">An array of positions.</param>
        public static void Permute<T>(this T[] array, int[] indices)
        {
            ValidateArray(array);

            _Permute(array, (int[])indices.Clone());
        }

        // Make the permuatation in O(n) swaps
        static void _Permute<T>(T[] array, int[] indices)
        {
            // A temporary variable for swapping.
            T Temp;

            for(int i = 0; i < array.Length; i++)
            {
                // If indices[i] == -1, then array[i] has already been involved in
                // a completed cyclic shift and therefore stands in its final position.
                if (indices[i] == -1)
                {
                    continue;
                }

                // If indices[i] != -1, then array[i] belongs to a cycle which must
                // be shifted. The following code shifts the cycle to its final
                // position, marking elements belonging to the cycle by -1 in the
                // indices array.

                int ThisPos = i;
                int NextPos = indices[i];
                while (NextPos != i)
                {
                    // Swap array[ThisPos] and array[NextPos]
                    Temp = array[ThisPos];
                    array[ThisPos] = array[NextPos];
                    array[NextPos] = Temp;

                    // After the swap, array[ThisPos] is in its final position.
                    indices[ThisPos] = -1;

                    // Now in the array[NextPos] slot is the element which came
                    // from the array[ThisPos] slot. It will be the next ThisPos.
                    ThisPos = NextPos;
                    // The next NextPos is determined by the indices array.
                    NextPos = indices[NextPos];
                }

                // When the shifting process is completed, the last shifted element
                // will not be marked.
                indices[ThisPos] = -1;
            }
        }

        static void BubbleSortAsc(int[] map, int[] inds)
        {
            int Temp = 0;

            for (int UnsortedLength = inds.Length; UnsortedLength > 1; UnsortedLength--)
            {
                for (int BubblePos = 0; BubblePos < (UnsortedLength - 1); BubblePos++)
                {
                    if (map[inds[BubblePos]] > map[inds[BubblePos + 1]])
                    {
                        Temp = inds[BubblePos];
                        inds[BubblePos] = inds[BubblePos + 1];
                        inds[BubblePos + 1] = Temp;
                    }
                }
            }
        }

        static void BubbleSortDsc(int[] map, int[] inds)
        {
            int Temp = 0;

            for (int UnsortedLength = inds.Length; UnsortedLength > 1; UnsortedLength--)
            {
                for (int BubblePos = 0; BubblePos < (UnsortedLength - 1); BubblePos++)
                {
                    if (map[inds[BubblePos]] < map[inds[BubblePos + 1]])
                    {
                        Temp = inds[BubblePos];
                        inds[BubblePos] = inds[BubblePos + 1];
                        inds[BubblePos + 1] = Temp;
                    }
                }
            }
        }

        public static int AmateurMax(this int[] array)
        {
            ValidateArray(array);

            int Max = array[0];

            for(int i = 1; i < array.Length; i++)
            {
                if(array[i] > Max)
                {
                    Max = array[i];
                }
            }

            return Max;
        }

        public static int AmateurMin(this int[] array)
        {
            ValidateArray(array);

            int Min = array[0];

            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] < Min)
                {
                    Min = array[i];
                }
            }

            return Min;
        }

        public static int AmateurTotal(this int[] array)
        {
            ValidateArray(array);

            int Total = array[0];

            for (int i = 1; i < array.Length; i++)
            {
                Total += array[i];
            }

            return Total;
        }

        static void ValidateArray<T>(T[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (array.Length == 0)
            {
                throw new ArgumentException("The length of the input array must be greater than zero.", nameof(array));
            }
        }
    }
}
