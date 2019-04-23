using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueTask.Tests
{
    public static class LinkedListExtensions
    {
        public static void Add<T>(this LinkedList<T> list, T value)
        {
            list.AddLast(value);
        }
    }
}
