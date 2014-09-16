using System;
using System.Globalization;
using ConsoleCalculator.Tokens;

namespace ConsoleCalculator.Tokenization
{
    public class NumberRule : ILexerRule
    {
        public Match GetMatch(string s, int pos, Token prevToken)
        {
            var numberLength = 0;
            double? number = null;
            while (pos + numberLength < s.Length)
            {
                var ss = s.Substring(pos, numberLength+1);
                
                double parsedNumber;
                if (!Double.TryParse(ss, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out parsedNumber))
                {
                    break;
                }

                number = parsedNumber;
                numberLength++;   
            }
            if (!number.HasValue)
                return null;
            return new Match(numberLength, new Token(number.Value));
        }
    }
}