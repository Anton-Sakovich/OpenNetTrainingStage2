using System;
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
    }
}
