using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BubbleTask
{
    public abstract class RowsComparer : IComparer<int[]>
    {
        protected abstract int RowMap(int[] row);

        public int Compare(int[] x, int[] y)
        {
            if (x == null)
                throw new ArgumentNullException(nameof(x));

            if (y == null)
                throw new ArgumentNullException(nameof(y));

            if (x.Length == 0)
                throw new ArgumentException("Array compared must contain at least one element.");

            if (y.Length == 0)
                throw new ArgumentException("Array compared must contain at least one element.");

            return RowMap(x) - RowMap(y);
        }
    }
}
