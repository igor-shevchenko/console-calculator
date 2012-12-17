using System.Collections.Generic;
using ConsoleCalculator.Tokens;

namespace ConsoleCalculator.Tree
{
    public interface IExpressionTreeBuilder
    {
        IExpressionTree Build(IList<Token> tokens);
    }
}