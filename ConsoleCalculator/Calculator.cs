using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleCalculator.Tree;

namespace ConsoleCalculator
{
    public class Calculator : ICalculator
    {
        private readonly ISplitter splitter;
        private readonly ITokenizer tokenizer;
        private readonly IExpressionTreeBuilder expressionTreeBuilder;

        public Calculator(ISplitter splitter, ITokenizer tokenizer, IExpressionTreeBuilder expressionTreeBuilder)
        {
            this.splitter = splitter;
            this.tokenizer = tokenizer;
            this.expressionTreeBuilder = expressionTreeBuilder;
        }

        public double Calculate(string s)
        {
            var splittedString = splitter.Split(s).ToList();
            var tokens = tokenizer.Tokenize(splittedString).ToList();
            var expressionTree = expressionTreeBuilder.Build(tokens);
            return expressionTree.GetResult();
        }
    }
}
