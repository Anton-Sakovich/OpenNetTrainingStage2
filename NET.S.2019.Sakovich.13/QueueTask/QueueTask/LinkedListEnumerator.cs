using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueTask
{
    public class LinkedListEnumerator<T> : IEnumerator<T>, IEnumerator
    {
        /* The pair (list, current) defines the state of the iterator:
         * (null, any) = after last (enumaration completed);
         * (!null, null) = before first (enumeration hasn't started);
         * (!null, !null) = active (enumeration is in progress). */
        private LinkedList<T> list;
        private LinkedListNode<T> current;

        public LinkedListEnumerator(LinkedList<T> list)
        {
            // It's OK to have null as list because it will simply lead
            // to a completed (empty) enumeration.
            this.list = list;
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
            if (list == null)
            {
                // Enumeration is completed
                return false;
            }
            else
            {
                // Enumeration is not completed
                if (current == null)
                {
                    // Enumeration has not started
                    current = list.First;
                }
                else
                {
                    // Enumeration is in progress
                    current = current.Next;
                }
            }

            // If current.Next turns out to be null, enumeration is completed.
            if (current == null)
            {
                list = null;
                return false;
            }
            else
            {
                return true;
            }
        }

        public void Reset()
        {
            throw new NotSupportedException();
        }

        public void Dispose()
        {
        }
    }
}
