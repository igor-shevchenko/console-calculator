using System.Collections.Generic;

namespace ConsoleCalculator.Operations
{
    public class OperatorListFactory : IOperatorListFactory
    {
        private IList<IBinaryOperator> cachedBinaryOperators;
        private IList<IUnaryOperator> cachedUnaryOperators;

        public IList<IBinaryOperator> GetBinaryOperators()
        {
            if (cachedBinaryOperators != null)
                return cachedBinaryOperators;

            cachedBinaryOperators = new IBinaryOperator[]
                                    {
                                        new AdditionOperator(),
                                        new DivisionOperator(),
                                        new MultiplicationOperator(), 
                                        new SubtractionOperator(),
                                    };
            return cachedBinaryOperators;

        }

        public IList<IUnaryOperator> GetUnaryOperators()
        {
            if (cachedUnaryOperators != null)
                return cachedUnaryOperators;
            
            cachedUnaryOperators = new IUnaryOperator[]
                                   {
                                       new NegationOperator(),
                                   };
            return cachedUnaryOperators;
        } 
    }
}