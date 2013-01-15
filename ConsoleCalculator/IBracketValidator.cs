using System.Collections.Generic;
using ConsoleCalculator.Tokens;

namespace ConsoleCalculator
{
    public interface IBracketValidator
    {
        bool IsValid(IList<Token> tokens);
    }
}