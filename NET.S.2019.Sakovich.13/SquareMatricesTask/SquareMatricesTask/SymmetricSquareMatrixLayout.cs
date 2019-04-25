using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareMatricesTask
{
    public class SymmetricSquareMatrixLayout<T> : SquareMatrixLayoutBase<T>, ISquareMatrixLayout<T>
    {
        private T[][] data;

        public SymmetricSquareMatrixLayout() : base()
        {
        }

        public SymmetricSquareMatrixLayout(int length) : base(length)
        {
        }

        public SymmetricSquareMatrixLayout(T[,] array) : base(array)
        {
        }

        public SymmetricSquareMatrixLayout(T[,] array, int length) : base(array, length)
        {
        }

        public int Length
        {
            get
            {
                return data.Length;
            }
        }

        public T GetValue(int row, int col)
        {
            if (col < row)
            {
                return data[row][col];
            }
            else
            {
                return data[col][row];
            }
        }

        public ISquareMatrixLayout<T> SetValue(int row, int col, T value)
        {
            if (row == col)
            {
                data[row][col] = value;
                return this;
            }
            else
            {
                T[,] newData = new T[data.Length, data.Length];

                for (int i = 0; i < data.Length; i++)
                {
                    newData[i, i] = data[i][i];
                }

                for (int r = 0; r < data.Length; r++)
                {
                    for (int c = 0; c < r; c++)
                    {
                        newData[r, c] = newData[c, r] = data[r][c];
                    }
                }

                newData[row, col] = newData[col, row] = value;

                return new SquareMatrixLayout<T>(newData);
            }
        }

        protected override void InitializeLayout(int length)
        {
            data = new T[length][];
            for (int row = 0; row < length; row++)
            {
                data[row] = new T[row + 1];
            }
        }

        protected override void BuildLayout(T[,] array)
        {
            for (int row = 0; row < Length; row++)
            {
                data[row][row] = array[row, row];

                for (int col = 0; col < row; col++)
                {
                    data[row][col] = array[row, col];
                }
            }
        }
    }
}
