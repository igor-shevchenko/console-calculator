using System.Collections.Generic;

namespace ConsoleCalculator.Operations
{
    public interface IOperatorListFactory
    {
        IList<IBinaryOperator> GetBinaryOperators();
        IList<IUnaryOperator> GetUnaryOperators();
    }
}