using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingTask.Tests
{
    static class ArrayExtenstions
    {
        public static bool IsSorted(this int[] array)
        {
            if (array == null)
                throw new ArgumentNullException("Array was null.");

            if (array.Length < 2)
                return true;

            for(int i = 0; i < (array.Length - 1); i++)
            {
                if (array[i] > array[i + 1])
                    return false;
            }

            return true;
        }
    }
}
