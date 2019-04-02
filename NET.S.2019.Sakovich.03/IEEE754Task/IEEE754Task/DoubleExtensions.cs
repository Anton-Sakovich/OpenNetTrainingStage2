using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEEE754Task
{
    public static class DoubleExtensions
    {
        private static FPointAdapter Adapter = new FPointAdapter(new FPointAnatomy(64, 52));

        public static string ToIEEE754String(this double d)
        {
            return Adapter.BinaryString(d);
        }
    }
}
