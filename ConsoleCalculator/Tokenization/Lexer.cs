using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using ConsoleCalculator.Tokens;

namespace ConsoleCalculator.Tokenization
{
    public class Lexer : ILexer
    {
        private readonly IList<ILexerRule> rules;

        public Lexer(ILexerRuleListFactory ruleListFactory)
        {
            this.rules = ruleListFactory.GetRules();
        }

        public IEnumerable<Token> Tokenize(string s)
        {
            var pos = 0;
            Token matchedToken = null;
            while (pos < s.Length)
            {
                if (Char.IsWhiteSpace(s[pos]))
                {
                    pos++;
                    continue;
                }
                Match match = null;
                foreach (var rule in rules)
                {
                    match = rule.GetMatch(s, pos, matchedToken);
                    if (match != null)
                    {
                        break;
                    }
                }
                if (match == null)
                {
                    throw new Exception("Can't parse: " + s);
                }
                matchedToken = match.Token;
                yield return matchedToken;
                pos += match.Length;
            }
        }
    }
}
