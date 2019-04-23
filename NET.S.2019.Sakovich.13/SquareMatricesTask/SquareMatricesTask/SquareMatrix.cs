using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareMatricesTask
{
    public class SquareMatrix<T> : ISquareMatrix<T>
    {
        private readonly T[,] data;

        public SquareMatrix()
        {
            data = new T[0, 0];
        }

        public SquareMatrix(int size)
        {
            if (size < 0)
            {
                throw new ArgumentException("Matrix size must be non-negative.", nameof(size));
            }

            data = new T[size, size];
        }

        public SquareMatrix(T[,] array)
        {
            int size = Math.Max(array.GetLength(0), array.GetLength(1));
            data = new T[size, size];

            for (int r = 0; r < size; r++)
            {
                for (int c = 0; c < size; c++)
                {
                    data[r, c] = array[r, c];
                }
            }
        }

        public SquareMatrix(T[,] array, int size)
            : this(size)
        {
            try
            {
                for (int r = 0; r < size; r++)
                {
                    for (int c = 0; c < size; c++)
                    {
                        data[r, c] = array[r, c];
                    }
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                throw new ArgumentException("The size specified is less than the size of the input array.", nameof(size), ex);
            }
            // ArgumentException on a negative size will be thrown automatically by this(size)
        }

        public event EventHandler<MatrixElementChangedEventArgs> MatrixElementChanged;

        public T this[int row, int col]
        {
            get
            {
                try
                {
                    return data[row, col];
                }
                catch (IndexOutOfRangeException ex)
                {
                    throw new IndexOutOfRangeException("The index was outside its bounds.", ex);
                }
            }
            set
            {
                try
                {
                    data[row, col] = value;

                    OnMatrixElementChanged(row, col);
                }
                catch (IndexOutOfRangeException ex)
                {
                    throw new IndexOutOfRangeException("The index was outside its bounds.", ex);
                }
            }
        }

        private void OnMatrixElementChanged(int row, int col)
        {
            MatrixElementChanged?.Invoke(this, new MatrixElementChangedEventArgs(row, col));
        }
    }
}
