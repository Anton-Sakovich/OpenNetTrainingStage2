using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSTTask
{
    public class BSTNode<TKey, TValue>
    {
        private BSTNode()
            : this(Comparer<TKey>.Default)
        {
        }

        private BSTNode(IComparer<TKey> comparer)
        {
            this.Comparer = comparer ?? throw new ArgumentNullException(nameof(comparer), "The comparer provided is null.");
        }

        public BSTNode(TKey key, TValue value)
            : this(Comparer<TKey>.Default)
        {
            this.Key = key;
            this.Value = value;
        }

        public BSTNode(TKey key, TValue value, IComparer<TKey> comparer)
            : this(comparer)
        {
            this.Key = key;
            this.Value = value;
        }

        private BSTNode(TKey key, TValue value, BSTNode<TKey, TValue> parent)
            : this(parent.Comparer)
        {
            this.Key = key;
            this.Value = value;
        }

        public IComparer<TKey> Comparer { get; }

        public TKey Key { get; }
        public TValue Value { get; set; }

        public BSTNode<TKey, TValue> Left { get; private set; }
        public BSTNode<TKey, TValue> Right { get; private set; }

        public BSTNode<TKey, TValue> Lookup(TKey key)
        {
            BSTNode<TKey, TValue> currentNode = this;
            int compareResult;

            while (currentNode != null)
            {
                compareResult = this.Comparer.Compare(key, currentNode.Key);

                if (compareResult == 0)
                {
                    break;
                }
                else if (compareResult > 0)
                {
                    currentNode = currentNode.Right;
                }
                else
                {
                    currentNode = currentNode.Left;
                }
            }

            return currentNode;
        }

        public BSTNode<TKey, TValue> Add(TKey key, TValue value)
        {
            int compareResult = this.Comparer.Compare(key, this.Key);

            if (compareResult == 0)
            {
                this.Value = value;
                return this;
            }
            else if (compareResult > 0)
            {
                this.Right = this.Right?.Add(key, value) ?? new BSTNode<TKey, TValue>(key, value, this);
                return this;
            }
            else
            {
                this.Left = this.Left?.Add(key, value) ?? new BSTNode<TKey, TValue>(key, value, this);
                return this;
            }
        }

        public override string ToString()
        {
            return $"BSTNode(Key = {this.Key}, Value = {this.Value})";
        }
    }
}
