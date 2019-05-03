using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareMatricesTask.Tests
{
    [TestFixture]
    public class SquareMatrixTests
    {
        [Test]
        public void Item_Test()
        {
            SquareMatrix<int> matrix =
                new SquareMatrix<int>(new DiagonalMatrixLayout<int>(2));

            Assert.That(matrix[0, 0], Is.EqualTo(0));
            Assert.That(matrix[0, 1], Is.EqualTo(0));
            Assert.That(matrix[1, 0], Is.EqualTo(0));
            Assert.That(matrix[1, 1], Is.EqualTo(0));
            Assert.That(matrix.Layout, Is.TypeOf<DiagonalMatrixLayout<int>>());

            matrix[0, 0] = 1;
            matrix[1, 1] = 2;

            Assert.That(matrix[0, 0], Is.EqualTo(1));
            Assert.That(matrix[0, 1], Is.EqualTo(0));
            Assert.That(matrix[1, 0], Is.EqualTo(0));
            Assert.That(matrix[1, 1], Is.EqualTo(2));
            Assert.That(matrix.Layout, Is.TypeOf<DiagonalMatrixLayout<int>>());

            matrix[0, 1] = 100;

            Assert.That(matrix[0, 0], Is.EqualTo(1));
            Assert.That(matrix[0, 1], Is.EqualTo(100));
            Assert.That(matrix[1, 0], Is.EqualTo(0));
            Assert.That(matrix[1, 1], Is.EqualTo(2));
            Assert.That(matrix.Layout, Is.TypeOf<SquareMatrixLayout<int>>());
        }

        [Test]
        public void MatrixElementChanged_Test()
        {
            object eventSender = null;
            MatrixElementChangedEventArgs eventArgs = null;

            SquareMatrix<int> matrix = new SquareMatrix<int>(2);

            matrix.MatrixElementChanged += (sender, e) => { eventSender = sender; eventArgs = e; };

            matrix[0, 1] = 1;

            Assert.That(eventSender, Is.EqualTo(matrix));
            Assert.That(eventArgs?.Row, Is.EqualTo(0));
            Assert.That(eventArgs?.Col, Is.EqualTo(1));
        }

        [Test]
        public void CombineWith_Test()
        {
            SquareMatrix<int> diag = new SquareMatrix<int>(new DiagonalMatrixLayout<int>(new int[,] { { 1, 0 }, { 0, 2 } }));
            SquareMatrix<int> sym = new SquareMatrix<int>(new SymmetricMatrixLayout<int>(new int[,] { { 3, 4 }, { 4, 5 } }));
            SquareMatrix<int> square = new SquareMatrix<int>(new SquareMatrixLayout<int>(new int[,] { { 6, 7 }, { 9, 10 } }));

            SquareMatrix<int> symResult = diag.CombineWith(sym, (x, y) => x + y);
            SquareMatrix<int> squareResult = square.CombineWith(sym, (x, y) => x + y);

            Assert.That(symResult.Layout.ToArray(), Is.EqualTo(new int[,] { { 4, 4 }, { 4, 7 } }));
            Assert.That(symResult.Layout, Is.TypeOf<SymmetricMatrixLayout<int>>());

            Assert.That(squareResult.Layout.ToArray(), Is.EqualTo(new int[,] { { 9, 11 }, { 13, 15 } }));
            Assert.That(squareResult.Layout, Is.TypeOf<SquareMatrixLayout<int>>());
        }
    }
}
