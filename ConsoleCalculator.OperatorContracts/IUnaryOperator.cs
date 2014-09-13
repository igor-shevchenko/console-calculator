namespace ConsoleCalculator.OperatorContracts
{
    public interface IUnaryOperator : IOperator
    {
        double Apply(double arg);
    }
}