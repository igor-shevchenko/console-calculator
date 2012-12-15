using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleCalculator.Operations;
using NUnit.Framework;

namespace ConsoleCalculator.Tests
{
    [TestFixture]
    class TokenizerTest
    {
        [Test]
        public void TokenizeValidTokens()
        {
            var validTokens = new List<string> {"1", "+", "2"};
            var tokenizer = new Tokenizer();

            var result = tokenizer.Tokenize(validTokens).ToList();

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(1, result[0].GetValue());
            Assert.IsInstanceOf<AdditionOperator>(result[1].GetBinaryOperator());
            Assert.AreEqual(2, result[2].GetValue());

        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void NotTokenizeInvalidTokens()
        {
            var invalidTokens = new List<string> { "aas", "ads"};
            var tokenizer = new Tokenizer();

            var result = tokenizer.Tokenize(invalidTokens).ToList();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void NotTokenizeUnbalancedLeftBracket()
        {
            var invalidTokens = new List<string> { "1", "+", "(", "2" };
            var tokenizer = new Tokenizer();

            var result = tokenizer.Tokenize(invalidTokens).ToList();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void NotTokenizeUnbalancedRightBracket()
        {
            var invalidTokens = new List<string> { "1", "+", "2", ")" };
            var tokenizer = new Tokenizer();

            var result = tokenizer.Tokenize(invalidTokens).ToList();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void NotTokenizeEmptyBrackets()
        {
            var invalidTokens = new List<string> { "1", "+", "(", ")" };
            var tokenizer = new Tokenizer();

            var result = tokenizer.Tokenize(invalidTokens).ToList();
        }

        [Test]
        public void TokenizeBalancedBrackets()
        {
            var validTokens = new List<string> { "1", "+", "(", "(", "(", "2", "*", "3", ")", "/", "4", ")", "-", "5", ")" };
            var tokenizer = new Tokenizer();

            var result = tokenizer.Tokenize(validTokens).ToList();

            Assert.AreEqual(15, result.Count);
            Assert.AreEqual(1, result[0].GetValue());
            Assert.IsInstanceOf<AdditionOperator>(result[1].GetBinaryOperator());
            Assert.AreEqual(Bracket.Opening, result[2].GetBracket());
            Assert.AreEqual(Bracket.Opening, result[3].GetBracket());
            Assert.AreEqual(Bracket.Opening, result[4].GetBracket());
            Assert.AreEqual(2, result[5].GetValue());
            Assert.IsInstanceOf<MultiplicationOperator>(result[6].GetBinaryOperator());
            Assert.AreEqual(3, result[7].GetValue());
            Assert.AreEqual(Bracket.Closing, result[8].GetBracket());
            Assert.IsInstanceOf<DivisionOperator>(result[9].GetBinaryOperator());
            Assert.AreEqual(4, result[10].GetValue());
            Assert.AreEqual(Bracket.Closing, result[11].GetBracket());
            Assert.IsInstanceOf<SubtractionOperator>(result[12].GetBinaryOperator());
            Assert.AreEqual(5, result[13].GetValue());
            Assert.AreEqual(Bracket.Closing, result[14].GetBracket());
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void NotTokenizeWrongPrefixOperation()
        {
            var invalidTokens = new List<string> {"+", "1", "+", "2",};
            var tokenizer = new Tokenizer();

            var result = tokenizer.Tokenize(invalidTokens).ToList();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void NotTokenizeWrongPrefixOperationInBrackets()
        {
            var invalidTokens = new List<string> { "1", "+", "(", "*", "2", ")", };
            var tokenizer = new Tokenizer();

            var result = tokenizer.Tokenize(invalidTokens).ToList();

        }

        [Test]
        public void TokenizeCorrectPrefixOperations()
        {
            var validTokens = new List<string> { "-", "1", "+", "(", "-", "2", ")", };
            var tokenizer = new Tokenizer();

            var result = tokenizer.Tokenize(validTokens).ToList();

            Assert.AreEqual(7, result.Count);
            Assert.IsInstanceOf<NegationOperator>(result[0].GetUnaryOperator());
            Assert.AreEqual(1, result[1].GetValue());
            Assert.IsInstanceOf<AdditionOperator>(result[2].GetBinaryOperator());
            Assert.AreEqual(Bracket.Opening, result[3].GetBracket());
            Assert.IsInstanceOf<NegationOperator>(result[4].GetUnaryOperator());
            Assert.AreEqual(2, result[5].GetValue());
            Assert.AreEqual(Bracket.Closing, result[6].GetBracket());
        }

        [Test]
        public void TokenizePrefixOperationBeforeBrackets()
        {
            var validTokens = new List<string> { "-", "(", "1", "-", "2", ")", };
            var tokenizer = new Tokenizer();

            var result = tokenizer.Tokenize(validTokens).ToList();

            Assert.AreEqual(6, result.Count);
            Assert.IsInstanceOf<NegationOperator>(result[0].GetUnaryOperator());
            Assert.AreEqual(Bracket.Opening, result[1].GetBracket());
            Assert.AreEqual(1, result[2].GetValue());
            Assert.IsInstanceOf<SubtractionOperator>(result[3].GetBinaryOperator());
            Assert.AreEqual(2, result[4].GetValue());
            Assert.AreEqual(Bracket.Closing, result[5].GetBracket());
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void NotTokenizeTwoOperators()
        {
            var invalidTokens = new List<string> { "1", "+", "-", "2" };
            var tokenizer = new Tokenizer();

            var result = tokenizer.Tokenize(invalidTokens).ToList();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void NotTokenizeBracketsWithOperatorOnly()
        {
            var invalidTokens = new List<string> { "1", "+", "(", "-", ")" };
            var tokenizer = new Tokenizer();

            var result = tokenizer.Tokenize(invalidTokens).ToList();
        }
    }
}