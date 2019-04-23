using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareMatricesTask
{
    public class DiagonalMatrix<T> : SquareMatrixBase<T>
    {
        private T[] data;

        public DiagonalMatrix() : base()
        {
        }

        public DiagonalMatrix(int length) : base(length)
        {
        }

        public DiagonalMatrix(T[,] array) : base(array)
        {
        }

        public DiagonalMatrix(T[,] array, int length) : base(array, length)
        {
        }

        protected override void InitializeArray(int length)
        {
            data = new T[length];
        }

        protected override void CopyArray(T[,] array, int length)
        {
            for (int i = 0; i < length; i++)
            {
                data[i] = array[i, i];
            }
        }

        protected override T GetValue(int row, int col)
        {
            if (row == col)
            {
                return data[row];
            }
            else
            {
                return default(T);
            }
        }

        protected override void SetValue(int row, int col, T value)
        {
            if (row == col)
            {
                data[row] = value;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}
