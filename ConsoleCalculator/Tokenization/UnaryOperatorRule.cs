using ConsoleCalculator.OperatorContracts;
using ConsoleCalculator.Tokens;

namespace ConsoleCalculator.Tokenization
{
    public class UnaryOperatorRule : ILexerRule
    {
        private readonly IUnaryOperator unaryOperator;

        public UnaryOperatorRule(IUnaryOperator unaryOperator)
        {
            this.unaryOperator = unaryOperator;
        }

        public Match GetMatch(string s, int pos, Token prevToken)
        {
            if (prevToken != null && prevToken.Type != TokenType.OpeningBracket)
            {
                return null;
            }
            if (s.Substring(pos).StartsWith(unaryOperator.Sign))
            {
                return new Match(unaryOperator.Sign.Length, new Token(unaryOperator));
            }
            return null;
        }
    }
}