using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ConsoleCalculator.Tokens;

namespace ConsoleCalculator.Tree
{
    /// <summary>
    /// Строит дерево выражения с учетом порядка операций.
    /// Находит в выражении наименее приоритетную операцию и разбивает по ней выражение 
    /// на две (для бинарных операций) части, которые аналогично обрабатываются рекурсивно.
    /// На выходе получается дерево, по которому можно снизу вверх вычислить значение выражения.
    /// </summary>
    public class ExpressionTreeBuilder : IExpressionTreeBuilder
    {
        private readonly IBracketValidator bracketValidator;

        public ExpressionTreeBuilder(IBracketValidator bracketValidator)
        {
            this.bracketValidator = bracketValidator;
        }

        public IExpressionTree Build(IList<Token> tokens)
        {
            if (tokens.Count == 0)
                return null;
            if (IsExpressionInBrackets(tokens))
            {
                // Если всё выражение в скобках, то просто отбросим их.
                return Build(tokens.Skip(1).Take(tokens.Count - 2).ToList());
            }
            if (IsExpressionASingleValue(tokens))
            {
                // Если выражение состоит из единственного значения, то возвращается узел дерева с этим значением.
                return new ExpressionTree(tokens[0], null);
            }

            // В остальных случаях находится наименее приоритетный оператор, по которому выражение разбивается на две части.
            var index = GetIndexOfOperatorWithLowestPrecedence(tokens);
            
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
            var startsWithOpeningBracket = tokens[0].Type == TokenType.OpeningBracket;
            var endsWithClosingBracket = tokens[count - 1].Type == TokenType.ClosingBracket;
            return startsWithOpeningBracket && endsWithClosingBracket && bracketValidator.IsValid(tokens.Skip(1).Take(count - 2).ToList());
        }

        private bool IsExpressionASingleValue(IList<Token> tokens)
        {
            return tokens.Count == 1 && tokens[0].Type == TokenType.Value;
        }

        private int GetIndexOfOperatorWithLowestPrecedence(IList<Token> tokens)
        {
            var leastIndex = -1;

            // Внутри скобок операторы искать не надо, поэтому будем считать, насколько глубоко мы внутри скобок.
            var bracketDepth = 0; 
            for (var i = 0; i < tokens.Count; ++i)
            {
                var token = tokens[i];
                if (token.Type == TokenType.OpeningBracket)
                {
                    bracketDepth++;
                    continue;
                }
                if (token.Type == TokenType.ClosingBracket)
                {
                    bracketDepth--;
                    continue;
                }
                if (bracketDepth > 0)
                    continue;

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