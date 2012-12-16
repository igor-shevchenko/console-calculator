using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ConsoleCalculator
{
    public class Splitter : ISplitter
    {
        private readonly ISeparatorDetector separatorDetector;

        public Splitter(ISeparatorDetector separatorDetector)
        {
            this.separatorDetector = separatorDetector;
        }

        public IEnumerable<string> Split(string s)
        {
            var accumulator = String.Empty;
            foreach (var c in s)
            {
                if (c == ' ')
                    continue;
                var t = c.ToString(CultureInfo.InvariantCulture);

                if (separatorDetector.IsSeparator(t))
                {
                    if (!String.IsNullOrEmpty(accumulator))
                    {
                        yield return accumulator;
                        accumulator = String.Empty;
                    }
                    yield return t;
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