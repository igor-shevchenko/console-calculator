using System.Linq;
using NUnit.Framework;
using ConsoleCalculator;
namespace ConsoleCalculator.Tests
{
    [TestFixture]
    class SplitterTest
    {
        [Test]
        public void SplitString()
        {
            var splitter = new Splitter();

            var result = splitter.Split("1+2").ToList();

            CollectionAssert.AreEqual(new [] {"1", "+", "2"}, result);
        }

        [Test]
        public void SplitMultidigitNumbers()
        {
            var splitter = new Splitter();

            var result = splitter.Split("11+22").ToList();

            CollectionAssert.AreEqual(new[] { "11", "+", "22" }, result);
        }

        [Test]
        public void SplitNegativeNumbers()
        {
            var splitter = new Splitter();

            var result = splitter.Split("-1+(-2)").ToList();

            CollectionAssert.AreEqual(new[] { "-", "1", "+", "(", "-", "2", ")" }, result);
        }

        [Test]
        public void SplitMultipleBrackets()
        {
            var splitter = new Splitter();

            var result = splitter.Split("1+(((2*3)/4)-5)").ToList();

            CollectionAssert.AreEqual(new[] { "1", "+", "(", "(", "(", "2", "*", "3", ")", "/", "4", ")", "-", "5", ")" },
                result);
        }
    }
}