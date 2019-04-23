using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueTask.Tests
{
    [TestFixture]
    public class SimpleQueueTests
    {
        [Test]
        public void SimpleQueue_InitializedAsEmptyEnumerable_Test()
        {
            SimpleQueue<int> queue1 = new SimpleQueue<int>();

            Assert.That(queue1.ToArray(), Is.EqualTo(new int[] { }));
        }

        [Test]
        public void Enqueue_AddsElementsFIFO_Test()
        {
            SimpleQueue<int> queue1 = new SimpleQueue<int>();

            queue1.Enqueue(1);
            queue1.Enqueue(2);
            queue1.Enqueue(3);

            Assert.That(queue1.ToArray(), Is.EqualTo(new int[] { 1, 2, 3 }));
        }

        [Test]
        public void Peek_ShowsTheLastElement_WithoutRemoving_Test()
        {
            SimpleQueue<int> queue1 = new SimpleQueue<int>();

            queue1.Enqueue(1);
            queue1.Enqueue(2);
            queue1.Enqueue(3);

            Assert.That(queue1.Peek(), Is.EqualTo(1));
            Assert.That(queue1.ToArray(), Is.EqualTo(new int[] { 1, 2, 3 }));
        }

        [Test]
        public void Dequeue_RemovesTheFirstElement_Test()
        {
            SimpleQueue<int> queue1 = new SimpleQueue<int>();

            queue1.Enqueue(1);
            queue1.Enqueue(2);
            queue1.Enqueue(3);

            Assert.That(queue1.Dequeue, Is.EqualTo(1));
            Assert.That(queue1.ToArray(), Is.EqualTo(new int[] { 2, 3 }));
        }

        [Test]
        public void Count_ShowsTheNumberOfElements_Test()
        {
            SimpleQueue<int> queue1 = new SimpleQueue<int>();
            Assert.That(queue1.Count, Is.EqualTo(0));

            queue1.Enqueue(1);
            Assert.That(queue1.Count, Is.EqualTo(1));

            queue1.Enqueue(2);
            Assert.That(queue1.Count, Is.EqualTo(2));

            queue1.Enqueue(3);
            Assert.That(queue1.Count, Is.EqualTo(3));

            queue1.Peek();
            Assert.That(queue1.Count, Is.EqualTo(3));

            queue1.Dequeue();
            Assert.That(queue1.Count, Is.EqualTo(2));

            queue1.Dequeue();
            Assert.That(queue1.Count, Is.EqualTo(1));

            queue1.Dequeue();
            Assert.That(queue1.Count, Is.EqualTo(0));
        }

        [Test]
        public void Peek_EmptyQueueExceptionThrown()
        {
            SimpleQueue<int> queue1 = new SimpleQueue<int>();

            Assert.That(() => queue1.Peek(), Throws.TypeOf<InvalidOperationException>());

            queue1.Enqueue(1);
            queue1.Dequeue();

            Assert.That(() => queue1.Peek(), Throws.TypeOf<InvalidOperationException>());
        }
    }
}
