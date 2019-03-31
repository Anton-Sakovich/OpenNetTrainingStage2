using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BubbleTask
{
    public class RowsMaxComparer : RowsComparer
    {
        protected override int RowMap(int[] row)
        {
            int Max = row[0];

            foreach (int elem in row)
            {
                if (elem > Max)
                    Max = elem;
            }

            return Max;
        }
    }
}
