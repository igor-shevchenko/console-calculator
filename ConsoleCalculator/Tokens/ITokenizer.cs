using System.Collections.Generic;

namespace ConsoleCalculator.Tokens
{
    public interface ITokenizer
    {
        IEnumerable<Token> Tokenize(IList<string> tokens);
    }
}