using System.Collections.Generic;

namespace ConsoleCalculator
{
    public interface IBracketValidator
    {
        void Validate(IList<Token> tokens);
    }
}