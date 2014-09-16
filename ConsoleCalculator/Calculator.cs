using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using ConsoleCalculator.Tokenization;
using ConsoleCalculator.Tokens;
using ConsoleCalculator.Tree;

namespace ConsoleCalculator
{
    public class Calculator : ICalculator
    {
        private readonly ILexer lexer;
        private readonly IExpressionTreeBuilder expressionTreeBuilder;
        private readonly IBracketValidator bracketValidator;

        public Calculator(ILexer lexer, IExpressionTreeBuilder expressionTreeBuilder, IBracketValidator bracketValidator)
        {
            this.lexer = lexer;
            this.expressionTreeBuilder = expressionTreeBuilder;
            this.bracketValidator = bracketValidator;
        }

        public double Calculate(string s)
        {
            var tokens = lexer.Tokenize(s).ToList();
            if (!bracketValidator.IsValid(tokens))
                throw new Exception("Bracket error");
            var expressionTree = expressionTreeBuilder.Build(tokens);
            return expressionTree.GetResult();
        }
    }
}
