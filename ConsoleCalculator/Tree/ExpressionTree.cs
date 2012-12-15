using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleCalculator.Tree
{
    public class ExpressionTree : IExpressionTree
    {
        private readonly Token token;
        private readonly IList<IExpressionTree> children;

        public ExpressionTree(Token token, IList<IExpressionTree> children)
        {
            this.token = token;
            this.children = children;
        }

        public double GetResult()
        {
            if (token.IsValue)
                return token.GetValue();
            if (token.IsUnaryOperator)
            {
                var operation = token.GetUnaryOperator();
                var childResult = children.First().GetResult();
                return operation.Apply(childResult);
            }
            if (token.IsBinaryOperator)
            {
                var operation = token.GetBinaryOperator();
                var leftChildResult = children[0].GetResult();
                var rightChildResult = children[1].GetResult();
                return operation.Apply(leftChildResult, rightChildResult);
            }
            throw new Exception();
        }
    }
}
