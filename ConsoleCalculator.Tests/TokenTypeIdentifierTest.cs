using System;
using System.Collections.Generic;
using ConsoleCalculator.Detectors;
using NUnit.Framework;
using Rhino.Mocks;

namespace ConsoleCalculator.Tests
{
    [TestFixture]
    class TokenTypeIdentifierTest
    {
        [Test]
        public void TestIdentifyUnaryOperator()
        {
            var tokens = new[] {"-", "1"};
            var operationSignDetector = MockRepository.GenerateMock<IOperationSignDetector>();
            var bracketSignDetector = MockRepository.GenerateMock<IBracketSignDetector>();
            operationSignDetector.Expect(d => d.IsUnaryOperator(tokens[0])).Return(true);
            bracketSignDetector.Expect(d => d.IsOpeningBracket(tokens[1])).Return(false);

            var identifier = new TokenTypeIdentifier(operationSignDetector, bracketSignDetector);
            var result = identifier.GetTokenType(tokens, 0);

            Assert.AreEqual(TokenType.UnaryOperator, result);
            operationSignDetector.VerifyAllExpectations();
            bracketSignDetector.VerifyAllExpectations();
        }

        [Test]
        public void TestIdentifyBinaryOperator()
        {
            var tokens = new[] { "2", "-", "1" };
            var operationSignDetector = MockRepository.GenerateMock<IOperationSignDetector>();
            var bracketSignDetector = MockRepository.GenerateMock<IBracketSignDetector>();
            operationSignDetector.Expect(d => d.IsUnaryOperator(tokens[1])).Return(false);
            operationSignDetector.Expect(d => d.IsBinaryOperator(tokens[1])).Return(true);
            bracketSignDetector.Expect(d => d.IsOpeningBracket(tokens[2])).Return(false).Repeat.Any();
            bracketSignDetector.Expect(d => d.IsClosingBracket(tokens[0])).Return(false).Repeat.Any();

            var identifier = new TokenTypeIdentifier(operationSignDetector, bracketSignDetector);
            var result = identifier.GetTokenType(tokens, 1);

            Assert.AreEqual(TokenType.BinaryOperator, result);
            operationSignDetector.VerifyAllExpectations();
            bracketSignDetector.VerifyAllExpectations();
        }

        [Test]
        public void TestIdentifyValue()
        {
            var tokens = new[] { "1" };
            var operationSignDetector = MockRepository.GenerateMock<IOperationSignDetector>();
            var bracketSignDetector = MockRepository.GenerateMock<IBracketSignDetector>();
            operationSignDetector.Expect(d => d.IsUnaryOperator(tokens[0])).Return(false);
            operationSignDetector.Expect(d => d.IsBinaryOperator(tokens[0])).Return(false);

            var identifier = new TokenTypeIdentifier(operationSignDetector, bracketSignDetector);
            var result = identifier.GetTokenType(tokens, 0);

            Assert.AreEqual(TokenType.Value, result);
            operationSignDetector.VerifyAllExpectations();
            bracketSignDetector.VerifyAllExpectations();
        }

        [Test]
        public void TestIdentifyBracket()
        {
            var tokens = new[] { "(" };
            var operationSignDetector = MockRepository.GenerateMock<IOperationSignDetector>();
            var bracketSignDetector = MockRepository.GenerateMock<IBracketSignDetector>();
            operationSignDetector.Expect(d => d.IsUnaryOperator(tokens[0])).Return(false);
            operationSignDetector.Expect(d => d.IsBinaryOperator(tokens[0])).Return(false);
            bracketSignDetector.Expect(d => d.IsOpeningBracket(tokens[0])).Return(true);
            var identifier = new TokenTypeIdentifier(operationSignDetector, bracketSignDetector);
            var result = identifier.GetTokenType(tokens, 0);

            Assert.AreEqual(TokenType.OpeningBracket, result);
            operationSignDetector.VerifyAllExpectations();
            bracketSignDetector.VerifyAllExpectations();
        }

        [ExpectedException(typeof(ArgumentException))]
        [Test]
        public void TestNotIdentifyInvalidToken()
        {
            var tokens = new[] { "aas" };
            var operationSignDetector = MockRepository.GenerateMock<IOperationSignDetector>();
            var bracketSignDetector = MockRepository.GenerateMock<IBracketSignDetector>();
            operationSignDetector.Expect(d => d.IsUnaryOperator(tokens[0])).Return(false);
            operationSignDetector.Expect(d => d.IsBinaryOperator(tokens[0])).Return(false);
            bracketSignDetector.Expect(d => d.IsBracket(tokens[0])).Return(false);
            var identifier = new TokenTypeIdentifier(operationSignDetector, bracketSignDetector);
            var result = identifier.GetTokenType(tokens, 0);

            operationSignDetector.VerifyAllExpectations();
            bracketSignDetector.VerifyAllExpectations();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void TestNotIdentifyWrongUnaryOperator()
        {
            var tokens = new[] { "+", "2", "-", "1" };
            var operationSignDetector = MockRepository.GenerateMock<IOperationSignDetector>();
            var bracketSignDetector = MockRepository.GenerateMock<IBracketSignDetector>();
            operationSignDetector.Expect(d => d.IsUnaryOperator(tokens[0])).Return(false);
            operationSignDetector.Expect(d => d.IsBinaryOperator(tokens[0])).Return(true);
            bracketSignDetector.Expect(d => d.IsOpeningBracket(tokens[1])).Return(false).Repeat.Any();

            var identifier = new TokenTypeIdentifier(operationSignDetector, bracketSignDetector);
            var result = identifier.GetTokenType(tokens, 0);

            operationSignDetector.VerifyAllExpectations();
            bracketSignDetector.VerifyAllExpectations();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void TestNotIdentifyWrongUnaryOperatorInBrackets()
        {
            var tokens = new[] { "1", "+", "(", "*", "2", ")" };
            var operationSignDetector = MockRepository.GenerateMock<IOperationSignDetector>();
            var bracketSignDetector = MockRepository.GenerateMock<IBracketSignDetector>();
            operationSignDetector.Expect(d => d.IsUnaryOperator(tokens[3])).Return(false);
            operationSignDetector.Expect(d => d.IsBinaryOperator(tokens[3])).Return(true);
            bracketSignDetector.Expect(d => d.IsOpeningBracket(tokens[2])).Return(true).Repeat.Any();
            bracketSignDetector.Expect(d => d.IsClosingBracket(tokens[2])).Return(false).Repeat.Any();
            bracketSignDetector.Expect(d => d.IsOpeningBracket(tokens[4])).Return(false).Repeat.Any();
            bracketSignDetector.Expect(d => d.IsClosingBracket(tokens[4])).Return(false).Repeat.Any();

            var identifier = new TokenTypeIdentifier(operationSignDetector, bracketSignDetector);
            var result = identifier.GetTokenType(tokens, 3);

            operationSignDetector.VerifyAllExpectations();
            bracketSignDetector.VerifyAllExpectations();
        }

        [Test]
        public void TestIdentifyUnaryOperationBeforeBrackets()
        {
            var tokens = new [] { "-", "(", "1", "-", "2", ")", };
            var operationSignDetector = MockRepository.GenerateMock<IOperationSignDetector>();
            var bracketSignDetector = MockRepository.GenerateMock<IBracketSignDetector>();
            operationSignDetector.Expect(d => d.IsUnaryOperator(tokens[0])).Return(true);
            bracketSignDetector.Expect(d => d.IsOpeningBracket(tokens[1])).Return(true);

            var identifier = new TokenTypeIdentifier(operationSignDetector, bracketSignDetector);
            var result = identifier.GetTokenType(tokens, 0);

            Assert.AreEqual(TokenType.UnaryOperator, result);
            operationSignDetector.VerifyAllExpectations();
            bracketSignDetector.VerifyAllExpectations();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void TestNotIdentifyTwoOperators()
        {
            var tokens = new[] { "1", "+", "-", "2" };
            var operationSignDetector = MockRepository.GenerateMock<IOperationSignDetector>();
            var bracketSignDetector = MockRepository.GenerateMock<IBracketSignDetector>();
            operationSignDetector.Expect(d => d.IsUnaryOperator(tokens[1])).Return(false);
            operationSignDetector.Expect(d => d.IsBinaryOperator(tokens[1])).Return(true);
            bracketSignDetector.Expect(d => d.IsOpeningBracket(tokens[0])).Return(false).Repeat.Any();
            bracketSignDetector.Expect(d => d.IsClosingBracket(tokens[0])).Return(false).Repeat.Any();
            bracketSignDetector.Expect(d => d.IsOpeningBracket(tokens[2])).Return(false).Repeat.Any();
            bracketSignDetector.Expect(d => d.IsClosingBracket(tokens[2])).Return(false).Repeat.Any();

            var identifier = new TokenTypeIdentifier(operationSignDetector, bracketSignDetector);
            var result = identifier.GetTokenType(tokens, 1);

            operationSignDetector.VerifyAllExpectations();
            bracketSignDetector.VerifyAllExpectations();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void TestNotIdentifyBracketsWithOperatorOnly()
        {
            var tokens = new[] { "1", "+", "(", "-", ")" };
            var operationSignDetector = MockRepository.GenerateMock<IOperationSignDetector>();
            var bracketSignDetector = MockRepository.GenerateMock<IBracketSignDetector>();
            operationSignDetector.Expect(d => d.IsUnaryOperator(tokens[3])).Return(false);
            operationSignDetector.Expect(d => d.IsBinaryOperator(tokens[3])).Return(true);
            bracketSignDetector.Expect(d => d.IsOpeningBracket(tokens[2])).Return(true).Repeat.Any();
            bracketSignDetector.Expect(d => d.IsClosingBracket(tokens[2])).Return(false).Repeat.Any();
            bracketSignDetector.Expect(d => d.IsOpeningBracket(tokens[4])).Return(false).Repeat.Any();
            bracketSignDetector.Expect(d => d.IsClosingBracket(tokens[4])).Return(true).Repeat.Any();

            var identifier = new TokenTypeIdentifier(operationSignDetector, bracketSignDetector);
            var result = identifier.GetTokenType(tokens, 1);

            operationSignDetector.VerifyAllExpectations();
            bracketSignDetector.VerifyAllExpectations();
        }
    }
}