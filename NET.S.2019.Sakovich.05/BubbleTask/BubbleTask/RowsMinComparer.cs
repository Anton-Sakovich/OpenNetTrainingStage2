using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BubbleTask
{
    public class RowsMinComparer : RowsComparer
    {
        protected override int RowMap(int[] row)
        {
            int Min = row[0];

            foreach (int elem in row)
            {
                if (elem < Min)
                    Min = elem;
            }

            return Min;
        }
    }
}
