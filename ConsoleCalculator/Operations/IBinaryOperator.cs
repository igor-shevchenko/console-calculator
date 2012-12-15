namespace ConsoleCalculator.Operations
{
    public interface IBinaryOperator
    {
        int Precedence { get; }
        double Apply(double arg1, double arg2);
    }
}