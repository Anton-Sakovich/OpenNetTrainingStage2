using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareMatricesTask
{
    public class SquareMatrix<T> : SquareMatrixBase<T>
    {
        private T[,] data;

        public SquareMatrix() : base()
        {
        }

        public SquareMatrix(int length) : base(length)
        {
        }

        public SquareMatrix(T[,] array) : base(array)
        {
        }

        public SquareMatrix(T[,] array, int length) : base(array, length)
        {
        }

        protected override void InitializeArray(int length)
        {
            data = new T[length, length];
        }

        protected override void CopyArray(T[,] array, int length)
        {
            for (int row = 0; row < length; row++)
            {
                for (int col = 0; col < length; col++)
                {
                    data[row, col] = array[row, col];
                }
            }
        }

        protected override T GetValue(int row, int col)
        {
            return data[row, col];
        }

        protected override void SetValue(int row, int col, T value)
        {
            data[row, col] = value;
        }
    }
}
