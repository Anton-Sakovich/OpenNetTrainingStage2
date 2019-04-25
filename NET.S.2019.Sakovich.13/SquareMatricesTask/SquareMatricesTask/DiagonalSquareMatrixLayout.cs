﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareMatricesTask
{
    public class DiagonalSquareMatrixLayout<T> : SquareMatrixLayoutBase<T>, ISquareMatrixLayout<T>
    {
        private T[] data;

        public DiagonalSquareMatrixLayout() : base()
        {
        }

        public DiagonalSquareMatrixLayout(int length) : base(length)
        {
        }

        public DiagonalSquareMatrixLayout(T[,] array) : base(array)
        {
        }

        public DiagonalSquareMatrixLayout(T[,] array, int length) : base(array, length)
        {
        }

        public DiagonalSquareMatrixLayout(ISquareMatrixLayout<T> layout) : base(layout)
        {
        }

        public static explicit operator DiagonalSquareMatrixLayout<T>(SquareMatrixLayout<T> squareLayout)
        {
            return new DiagonalSquareMatrixLayout<T>(squareLayout);
        }

        public static explicit operator DiagonalSquareMatrixLayout<T>(SymmetricSquareMatrixLayout<T> symLayout)
        {
            return new DiagonalSquareMatrixLayout<T>(symLayout);
        }

        public DiagonalSquareMatrixLayout<V> CombineWith<U, V>(DiagonalSquareMatrixLayout<U> other, Func<T, U, V> func)
        {
            DiagonalSquareMatrixLayout<V> result =
                new DiagonalSquareMatrixLayout<V>(Math.Min(this.Length, other.Length));

            for (int row = 0; row < result.Length; row++)
            {
                result.data[row] = func(this.data[row], other.data[row]);
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
