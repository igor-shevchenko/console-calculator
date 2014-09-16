using ConsoleCalculator.OperatorContracts;
using ConsoleCalculator.Tokens;

namespace ConsoleCalculator.Tokenization
{
    public class BinaryOperatorRule : ILexerRule
    {
        private readonly IBinaryOperator binaryOperator;

        public BinaryOperatorRule(IBinaryOperator binaryOperator)
        {
            this.binaryOperator = binaryOperator;
        }

        public Match GetMatch(string s, int pos, Token prevToken)
        {
            if (prevToken == null || prevToken.Type != TokenType.Value && prevToken.Type != TokenType.ClosingBracket)
            {
                return null;
            }
            if (s.Substring(pos).StartsWith(binaryOperator.Sign))
            {
                return new Match(binaryOperator.Sign.Length, new Token(binaryOperator));
            }
            return null;
        }
    }
}