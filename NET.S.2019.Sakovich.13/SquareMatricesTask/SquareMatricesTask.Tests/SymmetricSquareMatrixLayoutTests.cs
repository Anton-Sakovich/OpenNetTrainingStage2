using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareMatricesTask.Tests
{
    public class SymmetricSquareMatrixLayoutTests : ISquareMatrixLayoutTests<int>
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
            int[,] expected = new int[,] { { 0, 2 }, { 2, 3 } };

            TestCtor(array, expected);
        }

        [Test]
        public void Ctor_ExplicitArrayAndLength_Test()
        {
            int[,] array = new int[,] { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 }, { 9, 10, 11 } };
            int[,] expected = new int[,] { { 0, 3 }, { 3, 4 } };

            TestCtor(array, 2, expected);
        }

        [Test]
        public void SetValue_Tests()
        {
            int[,] initArray = new int[,] { { 0, 1 }, { 1, 2 } };

            int[][] points = new int[][]
            {
                new int[] {0, 0},
                new int[] {0, 1},
                new int[] {1, 0},
                new int[] {1, 1}
            };

            int[] setValues = new int[] { 100, 101, 102, 103 };
            int[] getValues = new int[] { 100, 1, 1, 103 };

            Type[] types = new Type[]
            {
                typeof(SymmetricSquareMatrixLayout<int>),
                typeof(SquareMatrixLayout<int>),
                typeof(SquareMatrixLayout<int>),
                typeof(SymmetricSquareMatrixLayout<int>)
            };

            TestTransitions(initArray, points, setValues, getValues, types);
        }

        [Test]
        public void Combine_WithSelf_Test()
        {
            SymmetricSquareMatrixLayout<int> sym1 =
                new SymmetricSquareMatrixLayout<int>(new int[,] { { 1, 2 }, { 2, 3 } });

            SymmetricSquareMatrixLayout<int> sym2 =
                new SymmetricSquareMatrixLayout<int>(new int[,] { { 4, 5 }, { 5, 6 } });

            SymmetricSquareMatrixLayout<int> result = sym1.CombineWith(sym2, (x, y) => x + y);

            Assert.That(result.ToArray(), Is.EqualTo(new int[,] { { 5, 7 }, { 7, 9 } }));
        }

        [Test]
        public void Combine_WithInterface_Test()
        {
            SymmetricSquareMatrixLayout<int> sym =
                new SymmetricSquareMatrixLayout<int>(new int[,] { { 1, 2 }, { 2, 3 } });

            DiagonalSquareMatrixLayout<int> diag =
                new DiagonalSquareMatrixLayout<int>(new int[,] { { 4, 5 }, { 6, 7 } });

            ISquareMatrixLayout<int> result = sym.CombineWith<int, int>(diag, (x, y) => x + y);

            Assert.That(result.ToArray(), Is.EqualTo(new int[,] { { 5, 2 }, { 2, 10 } }));
        }

        protected override ISquareMatrixLayout<int> CreateSquareMatrixLayout()
        {
            return new SymmetricSquareMatrixLayout<int>();
        }

        protected override ISquareMatrixLayout<int> CreateSquareMatrixLayout(int length)
        {
            return new SymmetricSquareMatrixLayout<int>(length);
        }

        protected override ISquareMatrixLayout<int> CreateSquareMatrixLayout(int[,] array)
        {
            return new SymmetricSquareMatrixLayout<int>(array);
        }

        protected override ISquareMatrixLayout<int> CreateSquareMatrixLayout(int[,] array, int length)
        {
            return new SymmetricSquareMatrixLayout<int>(array, length);
        }
    }
}
