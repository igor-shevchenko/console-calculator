using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleCalculator.Operations;
using NUnit.Framework;
using Rhino.Mocks;

namespace ConsoleCalculator.Tests
{
    [TestFixture]
    class TokenizerTest
    {
        [Test]
        public void TokenizeValidTokens()
        {
            var validTokens = new List<string> { "1", "+", "2" };
            var factory = MockRepository.GenerateMock<IOperatorFactory>();
            var op = MockRepository.GenerateStub<IBinaryOperator>();
            factory.Expect(f => f.GetBinaryOperator(Arg<string>.Is.Equal(validTokens[1])))
                        .Return(op);

            var tokenizer = new Tokenizer(factory);

            var result = tokenizer.Tokenize(validTokens).ToList();

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(1, result[0].GetValue());
            Assert.AreEqual(op, result[1].GetBinaryOperator());
            Assert.AreEqual(2, result[2].GetValue());
            factory.VerifyAllExpectations();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void NotTokenizeInvalidTokens()
        {
            var invalidTokens = new List<string> { "aas", "ads"};
            var factory = MockRepository.GenerateStub<IOperatorFactory>();
            
            var tokenizer = new Tokenizer(factory);

            var result = tokenizer.Tokenize(invalidTokens).ToList();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void NotTokenizeUnbalancedLeftBracket()
        {
            var invalidTokens = new List<string> { "1", "+", "(", "2" };
            var factory = MockRepository.GenerateStub<IOperatorFactory>();
            var tokenizer = new Tokenizer(factory);

            var result = tokenizer.Tokenize(invalidTokens).ToList();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void NotTokenizeUnbalancedRightBracket()
        {
            var invalidTokens = new List<string> { "1", "+", "2", ")" };
            var factory = MockRepository.GenerateStub<IOperatorFactory>();
            var tokenizer = new Tokenizer(factory);

            var result = tokenizer.Tokenize(invalidTokens).ToList();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void NotTokenizeEmptyBrackets()
        {
            var invalidTokens = new List<string> { "1", "+", "(", ")" };
            var factory = MockRepository.GenerateStub<IOperatorFactory>();
            var tokenizer = new Tokenizer(factory);

            var result = tokenizer.Tokenize(invalidTokens).ToList();
        }

        [Test]
        public void TokenizeBalancedBrackets()
        {
            var validTokens = new List<string> { "1", "+", "(", "(", "(", "2", "*", "3", ")", "/", "4", ")", "-", "5", ")" };
            var factory = MockRepository.GenerateMock<IOperatorFactory>();
            var addition = MockRepository.GenerateStub<IBinaryOperator>();
            var multiplication = MockRepository.GenerateStub<IBinaryOperator>();
            var division = MockRepository.GenerateStub<IBinaryOperator>();
            var subtraction = MockRepository.GenerateStub<IBinaryOperator>();
            
            factory.Expect(f => f.GetBinaryOperator(Arg<string>.Is.Equal(validTokens[1])))
                .Return(addition);
            factory.Expect(f => f.GetBinaryOperator(Arg<string>.Is.Equal(validTokens[6])))
                .Return(multiplication);
            factory.Expect(f => f.GetBinaryOperator(Arg<string>.Is.Equal(validTokens[9])))
                .Return(division);
            factory.Expect(f => f.GetBinaryOperator(Arg<string>.Is.Equal(validTokens[12])))
                .Return(subtraction);
            var tokenizer = new Tokenizer(factory);

            var result = tokenizer.Tokenize(validTokens).ToList();

            Assert.AreEqual(15, result.Count);
            Assert.AreEqual(1, result[0].GetValue());
            Assert.AreEqual(addition, result[1].GetBinaryOperator());
            Assert.AreEqual(Bracket.Opening, result[2].GetBracket());
            Assert.AreEqual(Bracket.Opening, result[3].GetBracket());
            Assert.AreEqual(Bracket.Opening, result[4].GetBracket());
            Assert.AreEqual(2, result[5].GetValue());
            Assert.AreEqual(multiplication, result[6].GetBinaryOperator());
            Assert.AreEqual(3, result[7].GetValue());
            Assert.AreEqual(Bracket.Closing, result[8].GetBracket());
            Assert.AreEqual(division, result[9].GetBinaryOperator());
            Assert.AreEqual(4, result[10].GetValue());
            Assert.AreEqual(Bracket.Closing, result[11].GetBracket());
            Assert.AreEqual(subtraction, result[12].GetBinaryOperator());
            Assert.AreEqual(5, result[13].GetValue());
            Assert.AreEqual(Bracket.Closing, result[14].GetBracket());
            factory.VerifyAllExpectations();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void NotTokenizeWrongPrefixOperation()
        {
            var invalidTokens = new List<string> {"+", "1", "+", "2",};
            var factory = MockRepository.GenerateStub<IOperatorFactory>();
            var tokenizer = new Tokenizer(factory);

            var result = tokenizer.Tokenize(invalidTokens).ToList();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void NotTokenizeWrongPrefixOperationInBrackets()
        {
            var invalidTokens = new List<string> { "1", "+", "(", "*", "2", ")", };
            var factory = MockRepository.GenerateStub<IOperatorFactory>();
            var tokenizer = new Tokenizer(factory);

            var result = tokenizer.Tokenize(invalidTokens).ToList();

        }

        [Test]
        public void TokenizeCorrectPrefixOperations()
        {
            var validTokens = new List<string> { "-", "1", "+", "(", "-", "2", ")", };
            var factory = MockRepository.GenerateMock<IOperatorFactory>();
            var negation = MockRepository.GenerateStub<IUnaryOperator>();
            var addition = MockRepository.GenerateStub<IBinaryOperator>();

            factory.Expect(f => f.GetUnaryOperator(Arg<string>.Is.Equal(validTokens[0])))
                .Return(negation).Repeat.Twice();
            factory.Expect(f => f.GetBinaryOperator(Arg<string>.Is.Equal(validTokens[2])))
                .Return(addition);

            var tokenizer = new Tokenizer(factory);

            var result = tokenizer.Tokenize(validTokens).ToList();

            Assert.AreEqual(7, result.Count);
            Assert.AreEqual(negation, result[0].GetUnaryOperator());
            Assert.AreEqual(1, result[1].GetValue());
            Assert.AreEqual(addition, result[2].GetBinaryOperator());
            Assert.AreEqual(Bracket.Opening, result[3].GetBracket());
            Assert.AreEqual(negation, result[4].GetUnaryOperator());
            Assert.AreEqual(2, result[5].GetValue());
            Assert.AreEqual(Bracket.Closing, result[6].GetBracket());
            factory.VerifyAllExpectations();
        }

        [Test]
        public void TokenizePrefixOperationBeforeBrackets()
        {
            var validTokens = new List<string> { "-", "(", "1", "-", "2", ")", };
            var factory = MockRepository.GenerateMock<IOperatorFactory>();
            var negation = MockRepository.GenerateStub<IUnaryOperator>();
            var subtraction = MockRepository.GenerateStub<IBinaryOperator>();

            factory.Expect(f => f.GetUnaryOperator(Arg<string>.Is.Equal(validTokens[0])))
                .Return(negation);
            factory.Expect(f => f.GetBinaryOperator(Arg<string>.Is.Equal(validTokens[3])))
                .Return(subtraction);

            var tokenizer = new Tokenizer(factory);

            var result = tokenizer.Tokenize(validTokens).ToList();

            Assert.AreEqual(6, result.Count);
            Assert.AreEqual(negation, result[0].GetUnaryOperator());
            Assert.AreEqual(Bracket.Opening, result[1].GetBracket());
            Assert.AreEqual(1, result[2].GetValue());
            Assert.AreEqual(subtraction, result[3].GetBinaryOperator());
            Assert.AreEqual(2, result[4].GetValue());
            Assert.AreEqual(Bracket.Closing, result[5].GetBracket());
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void NotTokenizeTwoOperators()
        {
            var invalidTokens = new List<string> {"1", "+", "-", "2"};
            var factory = MockRepository.GenerateStub<IOperatorFactory>();
            var tokenizer = new Tokenizer(factory);

            var result = tokenizer.Tokenize(invalidTokens).ToList();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void NotTokenizeBracketsWithOperatorOnly()
        {
            var invalidTokens = new List<string> { "1", "+", "(", "-", ")" };
            var factory = MockRepository.GenerateStub<IOperatorFactory>();
            var tokenizer = new Tokenizer(factory);

            var result = tokenizer.Tokenize(invalidTokens).ToList();
        }
    }
}