using ConsoleCalculator.Operations;

namespace ConsoleCalculator
{
    public interface IOperatorFactory
    {
        IBinaryOperator GetBinaryOperator(string sign);
        IUnaryOperator GetUnaryOperator(string sign);
    }
}