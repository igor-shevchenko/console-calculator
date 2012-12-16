namespace ConsoleCalculator.Operations
{
    public interface IUnaryOperator
    {
        string Sign { get; }
        int Precedence { get; }
        double Apply(double arg);
    }
}