using System;
using System.Collections.Generic;
using ConsoleCalculator.Tokenization;
using ConsoleCalculator.Tokens;
using ConsoleCalculator.Tree;
using NUnit.Framework;
using Rhino.Mocks;

namespace ConsoleCalculator.Tests
{
    [TestFixture]
    public class CalculatorTest
    {
        [Test]
        public void TestCalculate()
        {
            var expression = String.Empty;
            var tokens = new List<Token>();
            var value = 0;

            var lexer = MockRepository.GenerateMock<ILexer>();
            var builder = MockRepository.GenerateMock<IExpressionTreeBuilder>();
            var tree = MockRepository.GenerateMock<IExpressionTree>();

            lexer.Expect(l => l.Tokenize(Arg<string>.Is.Equal(expression))).Return(tokens);
            builder.Expect(b => b.Build(Arg<List<Token>>.Is.Equal(tokens))).Return(tree);
            tree.Expect(t => t.GetResult()).Return(value);
            
            var calculator = new Calculator(lexer, builder);

            var result = calculator.Calculate(expression);

            Assert.AreEqual(value, result);

            lexer.VerifyAllExpectations();
            builder.VerifyAllExpectations();
            tree.VerifyAllExpectations();
        }
    }
}