using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueTask.Tests
{
    [TestFixture]
    public class LinkedListEnumeratorTests
    {
        public static IEnumerable<TestCaseData> TestData
        {
            get
            {
                yield return new TestCaseData(new LinkedList<int> { 1 });
                yield return new TestCaseData(new LinkedList<int> { 1, 2 });
                yield return new TestCaseData(new LinkedList<int> { 1, 2, 3 });
            }
        }

        public static IEnumerable<T> AsEnum<T>(IEnumerator<T> enumerator)
        {
            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }

        [TestCaseSource(nameof(TestData))]
        public void NonEmptyList_IsEnumerated_Test<T>(LinkedList<T> list)
        {
            Assert.That(
                AsEnum(new LinkedListEnumerator<T>(list)).ToArray(),
                Is.EqualTo(list.ToArray()));
        }

        [Test]
        public void EmptyList_ResultsInEmptyEnumeration_Test()
        {
            Assert.That(
                AsEnum(new LinkedListEnumerator<int>(new LinkedList<int> { })).ToArray(),
                Is.EqualTo(new int[] { }));
        }

        [Test]
        public void NullList_ResultsInEmptyEnumeration_Test()
        {
            Assert.That(
                AsEnum(new LinkedListEnumerator<int>(null)).ToArray(),
                Is.EqualTo(new int[] { }));
        }
    }
}
