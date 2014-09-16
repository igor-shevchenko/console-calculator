using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleCalculator.Tokens;

namespace ConsoleCalculator.Tree
{
    public class ExpressionTree : IExpressionTree
    {
        public Token Token { get; private set; }

        private readonly IList<IExpressionTree> children;
        public IEnumerable<IExpressionTree> Children
        {
            get { return children.AsEnumerable(); }
        }

        public ExpressionTree(Token token, IList<IExpressionTree> children)
        {
            this.Token = token;
            this.children = children;
        }

        public double GetResult()
        {
            if (Token.Type == TokenType.Number)
                return Token.GetNumber();
            if (Token.Type == TokenType.UnaryOperator)
            {
                var operation = Token.GetUnaryOperator();
                var childResult = children.First().GetResult();
                return operation.Apply(childResult);
            }
            if (Token.Type == TokenType.BinaryOperator)
            {
                var operation = Token.GetBinaryOperator();
                var leftChildResult = children[0].GetResult();
                var rightChildResult = children[1].GetResult();
                return operation.Apply(leftChildResult, rightChildResult);
            }
            throw new Exception("Unexpected token type");
        }
    }
}
