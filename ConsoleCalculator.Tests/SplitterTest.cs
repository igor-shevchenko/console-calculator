using System.Linq;
using NUnit.Framework;
using ConsoleCalculator;
using Rhino.Mocks;

namespace ConsoleCalculator.Tests
{
    [TestFixture]
    class SplitterTest
    {
        [Test]
        public void TestSplitString()
        {
            var s = "1+2";
            var separatorProvider = MockRepository.GenerateMock<ISeparatorProvider>();
            separatorProvider.Expect(p => p.IsSeparator(Arg<string>.Is.Equal("1")))
                .Return(false);
            separatorProvider.Expect(p => p.IsSeparator(Arg<string>.Is.Equal("+")))
                .Return(true);
            separatorProvider.Expect(p => p.IsSeparator(Arg<string>.Is.Equal("2")))
                .Return(false);
            
            var splitter = new Splitter(separatorProvider);
            
            var result = splitter.Split(s).ToList();

            CollectionAssert.AreEqual(new [] {"1", "+", "2"}, result);
            separatorProvider.VerifyAllExpectations();
        }

        [Test]
        public void TestSplitMultidigitNumbers()
        {
            var s = "11+22";
            var separatorProvider = MockRepository.GenerateMock<ISeparatorProvider>();
            separatorProvider.Expect(p => p.IsSeparator(Arg<string>.Is.Equal("1")))
                .Return(false).Repeat.Twice();
            separatorProvider.Expect(p => p.IsSeparator(Arg<string>.Is.Equal("+")))
                .Return(true);
            separatorProvider.Expect(p => p.IsSeparator(Arg<string>.Is.Equal("2")))
                .Return(false).Repeat.Twice();

            var splitter = new Splitter(separatorProvider);

            var result = splitter.Split(s).ToList();

            CollectionAssert.AreEqual(new[] { "11", "+", "22" }, result);
            separatorProvider.VerifyAllExpectations();
        }

        [Test]
        public void TestSplitMultipleSeparators()
        {
            var s = "1++2";
            var separatorProvider = MockRepository.GenerateMock<ISeparatorProvider>();
            separatorProvider.Expect(p => p.IsSeparator(Arg<string>.Is.Equal("1")))
                .Return(false);
            separatorProvider.Expect(p => p.IsSeparator(Arg<string>.Is.Equal("+")))
                .Return(true).Repeat.Twice();
            separatorProvider.Expect(p => p.IsSeparator(Arg<string>.Is.Equal("2")))
                .Return(false);

            var splitter = new Splitter(separatorProvider);

            var result = splitter.Split(s).ToList();

            CollectionAssert.AreEqual(new[] { "1", "+", "+", "2"}, result);
            separatorProvider.VerifyAllExpectations();
        }

    }
}