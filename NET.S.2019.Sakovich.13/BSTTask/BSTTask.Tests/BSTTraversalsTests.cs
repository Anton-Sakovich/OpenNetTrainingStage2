using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSTTask.Tests
{
    [TestFixture]
    public class BSTTraversalsTests
    {
        [Test]
        public void InorderTraversal_Test()
        {
            BSTNode<int, int> root = new BSTNode<int, int>(9, 9);
            Assert.That(root.EnumerateInorder().Select(n => n.Key).ToArray(), Is.EqualTo(new int[] { 9 }));

            root.Add(15, 15);
            Assert.That(root.EnumerateInorder().Select(n => n.Key).ToArray(), Is.EqualTo(new int[] { 9, 15 }));

            root.Add(4, 4);
            Assert.That(root.EnumerateInorder().Select(n => n.Key).ToArray(), Is.EqualTo(new int[] { 4, 9, 15 }));

            root.Add(2, 2);
            Assert.That(root.EnumerateInorder().Select(n => n.Key).ToArray(), Is.EqualTo(new int[] { 2, 4, 9, 15 }));

            root.Add(6, 6);
            Assert.That(root.EnumerateInorder().Select(n => n.Key).ToArray(), Is.EqualTo(new int[] { 2, 4, 6, 9, 15 }));

            root.Add(17, 17);
            Assert.That(root.EnumerateInorder().Select(n => n.Key).ToArray(), Is.EqualTo(new int[] { 2, 4, 6, 9, 15, 17 }));

            root.Add(11, 11);
            Assert.That(root.EnumerateInorder().Select(n => n.Key).ToArray(), Is.EqualTo(new int[] { 2, 4, 6, 9, 11, 15, 17 }));
        }

        [Test]
        public void PreorderTraversal_Test()
        {
            BSTNode<int, int> root = new BSTNode<int, int>(9, 9);
            Assert.That(root.EnumeratePreorder().Select(n => n.Key).ToArray(), Is.EqualTo(new int[] { 9 }));

            root.Add(15, 15);
            Assert.That(root.EnumeratePreorder().Select(n => n.Key).ToArray(), Is.EqualTo(new int[] { 9, 15 }));

            root.Add(4, 4);
            Assert.That(root.EnumeratePreorder().Select(n => n.Key).ToArray(), Is.EqualTo(new int[] { 9, 4, 15 }));

            root.Add(2, 2);
            Assert.That(root.EnumeratePreorder().Select(n => n.Key).ToArray(), Is.EqualTo(new int[] { 9, 4, 2, 15 }));

            root.Add(6, 6);
            Assert.That(root.EnumeratePreorder().Select(n => n.Key).ToArray(), Is.EqualTo(new int[] { 9, 4, 2, 6, 15 }));

            root.Add(17, 17);
            Assert.That(root.EnumeratePreorder().Select(n => n.Key).ToArray(), Is.EqualTo(new int[] { 9, 4, 2, 6, 15, 17 }));

            root.Add(11, 11);
            Assert.That(root.EnumeratePreorder().Select(n => n.Key).ToArray(), Is.EqualTo(new int[] { 9, 4, 2, 6, 15, 11, 17 }));
        }

        [Test]
        public void PostorderTraversal_Test()
        {
            BSTNode<int, int> root = new BSTNode<int, int>(9, 9);
            Assert.That(root.EnumeratePostorder().Select(n => n.Key).ToArray(), Is.EqualTo(new int[] { 9 }));

            root.Add(15, 15);
            Assert.That(root.EnumeratePostorder().Select(n => n.Key).ToArray(), Is.EqualTo(new int[] { 15, 9 }));

            root.Add(4, 4);
            Assert.That(root.EnumeratePostorder().Select(n => n.Key).ToArray(), Is.EqualTo(new int[] { 4, 15, 9 }));

            root.Add(2, 2);
            Assert.That(root.EnumeratePostorder().Select(n => n.Key).ToArray(), Is.EqualTo(new int[] { 2, 4, 15, 9 }));

            root.Add(6, 6);
            Assert.That(root.EnumeratePostorder().Select(n => n.Key).ToArray(), Is.EqualTo(new int[] { 2, 6, 4, 15, 9 }));

            root.Add(17, 17);
            Assert.That(root.EnumeratePostorder().Select(n => n.Key).ToArray(), Is.EqualTo(new int[] { 2, 6, 4, 17, 15, 9 }));

            root.Add(11, 11);
            Assert.That(root.EnumeratePostorder().Select(n => n.Key).ToArray(), Is.EqualTo(new int[] { 2, 6, 4, 11, 17, 15, 9 }));
        }
    }
}
