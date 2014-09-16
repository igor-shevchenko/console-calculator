using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ConsoleCalculator.Tokens;

namespace ConsoleCalculator.Tree
{
    /// <summary>
    /// —троит дерево выражени€ с учетом пор€дка операций.
    /// Ќаходит в выражении наименее приоритетную операцию и разбивает по ней выражение 
    /// на две (дл€ бинарных операций) части, которые аналогично обрабатываютс€ рекурсивно.
    /// Ќа выходе получаетс€ дерево, по которому можно снизу вверх вычислить значение выражени€.
    /// </summary>
    public class ExpressionTreeBuilder : IExpressionTreeBuilder
    {
        public IExpressionTree Build(IList<Token> tokens)
        {
            if (tokens.Count == 0)
                return null;

            if (IsExpressionASingleValue(tokens))
            {
                // ≈сли выражение состоит из единственного значени€, то возвращаетс€ узел дерева с этим значением.
                return new ExpressionTree(tokens[0], null);
            }

            // ѕопытаемс€ наименее приоритетный оператор, по которому выражение разбиваетс€ на две части.
            var index = GetIndexOfOperatorWithLowestPrecedence(tokens);

            if (index != -1)
            {
                var leftchild = Build(tokens.Take(index).ToList());
                var rightchild = Build(tokens.Skip(index + 1).ToList());

                var children = new List<IExpressionTree>();
                if (leftchild != null)
                    children.Add(leftchild);
                if (rightchild != null)
                    children.Add(rightchild);

                return new ExpressionTree(tokens[index], children);
            }

            // ≈сли всЄ выражение в скобках, то попытаемс€ отпарсить то, что внутри
            if (IsExpressionInBrackets(tokens))
            {
                
                return Build(tokens.Skip(1).Take(tokens.Count - 2).ToList());
            }

            throw new Exception("Can't parse: " + String.Join("", tokens.Select(t => t.ToString())));
            
        }

        private bool IsExpressionInBrackets(IList<Token> tokens)
        {
            var count = tokens.Count;
            if (count < 3)
                return false; // ¬ыражение в скобках Ч как минимум три токена.
            var startsWithOpeningBracket = tokens[0].Type == TokenType.OpeningBracket;
            var endsWithClosingBracket = tokens[count - 1].Type == TokenType.ClosingBracket;
            return startsWithOpeningBracket && endsWithClosingBracket;
        }

        private bool IsExpressionASingleValue(IList<Token> tokens)
        {
            return tokens.Count == 1 && tokens[0].Type == TokenType.Number;
        }

        private int GetIndexOfOperatorWithLowestPrecedence(IList<Token> tokens)
        {
            var leastIndex = -1;

            // ¬нутри скобок операторы искать не надо, поэтому будем считать, насколько глубоко мы внутри скобок.
            var bracketDepth = 0; 
            for (var i = 0; i < tokens.Count; ++i)
            {
                var token = tokens[i];
                if (token.Type == TokenType.OpeningBracket)
                {
                    bracketDepth++;
                }
                if (token.Type == TokenType.ClosingBracket)
                {
                    bracketDepth--;
                }
                if (bracketDepth > 0)
                    continue;
                if (bracketDepth < 0) // ћы вышли из скобок больше раз, чем зашли в них.
                    throw new Exception("Invalid use of brackets");

                if (token.Type == TokenType.BinaryOperator || 
                    token.Type == TokenType.UnaryOperator)
                {
                    if (leastIndex == -1 || 
                        token.GetOperatorPrecedence() <= tokens[leastIndex].GetOperatorPrecedence())
                    {
                        leastIndex = i;
                    }
                }
            }
            return leastIndex;
        }
    }
}