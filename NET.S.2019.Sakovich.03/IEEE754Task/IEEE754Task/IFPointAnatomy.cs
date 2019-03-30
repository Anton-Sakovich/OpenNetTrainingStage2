using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEEE754Task
{
    public interface IFPointAnatomy<T>
    {
        int FullLength { get; }

        int MantissaLength { get; }

        int ExponentLength { get; }

        int ExponentShift { get; }

        T Zero { get; }

        T One { get; }

        T MinNormalNumber { get; }
    }
}
