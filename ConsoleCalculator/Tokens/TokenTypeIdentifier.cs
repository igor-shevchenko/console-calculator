using System;
using System.Collections.Generic;
using ConsoleCalculator.Detectors;

namespace ConsoleCalculator.Tokens
{
    public class TokenTypeIdentifier : ITokenTypeIdentifier
    {

        private readonly IOperationSignDetector operationSignDetector;
        private readonly IBracketSignDetector bracketSignDetector;

        public TokenTypeIdentifier(IOperationSignDetector operationSignDetector, IBracketSignDetector bracketSignDetector)
        {
            this.operationSignDetector = operationSignDetector;
            this.bracketSignDetector = bracketSignDetector;
        }

        public TokenType GetTokenType(IList<string> tokens, int position)
        {
            var token = tokens[position];
            double n;
            if (IsUnaryOperator(tokens, position))
                return TokenType.UnaryOperator;
            if (IsBinaryOperation(tokens, position))
                return TokenType.BinaryOperator;
            if (Double.TryParse(token, out n))
                return TokenType.Value;
            if (bracketSignDetector.IsOpeningBracket(token))
                return TokenType.OpeningBracket;
            if (bracketSignDetector.IsClosingBracket(token))
                return TokenType.ClosingBracket;
            throw new ArgumentException("Unknown token");
        }

        private bool IsUnaryOperator(IList<string> tokens, int position)
        {
            double n;
            var isBeforeNumber = (position < tokens.Count - 1) && Double.TryParse(tokens[position + 1], out n);
            var isBeforeOpeningBracket = (position < tokens.Count - 1) &&
                                         bracketSignDetector.IsOpeningBracket(tokens[position + 1]);
            var isAtBeginning = position == 0;
            var isAfterOpeningBracket = (position > 0) && bracketSignDetector.IsOpeningBracket(tokens[position - 1]);
            var isCorrectUnaryOperator = operationSignDetector.IsUnaryOperator(tokens[position]);
            return isCorrectUnaryOperator && (isBeforeNumber || isBeforeOpeningBracket) && (isAtBeginning || isAfterOpeningBracket);
        }

        private bool IsBinaryOperation(IList<string> tokens, int position)
        {
            double n;
            var isBeforeNumber = (position < tokens.Count - 1) && Double.TryParse(tokens[position + 1], out n);
            var isBeforeOpeningBracket = (position < tokens.Count - 1) &&
                                         bracketSignDetector.IsOpeningBracket(tokens[position + 1]);
            var isAfterNumber = (position > 0) && Double.TryParse(tokens[position - 1], out n);
            var isAfterClosingBracket = (position > 0) && bracketSignDetector.IsClosingBracket(tokens[position - 1]);
            var isCorrectBinaryOperation = operationSignDetector.IsBinaryOperator(tokens[position]);
            return isCorrectBinaryOperation && (isBeforeNumber || isBeforeOpeningBracket) && (isAfterNumber || isAfterClosingBracket);
        }
    }
}