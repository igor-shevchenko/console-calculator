using System.Collections.Generic;
using ConsoleCalculator.Tokens;

namespace ConsoleCalculator.Tree
{
    public interface IExpressionTree
    {
        Token Token { get; }
        IList<IExpressionTree> Children { get; }
        double GetResult();
    }
}