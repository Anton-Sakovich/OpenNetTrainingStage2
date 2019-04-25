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

        public SquareMatrixLayout(SymmetricSquareMatrixLayout<T> symLayout)
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

        public SquareMatrixLayout(DiagonalSquareMatrixLayout<T> diagLayout)
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
    }
}
