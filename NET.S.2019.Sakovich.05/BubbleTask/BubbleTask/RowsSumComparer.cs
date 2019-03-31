using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BubbleTask
{
    public class RowsSumComparer : RowsComparer
    {
        protected override int RowMap(int[] row)
        {
            int Sum = 0;

            foreach (int elem in row)
                Sum += elem;

            return Sum;
        }
    }
}
