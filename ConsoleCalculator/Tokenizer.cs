using System;
using System.Collections.Generic;

namespace ConsoleCalculator
{
    public class Tokenizer : ITokenizer
    {
        private readonly IOperatorFactory factory;

        private readonly List<string> infixOperations = new List<string> { "+", "-", "*", "/" };
        private readonly List<string> prefixOperations = new List<string> { "-" };
        private readonly List<string> brackets = new List<string> { "(", ")" };

        public Tokenizer(IOperatorFactory factory)
        {
            this.factory = factory;
        }


        public IEnumerable<Token> Tokenize(IList<string> tokens)
        {
            
            var openedBracketsCount = 0;
            for (var i = 0; i < tokens.Count; ++i)
            {
                var token = tokens[i];
                double n;
                if (IsPrefixOperation(tokens, i))
                {
                    yield return new Token(factory.GetUnaryOperator(token)); 
                } 
                else if (IsInfixOperation(tokens, i))
                {
                    yield return new Token(factory.GetBinaryOperator(token));
                } 
                else if (Double.TryParse(token, out n))
                {
                    yield return new Token(n);
                }
                else if (brackets.Contains(token))
                {
                    if (token == brackets[0])
                    {
                        openedBracketsCount++;
                        if (!AreBracketsEmpty(tokens, i))
                        {
                            throw new ArgumentException("Empty brackets");
                        }
                        yield return new Token(Bracket.Opening);
                    }
                    else
                    {
                        openedBracketsCount--;
                        if (openedBracketsCount < 0)
                            throw new ArgumentException("Unbalanced brackets");
                        yield return new Token(Bracket.Closing);
                    }
                }
                else
                {
                    throw new ArgumentException("Unknown token");
                }
            }
            if (openedBracketsCount != 0)
                throw new ArgumentException("Unbalanced brackets");
        }

        private bool AreBracketsEmpty(IList<string> tokens, int position)
        {
            return (position < tokens.Count - 1) && (tokens[position + 1] != brackets[1]);
        }

        private bool IsPrefixOperation(IList<string> tokens, int position)
        {
            double n;
            var isBeforeNumberOrOpeningBracket = (position < tokens.Count - 1) && (tokens[position + 1] == brackets[0] || Double.TryParse(tokens[position + 1], out n));
            var isAtBeginning = position == 0;
            var isAfterOpeningBracket = (position > 0) && tokens[position - 1] == brackets[0];
            var isCorrectPrefixOperation = prefixOperations.Contains(tokens[position]);
            return isBeforeNumberOrOpeningBracket && isCorrectPrefixOperation && (isAtBeginning || isAfterOpeningBracket);
        }

        private bool IsInfixOperation(IList<string> tokens, int position)
        {
            double n;
            var isBeforeNumberOrOpeningBracket = (position < tokens.Count - 1) && (tokens[position + 1] == brackets[0] || Double.TryParse(tokens[position + 1], out n));
            var isAfterNumberOrClosingBracket = (position > 0) && (tokens[position - 1] == brackets[1] || Double.TryParse(tokens[position - 1], out n));
            var isCorrectInfixOperation = infixOperations.Contains(tokens[position]);
            return isBeforeNumberOrOpeningBracket && isAfterNumberOrClosingBracket && isCorrectInfixOperation;

        }
    }
}