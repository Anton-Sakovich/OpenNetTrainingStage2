using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSTTask
{
    public static class BSTTraversals
    {
        public static IEnumerator<BSTNode<TKey, TValue>> EnumerateInorder<TKey, TValue>(this BSTNode<TKey, TValue> root)
        {
            Stack<BSTNode<TKey, TValue>> stack = new Stack<BSTNode<TKey, TValue>>();
            BSTNode<TKey, TValue> current = root;

            while (current != null || stack.Count > 0)
            {
                if (current.Left != null)
                {
                    stack.Push(current);
                    current = current.Left;
                    continue;
                }

                yield return current;

                if (current.Right != null)
                {
                    stack.Push(current);
                    current = current.Right;
                    continue;
                }

                current = stack.Pop();
            }
        }
    }
}
