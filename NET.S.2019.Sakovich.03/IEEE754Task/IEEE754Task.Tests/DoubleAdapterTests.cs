using System;
using System.Collections.Generic;
using IEEE754Task;
using NUnit.Framework;

namespace IEEE754Task.Tests
{
    [TestFixture]
    public class DoubleAdapterTests
    {
        static DoubleAdapter Adapter = new DoubleAdapter();

        /*IntegerString[FromDigits[Reverse @ ImportString[ExportString[255.255, "Real64"], "Byte"], 256], 2, 64]*/
        static IEnumerable<TestCaseData> NormalNumbers
        {
            get
            {
                yield return new TestCaseData(255.255).Returns("0100000001101111111010000010100011110101110000101000111101011100");
                yield return new TestCaseData(-255.255).Returns("1100000001101111111010000010100011110101110000101000111101011100");
                yield return new TestCaseData(1.2569733585405607e9).Returns("0100000111010010101110101111100100001011101000101001100010001100");
                yield return new TestCaseData(4.235732815836542e307).Returns("0111111111001110001010001100111110101010110110000001111010001111");
                yield return new TestCaseData(1.1056034629523437e306).Returns("0111111101111001001100001101111100101110011110011001111111111111");
                yield return new TestCaseData(7.453134903629962e307).Returns("0111111111011010100010001011011011101001100011110000010101111111");
                yield return new TestCaseData(1.3716442626399616e300).Returns("0111111001000000011000101010011001111011001111001011000110011000");
                yield return new TestCaseData(2.2250738585072014e-308).Returns("0000000000010000000000000000000000000000000000000000000000000000");
            }
        }

        static IEnumerable<TestCaseData> SubnormalNumbers
        {
            get
            {
                yield return new TestCaseData(double.Epsilon).Returns("0000000000000000000000000000000000000000000000000000000000000001");
                yield return new TestCaseData(double.Epsilon * 2D).Returns("0000000000000000000000000000000000000000000000000000000000000010");
                yield return new TestCaseData(double.Epsilon + double.Epsilon * 2D).Returns("0000000000000000000000000000000000000000000000000000000000000011");
                yield return new TestCaseData(1.1125369292536007e-308).Returns("0000000000001000000000000000000000000000000000000000000000000000");
            }
        }

        [TestCaseSource(nameof(NormalNumbers))]
        public string NormalNumbersTests(double d)
        {
            return Adapter.BinaryString(d);
        }

        [TestCaseSource(nameof(SubnormalNumbers))]
        public string SubnormalNumbersTests(double d)
        {
            return Adapter.BinaryString(d);
        }

        [Test]
        public void ZerosTests()
        {
            Assert.That(Adapter.BinaryString(0), Is.EqualTo("0000000000000000000000000000000000000000000000000000000000000000"));
            Assert.That(Adapter.BinaryString(-0.0), Is.EqualTo("1000000000000000000000000000000000000000000000000000000000000000"));
        }

        [Test]
        public void NotNumbersTests()
        {
            Assert.That(Adapter.BinaryString(double.NaN), Is.EqualTo("1111111111111000000000000000000000000000000000000000000000000000"));
            Assert.That(Adapter.BinaryString(double.PositiveInfinity), Is.EqualTo("0111111111110000000000000000000000000000000000000000000000000000"));
            Assert.That(Adapter.BinaryString(double.NegativeInfinity), Is.EqualTo("1111111111110000000000000000000000000000000000000000000000000000"));
        }
    }
}
