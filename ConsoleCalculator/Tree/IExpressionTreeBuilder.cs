using System.Collections.Generic;

namespace ConsoleCalculator.Tree
{
    public interface IExpressionTreeBuilder
    {
        ExpressionTree Build(List<Token> tokens);
    }
}