using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BubbleTask
{
    public class BubbleSort
    {
        public void Sort(int[][] array, IComparer<int[]> comp, bool desc = false)
        {
            int[] Temp;
            int CompareResult;

            for (int UnsortedLength = array.Length; UnsortedLength > 1; UnsortedLength--)
            {
                for(int BubblePos = 0; BubblePos < (UnsortedLength - 1); BubblePos++)
                {
                    CompareResult = comp.Compare(array[BubblePos], array[BubblePos + 1]);

                    // & (CompareResult != 0) is required to keep the algorithm stable when desc = true
                    if (((CompareResult > 0) ^ desc) & (CompareResult != 0))
                    {
                        Temp = array[BubblePos];
                        array[BubblePos] = array[BubblePos + 1];
                        array[BubblePos + 1] = Temp;
                    }
                }
            }
        }
    }
}
