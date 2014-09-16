using ConsoleCalculator.Tokens;

namespace ConsoleCalculator.Tokenization
{
    public interface ILexerRule
    {
        Match GetMatch(string s, int pos, Token prevToken);
    }
}