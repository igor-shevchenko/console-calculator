using System;

namespace ConsoleCalculator
{
    public class TokenBuilder : ITokenBuilder
    {
        private readonly IOperatorFactory factory;

        public TokenBuilder(IOperatorFactory factory)
        {
            this.factory = factory;
        }

        public Token Build(string token, TokenType type)
        {
            switch (type)
            {
                case TokenType.BinaryOperator:
                    return new Token(factory.GetBinaryOperator(token));
                case TokenType.UnaryOperator:
                    return new Token(factory.GetUnaryOperator(token));
                case TokenType.Value:
                    return new Token(Double.Parse(token));
                case TokenType.OpeningBracket:
                    return new Token(Bracket.Opening);
                case TokenType.ClosingBracket:
                    return new Token(Bracket.Closing);
                default:
                    throw new Exception("Unknown token type");
            }
        }
    }
}