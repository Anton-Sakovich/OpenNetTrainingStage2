using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueTask
{
    public class SimpleQueue<T> : IEnumerable<T>
    {
        private LinkedList<T> list;

        public SimpleQueue()
        {
            list = new LinkedList<T>();
        }

        public int Count
        {
            get
            {
                return list.Count;
            }
        }

        public void Enqueue(T value)
        {
            list.AddLast(value);
        }

        public T Peek()
        {
            if (list.First == null)
            {
                throw new InvalidOperationException("The queue is empty.");
            }

            return list.First.Value;
        }

        public T Dequeue()
        {
            T first = Peek();

            list.RemoveFirst();

            return first;
        }

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
