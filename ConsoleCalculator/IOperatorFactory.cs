using ConsoleCalculator.Operations;
using ConsoleCalculator.OperatorContracts;

namespace ConsoleCalculator
{
    public interface IOperatorFactory
    {
        IBinaryOperator GetBinaryOperator(string sign);
        IUnaryOperator GetUnaryOperator(string sign);
    }
}