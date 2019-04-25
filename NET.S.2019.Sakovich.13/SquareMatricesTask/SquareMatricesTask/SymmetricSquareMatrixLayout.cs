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

        public SymmetricSquareMatrixLayout(ISquareMatrixLayout<T> layout) : base(layout)
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

        public static implicit operator SymmetricSquareMatrixLayout<T>(DiagonalSquareMatrixLayout<T> diagLayout)
        {
            return new SymmetricSquareMatrixLayout<T>(diagLayout);
        }

        public static explicit operator SymmetricSquareMatrixLayout<T>(SquareMatrixLayout<T> squareLayout)
        {
            return new SymmetricSquareMatrixLayout<T>(squareLayout);
        }

        public SymmetricSquareMatrixLayout<V> CombineWith<U, V>(SymmetricSquareMatrixLayout<U> other, Func<T, U, V> func)
        {
            SymmetricSquareMatrixLayout<V> result =
                new SymmetricSquareMatrixLayout<V>(Math.Min(this.Length, other.Length));

            for (int row = 0; row < result.Length; row++)
            {
                for (int col = 0; col <= row; col++)
                {
                    result.data[row][col] = func(this.data[row][col], other.data[row][col]);
                }
            }

            return result;
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
                for (int col = 0; col <= row; col++)
                {
                    data[row][col] = array[row, col];
                }
            }
        }

        protected override void BuildLayout(ISquareMatrixLayout<T> layout)
        {
            for (int row = 0; row < Length; row++)
            {
                for (int col = 0; col <= row; col++)
                {
                    data[row][col] = layout.GetValue(row, col);
                }
            }
        }
    }
}
