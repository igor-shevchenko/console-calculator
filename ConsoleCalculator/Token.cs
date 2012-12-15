using System;
using ConsoleCalculator.Operations;

namespace ConsoleCalculator
{
    public class Token
    {
        private readonly double value;
        private readonly IBinaryOperator binaryOperator;
        private readonly IUnaryOperator unaryOperator;
        private readonly Bracket bracket;
        public readonly bool IsValue;
        public readonly bool IsBinaryOperator;
        public readonly bool IsUnaryOperator;
        public readonly bool IsBracket;

        public Token(double value)
        {
            this.value = value;
            IsValue = true;
        }

        public Token(IBinaryOperator binaryOperator)
        {
            this.binaryOperator = binaryOperator;
            IsBinaryOperator = true;
        }

        public Token(IUnaryOperator unaryOperator)
        {
            this.unaryOperator = unaryOperator;
            IsUnaryOperator = true;
        }

        public Token (Bracket bracket)
        {
            this.bracket = bracket;
            IsBracket = true;
        }

        public double GetValue()
        {
            if (!IsValue)
                throw new Exception();
            return value;
        }

        public IBinaryOperator GetBinaryOperator()
        {
            if (!IsBinaryOperator)
                throw new Exception();
            return binaryOperator;
        }

        public IUnaryOperator GetUnaryOperator()
        {
            if (!IsUnaryOperator)
                throw new Exception();
            return unaryOperator;
        }

        public Bracket GetBracket()
        {
            if (!IsBracket)
                throw new Exception();
            return bracket;
        }
    }
}