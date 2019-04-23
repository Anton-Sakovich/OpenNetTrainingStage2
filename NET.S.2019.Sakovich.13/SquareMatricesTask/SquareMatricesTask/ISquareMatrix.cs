using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareMatricesTask
{
    public interface ISquareMatrix<T>
    {
        event EventHandler<MatrixElementChangedEventArgs> MatrixElementChanged;

        T this[int row, int col] { get; set; }
    }
}
