namespace ConsoleCalculator.Operations
{
    public interface IUnaryOperator : IOperator
    {
        double Apply(double arg);
    }
}