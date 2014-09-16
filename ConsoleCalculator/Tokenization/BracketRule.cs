using ConsoleCalculator.Tokens;

namespace ConsoleCalculator.Tokenization
{
    public class BracketRule : ILexerRule
    {
        public Match GetMatch(string s, int pos, Token prevToken)
        {
            if (s[pos] == '(')
                return new Match(1, new Token(Bracket.Opening));
            if (s[pos] == ')')
                return new Match(1, new Token(Bracket.Closing));
            return null;
        }
    }
}