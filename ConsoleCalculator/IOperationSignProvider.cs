using System.Collections.Generic;

namespace ConsoleCalculator
{
    public interface IOperationSignProvider
    {
        IList<string> GetBinaryOperatorSigns();
        IList<string> GetUnaryOperatorSigns();
        IList<string> GetOperatorSigns();
        bool IsBinaryOperator(string sign);
        bool IsUnaryOperator(string sign);
        bool IsOperator(string sign);
    }
}