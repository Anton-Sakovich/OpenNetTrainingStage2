using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SortingTask.Tests
{
    public class SortTestBase
    {
        private readonly ISortingEngine<int> TestedEngine;

        public SortTestBase(ISortingEngine<int> engine)
        {
            TestedEngine = engine;
        }

        private string ElementWiseToString(int[] array)
        {
            if (array.Length < 20)
            {
                return String.Concat(array.Select(i => i.ToString() + " ").ToArray());
            }
            else
            {
                return $"Array(Length = {array.Length})";
            }
        }

        protected void RandomTests(int testsCount, int maxLength, int minValue, int maxValue)
        {
            int[] TestArray;
            Random RandomGen = new Random();

            for(int i = 0; i < testsCount; i++)
            {
                TestArray = new int[RandomGen.Next(1, maxLength)];
                for (int j = 0; i < TestArray.Length; i++)
                    TestArray[j] = RandomGen.Next(minValue, maxValue);

                string InputString = ElementWiseToString(TestArray);
                TestedEngine.Sort(TestArray);
                string OutputString = ElementWiseToString(TestArray);

                Assert.That(TestArray.IsSorted(), Is.True, InputString + "\n" + OutputString);
            }
        }
    }
}
