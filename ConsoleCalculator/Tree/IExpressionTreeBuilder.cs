using System.Collections.Generic;

namespace ConsoleCalculator.Tree
{
    public interface IExpressionTreeBuilder
    {
        IExpressionTree Build(IList<Token> tokens);
    }
}