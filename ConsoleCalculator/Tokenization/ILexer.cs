using System.Collections.Generic;
using ConsoleCalculator.Tokens;

namespace ConsoleCalculator.Tokenization
{
    public interface ILexer
    {
        IEnumerable<Token> Tokenize(string s);
    }
}