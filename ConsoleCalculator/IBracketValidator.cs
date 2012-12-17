using System.Collections.Generic;
using ConsoleCalculator.Tokens;

namespace ConsoleCalculator
{
    public interface IBracketValidator
    {
        void Validate(IList<Token> tokens);
    }
}