using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueTask
{
    /// <summary>
    /// Represents a simple generic FIFO data structure.
    /// </summary>
    /// <typeparam name="T">The type of elements in the queue.</typeparam>
    public class SimpleQueue<T> : IEnumerable<T>
    {
        private LinkedList<T> list;

        /// <summary>
        /// Creates a new empty queue.
        /// </summary>
        public SimpleQueue()
        {
            list = new LinkedList<T>();
        }

        /// <summary>
        /// Gets the number of elements in the queue.
        /// </summary>
        public int Count
        {
            get
            {
                return list.Count;
            }
        }

        /// <summary>
        /// Appends a specified element to the queue.
        /// </summary>
        /// <param name="value">The value to append.</param>
        public void Enqueue(T value)
        {
            list.AddLast(value);
        }

        /// <summary>
        /// Returns the first element in the queue without removing it form the queue.
        /// </summary>
        /// <returns>The first element in the queue</returns>
        /// <exception cref="InvalidOperationException">Thrown when the queue is empty.</exception>
        public T Peek()
        {
            if (list.First == null)
            {
                throw new InvalidOperationException("The queue is empty.");
            }

            return list.First.Value;
        }

        /// <summary>
        /// Removes the first element in the queue and returns it.
        /// </summary>
        /// <returns>The first element in the queue before the removal.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the queue is empty.</exception>
        public T Dequeue()
        {
            T first = Peek();

            list.RemoveFirst();

            return first;
        }

        /// <summary>
        /// Returns an enumerator for the queue.
        /// </summary>
        /// <returns>Enumerator for the queue</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return new LinkedListEnumerator<T>(list);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
