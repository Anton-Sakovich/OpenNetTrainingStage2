using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueTask
{
    internal class LinkedListEnumerator<T> : IEnumerator<T>, IEnumerator
    {
        private LinkedList<T> list;
        private LinkedListNode<T> current;
        private bool currentNullMeansEnd;

        public LinkedListEnumerator(LinkedList<T> list)
        {
            if (list == null)
            {
                currentNullMeansEnd = true;
            }
            else
            {
                this.list = list;
                currentNullMeansEnd = false;
            }
        }

        public T Current
        {
            get
            {
                if (current == null)
                {
                    throw new InvalidOperationException("The LinkedListEnumerator is either not initialized of completed.");
                }
                else
                {
                    return current.Value;
                }
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public bool MoveNext()
        {
            if (current == null)
            {
                if (currentNullMeansEnd)
                {
                    return false;
                }
                else
                {
                    currentNullMeansEnd = true;
                    current = list.First;
                    return current == null;
                }
            }
            else
            {
                current = current.Next;
                return current == null;
            }
        }

        public void Reset()
        {
            currentNullMeansEnd = list == null;
        }

        public void Dispose()
        {
        }
    }
}
