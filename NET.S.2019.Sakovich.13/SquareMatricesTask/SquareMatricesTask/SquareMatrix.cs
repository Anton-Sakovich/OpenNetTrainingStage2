using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareMatricesTask
{
    public class SquareMatrix<T>
    {
        public SquareMatrix(ISquareMatrixLayout<T> layout)
        {
            Layout = layout;
        }

        public event EventHandler<MatrixElementChangedEventArgs> MatrixElementChanged;

        public T this[int row, int col]
        {
            get
            {
                return Layout.GetValue(row, col);
            }

            set
            {
                Layout = Layout.SetValue(row, col, value);
                OnMatrixElementChanged(row, col);
            }
        }

        public ISquareMatrixLayout<T> Layout { get; private set; }

        protected virtual void OnMatrixElementChanged(int row, int col)
        {
            MatrixElementChanged?.Invoke(this, new MatrixElementChangedEventArgs(row, col));
        }
    }
}
