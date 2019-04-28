using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace BSTTask.Tests
{
    [TestFixture]
    public class BSTNodeTests
    {
        [Test]
        public void Add_Test()
        {
            BSTNode<int, string> root = new BSTNode<int, string>(9, "9");
            root.Add(4, "4");
            root.Add(15, "15");
            root.Add(2, "2");
            root.Add(6, "6");
            root.Add(11, "11");
            root.Add(17, "17");

            Assert.That(root.Value, Is.EqualTo("9"));
            Assert.That(root.Left.Value, Is.EqualTo("4"));
            Assert.That(root.Right.Value, Is.EqualTo("15"));
            Assert.That(root.Left.Left.Value, Is.EqualTo("2"));
            Assert.That(root.Left.Right.Value, Is.EqualTo("6"));
            Assert.That(root.Right.Left.Value, Is.EqualTo("11"));
            Assert.That(root.Right.Right.Value, Is.EqualTo("17"));
            Assert.That(root.Left.Left.Left, Is.Null);
            Assert.That(root.Left.Left.Right, Is.Null);
            Assert.That(root.Left.Right.Left, Is.Null);
            Assert.That(root.Left.Right.Right, Is.Null);
            Assert.That(root.Right.Left.Left, Is.Null);
            Assert.That(root.Right.Left.Right, Is.Null);
            Assert.That(root.Right.Right.Left, Is.Null);
            Assert.That(root.Right.Right.Right, Is.Null);

            root.Add(15, "15+");

            Assert.That(root.Value, Is.EqualTo("9"));
            Assert.That(root.Left.Value, Is.EqualTo("4"));
            Assert.That(root.Right.Value, Is.EqualTo("15+"));
            Assert.That(root.Left.Left.Value, Is.EqualTo("2"));
            Assert.That(root.Left.Right.Value, Is.EqualTo("6"));
            Assert.That(root.Right.Left.Value, Is.EqualTo("11"));
            Assert.That(root.Right.Right.Value, Is.EqualTo("17"));
            Assert.That(root.Left.Left.Left, Is.Null);
            Assert.That(root.Left.Left.Right, Is.Null);
            Assert.That(root.Left.Right.Left, Is.Null);
            Assert.That(root.Left.Right.Right, Is.Null);
            Assert.That(root.Right.Left.Left, Is.Null);
            Assert.That(root.Right.Left.Right, Is.Null);
            Assert.That(root.Right.Right.Left, Is.Null);
            Assert.That(root.Right.Right.Right, Is.Null);
        }

        [Test]
        public void Lookup_Test()
        {
            int[] keys = new int[] { 9, 4, 15, 2, 6, 11, 17 };
            string[] values = new string[] { "9", "4", "15", "2", "6", "11", "17" };

            BSTNode<int, string> root = new BSTNode<int, string>(keys[0], values[0]);
            for (int i = 1; i < keys.Length; i++)
            {
                root.Add(keys[i], values[i]);
            }

            for (int i = 0; i < keys.Length; i++)
            {
                Assert.That(root.Lookup(keys[i])?.Value, Is.EqualTo(values[i]));
            }

            Assert.That(root.Lookup(-1), Is.Null);
        }
    }
}
