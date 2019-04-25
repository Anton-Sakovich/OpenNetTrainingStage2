using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareMatricesTask
{
    public abstract class SquareMatrixLayoutBase<T>
    {
        public SquareMatrixLayoutBase()
        {
            InitializeLayout(0);
        }

        public SquareMatrixLayoutBase(int length)
        {
            if (length < 0)
            {
                throw new ArgumentException("SquareMatrixLayout must have non-negative length.", nameof(length));
            }

            InitializeLayout(length);
        }

        public SquareMatrixLayoutBase(T[,] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array), "The array provided for SquareMatrixLayout was null.");
            }

            int length = Math.Max(array.GetLength(0), array.GetLength(1));

            InitializeLayout(length);

            BuildLayout(array);
        }

        public SquareMatrixLayoutBase(T[,] array, int length)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array), "The array provided for SquareMatrixLayout was null.");
            }

            if (length > Math.Min(array.GetLength(0), array.GetLength(1)))
            {
                throw new ArgumentException("The length of SquareMatrixLayout was less than the length of the input array.", nameof(length));
            }

            InitializeLayout(length);

            BuildLayout(array);
        }

        protected abstract void InitializeLayout(int length);

        protected abstract void BuildLayout(T[,] array);
    }
}
