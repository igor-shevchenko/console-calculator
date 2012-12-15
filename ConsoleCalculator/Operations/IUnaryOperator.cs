namespace ConsoleCalculator.Operations
{
    public interface IUnaryOperator
    {
        int Precedence { get; }
        double Apply(double arg);
    }
}