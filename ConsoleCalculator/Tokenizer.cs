using System;
using System.Collections.Generic;

namespace ConsoleCalculator
{
    public class Tokenizer : ITokenizer
    {
        private readonly IOperatorFactory factory;
        private readonly ITokenTypeIdentifier tokenTypeIdentifier;

        public Tokenizer(IOperatorFactory factory, ITokenTypeIdentifier tokenTypeIdentifier)
        {
            this.factory = factory;
            this.tokenTypeIdentifier = tokenTypeIdentifier;
        }


        public IEnumerable<Token> Tokenize(IList<string> tokens)
        {
            for (var i = 0; i < tokens.Count; ++i)
            {
                var type = tokenTypeIdentifier.GetTokenType(tokens, i);
                switch(type)
                {
                    case TokenType.BinaryOperator:
                        yield return new Token(factory.GetBinaryOperator(tokens[i]));
                        break;
                    case TokenType.UnaryOperator:
                        yield return new Token(factory.GetUnaryOperator(tokens[i]));
                        break;
                    case TokenType.Value:
                        yield return new Token(Double.Parse(tokens[i]));
                        break;
                    case TokenType.Bracket:
                        if (tokens[i] == "(")
                            yield return new Token(Bracket.Opening);
                        else
                            yield return new Token(Bracket.Closing);
                        break;
                }
            }
        }

    }
}