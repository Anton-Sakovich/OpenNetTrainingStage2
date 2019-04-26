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
    }
}
