using System.Collections.Generic;
using System.Linq;
using ConsoleCalculator.Operations;

namespace ConsoleCalculator.Tokenization
{
    public class LexerRuleListFactory : ILexerRuleListFactory
    {
        private readonly IOperatorListFactory operatorListFactory;

        public LexerRuleListFactory(IOperatorListFactory operatorListFactory)
        {
            this.operatorListFactory = operatorListFactory;
        }

        public IList<ILexerRule> GetRules()
        {
            var binaryOperatorRules = operatorListFactory.GetBinaryOperators()
                .Select(b => new BinaryOperatorRule(b));
            var unaryOperatorRules = operatorListFactory.GetUnaryOperators()
                .Select(u => new UnaryOperatorRule(u));
            var baseRules = new ILexerRule[]
            {
                new BracketRule(),
                new NumberRule()
            };
            return baseRules.Concat(binaryOperatorRules)
                .Concat(unaryOperatorRules)
                .ToList();
        }
    }
}