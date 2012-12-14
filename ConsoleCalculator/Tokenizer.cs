using System;
using System.Collections.Generic;
using System.Globalization;

namespace ConsoleCalculator
{
    public class Tokenizer : ITokenizer
    {
        private readonly List<char> separators = new List<char> {'+', '-', '*', '/', '(', ')'};
        public IEnumerable<string> GetTokensFrom(string s)
        {
            var accumulator = String.Empty;
            foreach (var t in s)
            {
                if (t == ' ')
                    continue;
                if (separators.Contains(t))
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