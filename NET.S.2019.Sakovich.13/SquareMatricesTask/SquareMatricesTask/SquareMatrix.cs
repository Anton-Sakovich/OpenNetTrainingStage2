using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareMatricesTask
{
    public class SquareMatrix<T>
    {
        public SquareMatrix() : this(new SquareMatrixLayout<T>())
        {
        }

        public SquareMatrix(int length) : this(new SquareMatrixLayout<T>(length))
        {
        }

        public SquareMatrix(T[,] array) : this(new SquareMatrixLayout<T>(array))
        {
        }

        public SquareMatrix(T[,] array, int length) : this(new SquareMatrixLayout<T>(array, length))
        {
        }

        public SquareMatrix(ISquareMatrixLayout<T> layout)
        {
            Layout = layout;
        }

        public event EventHandler<MatrixElementChangedEventArgs> MatrixElementChanged;

        public ISquareMatrixLayout<T> Layout { get; private set; }

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

        protected virtual void OnMatrixElementChanged(int row, int col)
        {
            MatrixElementChanged?.Invoke(this, new MatrixElementChangedEventArgs(row, col));
        }
    }
}
