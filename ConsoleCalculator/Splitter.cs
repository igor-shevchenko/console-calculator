using System;
using System.Collections.Generic;
using System.Globalization;

namespace ConsoleCalculator
{
    public class Splitter : ISplitter
    {
        private readonly List<char> separators = new List<char> {'+', '-', '*', '/', '(', ')'};
        public IEnumerable<string> Split(string s)
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