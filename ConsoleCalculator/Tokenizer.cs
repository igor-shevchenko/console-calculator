using System;
using System.Collections.Generic;
using System.Globalization;

namespace ConsoleCalculator
{
    public class Tokenizer : ITokenizer
    {
        private readonly List<char> infixOperations = new List<char> {'+', '-', '*', '/'};
        private readonly List<char> prefixOperations = new List<char> { '-' };
        private readonly List<char> parentheses = new List<char> {'(', ')'};
        public IEnumerable<string> GetTokensFrom(string s)
        {
            var accumulator = String.Empty;
            foreach (var t in s)
            {
                if (t == ' ')
                    continue;
                if (prefixOperations.Contains(t) && String.IsNullOrEmpty(accumulator))
                {
                    accumulator += t;
                }
                else if (infixOperations.Contains(t) || parentheses.Contains(t))
                {
                    if (!String.IsNullOrEmpty(accumulator))
                    {
                        yield return accumulator;
                        accumulator = String.Empty;
                    }
                    yield return t.ToString(CultureInfo.InvariantCulture);
                } else
                {
                    accumulator += t;
                }
            }
            if (!String.IsNullOrEmpty(accumulator))
                yield return accumulator;
        }
    }
}