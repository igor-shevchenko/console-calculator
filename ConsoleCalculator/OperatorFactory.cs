using System;
using ConsoleCalculator.Operations;

namespace ConsoleCalculator
{
    public class OperatorFactory : IOperatorFactory
    {
        public IBinaryOperator GetBinaryOperator(string sign)
        {
            switch(sign)
            {
                case "+":
                    return new AdditionOperator();
                case "-":
                    return new SubtractionOperator();
                case "*":
                    return new MultiplicationOperator();
                case "/":
                    return new DivisionOperator();
                default:
                    throw new ArgumentException("Unknown binary operator: {0}", sign);
            }
        }
        
        public IUnaryOperator GetUnaryOperator(string sign)
        {
            switch(sign)
            {
                case "-":
                    return new NegationOperator();
                default:
                    throw new ArgumentException("Unknown unary operator: {0}", sign);
            }
        }
    }

}