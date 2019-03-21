using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SortingTask.Tests
{
    [TestFixture]
    public class MergesortTests : SortTestBase
    {
        public MergesortTests() : base(new Mergesort())
        {

        }

        [Test]
        public void LightweightRandomTests()
        {
            RandomTests(1000, 15, 0, 100);
        }

        [Test]
        public void HeavyRandomTests()
        {
            RandomTests(1000, 10000, int.MinValue, int.MaxValue);
        }
    }
}
