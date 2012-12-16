using System.Collections.Generic;

namespace ConsoleCalculator
{
    public interface IOperationSignProvider
    {
        bool IsBinaryOperator(string sign);
        bool IsUnaryOperator(string sign);
        bool IsOperator(string sign);
    }
}