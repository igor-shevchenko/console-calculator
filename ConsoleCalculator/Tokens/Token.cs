using System;
using ConsoleCalculator.Operations;

namespace ConsoleCalculator.Tokens
{
    public class Token
    {
        private readonly object token;
        public readonly TokenType Type;
        
        public Token(double value)
        {
            token = value;
            Type = TokenType.Value;
        }

        public Token(IBinaryOperator binaryOperator)
        {
            token = binaryOperator;
            Type = TokenType.BinaryOperator;
        }

        public Token(IUnaryOperator unaryOperator)
        {
            token = unaryOperator;
            Type = TokenType.UnaryOperator;
        }

        public Token(Bracket bracket)
        {
            token = bracket;
            Type = bracket == Bracket.Opening? TokenType.OpeningBracket : TokenType.ClosingBracket;
        }


        public double GetValue()
        {
            if (Type != TokenType.Value)
                throw new Exception();
            return (double)token;
        }

        public IBinaryOperator GetBinaryOperator()
        {
            if (Type != TokenType.BinaryOperator)
                throw new Exception();
            return (IBinaryOperator)token;
        }

        public IUnaryOperator GetUnaryOperator()
        {
            if (Type != TokenType.UnaryOperator)
                throw new Exception();
            return (IUnaryOperator)token;
        }

        public Bracket GetBracket()
        {
            if (Type != TokenType.OpeningBracket || Type != TokenType.ClosingBracket)
                throw new Exception();
            return (Bracket)token;
        }
    }
}