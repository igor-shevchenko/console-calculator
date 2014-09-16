using System;
using ConsoleCalculator.Operations;
using ConsoleCalculator.OperatorContracts;

namespace ConsoleCalculator.Tokens
{
    public class Token
    {
        private readonly object value;
        public readonly TokenType Type;

        public Token(double value)
        {
            this.value = value;
            Type = TokenType.Number;
        }

        public Token(IBinaryOperator binaryOperator)
        {
            value = binaryOperator;
            Type = TokenType.BinaryOperator;
        }

        public Token(IUnaryOperator unaryOperator)
        {
            value = unaryOperator;
            Type = TokenType.UnaryOperator;
        }

        public Token(Bracket bracket)
        {
            value = bracket;
            Type = bracket == Bracket.Opening ? TokenType.OpeningBracket : TokenType.ClosingBracket;
        }


        public double GetValue()
        {
            if (Type != TokenType.Number)
                throw new Exception(Type + " is not number");
            return (double) value;
        }

        public IBinaryOperator GetBinaryOperator()
        {
            if (Type != TokenType.BinaryOperator)
                throw new Exception(Type + " is not binary operator");
            return (IBinaryOperator) value;
        }

        public IUnaryOperator GetUnaryOperator()
        {
            if (Type != TokenType.UnaryOperator)
                throw new Exception(Type + " is not unary operator");
            return (IUnaryOperator) value;
        }

        public Bracket GetBracket()
        {
            if (Type != TokenType.OpeningBracket && Type != TokenType.ClosingBracket)
                throw new Exception(Type + " is not bracket");
            return (Bracket) value;
        }

        public int GetOperatorPrecedence()
        {
            if (Type == TokenType.BinaryOperator || Type == TokenType.UnaryOperator)
                return ((IOperator) value).Precedence;
            throw new Exception(Type + " is not operator");
        }
    }
}