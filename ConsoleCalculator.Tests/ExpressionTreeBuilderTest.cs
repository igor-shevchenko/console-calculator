using System.Collections.Generic;
using System.Linq;
using ConsoleCalculator.OperatorContracts;
using ConsoleCalculator.Tokens;
using ConsoleCalculator.Tree;
using NUnit.Framework;
using Rhino.Mocks;

namespace ConsoleCalculator.Tests
{
    [TestFixture]
    class ExpressionTreeBuilderTest
    {
        [Test]
        public void TestBuildTree()
        {
            var tokens = new List<Token>
                             {
                                 new Token(1),
                                 new Token(MockRepository.GenerateStub<IBinaryOperator>()),
                                 new Token(2)
                             };
            var builder = new ExpressionTreeBuilder();

            var tree = builder.Build(tokens);

            Assert.AreEqual(tree.Token, tokens[1]);
            var children = tree.Children.ToList();
            Assert.AreEqual(children[0].Token, tokens[0]);
            Assert.AreEqual(children[1].Token, tokens[2]);
        }

        [Test]
        public void TestBuildWithUnaryOperation()
        {
            var tokens = new List<Token>
                             {
                                 new Token(MockRepository.GenerateStub<IUnaryOperator>()),
                                 new Token(1),
                             };
            var builder = new ExpressionTreeBuilder();

            var tree = builder.Build(tokens);

            Assert.AreEqual(tree.Token, tokens[0]);
            Assert.AreEqual(tree.Children.ToList()[0].Token, tokens[1]);
        }

        [Test]
        public void TestBuildWithBrackets()
        {
            var tokens = new List<Token>
                             {
                                 new Token(1),
                                 new Token(MockRepository.GenerateStub<IBinaryOperator>()),
                                 new Token(Bracket.Opening),
                                 new Token(2),
                                 new Token(MockRepository.GenerateStub<IBinaryOperator>()),
                                 new Token(3),
                                 new Token(Bracket.Closing),
                             };

            var builder = new ExpressionTreeBuilder();

            var tree = builder.Build(tokens);

            Assert.AreEqual(tree.Token, tokens[1]);
            var children = tree.Children.ToList();
            Assert.AreEqual(children[0].Token, tokens[0]);
            Assert.AreEqual(children[1].Token, tokens[4]);
            var rightGrandchildren = children[1].Children.ToList();
            Assert.AreEqual(rightGrandchildren[0].Token, tokens[3]);
            Assert.AreEqual(rightGrandchildren[1].Token, tokens[5]);
            
        }

        [Test]
        public void TestBuildWithCorrectPrecedence()
        {
            var operatorWithLowPriority = MockRepository.GenerateStub<IBinaryOperator>();
            var operatorWithHighPriority = MockRepository.GenerateStub<IBinaryOperator>();
            
            operatorWithLowPriority.Expect(o => o.Precedence).Return(1);
            operatorWithHighPriority.Expect(o => o.Precedence).Return(2);

            var tokens = new List<Token>
                             {
                                 new Token(1),
                                 new Token(operatorWithLowPriority),
                                 new Token(2),
                                 new Token(operatorWithHighPriority),
                                 new Token(3),
                             };
            var builder = new ExpressionTreeBuilder();

            var tree = builder.Build(tokens);

            Assert.AreEqual(tree.Token, tokens[1]);
            var children = tree.Children.ToList();
            Assert.AreEqual(children[0].Token, tokens[0]);
            Assert.AreEqual(children[1].Token, tokens[3]);
            var rightGrandchildren = children[1].Children.ToList();
            Assert.AreEqual(rightGrandchildren[0].Token, tokens[2]);
            Assert.AreEqual(rightGrandchildren[1].Token, tokens[4]);
        }

        [Test]
        public void TestBuildWithRightAssociativity()
        {
            var tokens = new List<Token>
                             {
                                 new Token(1),
                                 new Token(MockRepository.GenerateStub<IBinaryOperator>()),
                                 new Token(2),
                                 new Token(MockRepository.GenerateStub<IBinaryOperator>()),
                                 new Token(3),
                             };
            var builder = new ExpressionTreeBuilder();

            var tree = builder.Build(tokens);

            Assert.AreEqual(tree.Token, tokens[3]);
            var children = tree.Children.ToList();
            Assert.AreEqual(children[0].Token, tokens[1]);
            Assert.AreEqual(children[1].Token, tokens[4]);
            var rightGrandchildren = children[0].Children.ToList();
            Assert.AreEqual(rightGrandchildren[0].Token, tokens[0]);
            Assert.AreEqual(rightGrandchildren[1].Token, tokens[2]);
        }

        [Test]
        public void TestBuildWithOuterBrackets()
        {
            var tokens = new List<Token>
                             {
                                 new Token(Bracket.Opening),
                                 new Token(1),
                                 new Token(Bracket.Closing),
                                 new Token(MockRepository.GenerateStub<IBinaryOperator>()),
                                 new Token(Bracket.Opening),
                                 new Token(2),
                                 new Token(Bracket.Closing),
                             };
            var builder = new ExpressionTreeBuilder();

            var tree = builder.Build(tokens);

            Assert.AreEqual(tree.Token, tokens[3]);
            var children = tree.Children.ToList();
            Assert.AreEqual(children[0].Token, tokens[1]);
            Assert.AreEqual(children[1].Token, tokens[5]);
        }

        [Test]
        [ExpectedException]
        public void TestRaiseExceptionWithInvalidBrackets()
        {
            var tokens = new List<Token>
                             {
                                 new Token(Bracket.Closing),
                                 new Token(2),
                                 new Token(MockRepository.GenerateStub<IBinaryOperator>()),
                                 new Token(3),
                                 new Token(Bracket.Opening),
                             };

            var builder = new ExpressionTreeBuilder();

            var tree = builder.Build(tokens);
        }

        [Test]
        [ExpectedException]
        public void TestRaiseExceptionWithUnmatchedBrackets()
        {
            var tokens = new List<Token>
                             {
                                 new Token(Bracket.Opening),
                                 new Token(2),
                                 new Token(MockRepository.GenerateStub<IBinaryOperator>()),
                                 new Token(3),
                                 new Token(Bracket.Opening),
                             };

            var builder = new ExpressionTreeBuilder();

            var tree = builder.Build(tokens);
        }

        [Test]
        [ExpectedException]
        public void TestRaiseExceptionWithEmptyBrackets()
        {
            var tokens = new List<Token>
                             {
                                 new Token(Bracket.Opening),
                                 new Token(Bracket.Closing),
                             };

            var builder = new ExpressionTreeBuilder();

            var tree = builder.Build(tokens);
        }
    }
}