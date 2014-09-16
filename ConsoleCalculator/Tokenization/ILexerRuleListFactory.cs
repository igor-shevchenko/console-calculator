using System.Collections.Generic;

namespace ConsoleCalculator.Tokenization
{
    public interface ILexerRuleListFactory
    {
        IList<ILexerRule> GetRules();
    }
}