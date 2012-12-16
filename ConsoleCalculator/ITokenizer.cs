using System.Collections.Generic;

namespace ConsoleCalculator
{
    public interface ITokenizer
    {
        IEnumerable<Token> Tokenize(IList<string> tokens);
    }
}