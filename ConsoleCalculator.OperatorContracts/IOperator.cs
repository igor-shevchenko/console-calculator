namespace ConsoleCalculator.OperatorContracts
{
    public interface IOperator
    {
        string Sign { get; }
        int Precedence { get; }
    }
}