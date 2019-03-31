using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BubbleTask
{
    public class BubbleSort
    {
        public void Sort(int[][] array, IComparer<int[]> comp)
        {
            int[] Temp;

            for (int UnsortedLength = array.Length; UnsortedLength > 1; UnsortedLength--)
            {
                for(int BubblePos = 0; BubblePos < (UnsortedLength - 1); BubblePos++)
                {
                    if(comp.Compare(array[BubblePos], array[BubblePos + 1]) > 0)
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
