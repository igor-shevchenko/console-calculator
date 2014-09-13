using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleCalculator.Operations;
using ConsoleCalculator.OperatorContracts;
using ConsoleCalculator.Tokens;
using NUnit.Framework;
using Rhino.Mocks;

namespace ConsoleCalculator.Tests
{
    [TestFixture]
    class BracketValidatorTest
    {
        [Test]
        public void TestNotValidateUnbalancedLeftBracket()
        {
            var op = MockRepository.GenerateStub<IBinaryOperator>();
            var invalidTokens = new List<Token>
                                    {
                                        new Token(1),
                                        new Token(op),
                                        new Token(Bracket.Opening),
                                        new Token(2)
                                    };

            var validator = new BracketValidator();

            var result = validator.IsValid(invalidTokens);

            Assert.IsFalse(result);
        }

        [Test]
        public void TestNotValidateUnbalancedRightBracket()
        {
            var op = MockRepository.GenerateStub<IBinaryOperator>();
            var invalidTokens = new List<Token>
                                    {
                                        new Token(1),
                                        new Token(op),
                                        new Token(2),
                                        new Token(Bracket.Closing),
                                    };

            var validator = new BracketValidator();

            var result = validator.IsValid(invalidTokens);

            Assert.IsFalse(result);
        }

        [Test]
        public void TestNotValidateSwitchedBracket()
        {
            var op = MockRepository.GenerateStub<IBinaryOperator>();
            var invalidTokens = new List<Token>
                                    {
                                        new Token(Bracket.Opening),
                                        new Token(1),
                                        new Token(op),
                                        new Token(2),
                                        new Token(Bracket.Opening),
                                    };

            var validator = new BracketValidator();

            var result = validator.IsValid(invalidTokens);

            Assert.IsFalse(result);
        }

        [Test]
        public void TestNotValidateEmptyBrackets()
        {
            var op = MockRepository.GenerateStub<IBinaryOperator>();
            var invalidTokens = new List<Token>
                                    {
                                        new Token(1),
                                        new Token(op),
                                        new Token(Bracket.Opening),
                                        new Token(Bracket.Closing),
                                    };

            var validator = new BracketValidator();

            var result = validator.IsValid(invalidTokens);

            Assert.IsFalse(result);
        }

        [Test]
        public void TestValidateBalancedBrackets()
        {
            var addition = MockRepository.GenerateStub<IBinaryOperator>();
            var multiplication = MockRepository.GenerateStub<IBinaryOperator>();
            var division = MockRepository.GenerateStub<IBinaryOperator>();
            var subtraction = MockRepository.GenerateStub<IBinaryOperator>();
            var validTokens = new List<Token>
                                    {
                                        new Token(1),
                                        new Token(addition),
                                        new Token(Bracket.Opening),
                                        new Token(Bracket.Opening),
                                        new Token(Bracket.Opening),
                                        new Token(2),
                                        new Token(multiplication),
                                        new Token(3),
                                        new Token(Bracket.Closing),
                                        new Token(division),
                                        new Token(4),
                                        new Token(Bracket.Closing),
                                        new Token(subtraction),
                                        new Token(5),
                                        new Token(Bracket.Closing),
                                    };
            var validator = new BracketValidator();

            var result = validator.IsValid(validTokens);

            Assert.IsTrue(result);
        }
    }
}