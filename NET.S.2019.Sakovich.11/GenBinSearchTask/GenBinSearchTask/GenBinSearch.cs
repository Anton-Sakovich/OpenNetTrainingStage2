using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenBinSearchTask
{
    public class GenBinSearch
    {
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

            int first = 0;
            int middle;
            int half;
            int order;

            for (int count = data.Count; count > 1; count = half)
            {
                half = count / 2;
                middle = first + half;
                order = comp(value, data[middle]);

                if (order == 0)
                {
                    return middle;
                }

                if (order > 0)
                {
                    first = middle;
                }
            }

            return comp(value, data[first]) == 0 ? first : ~first;
        }
    }
}
