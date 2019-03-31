using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PolynomialTask.Tests
{
    [TestFixture]
    public class PolynomialTests
    {
        [Test]
        public void Polynomial_Initialization_Tests()
        {
            int[] Poly1_Powers = new int[] { 1, 0, 4 };
            int[] Poly1_Coefficients = new int[] { 2, 3, 4 };

            Polynomial Poly1 = new Polynomial(Poly1_Powers, Poly1_Coefficients);

            Assert.That(Poly1.Coefficients, Is.EquivalentTo(new int[] { 3, 2, 4}));
            Assert.That(Poly1.Powers, Is.EquivalentTo(new int[] { 0, 1, 4}));

            Assert.That(Poly1.MaxPower, Is.EqualTo(4));
        }

        [Test]
        public void DataArrays_AreCopied_WhenAccessed()
        {
            Polynomial Poly1 = new Polynomial(new int[] { 0, 1, 2 }, new int[] { 1, 2, 3 });

            Assert.That(ReferenceEquals(Poly1.Coefficients, Poly1.Coefficients), Is.False);
            Assert.That(ReferenceEquals(Poly1.Powers, Poly1.Powers), Is.False);
        }

        [Test]
        public void ToString_Tests()
        {
            Polynomial Poly1 = new Polynomial(new int[] { 0, 1, 2 }, new int[] { 1, 2, 3 });

            Assert.That(Poly1.ToString(), Is.EqualTo("1 + 2 * x + 3 * x^2"));
        }
    }
}
