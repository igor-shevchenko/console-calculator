using System.Collections.Generic;

namespace ConsoleCalculator
{
    public interface ITokenTypeIdentifier
    {
        TokenType GetTokenType(IList<string> tokens, int position);
    }
}