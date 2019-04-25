using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareMatricesTask
{
    public interface ISquareMatrixLayout<T>
    {
        int Length { get; }

        T[,] ToArray();

        T GetValue(int row, int col);

        ISquareMatrixLayout<T> SetValue(int row, int col, T value);
    }
}
