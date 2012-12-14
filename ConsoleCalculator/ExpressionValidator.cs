using System;
using System.Collections.Generic;

namespace ConsoleCalculator
{
    public class ExpressionValidator
    {
        private readonly List<string> infixOperations = new List<string> { "+", "-", "*", "/" };
        private readonly List<string> prefixOperations = new List<string> { "-" };
        private readonly List<string> brackets = new List<string> { "(", ")" };

        public bool Validate(List<string> tokens)
        {
            var openedBracketsCount = 0;
            for (var i = 0; i < tokens.Count; ++i)
            {
                var token = tokens[i];
                double n;
                if (IsPrefixOperation(tokens, i) ||
                    IsInfixOperation(tokens, i) ||
                    Double.TryParse(token, out n))
                {
                    continue;
                }
                if (brackets.Contains(token))
                {
                    if (token == brackets[0])
                    {
                        openedBracketsCount++;
                        if (!AreBracketsEmpty(tokens, i))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        openedBracketsCount--;
                        if (openedBracketsCount < 0)
                            return false;
                    }
                    continue;
                }
                return false;
            }
            return openedBracketsCount == 0;
        }

        private bool AreBracketsEmpty(List<string> tokens, int position)
        {
            return (position < tokens.Count - 1) && (tokens[position + 1] != brackets[1]);
        }

        private bool IsPrefixOperation(List<string> tokens, int position)
        {
            double n;
            var isBeforeNumberOrOpeningBracket = (position < tokens.Count - 1) && (tokens[position + 1] == brackets[0] || Double.TryParse(tokens[position + 1], out n));
            var isAtBeginning = position == 0;
            var isAfterOpeningBracket = (position > 0) && tokens[position - 1] == brackets[0];
            var isCorrectPrefixOperation = prefixOperations.Contains(tokens[position]);
            return isBeforeNumberOrOpeningBracket && isCorrectPrefixOperation && (isAtBeginning || isAfterOpeningBracket);
        }

        private bool IsInfixOperation(List<string> tokens, int position)
        {
            double n;
            var isBeforeNumberOrOpeningBracket = (position < tokens.Count - 1) && (tokens[position + 1] == brackets[0] || Double.TryParse(tokens[position + 1], out n));
            var isAfterNumberOrClosingBracket = (position > 0) && (tokens[position - 1] == brackets[1] || Double.TryParse(tokens[position - 1], out n));
            var isCorrectInfixOperation = infixOperations.Contains(tokens[position]);
            return isBeforeNumberOrOpeningBracket && isAfterNumberOrClosingBracket && isCorrectInfixOperation;

        }
    }
}