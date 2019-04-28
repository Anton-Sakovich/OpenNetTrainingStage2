﻿using NUnit.Framework;
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
            int[] keys = new int[] { 9, 4, 15, 2, 6, 11, 17 };
            string[] values = new string[] { "9", "4", "15", "2", "6", "11", "17" };

            BSTNode<int, string> root = new BSTNode<int, string>(keys[0], values[0]);
            for (int i = 1; i < keys.Length; i++)
            {
                root.Add(keys[i], values[i]);
            }

            int[] expected = new int[] { 2, 4, 6, 9, 11, 15, 17 };
            int[] actual = root.EnumerateInorder().Select(n => n.Key).ToArray();

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
