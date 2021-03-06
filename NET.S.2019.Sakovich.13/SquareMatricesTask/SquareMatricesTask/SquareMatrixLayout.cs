﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareMatricesTask
{
    public class SquareMatrixLayout<T> : SquareMatrixLayoutBase<T>, ISquareMatrixLayout<T>
    {
        private T[,] data;

        public SquareMatrixLayout() : base()
        {
        }

        public SquareMatrixLayout(int length) : base(length)
        {
        }

        public SquareMatrixLayout(T[,] array) : base(array)
        {
        }

        public SquareMatrixLayout(T[,] array, int length) : base(array, length)
        {
        }

        public SquareMatrixLayout(ISquareMatrixLayout<T> layout) : base(layout)
        {
        }

        public SquareMatrixLayout(DiagonalMatrixLayout<T> diagLayout)
        {
            if (diagLayout == null)
            {
                throw new ArgumentNullException(nameof(diagLayout));
            }

            InitializeLayout(diagLayout.Length);

            for (int row = 0; row < diagLayout.Length; row++)
            {
                data[row, row] = diagLayout.GetValue(row, row);
            }
        }

        public SquareMatrixLayout(SymmetricMatrixLayout<T> symLayout)
        {
            if (symLayout == null)
            {
                throw new ArgumentNullException(nameof(symLayout));
            }

            InitializeLayout(symLayout.Length);

            for (int row = 0; row < symLayout.Length; row++)
            {
                data[row, row] = symLayout.GetValue(row, row);
            }

            for (int row = 1; row < symLayout.Length; row++)
            {
                for (int col = 0; col < row; col++)
                {
                    data[row, col] = data[col, row] = symLayout.GetValue(row, col);
                }
            }
        }

        public static implicit operator SquareMatrixLayout<T>(SymmetricMatrixLayout<T> symLayout)
        {
            return new SquareMatrixLayout<T>(symLayout);
        }

        public static implicit operator SquareMatrixLayout<T>(DiagonalMatrixLayout<T> diagLayout)
        {
            return new SquareMatrixLayout<T>(diagLayout);
        }

        public SquareMatrixLayout<V> CombineWith<U, V>(DiagonalMatrixLayout<U> other, Func<T, U, V> func)
        {
            int length = Math.Min(this.Length, other.Length);

            SquareMatrixLayout<V> result = new SquareMatrixLayout<V>(length);

            for (int row = 0; row < length; row++)
            {
                result.data[row, row] = func(this.data[row, row], other.GetValue(row, row));
            }

            for (int row = 1; row < length; row++)
            {
                for (int col = 0; col < row; col++)
                {
                    result.data[row, col] = func(this.data[row, col], default(U));
                    result.data[col, row] = func(this.data[col, row], default(U));
                }
            }

            return result;
        }

        public SquareMatrixLayout<V> CombineWith<U, V>(SymmetricMatrixLayout<U> other, Func<T, U, V> func)
        {
            int length = Math.Min(this.Length, other.Length);

            SquareMatrixLayout<V> result = new SquareMatrixLayout<V>(length);

            for (int row = 0; row < length; row++)
            {
                result.data[row, row] = func(this.data[row, row], other.GetValue(row, row));
            }

            for (int row = 1; row < length; row++)
            {
                for (int col = 0; col < row; col++)
                {
                    U otherVal = other.GetValue(row, col);
                    result.data[row, col] = func(this.data[row, col], otherVal);
                    result.data[col, row] = func(this.data[col, row], otherVal);
                }
            }

            return result;
        }

        public SquareMatrixLayout<V> CombineWith<U, V>(SquareMatrixLayout<U> other, Func<T, U, V> func)
        {
            int length = Math.Min(this.Length, other.Length);

            SquareMatrixLayout<V> result = new SquareMatrixLayout<V>(length);

            for (int row = 0; row < length; row++)
            {
                for (int col = 0; col < length; col++)
                {
                    result.data[row, col] = func(this.data[row, col], other.data[row, col]);
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

            if (other is SquareMatrixLayout<U> squareLayout)
            {
                return this.CombineWith(squareLayout, func);
            }

            if (tryOther)
            {
                return other.CombineWith(this, (his, my) => func(my, his), false);
            }

            int length = Math.Min(this.Length, other.Length);

            SquareMatrixLayout<V> result = new SquareMatrixLayout<V>(length);

            for (int row = 0; row < length; row++)
            {
                for (int col = 0; col < length; col++)
                {
                    result.data[row, col] = func(this.data[row, col], other.GetValue(row, col));
                }
            }

            return result;
        }

        public int Length
        {
            get
            {
                return data.GetLength(0);
            }
        }

        public T GetValue(int row, int col)
        {
            return data[row, col];
        }

        public ISquareMatrixLayout<T> SetValue(int row, int col, T value)
        {
            data[row, col] = value;
            return this;
        }

        public T[,] ToArray()
        {
            return (T[,])data.Clone();
        }

        protected override void InitializeLayout(int length)
        {
            data = new T[length, length];
        }

        protected override void BuildLayout(T[,] array)
        {
            for (int row = 0; row < Length; row++)
            {
                for (int col = 0; col < Length; col++)
                {
                    data[row, col] = array[row, col];
                }
            }
        }

        protected override void BuildLayout(ISquareMatrixLayout<T> layout)
        {
            for (int row = 0; row < Length; row++)
            {
                for (int col = 0; col < Length; col++)
                {
                    data[row, col] = layout.GetValue(row, col);
                }
            }
        }
    }
}
