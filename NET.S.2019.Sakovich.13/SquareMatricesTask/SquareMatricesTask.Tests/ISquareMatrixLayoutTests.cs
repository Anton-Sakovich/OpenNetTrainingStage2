using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareMatricesTask.Tests
{
    public abstract class ISquareMatrixLayoutTests<T>
    {
        protected abstract ISquareMatrixLayout<T> CreateSquareMatrixLayout();

        protected abstract ISquareMatrixLayout<T> CreateSquareMatrixLayout(int length);

        protected abstract ISquareMatrixLayout<T> CreateSquareMatrixLayout(T[,] array);

        protected abstract ISquareMatrixLayout<T> CreateSquareMatrixLayout(T[,] array, int length);

        protected void TestCtor()
        {
            ISquareMatrixLayout<T> layout = CreateSquareMatrixLayout();

            Assert.That(layout.Length, Is.EqualTo(0));
        }

        protected void TestCtor(int length)
        {
            ISquareMatrixLayout<T> layout = CreateSquareMatrixLayout(length);

            Assert.That(layout.Length, Is.EqualTo(length));

            for (int row = 0; row < length; row++)
            {
                for (int col = 0; col < length; col++)
                {
                    Assert.That(layout.GetValue(row, col), Is.EqualTo(default(T)));
                }
            }
        }

        protected void TestCtor(T[,] array, T[,] expected)
        {
            ISquareMatrixLayout<T> layout = CreateSquareMatrixLayout(array);

            int expectedLength = Math.Min(array.GetLength(0), array.GetLength(1));

            Assert.That(layout.Length, Is.EqualTo(expectedLength));

            for (int row = 0; row < layout.Length; row++)
            {
                for (int col = 0; col < layout.Length; col++)
                {
                    Assert.That(layout.GetValue(row, col), Is.EqualTo(expected[row, col]));
                }
            }
        }

        protected void TestCtor(T[,] array, int length, T[,] expected)
        {
            ISquareMatrixLayout<T> layout = CreateSquareMatrixLayout(array, length);

            Assert.That(layout.Length, Is.EqualTo(length));

            for (int row = 0; row < layout.Length; row++)
            {
                for (int col = 0; col < layout.Length; col++)
                {
                    Assert.That(layout.GetValue(row, col), Is.EqualTo(expected[row, col]));
                }
            }
        }

        protected void TestTransitions(T[,] initArray, int[][] points, T[] setValues, T[] getValues, Type[] types)
        {
            ISquareMatrixLayout<T> layout = CreateSquareMatrixLayout(initArray);

            for (int i = 0; i < points.Length; i++)
            {
                Assert.That(layout.SetValue(points[i][0], points[i][1], setValues[i]), Is.TypeOf(types[i]));
                Assert.That(layout.GetValue(points[i][0], points[i][1]), Is.EqualTo(getValues[i]));
            }
        }
    }
}
