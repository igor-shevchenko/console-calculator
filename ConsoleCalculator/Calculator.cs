using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleCalculator.Tokens;
using ConsoleCalculator.Tree;

namespace ConsoleCalculator
{
    public class Calculator : ICalculator
    {
        private readonly ISplitter splitter;
        private readonly ITokenizer tokenizer;
        private readonly IExpressionTreeBuilder expressionTreeBuilder;
        private readonly IBracketValidator bracketValidator;

        public Calculator(ISplitter splitter, ITokenizer tokenizer, IExpressionTreeBuilder expressionTreeBuilder, IBracketValidator bracketValidator)
        {
            this.splitter = splitter;
            this.tokenizer = tokenizer;
            this.expressionTreeBuilder = expressionTreeBuilder;
            this.bracketValidator = bracketValidator;
        }

        public double Calculate(string s)
        {
            var splittedString = splitter.Split(s).ToList();
            var tokens = tokenizer.Tokenize(splittedString).ToList();
            if (!bracketValidator.IsValid(tokens))
                throw new Exception("Bracket error");
            var expressionTree = expressionTreeBuilder.Build(tokens);
            return expressionTree.GetResult();
        }
    }
}
