using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareMatricesTask
{
    public interface ISquareMatrix<T>
    {
        T this[int row, int col] { get; set; }

        event EventHandler<MatrixElementChangedEventArgs> MatrixElementChanged;
    }
}
