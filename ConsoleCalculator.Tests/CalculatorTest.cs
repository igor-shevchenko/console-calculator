using System;
using System.Collections.Generic;
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
            var splittedString = new List<string>();
            var tokens = new List<Token>();
            var value = 0;

            var splitter = MockRepository.GenerateMock<ISplitter>();
            var tokenizer = MockRepository.GenerateMock<ITokenizer>();
            var builder = MockRepository.GenerateMock<IExpressionTreeBuilder>();
            var tree = MockRepository.GenerateMock<IExpressionTree>();
            var validator = MockRepository.GenerateMock<IBracketValidator>();

            splitter.Expect(s => s.Split(Arg<string>.Is.Equal(expression))).Return(splittedString);
            tokenizer.Expect(t => t.Tokenize(Arg<List<string>>.Is.Equal(splittedString))).Return(tokens);
            validator.Expect(v => v.Validate(Arg<List<Token>>.Is.Equal(tokens)));
            builder.Expect(b => b.Build(Arg<List<Token>>.Is.Equal(tokens))).Return(tree);
            tree.Expect(t => t.GetResult()).Return(value);
            
            var calculator = new Calculator(splitter, tokenizer, builder, validator);

            var result = calculator.Calculate(expression);

            Assert.AreEqual(value, result);

            splitter.VerifyAllExpectations();
            tokenizer.VerifyAllExpectations();
            builder.VerifyAllExpectations();
            tree.VerifyAllExpectations();
            validator.VerifyAllExpectations();
        }
    }
}