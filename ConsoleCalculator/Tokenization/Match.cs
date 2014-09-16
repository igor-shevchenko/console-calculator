using ConsoleCalculator.Tokens;

namespace ConsoleCalculator.Tokenization
{
    public class Match
    {
        public readonly int Length;
        public readonly Token Token;

        public Match(int length, Token token)
        {
            Length = length;
            Token = token;
        }
    }
}