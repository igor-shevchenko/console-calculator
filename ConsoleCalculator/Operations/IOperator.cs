namespace ConsoleCalculator.Operations
{
    public interface IOperator
    {
        string Sign { get; }
        int Precedence { get; }
    }
}