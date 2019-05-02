using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareMatricesTask
{
    public class DiagonalMatrixLayout<T> : SquareMatrixLayoutBase<T>, ISquareMatrixLayout<T>
    {
        private T[] data;

        public DiagonalMatrixLayout() : base()
        {
        }

        public DiagonalMatrixLayout(int length) : base(length)
        {
        }

        public DiagonalMatrixLayout(T[,] array) : base(array)
        {
        }

        public DiagonalMatrixLayout(T[,] array, int length) : base(array, length)
        {
        }

        public DiagonalMatrixLayout(ISquareMatrixLayout<T> layout) : base(layout)
        {
        }

        public static explicit operator DiagonalMatrixLayout<T>(SquareMatrixLayout<T> squareLayout)
        {
            return new DiagonalMatrixLayout<T>(squareLayout);
        }

        public static explicit operator DiagonalMatrixLayout<T>(SymmetricMatrixLayout<T> symLayout)
        {
            return new DiagonalMatrixLayout<T>(symLayout);
        }

        public DiagonalMatrixLayout<V> CombineWith<U, V>(DiagonalMatrixLayout<U> other, Func<T, U, V> func)
        {
            int length = Math.Min(this.Length, other.Length);

            DiagonalMatrixLayout<V> result = new DiagonalMatrixLayout<V>(length);

            for (int row = 0; row < length; row++)
            {
                result.data[row] = func(this.data[row], other.data[row]);
            }

            return result;
        }

        public ISquareMatrixLayout<V> CombineWith<U, V>(ISquareMatrixLayout<U> other, Func<T, U, V> func, bool tryOther)
        {
            if (other is DiagonalMatrixLayout<U> diagLayout)
            {
                return this.CombineWith(diagLayout, func);
            }

            if (tryOther)
            {
                return other.CombineWith(this, (his, my) => func(my, his), false);
            }

            int length = Math.Min(this.Length, other.Length);

            SquareMatrixLayout<V> result = new SquareMatrixLayout<V>(length);

            for (int row = 0; row < length; row++)
            {
                result.SetValue(row, row, func(this.data[row], other.GetValue(row, row)));
            }

            for (int row = 1; row < length; row++)
            {
                for (int col = 0; col < row; col++)
                {
                    result.SetValue(row, col, func(default(T), other.GetValue(row, col)));
                    result.SetValue(col, row, func(default(T), other.GetValue(col, row)));
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
            if (row == col)
            {
                return data[row];
            }
            else
            {
                return default(T);
            }
        }

        public ISquareMatrixLayout<T> SetValue(int row, int col, T value)
        {
            if (row == col)
            {
                data[row] = value;
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
                array[i, i] = data[i];
            }

            return array;
        }

        protected override void InitializeLayout(int length)
        {
            data = new T[length];
        }

        protected override void BuildLayout(T[,] array)
        {
            for (int i = 0; i < Length; i++)
            {
                data[i] = array[i, i];
            }
        }

        protected override void BuildLayout(ISquareMatrixLayout<T> layout)
        {
            for (int row = 0; row < Length; row++)
            {
                data[row] = layout.GetValue(row, row);
            }
        }
    }
}
