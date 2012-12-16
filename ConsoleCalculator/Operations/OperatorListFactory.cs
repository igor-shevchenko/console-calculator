using System.Collections.Generic;

namespace ConsoleCalculator.Operations
{
    public class OperatorListFactory : IOperatorListFactory
    {
        public IList<IBinaryOperator> GetBinaryOperators()
        {
            return new IBinaryOperator[]
                       {
                           new AdditionOperator(),
                           new DivisionOperator(),
                           new MultiplicationOperator(), 
                           new SubtractionOperator(),
                       };
        }

        public IList<IUnaryOperator> GetUnaryOperators()
        {
            return new IUnaryOperator[]
                       {
                           new NegationOperator(),
                       };
        } 
    }
}