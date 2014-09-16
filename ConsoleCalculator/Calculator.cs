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

        public Calculator(ILexer lexer, IExpressionTreeBuilder expressionTreeBuilder)
        {
            this.lexer = lexer;
            this.expressionTreeBuilder = expressionTreeBuilder;
        }

        public double Calculate(string s)
        {
            var tokens = lexer.Tokenize(s).ToList();
            var expressionTree = expressionTreeBuilder.Build(tokens);
            return expressionTree.GetResult();
        }
    }
}
