using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareMatricesTask
{
    public class SymmetricMatrixLayout<T> : SquareMatrixLayoutBase<T>, ISquareMatrixLayout<T>
    {
        private T[][] data;

        public SymmetricMatrixLayout() : base()
        {
        }

        public SymmetricMatrixLayout(int length) : base(length)
        {
        }

        public SymmetricMatrixLayout(T[,] array) : base(array)
        {
        }

        public SymmetricMatrixLayout(T[,] array, int length) : base(array, length)
        {
        }

        public SymmetricMatrixLayout(ISquareMatrixLayout<T> layout) : base(layout)
        {
        }

        public SymmetricMatrixLayout(DiagonalMatrixLayout<T> diagLayout)
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

        public static implicit operator SymmetricMatrixLayout<T>(DiagonalMatrixLayout<T> diagLayout)
        {
            return new SymmetricMatrixLayout<T>(diagLayout);
        }

        public static explicit operator SymmetricMatrixLayout<T>(SquareMatrixLayout<T> squareLayout)
        {
            return new SymmetricMatrixLayout<T>(squareLayout);
        }

        public SymmetricMatrixLayout<V> CombineWith<U, V>(DiagonalMatrixLayout<U> other, Func<T, U, V> func)
        {
            int length = Math.Min(this.Length, other.Length);

            SymmetricMatrixLayout<V> result = new SymmetricMatrixLayout<V>(length);

            for (int row = 0; row < length; row++)
            {
                result.data[row][row] = func(this.data[row][row], other.GetValue(row, row));
            }

            return result;
        }

        public SymmetricMatrixLayout<V> CombineWith<U, V>(SymmetricMatrixLayout<U> other, Func<T, U, V> func)
        {
            int length = Math.Min(this.Length, other.Length);

            SymmetricMatrixLayout<V> result = new SymmetricMatrixLayout<V>(length);

            for (int row = 0; row < length; row++)
            {
                for (int col = 0; col <= row; col++)
                {
                    result.data[row][col] = func(this.data[row][col], other.data[row][col]);
                }
            }

            return result;
        }

        public ISquareMatrixLayout<V> CombineWith<U, V>(ISquareMatrixLayout<U> other, Func<T, U, V> func, bool tryOther)
        {
            if (other is DiagonalMatrixLayout<U> diagLayout)
            {
                return this.CombineWith(diagLayout, func);
            }

            if (other is SymmetricMatrixLayout<U> symLayout)
            {
                return this.CombineWith(symLayout, func);
            }

            if (tryOther)
            {
                return other.CombineWith(this, (his, my) => func(my, his), false);
            }

            int length = Math.Min(this.Length, other.Length);

            SquareMatrixLayout<V> result = new SquareMatrixLayout<V>(length);

            for (int row = 0; row < length; row++)
            {
                result.SetValue(row, row, func(this.data[row][row], other.GetValue(row, row)));
            }

            for (int row = 1; row < length; row++)
            {
                for (int col = 0; col < row; col++)
                {
                    result.SetValue(row, col, func(this.data[row][col], other.GetValue(row, col)));
                    result.SetValue(col, row, func(this.data[row][col], other.GetValue(col, row)));
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
