using System;
using ConsoleCalculator.Operations;
using NUnit.Framework;

namespace ConsoleCalculator.Tests
{
    [TestFixture]
    class OperatorFactoryTest
    {
        [Test]
        public void TestGetAddition()
        {
            var factory = new OperatorFactory();

            var result = factory.GetBinaryOperator("+");

            Assert.IsInstanceOf<AdditionOperator>(result);
        }

        [Test]
        public void TestGetSubtraction()
        {
            var factory = new OperatorFactory();

            var result = factory.GetBinaryOperator("-");

            Assert.IsInstanceOf<SubtractionOperator>(result);
        }

        [Test]
        public void TestGeMultiplication()
        {
            var factory = new OperatorFactory();

            var result = factory.GetBinaryOperator("*");

            Assert.IsInstanceOf<MultiplicationOperator>(result);
        }

        [Test]
        public void TestGetDivision()
        {
            var factory = new OperatorFactory();

            var result = factory.GetBinaryOperator("/");

            Assert.IsInstanceOf<DivisionOperator>(result);
        }

        [Test]
        public void TestGetNegation()
        {
            var factory = new OperatorFactory();

            var result = factory.GetUnaryOperator("-");

            Assert.IsInstanceOf<NegationOperator>(result);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void TestGetInvalidUnaryOperator()
        {
            var factory = new OperatorFactory();

            var result = factory.GetUnaryOperator("+");
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void TestGetInvalidBinaryOperator()
        {
            var factory = new OperatorFactory();

            var result = factory.GetBinaryOperator("?");
        }
    }
}