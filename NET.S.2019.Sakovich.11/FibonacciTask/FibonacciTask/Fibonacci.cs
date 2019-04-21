using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FibonacciTask
{
    public class Fibonacci : IEnumerable<int>, IEnumerable, IEnumerator<int>, IEnumerator
    {
        // Current element, lies in range 0 (before first), 1, 1, 2, 3, ..., max, max + ... (after last)
        private int current = 0;

        // Next element
        private int next = 1;

        // Maximal value which current element can take for Fibonacci to be an active enumerator
        private int max;

        public Fibonacci(int max)
        {
            this.max = max;
        }

        public int Current
        {
            get
            {
                if (current == 0 || current > max)
                {
                    throw new InvalidOperationException("Cannot access inactive Fibonacci enumaerator.");
                }

                return current;
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public IEnumerator<int> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            if (current > max)
            {
                return false;
            }

            int temp = current;
            current = next;
            next = temp + next;

            return current <= max;
        }

        public void Reset()
        {
            current = 0;
            next = 1;
        }

        public void Dispose()
        {
        }
    }
}
