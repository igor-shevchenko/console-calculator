using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleCalculator.OperatorContracts;
using ConsoleCalculator.Tokenization;
using ConsoleCalculator.Tokens;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Rhino.Mocks;

namespace ConsoleCalculator.Tests
{
    [TestFixture]    
    class LexerRulesTest
    {
        [Test]
        public void TestNumberRule()
        {
            var rule = new NumberRule();
            var validMatch = rule.GetMatch("2.58", 0, null);
            Assert.NotNull(validMatch);
            Assert.AreEqual(2.58, validMatch.Token.GetValue());
            Assert.AreEqual(4, validMatch.Length);

            var partialMatch = rule.GetMatch("2.58+7", 0, null);
            Assert.NotNull(partialMatch);
            Assert.AreEqual(2.58, partialMatch.Token.GetValue());
            Assert.AreEqual(4, partialMatch.Length);

            var negativeMatch = rule.GetMatch("-2.58", 0, null);
            Assert.Null(negativeMatch);

            var matchFromMiddle = rule.GetMatch("1+2.58", 2, null);
            Assert.NotNull(matchFromMiddle);
            Assert.AreEqual(2.58, matchFromMiddle.Token.GetValue());
            Assert.AreEqual(4, matchFromMiddle.Length);


            var noMatch = rule.GetMatch(".", 0, null);
            Assert.Null(noMatch);
        }

        [Test]
        public void TestBracketRule()
        {
            var rule = new BracketRule();
            var s = "(1+2)";
            var openingMatch = rule.GetMatch(s, 0, null);
            Assert.NotNull(openingMatch);
            Assert.AreEqual(Bracket.Opening, openingMatch.Token.GetBracket());
            Assert.AreEqual(1, openingMatch.Length);

            var closingMatch = rule.GetMatch(s, 4, null);
            Assert.NotNull(closingMatch);
            Assert.AreEqual(Bracket.Closing, closingMatch.Token.GetBracket());
            Assert.AreEqual(1, closingMatch.Length);


            var noMatch = rule.GetMatch(s, 2, null);
            Assert.Null(noMatch);
        }

        [Test]
        public void TestBinaryOperatorRule()
        {
            var binaryOperator = MockRepository.GenerateStub<IBinaryOperator>();
            binaryOperator.Expect(b => b.Sign).Return("+");
            var rule = new BinaryOperatorRule(binaryOperator);

            var binaryMatch = rule.GetMatch("2+3", 1, new Token(2));
            Assert.NotNull(binaryMatch);
            Assert.AreEqual(binaryOperator, binaryMatch.Token.GetBinaryOperator());
            Assert.AreEqual(1, binaryMatch.Length);

            var bracketMatch = rule.GetMatch("(1+2)+3", 5, new Token(Bracket.Closing));
            Assert.NotNull(bracketMatch);
            Assert.AreEqual(binaryOperator, bracketMatch.Token.GetBinaryOperator());
            Assert.AreEqual(1, bracketMatch.Length);

            var noMatchAtStart = rule.GetMatch("+2", 0, null);
            Assert.Null(noMatchAtStart);

            var noMatchInBrackets = rule.GetMatch("1+(+2)", 3, new Token(Bracket.Opening));
            Assert.Null(noMatchInBrackets);
        }

        [Test]
        public void TestUnaryOperatorRule()
        {
            var unaryOperator = MockRepository.GenerateStub<IUnaryOperator>();
            unaryOperator.Expect(b => b.Sign).Return("-");
            var rule = new UnaryOperatorRule(unaryOperator);

            var matchAtStart = rule.GetMatch("-3", 0, null);
            Assert.NotNull(matchAtStart);
            Assert.AreEqual(unaryOperator, matchAtStart.Token.GetUnaryOperator());
            Assert.AreEqual(1, matchAtStart.Length);

            var matchInBrackets = rule.GetMatch("1+(-3)", 3, new Token(Bracket.Opening));
            Assert.NotNull(matchInBrackets);
            Assert.AreEqual(unaryOperator, matchInBrackets.Token.GetUnaryOperator());
            Assert.AreEqual(1, matchInBrackets.Length);

            var noBinaryMatch = rule.GetMatch("1-2", 1, new Token(1));
            Assert.Null(noBinaryMatch);

            var noMatchAfterBrackets = rule.GetMatch("(1+2)-3", 4, new Token(Bracket.Closing));
            Assert.Null(noMatchAfterBrackets);
        }

    }
}
