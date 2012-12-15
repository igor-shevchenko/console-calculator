using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ConsoleCalculator.Tree
{
    public class ExpressionTreeBuilder : IExpressionTreeBuilder
    {
        public IExpressionTree Build(List<Token> tokens)
        {
            if (tokens.Count == 0)
                return null;
            if (IsExpressionInBrackets(tokens))
            {
                return Build(tokens.GetRange(1, tokens.Count - 2));
            }
            if (IsExpressionASingleValue(tokens))
            {
                return new ExpressionTree(tokens[0], null);
            }
            var index = GetIndexOfLeastPrecedentOperation(tokens);
            var leftchild = Build(tokens.GetRange(0, index));
            var rightchild = Build(tokens.GetRange(index + 1, tokens.Count - 1 - index));
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
                    if (leastIndex == -1 || GetOperatorPrecedence(token) < GetOperatorPrecedence(tokens[leastIndex]))
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