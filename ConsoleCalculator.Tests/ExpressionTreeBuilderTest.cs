using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ConsoleCalculator.Operations;
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
            var validator = MockRepository.GenerateStub<IBracketValidator>();
            var tokens = new List<Token>
                             {
                                 new Token(1),
                                 new Token(new AdditionOperator()),
                                 new Token(2)
                             };
            var builder = new ExpressionTreeBuilder(validator);

            var tree = builder.Build(tokens);

            Assert.AreEqual(tree.Token, tokens[1]);
            Assert.AreEqual(tree.Children[0].Token, tokens[0]);
            Assert.AreEqual(tree.Children[1].Token, tokens[2]);
        }

        [Test]
        public void TestBuildWithUnaryOperation()
        {
            var validator = MockRepository.GenerateStub<IBracketValidator>();
            var tokens = new List<Token>
                             {
                                 new Token(new NegationOperator()),
                                 new Token(1),
                             };
            var builder = new ExpressionTreeBuilder(validator);

            var tree = builder.Build(tokens);

            Assert.AreEqual(tree.Token, tokens[0]);
            Assert.AreEqual(tree.Children[0].Token, tokens[1]);
        }

        [Test]
        public void TestBuildWithBrackets()
        {
            var validator = MockRepository.GenerateMock<IBracketValidator>();
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

            validator.Expect(v => v.IsValid(Arg<IList<Token>>.Is.Anything)).Return(true);
            var builder = new ExpressionTreeBuilder(validator);

            var tree = builder.Build(tokens);

            Assert.AreEqual(tree.Token, tokens[1]);
            Assert.AreEqual(tree.Children[0].Token, tokens[0]);
            Assert.AreEqual(tree.Children[1].Token, tokens[4]);
            Assert.AreEqual(tree.Children[1].Children[0].Token, tokens[3]);
            Assert.AreEqual(tree.Children[1].Children[1].Token, tokens[5]);
            
            validator.VerifyAllExpectations();
        }

        [Test]
        public void TestBuildWithCorrectPrecedence()
        {
            var validator = MockRepository.GenerateStub<IBracketValidator>();
            var tokens = new List<Token>
                             {
                                 new Token(1),
                                 new Token(new AdditionOperator()),
                                 new Token(2),
                                 new Token(new MultiplicationOperator()),
                                 new Token(3),
                             };
            var builder = new ExpressionTreeBuilder(validator);

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
            var validator = MockRepository.GenerateStub<IBracketValidator>();
            var tokens = new List<Token>
                             {
                                 new Token(1),
                                 new Token(new SubtractionOperator()),
                                 new Token(2),
                                 new Token(new SubtractionOperator()),
                                 new Token(3),
                             };
            var builder = new ExpressionTreeBuilder(validator);

            var tree = builder.Build(tokens);

            Assert.AreEqual(tree.Token, tokens[3]);
            Assert.AreEqual(tree.Children[0].Token, tokens[1]);
            Assert.AreEqual(tree.Children[1].Token, tokens[4]);
            Assert.AreEqual(tree.Children[0].Children[0].Token, tokens[0]);
            Assert.AreEqual(tree.Children[0].Children[1].Token, tokens[2]);
        }

        [Test]
        public void TestBuildWithWrongOuterBrackets()
        {
            var validator = MockRepository.GenerateMock<IBracketValidator>();
            var tokens = new List<Token>
                             {
                                 new Token(Bracket.Opening),
                                 new Token(1),
                                 new Token(Bracket.Closing),
                                 new Token(new AdditionOperator()),
                                 new Token(Bracket.Opening),
                                 new Token(2),
                                 new Token(Bracket.Closing),
                             };
            validator.Expect(v => v.IsValid(Arg<IList<Token>>.Is.Equal(tokens.Skip(1).Take(tokens.Count-2).ToList()))).Return(false);
            validator.Expect(v => v.IsValid(Arg<IList<Token>>.Is.Anything)).Return(true);
            var builder = new ExpressionTreeBuilder(validator);

            var tree = builder.Build(tokens);

            Assert.AreEqual(tree.Token, tokens[3]);
            Assert.AreEqual(tree.Children[0].Token, tokens[1]);
            Assert.AreEqual(tree.Children[1].Token, tokens[5]);

            validator.VerifyAllExpectations();
        }
    }
}