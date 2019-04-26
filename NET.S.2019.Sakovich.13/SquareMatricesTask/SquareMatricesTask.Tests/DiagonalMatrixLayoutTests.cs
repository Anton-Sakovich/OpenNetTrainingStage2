using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareMatricesTask.Tests
{
    public class DiagonalMatrixLayoutTests : ISquareMatrixLayoutTests<int>
    {
        [Test]
        public void Ctor_Empty_Test()
        {
            TestCtor();
        }

        [Test]
        public void Ctor_PositiveLength_Test()
        {
            TestCtor(2);
        }

        [Test]
        public void Ctor_ExplicitArray_Test()
        {
            int[,] array = new int[,] { { 1, 2 }, { 3, 4 } };
            int[,] expected = new int[,] { { 1, 0 }, { 0, 4 } };

            TestCtor(array, expected);
        }

        [Test]
        public void Ctor_ExplicitArrayAndLength_Test()
        {
            int[,] array = new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            int[,] expected = new int[,] { { 1, 0 }, { 0, 5 } };

            TestCtor(array, 2, expected);
        }

        [Test]
        public void SetValue_Tests()
        {
            int[,] initArray = new int[,] { { 1, 0 }, { 0, 2 } };

            int[][] points = new int[][]
            {
                new int[] {0, 0},
                new int[] {0, 1},
                new int[] {1, 0},
                new int[] {1, 1}
            };

            int[] setValues = new int[] { 100, 101, 102, 103 };
            int[] getValues = new int[] { 100, 0, 0, 103 };

            Type[] types = new Type[]
            {
                typeof(DiagonalMatrixLayout<int>),
                typeof(SquareMatrixLayout<int>),
                typeof(SquareMatrixLayout<int>),
                typeof(DiagonalMatrixLayout<int>)
            };

            TestTransitions(initArray, points, setValues, getValues, types);
        }

        protected override ISquareMatrixLayout<int> CreateSquareMatrixLayout()
        {
            return new DiagonalMatrixLayout<int>();
        }

        protected override ISquareMatrixLayout<int> CreateSquareMatrixLayout(int length)
        {
            return new DiagonalMatrixLayout<int>(length);
        }

        protected override ISquareMatrixLayout<int> CreateSquareMatrixLayout(int[,] array)
        {
            return new DiagonalMatrixLayout<int>(array);
        }

        protected override ISquareMatrixLayout<int> CreateSquareMatrixLayout(int[,] array, int length)
        {
            return new DiagonalMatrixLayout<int>(array, length);
        }
    }
}
