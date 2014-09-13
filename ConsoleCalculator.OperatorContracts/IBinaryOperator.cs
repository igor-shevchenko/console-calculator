namespace ConsoleCalculator.OperatorContracts
{
    public interface IBinaryOperator : IOperator
    {
        double Apply(double arg1, double arg2);
    }
}