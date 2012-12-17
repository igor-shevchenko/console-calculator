using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleCalculator.Tokens;

namespace ConsoleCalculator.Tree
{
    public class ExpressionTree : IExpressionTree
    {
        public Token Token { get; private set; }

        public IList<IExpressionTree> Children { get; private set; }

        public ExpressionTree(Token token, IList<IExpressionTree> children)
        {
            this.Token = token;
            this.Children = children;
        }

        public double GetResult()
        {
            if (Token.Type == TokenType.Value)
                return Token.GetValue();
            if (Token.Type == TokenType.UnaryOperator)
            {
                var operation = Token.GetUnaryOperator();
                var childResult = Children.First().GetResult();
                return operation.Apply(childResult);
            }
            if (Token.Type == TokenType.BinaryOperator)
            {
                var operation = Token.GetBinaryOperator();
                var leftChildResult = Children[0].GetResult();
                var rightChildResult = Children[1].GetResult();
                return operation.Apply(leftChildResult, rightChildResult);
            }
            throw new Exception();
        }
    }
}
