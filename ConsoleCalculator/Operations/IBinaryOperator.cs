namespace ConsoleCalculator.Operations
{
    public interface IBinaryOperator
    {
        string Sign { get; }
        int Precedence { get; }
        double Apply(double arg1, double arg2);
    }
}