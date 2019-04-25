using System;
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
                T[,] newData = new T[Length, Length];

                for (int i = 0; i < Length; i++)
                {
                    newData[i, i] = data[i];
                }

                newData[row, col] = value;

                return new SquareMatrixLayout<T>(newData);
            }
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
    }
}
