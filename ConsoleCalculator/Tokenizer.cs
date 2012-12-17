using System;
using System.Collections.Generic;

namespace ConsoleCalculator
{
    public class Tokenizer : ITokenizer
    {
        private readonly ITokenBuilder tokenBuilder;
        private readonly ITokenTypeIdentifier tokenTypeIdentifier;

        public Tokenizer(ITokenTypeIdentifier tokenTypeIdentifier, ITokenBuilder tokenBuilder)
        {
            this.tokenTypeIdentifier = tokenTypeIdentifier;
            this.tokenBuilder = tokenBuilder;
        }


        public IEnumerable<Token> Tokenize(IList<string> tokens)
        {
            for (var i = 0; i < tokens.Count; ++i)
            {
                var type = tokenTypeIdentifier.GetTokenType(tokens, i);
                yield return tokenBuilder.Build(tokens[i], type);
            }
        }

    }
}