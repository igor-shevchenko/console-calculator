using System.Collections.Generic;
using ConsoleCalculator.Operations;
using ConsoleCalculator.Tokens;
using ConsoleCalculator.Tree;
using NUnit.Framework;
using Rhino.Mocks;

namespace ConsoleCalculator.Tests
{
    [TestFixture]
    class ExpressionTreeTest
    {
        [Test]
        public void TestGetValue()
        {
            var token = new Token(1);
            var tree = new ExpressionTree(token, null);

            var result = tree.GetResult();

            Assert.AreEqual(1, result);
        }

        [Test]
        public void TestGetBinaryOperationResult()
        {
            var child1 = MockRepository.GenerateMock<IExpressionTree>();
            child1.Expect(t => t.GetResult())
                    .Return(1);
            var child2 = MockRepository.GenerateMock<IExpressionTree>();
            child2.Expect(t => t.GetResult())
                    .Return(2);
            var op = MockRepository.GenerateMock<IBinaryOperator>();
            op.Expect(o => o.Apply(Arg<double>.Is.Equal(1), Arg<double>.Is.Equal(2)))
                    .Return(3);
            var token = new Token(op);
            var tree = new ExpressionTree(token, new List<IExpressionTree> {child1, child2});

            var result = tree.GetResult();

            Assert.AreEqual(3, result);
        }

        [Test]
        public void TestGetUnaryOperationResult()
        {
            var child = MockRepository.GenerateMock<IExpressionTree>();
            child.Expect(t => t.GetResult())
                    .Return(1);
            var op = MockRepository.GenerateMock<IUnaryOperator>();
            op.Expect(o => o.Apply(Arg<double>.Is.Equal(1)))
                    .Return(-1);
            var token = new Token(op);
            var tree = new ExpressionTree(token, new List<IExpressionTree> { child });

            var result = tree.GetResult();

            Assert.AreEqual(-1, result);
        }
    }
}