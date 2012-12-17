using System.Collections.Generic;

namespace ConsoleCalculator.Tokens
{
    public interface ITokenTypeIdentifier
    {
        TokenType GetTokenType(IList<string> tokens, int position);
    }
}