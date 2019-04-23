using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareMatricesTask
{
    public class MatrixElementChangedEventArgs : EventArgs
    {
        public int Row { get; }
        public int Col { get; }

        public MatrixElementChangedEventArgs(int row, int col)
        {
            Row = row;
            Col = col;
        }
    }
}
