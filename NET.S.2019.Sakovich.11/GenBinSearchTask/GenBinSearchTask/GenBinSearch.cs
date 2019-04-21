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
