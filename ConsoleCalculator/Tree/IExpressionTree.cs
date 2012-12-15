using System.Collections.Generic;

namespace ConsoleCalculator.Tree
{
    public interface IExpressionTree
    {
        Token Token { get; }
        IList<IExpressionTree> Children { get; }
        double GetResult();
    }
}