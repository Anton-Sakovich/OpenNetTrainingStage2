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
        // Current element, lies in range 0 (before first), 1, 1, 2, 3, ..., <=max (last), > max (after last)
        private int current = 0;

        // Next element
        private int next = 1;

        // Upper bound for a valid current
        private int max;

        /// <summary>
        /// Creates a new Fibonacci sequence whose last element is not greater than an upper bound.
        /// </summary>
        /// <param name="max">The upper bound for the given sequence.</param>
        /// <exception cref="ArgumentException">Thrown when the upper bound provided is less than one.</exception>
        public Fibonacci(int max)
        {
            if (max < 1)
            {
                throw new ArgumentException("The upper bound must be not less than one.", nameof(max));
            }

            this.max = max;
        }

        /// <summary>
        /// Returns the current element of the Fibonacci sequence.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the Fibonacci sequence has not been started or has been completed.
        /// </exception>
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

        /// <summary>
        /// Moves to the next element in the Fibonacci sequence.
        /// </summary>
        /// <returns>
        /// True if the move resulted in a valid sequence element and False if the
        /// sequnce has been finished.
        /// </returns>
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

        /// <summary>
        /// Resets the Fibonacci sequence.
        /// </summary>
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
