using System.Linq;
using NUnit.Framework;
using ConsoleCalculator;
namespace ConsoleCalculator.Tests
{
    [TestFixture]
    class TokenizerTest
    {
        [Test]
        public void TokenizeString()
        {
            var tokenizer = new Tokenizer();

            var result = tokenizer.GetTokensFrom("1+2").ToList();

            CollectionAssert.AreEqual(new [] {"1", "+", "2"}, result);
        }

        [Test]
        public void TokenizeMultidigitNumbers()
        {
            var tokenizer = new Tokenizer();

            var result = tokenizer.GetTokensFrom("11+22").ToList();

            CollectionAssert.AreEqual(new[] { "11", "+", "22" }, result);
        }

        [Test]
        public void TokenizeNegativeNumbers()
        {
            var tokenizer = new Tokenizer();

            var result = tokenizer.GetTokensFrom("-1+(-2)").ToList();

            CollectionAssert.AreEqual(new[] { "-", "1", "+", "(", "-", "2", ")" }, result);
        }

        [Test]
        public void TokenizeMultipleParentheses()
        {
            var tokenizer = new Tokenizer();

            var result = tokenizer.GetTokensFrom("1+(((2*3)/4)-5)").ToList();

            CollectionAssert.AreEqual(new[] { "1", "+", "(", "(", "(", "2", "*", "3", ")", "/", "4", ")", "-", "5", ")" },
                result);
        }
    }
}