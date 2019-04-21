using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenBinSearchTask
{
    public class GenBinSearch
    {
        /// <summary>
        /// Returns the position of the specified element in a sorted list using a delegate to
        /// compare values of the list and binary search algorithm.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="data">The sorted list to search in.</param>
        /// <param name="value">The value to search for.</param>
        /// <param name="comp">The delegate to use to compare different values in the list.</param>
        /// <returns>The position of the specified element.</returns>
        /// <exception cref="ArgumentNullException">Thrown if either the list or the delegate is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the list is empty.</exception>
        public static int BinarySearch<T>(IList<T> data, T value, Func<T, T, int> comp)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (comp == null)
            {
                throw new ArgumentNullException(nameof(comp));
            }

            if (data.Count == 0)
            {
                throw new ArgumentException("Data list must contain at least one element.", nameof(data));
            }

            int left = 0;
            int right = data.Count - 1;
            int middle;

            while (left < right)
            {
                middle = (left + right) >> 1;

                if (comp(value, data[middle]) > 0)
                {
                    left = middle + 1;
                }
                else
                {
                    right = middle;
                }
            }

            return comp(value, data[left]) == 0 ? left : ~left;
        }
    }
}
