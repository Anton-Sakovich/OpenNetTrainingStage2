using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingTask
{
    public class Quicksort : ISortingEngine<int>
    {
        public void Sort(int[] array)
        {
            if (array == null)
                throw new ArgumentNullException("Input array must not be null.", nameof(array));

            Step(array, 0, array.Length - 1);
        }

        private void Step(int[] array, int left, int right)
        {
            if (left >= right)
                return;

            int pivot = SelectPrivot(array, left, right);
            int boundary = FindBoundary(array, left, right, pivot);

            Step(array, left, boundary - 1);
            Step(array, boundary + 1, right);
        }

        private int FindBoundary(int[] array, int left, int right, int pivot)
        {
            int l = left;
            int r = right;
            int temp;

            while (l <= r)
            {
                for(; l <= r; l++)
                {
                    if (array[l] > array[pivot])
                        break;
                }

                for(; r >= l; r--)
                {
                    if (array[r] < array[pivot])
                        break;
                }

                if (l < r)
                {
                    temp = array[r];
                    array[r] = array[l];
                    array[l] = temp;
                    l++;
                    r--;
                }
            }

            if(pivot > l)
            {
                temp = array[l];
                array[l] = array[pivot];
                array[pivot] = temp;
                return l;
            }
            else if(pivot < r)
            {
                temp = array[r];
                array[r] = array[pivot];
                array[pivot] = temp;
                return r;
            }
            else
            {
                return pivot;
            }
        }

        private int SelectPrivot(int[] array, int left, int right)
        {
            int middle = (right + left) / 2;

            if (array[left] < array[middle])
            {
                if (array[middle] < array[right])
                    return middle;
                else if (array[left] < array[right])
                    return right;
                else
                    return left;
            }
            else
            {
                if (array[left] < array[right])
                    return left;
                else if (array[middle] < array[right])
                    return right;
                else
                    return middle;
            }
        }
    }
}
