using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareMatricesTask.Tests
{
    [TestFixture]
    public class SquareMatrixLayoutTests : ISquareMatrixLayoutTests<int>
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
            int[,] array = new int[,] { { 0, 1 }, { 2, 3 } };
            int[,] expected = array;

            TestCtor(array, expected);
        }

        [Test]
        public void Ctor_ExplicitArrayAndLength_Test()
        {
            int[,] array = new int[,] { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 }, { 9, 10, 11 } };
            int[,] expected = new int[,] { { 0, 1 }, { 3, 4 } };

            TestCtor(array, 2, expected);
        }

        [Test]
        public void SetValue_Tests()
        {
            int[,] initArray = new int[,] { { 0, 1 }, { 2, 3 } };

            int[][] points = new int[][]
            {
                new int[] {0, 0},
                new int[] {0, 1},
                new int[] {1, 0},
                new int[] {1, 1}
            };

            int[] setValues = new int[] { 100, 101, 102, 103 };
            int[] getValues = setValues;

            Type[] types = new Type[]
            {
                typeof(SquareMatrixLayout<int>),
                typeof(SquareMatrixLayout<int>),
                typeof(SquareMatrixLayout<int>),
                typeof(SquareMatrixLayout<int>)
            };

            TestTransitions(initArray, points, setValues, getValues, types);
        }

        protected override ISquareMatrixLayout<int> CreateSquareMatrixLayout()
        {
            return new SquareMatrixLayout<int>();
        }

        protected override ISquareMatrixLayout<int> CreateSquareMatrixLayout(int length)
        {
            return new SquareMatrixLayout<int>(length);
        }

        protected override ISquareMatrixLayout<int> CreateSquareMatrixLayout(int[,] array)
        {
            return new SquareMatrixLayout<int>(array);
        }

        protected override ISquareMatrixLayout<int> CreateSquareMatrixLayout(int[,] array, int length)
        {
            return new SquareMatrixLayout<int>(array, length);
        }
    }
}
