using System;
using System.Collections.Generic;
using ConsoleCalculator.Tokens;

namespace ConsoleCalculator
{
    public class BracketValidator : IBracketValidator
    {
        public bool IsValid(IList<Token> tokens)
        {
            var openedBracketCount = 0;
            for (var i = 0; i < tokens.Count; ++i)
            {
                var token = tokens[i];
                if (token.Type == TokenType.OpeningBracket)
                {
                    openedBracketCount++;
                    if (AreBracketsEmpty(tokens, i))
                        return false;
                } else if (token.Type == TokenType.ClosingBracket)
                {
                    openedBracketCount--;
                    if (openedBracketCount < 0)
                        return false;
                }
            }
            return openedBracketCount == 0;
        }

        private bool AreBracketsEmpty(IList<Token> tokens, int positionOfOpeningBracket)
        {
            return (positionOfOpeningBracket < tokens.Count - 1) && 
                    tokens[positionOfOpeningBracket+1].Type == TokenType.ClosingBracket;
        }
    }
}