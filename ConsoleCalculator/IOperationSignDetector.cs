using System.Collections.Generic;

namespace ConsoleCalculator
{
    public interface IOperationSignDetector
    {
        bool IsBinaryOperator(string sign);
        bool IsUnaryOperator(string sign);
        bool IsOperator(string sign);
    }
}