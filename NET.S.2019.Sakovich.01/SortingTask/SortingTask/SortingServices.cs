using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingTask
{
    public interface ISortingEngine<T>
    {
        void Sort(T[] array);
    }
}
