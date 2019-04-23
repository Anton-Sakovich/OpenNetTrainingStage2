using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareMatricesTask
{
    public abstract class SquareMatrixBase<T> : ISquareMatrix<T>
    {
        protected SquareMatrixBase()
        {
            InitializeArray(0);
        }

        protected SquareMatrixBase(int length)
        {
            if (length < 0)
            {
                throw new ArgumentException("Matrix size must be non-negative.", nameof(length));
            }

            InitializeArray(length);
        }

        protected SquareMatrixBase(T[,] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException();
            }

            int length = Math.Max(array.GetLength(0), array.GetLength(1));

            InitializeArray(length);

            CopyArray(array, length);
        }

        protected SquareMatrixBase(T[,] array, int length)
        {
            if (array == null)
            {
                throw new ArgumentNullException();
            }

            if (length < Math.Min(array.GetLength(0), array.GetLength(1)))
            {
                throw new ArgumentException("The size provided is less than the array's minimal length.");
            }

            InitializeArray(length);

            CopyArray(array, length);
        }

        public event EventHandler<MatrixElementChangedEventArgs> MatrixElementChanged;

        public T this[int row, int col]
        {
            get
            {
                try
                {
                    return GetValue(row, col);
                }
                catch (IndexOutOfRangeException ex)
                {
                    throw new IndexOutOfRangeException("Either row or col was ouside the bounds of the matrix.", ex);
                }
            }

            set
            {
                try
                {
                    SetValue(row, col, value);
                    OnMatrixElementChanged(row, col);
                }
                catch (IndexOutOfRangeException ex)
                {
                    throw new IndexOutOfRangeException("Either row or col was ouside the bounds of the matrix.", ex);
                }
            }
        }

        protected abstract void InitializeArray(int length);

        protected abstract void CopyArray(T[,] array, int length);

        protected abstract T GetValue(int row, int col);

        protected abstract void SetValue(int row, int col, T value);

        protected virtual void OnMatrixElementChanged(int row, int col)
        {
            MatrixElementChanged?.Invoke(this, new MatrixElementChangedEventArgs(row, col));
        }
    }
}
