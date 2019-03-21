using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingTask
{
    public class Mergesort : ISortingEngine<int>
    {
        int[] Temp;

        public void Sort(int[] array)
        {
            Temp = new int[array.Length];
            
            // PartLength is the length of a single group at a current iteration
            //
            // NextPartLength is the length of single group at the next iteration
            //
            // The length of a merged group is NextPartLength at a current iteration
            // (except maybe the last pair)
            //
            // start is the first index in the first group to be merged with the next
            // one
            // Example for PartLength = 4
            // x   x   x   x|  x   x   x   x|  x   x   x   x|   x
            // s1                              s2
            int PartLength, NextPartLength, start;
            for (PartLength = 1; PartLength < array.Length; PartLength = NextPartLength)
            {
                NextPartLength = PartLength << 1;
                for (start = 0; start < array.Length - PartLength; start += NextPartLength)
                {
                    MergeSortedSubArrays(array, start, PartLength);
                }
            }
        }

        // Merges two adjacent groups together
        void MergeSortedSubArrays(int[] baseArray, int start, int partLength)
        {
            // Length of the array if the current group were the last one
            int Length1 = start + partLength;
            // Length of the array if the next group were the last one
            int Length2 = Length1 + Math.Min(partLength, baseArray.Length - partLength - start);
            // Index inside the current group
            int j1 = start;
            // Index inside the next group
            int j2 = Length1;
            // Index inside the resulting array
            int j = 0;

            while (j1 < Length1 && j2 < Length2)
            {
                if (baseArray[j1] < baseArray[j2])
                {
                    Temp[j++] = baseArray[j1++];
                }
                else
                {
                    Temp[j++] = baseArray[j2++];
                }
            }

            // Only one loop form the two below which correspons to left-overs
            // will be executed
            for (; j1 < Length1; j1++)
            {
                Temp[j++] = baseArray[j1];
            }

            for (; j2 < Length2; j2++)
            {
                Temp[j++] = baseArray[j2];
            }

            Array.Copy(Temp, 0, baseArray, start, Length2 - start);
        }
    }
}
