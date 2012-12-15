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
        private readonly bool isValue;
        private readonly bool isBinaryOperator;
        private readonly bool isUnaryOperator;
        private readonly bool isBracket;

        public Token(double value)
        {
            this.value = value;
            isValue = true;
        }

        public Token(IBinaryOperator binaryOperator)
        {
            this.binaryOperator = binaryOperator;
            isBinaryOperator = true;
        }

        public Token(IUnaryOperator unaryOperator)
        {
            this.unaryOperator = unaryOperator;
            isUnaryOperator = true;
        }

        public Token (Bracket bracket)
        {
            this.bracket = bracket;
            isBracket = true;
        }

        public double GetValue()
        {
            if (!isValue)
                throw new Exception();
            return value;
        }

        public IBinaryOperator GetBinaryOperator()
        {
            if (!isBinaryOperator)
                throw new Exception();
            return binaryOperator;
        }

        public IUnaryOperator GetUnaryOperator()
        {
            if (!isUnaryOperator)
                throw new Exception();
            return unaryOperator;
        }

        public Bracket GetBracket()
        {
            if (!isBracket)
                throw new Exception();
            return bracket;
        }
    }
}