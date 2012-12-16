using System;
using System.Collections.Generic;

namespace ConsoleCalculator
{
    public class BracketValidator : IBracketValidator
    {
        public void Validate(IList<Token> tokens)
        {
            var openedBracketCount = 0;
            for (var i = 0; i < tokens.Count; ++i)
            {
                var token = tokens[i];
                if (token.IsBracket)
                {
                    if (token.GetBracket() == Bracket.Opening)
                    {
                        openedBracketCount++;
                        if (AreBracketsEmpty(tokens, i))
                            throw new ArgumentException("Empty brackets");
                    } else
                    {
                        openedBracketCount--;
                        if (openedBracketCount < 0)
                            throw new ArgumentException("Unbalanced brackets");
                    }
                }
            }
            if (openedBracketCount != 0)
                throw new ArgumentException("Unbalanced brackets");
        }

        private bool AreBracketsEmpty(IList<Token> tokens, int positionOfOpeningBracket)
        {
            return (positionOfOpeningBracket < tokens.Count - 1) && 
                    tokens[positionOfOpeningBracket+1].IsBracket && 
                    tokens[positionOfOpeningBracket+1].GetBracket() == Bracket.Closing;
        }
    }
}