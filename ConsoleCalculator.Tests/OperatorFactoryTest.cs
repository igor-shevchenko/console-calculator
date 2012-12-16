using System;
using System.Collections.Generic;
using ConsoleCalculator.Operations;
using NUnit.Framework;
using Rhino.Mocks;

namespace ConsoleCalculator.Tests
{
    [TestFixture]
    class OperatorFactoryTest
    {
        [Test]
        public void TestGetValidBinaryOperator()
        {
            var sign = "+";
            var listFactory = MockRepository.GenerateMock<IOperatorListFactory>();
            var op = MockRepository.GenerateMock<IBinaryOperator>();
            op.Expect(o => o.Sign).Return(sign);
            listFactory.Expect(f => f.GetBinaryOperators()).Return(new[] {op});

            var factory = new OperatorFactory(listFactory);

            var result = factory.GetBinaryOperator(sign);

            Assert.AreEqual(op, result);
            op.VerifyAllExpectations();
            listFactory.VerifyAllExpectations();
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestGetInvalidBinaryOperator()
        {
            var listFactory = MockRepository.GenerateMock<IOperatorListFactory>();
            var op = MockRepository.GenerateMock<IBinaryOperator>();
            op.Expect(o => o.Sign).Return("+");
            listFactory.Expect(f => f.GetBinaryOperators()).Return(new[] { op });

            var factory = new OperatorFactory(listFactory);

            var result = factory.GetBinaryOperator("-");

            op.VerifyAllExpectations();
            listFactory.VerifyAllExpectations();
        }

        [Test]
        public void TestGetValidUnaryOperator()
        {
            var sign = "-";
            var listFactory = MockRepository.GenerateMock<IOperatorListFactory>();
            var op = MockRepository.GenerateMock<IUnaryOperator>();
            op.Expect(o => o.Sign).Return(sign);
            listFactory.Expect(f => f.GetUnaryOperators()).Return(new[] { op });

            var factory = new OperatorFactory(listFactory);

            var result = factory.GetUnaryOperator(sign);

            Assert.AreEqual(op, result);
            op.VerifyAllExpectations();
            listFactory.VerifyAllExpectations();
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestGetInvalidUnaryOperator()
        {
            var listFactory = MockRepository.GenerateMock<IOperatorListFactory>();
            var op = MockRepository.GenerateMock<IUnaryOperator>();
            op.Expect(o => o.Sign).Return("-");
            listFactory.Expect(f => f.GetUnaryOperators()).Return(new[] { op });

            var factory = new OperatorFactory(listFactory);

            var result = factory.GetUnaryOperator("+");

            op.VerifyAllExpectations();
            listFactory.VerifyAllExpectations();
        }
    }
}