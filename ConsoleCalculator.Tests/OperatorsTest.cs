using ConsoleCalculator.MyOperators;
using ConsoleCalculator.Operations;
using NUnit.Framework;

namespace ConsoleCalculator.Tests
{
    [TestFixture]
    class OperatorsTest
    {
        [Test]
        public void TestAddition()
        {
            var op = new AdditionOperator();

            var result = op.Apply(1, 2);

            Assert.AreEqual(3, result);
        }

        [Test]
        public void TestSubtraction()
        {
            var op = new SubtractionOperator();

            var result = op.Apply(1, 2);

            Assert.AreEqual(-1, result);
        }

        [Test]
        public void TestMultiplication()
        {
            var op = new MultiplicationOperator();

            var result = op.Apply(1, 2);

            Assert.AreEqual(2, result);
        }

        [Test]
        public void TestDivision()
        {
            var op = new DivisionOperator();

            var result = op.Apply(1, 2);

            Assert.AreEqual(0.5, result);
        }

        [Test]
        public void TestNegation()
        {
            var op = new NegationOperator();

            var result = op.Apply(1);

            Assert.AreEqual(-1, result);
        }

        [Test]
        public void TestPrecedence()
        {
            var addition = new AdditionOperator();
            var subtraction = new SubtractionOperator();
            var multiplication = new MultiplicationOperator();
            var division = new DivisionOperator();
            var negation = new NegationOperator();

            Assert.IsTrue(addition.Precedence < multiplication.Precedence);
            Assert.IsTrue(addition.Precedence < division.Precedence);
            Assert.IsTrue(subtraction.Precedence < multiplication.Precedence);
            Assert.IsTrue(subtraction.Precedence < division.Precedence);
            Assert.IsTrue(multiplication.Precedence < negation.Precedence);
            Assert.IsTrue(division.Precedence < negation.Precedence);
        }
    }
}