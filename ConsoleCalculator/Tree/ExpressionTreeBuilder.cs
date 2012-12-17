using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ConsoleCalculator.Tree
{
    public class ExpressionTreeBuilder : IExpressionTreeBuilder
    {
        public IExpressionTree Build(IList<Token> tokens)
        {
            if (tokens.Count == 0)
                return null;
            if (IsExpressionInBrackets(tokens))
            {
                return Build(tokens.Skip(1).Take(tokens.Count - 2).ToList());
            }
            if (IsExpressionASingleValue(tokens))
            {
                return new ExpressionTree(tokens[0], null);
            }
            var index = GetIndexOfLeastPrecedentOperation(tokens);
            var leftchild = Build(tokens.Take(index).ToList());
            var rightchild = Build(tokens.Skip(index + 1).ToList());
            var children = new List<IExpressionTree>();
            if (leftchild != null)
                children.Add(leftchild);
            if (rightchild != null)
                children.Add(rightchild);
            return new ExpressionTree(tokens[index], children);
        }

        private bool IsExpressionInBrackets(IList<Token> tokens)
        {
            var count = tokens.Count;
            if (count < 2)
                return false;
            var startsWithOpeningBracket = tokens[0].IsBracket && tokens[0].GetBracket() == Bracket.Opening;
            var endsWithClosingBracket = tokens[count - 1].IsBracket && tokens[count - 1].GetBracket() == Bracket.Closing;
            return startsWithOpeningBracket && endsWithClosingBracket;
        }

        private bool IsExpressionASingleValue(IList<Token> tokens)
        {
            if (tokens.Count > 1)
                return false;
            return tokens[0].IsValue;
        }

        private int GetIndexOfLeastPrecedentOperation(IList<Token> tokens)
        {
            var leastIndex = -1;
            var bracketDepth = 0;
            for (var i = 0; i < tokens.Count; ++i)
            {
                var token = tokens[i];
                if (token.IsBracket)
                {
                    if (token.GetBracket() == Bracket.Opening)
                        bracketDepth++;
                    else
                        bracketDepth--;
                    continue;
                }
                if (bracketDepth > 0)
                    continue;

                if (token.IsBinaryOperator || token.IsUnaryOperator)
                {
                    if (leastIndex == -1 || GetOperatorPrecedence(token) <= GetOperatorPrecedence(tokens[leastIndex]))
                    {
                        leastIndex = i;
                    }
                }
            }
            return leastIndex;
        }

        private int GetOperatorPrecedence(Token op)
        {
            if (op.IsBinaryOperator)
                return op.GetBinaryOperator().Precedence;
            if (op.IsUnaryOperator)
                return op.GetUnaryOperator().Precedence;
            throw new Exception();
        }
    }
}