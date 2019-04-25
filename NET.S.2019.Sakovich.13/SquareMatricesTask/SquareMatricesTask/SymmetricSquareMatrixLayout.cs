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

        public SymmetricSquareMatrixLayout(DiagonalSquareMatrixLayout<T> diagLayout)
        {
            if (diagLayout == null)
            {
                throw new ArgumentNullException(nameof(diagLayout));
            }

            InitializeLayout(diagLayout.Length);

            for (int row = 0; row < diagLayout.Length; row++)
            {
                data[row][row] = diagLayout.GetValue(row, row);
            }
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
                return new SquareMatrixLayout<T>(this).SetValue(row, col, value);
            }
        }

        public T[,] ToArray()
        {
            T[,] array = new T[Length, Length];

            for (int i = 0; i < Length; i++)
            {
                array[i, i] = data[i][i];
            }

            for (int r = 0; r < Length; r++)
            {
                for (int c = 0; c < r; c++)
                {
                    array[r, c] = array[c, r] = data[r][c];
                }
            }

            return array;
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
