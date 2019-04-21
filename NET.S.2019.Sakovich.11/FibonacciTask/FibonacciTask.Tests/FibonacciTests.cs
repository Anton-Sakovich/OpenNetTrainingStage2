using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FibonacciTask.Tests
{
    [TestFixture]
    public class FibonacciTests
    {
        public static IEnumerable<TestCaseData> TestData
        {
            get
            {
                yield return new TestCaseData(1, new int[] { 1, 1 });
                yield return new TestCaseData(2, new int[] { 1, 1, 2 });
                yield return new TestCaseData(3, new int[] { 1, 1, 2, 3 });
                yield return new TestCaseData(4, new int[] { 1, 1, 2, 3 });
                yield return new TestCaseData(5, new int[] { 1, 1, 2, 3, 5 });
                yield return new TestCaseData(6, new int[] { 1, 1, 2, 3, 5 });
                yield return new TestCaseData(7, new int[] { 1, 1, 2, 3, 5 });
                yield return new TestCaseData(8, new int[] { 1, 1, 2, 3, 5, 8 });
                yield return new TestCaseData(9, new int[] { 1, 1, 2, 3, 5, 8 });
            }
        }

        [TestCaseSource(nameof(TestData))]
        public void Enumaration_Test(int max, int[] expected)
        {
            int[] actual = new Fibonacci(max).ToArray();

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void MoveNext_ReturnValue_Test()
        {
            IEnumerator<int> fib = new Fibonacci(1);

            Assert.That(fib.MoveNext(), Is.True); // 1
            Assert.That(fib.MoveNext(), Is.True); // 1
            Assert.That(fib.MoveNext(), Is.False); // 2
            Assert.That(fib.MoveNext(), Is.False); // 2
        }

        [Test]
        public void InvalidBound_ArgumentExceptionThrown_Test()
        {
            Assert.That(() => new Fibonacci(0), Throws.TypeOf<ArgumentException>());
            Assert.That(() => new Fibonacci(-1), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void InvalidCurrent_InvalidOperationExceptionThrown_Test()
        {
            IEnumerator<int> fib = new Fibonacci(1);

            Assert.That(() => fib.Current, Throws.TypeOf<InvalidOperationException>());

            fib.MoveNext(); // 1, true
            fib.MoveNext(); // 1, true
            fib.MoveNext(); // 2, false

            Assert.That(() => fib.Current, Throws.TypeOf<InvalidOperationException>());
        }
    }
}
