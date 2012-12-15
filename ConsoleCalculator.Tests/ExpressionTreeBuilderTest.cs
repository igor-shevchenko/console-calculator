using System.Collections.Generic;
using ConsoleCalculator.Operations;
using ConsoleCalculator.Tree;
using NUnit.Framework;

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
                                 new Token(new AdditionOperator()),
                                 new Token(2)
                             };
            var builder = new ExpressionTreeBuilder();

            var tree = builder.Build(tokens);

            Assert.AreEqual(tree.Token, tokens[1]);
            Assert.AreEqual(tree.Children[0].Token, tokens[0]);
            Assert.AreEqual(tree.Children[1].Token, tokens[2]);
        }

        [Test]
        public void TestBuildWithUnaryOperation()
        {
            var tokens = new List<Token>
                             {
                                 new Token(new NegationOperator()),
                                 new Token(1),
                             };
            var builder = new ExpressionTreeBuilder();

            var tree = builder.Build(tokens);

            Assert.AreEqual(tree.Token, tokens[0]);
            Assert.AreEqual(tree.Children[0].Token, tokens[1]);
        }

        [Test]
        public void TestBuildWithBrackets()
        {
            var tokens = new List<Token>
                             {
                                 new Token(1),
                                 new Token(new AdditionOperator()),
                                 new Token(Bracket.Opening),
                                 new Token(2),
                                 new Token(new AdditionOperator()),
                                 new Token(3),
                                 new Token(Bracket.Closing),
                             };
            var builder = new ExpressionTreeBuilder();

            var tree = builder.Build(tokens);

            Assert.AreEqual(tree.Token, tokens[1]);
            Assert.AreEqual(tree.Children[0].Token, tokens[0]);
            Assert.AreEqual(tree.Children[1].Token, tokens[4]);
            Assert.AreEqual(tree.Children[1].Children[0].Token, tokens[3]);
            Assert.AreEqual(tree.Children[1].Children[1].Token, tokens[5]);
        }

        [Test]
        public void TestBuildWithCorrectPrecedence()
        {
            var tokens = new List<Token>
                             {
                                 new Token(1),
                                 new Token(new AdditionOperator()),
                                 new Token(2),
                                 new Token(new MultiplicationOperator()),
                                 new Token(3),
                             };
            var builder = new ExpressionTreeBuilder();

            var tree = builder.Build(tokens);

            Assert.AreEqual(tree.Token, tokens[1]);
            Assert.AreEqual(tree.Children[0].Token, tokens[0]);
            Assert.AreEqual(tree.Children[1].Token, tokens[3]);
            Assert.AreEqual(tree.Children[1].Children[0].Token, tokens[2]);
            Assert.AreEqual(tree.Children[1].Children[1].Token, tokens[4]);
        }

        [Test]
        public void TestBuildWithRightAssociativity()
        {
            var tokens = new List<Token>
                             {
                                 new Token(1),
                                 new Token(new SubtractionOperator()),
                                 new Token(2),
                                 new Token(new SubtractionOperator()),
                                 new Token(3),
                             };
            var builder = new ExpressionTreeBuilder();

            var tree = builder.Build(tokens);

            Assert.AreEqual(tree.Token, tokens[3]);
            Assert.AreEqual(tree.Children[0].Token, tokens[1]);
            Assert.AreEqual(tree.Children[1].Token, tokens[4]);
            Assert.AreEqual(tree.Children[0].Children[0].Token, tokens[0]);
            Assert.AreEqual(tree.Children[0].Children[1].Token, tokens[2]);
        }
    }
}