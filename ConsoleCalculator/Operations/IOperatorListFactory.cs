using System.Collections.Generic;
using ConsoleCalculator.OperatorContracts;

namespace ConsoleCalculator.Operations
{
    public interface IOperatorListFactory
    {
        IList<IBinaryOperator> GetBinaryOperators();
        IList<IUnaryOperator> GetUnaryOperators();
    }
}