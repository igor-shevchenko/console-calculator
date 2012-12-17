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
        public void TestTokenize()
        {
            var tokens = new string[] {"-", "1"};
            var tokenType1 = TokenType.UnaryOperator;
            var tokenType2 = TokenType.Value;
            var token1 = new Token(1);
            var token2 = new Token(2);
            var identifier = MockRepository.GenerateMock<ITokenTypeIdentifier>();
            var builder = MockRepository.GenerateMock<ITokenBuilder>();
            identifier.Expect(i => i.GetTokenType(Arg<IList<string>>.Is.Equal(tokens), Arg<int>.Is.Equal(0)))
                .Return(tokenType1);
            identifier.Expect(i => i.GetTokenType(Arg<IList<string>>.Is.Equal(tokens), Arg<int>.Is.Equal(1)))
                .Return(tokenType2);
            builder.Expect(b => b.Build(Arg<string>.Is.Equal(tokens[0]), Arg<TokenType>.Is.Equal(tokenType1)))
                .Return(token1);
            builder.Expect(b => b.Build(Arg<string>.Is.Equal(tokens[1]), Arg<TokenType>.Is.Equal(tokenType2)))
                .Return(token2);
            var tokenizer = new Tokenizer(identifier, builder);

            var result = tokenizer.Tokenize(tokens).ToList();

            CollectionAssert.AreEqual(new [] {token1, token2}, result);
            identifier.VerifyAllExpectations();
            builder.VerifyAllExpectations();
        }
    }
}