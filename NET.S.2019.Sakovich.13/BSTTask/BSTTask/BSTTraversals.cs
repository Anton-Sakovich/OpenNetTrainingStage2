using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSTTask
{
    public static class BSTTraversals
    {
        public static IEnumerable<BSTNode<TKey, TValue>> EnumerateInorder<TKey, TValue>(this BSTNode<TKey, TValue> root)
        {
            Stack<BSTNode<TKey, TValue>> stack = new Stack<BSTNode<TKey, TValue>>();
            BSTNode<TKey, TValue> current = root;

            while (current != null || stack.Count > 0)
            {
                if (current == null)
                {
                    current = stack.Pop();
                    yield return current;
                    current = current.Right;
                }
                else
                {
                    stack.Push(current);
                    current = current.Left;
                }
            }
        }

        public static IEnumerable<BSTNode<TKey, TValue>> EnumeratePreorder<TKey, TValue>(this BSTNode<TKey, TValue> root)
        {
            Stack<BSTNode<TKey, TValue>> stack = new Stack<BSTNode<TKey, TValue>>();
            BSTNode<TKey, TValue> current = root;

            while (current != null || stack.Count > 0)
            {
                if (current == null)
                {
                    current = stack.Pop();
                }
                else
                {
                    yield return current;
                    stack.Push(current.Right);
                    current = current.Left;
                }
            }
        }
    }
}
