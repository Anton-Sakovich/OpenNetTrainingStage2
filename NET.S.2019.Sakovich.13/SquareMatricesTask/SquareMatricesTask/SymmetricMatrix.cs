using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareMatricesTask
{
    public class SymmetricMatrix<T> : SquareMatrixBase<T>
    {
        private T[][] data;

        public SymmetricMatrix() : base()
        {
        }

        public SymmetricMatrix(int length) : base(length)
        {
        }

        public SymmetricMatrix(T[,] array) : base(array)
        {
        }

        public SymmetricMatrix(T[,] array, int length) : base(array, length)
        {
        }

        protected override void InitializeArray(int length)
        {
            data = new T[length][];
            for (int row = 0; row < length; row++)
            {
                data[row] = new T[row + 1];
            }
        }

        protected override void CopyArray(T[,] array, int length)
        {
            for (int row = 0; row < length; row++)
            {
                data[row][row] = array[row, row];

                for (int col = row - 1; col >= 0; col--)
                {
                    data[row][col] = array[row, col];
                }
            }
        }

        protected override T GetValue(int row, int col)
        {
            if (row >= col)
            {
                return data[row][col];
            }
            else
            {
                return data[col][row];
            }
        }

        protected override void SetValue(int row, int col, T value)
        {
            if (row >= col)
            {
                data[row][col] = value;
            }
            else
            {
                data[col][row] = value;
            }
        }
    }
}
