using System.Linq;
using ConsoleCalculator.Detectors;
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
            var separatorDetector = MockRepository.GenerateMock<ISeparatorDetector>();
            separatorDetector.Expect(p => p.IsSeparator(Arg<string>.Is.Equal("1")))
                .Return(false);
            separatorDetector.Expect(p => p.IsSeparator(Arg<string>.Is.Equal("+")))
                .Return(true);
            separatorDetector.Expect(p => p.IsSeparator(Arg<string>.Is.Equal("2")))
                .Return(false);
            
            var splitter = new Splitter(separatorDetector);
            
            var result = splitter.Split(s).ToList();

            CollectionAssert.AreEqual(new [] {"1", "+", "2"}, result);
            separatorDetector.VerifyAllExpectations();
        }

        [Test]
        public void TestSplitMultidigitNumbers()
        {
            var s = "11+22";
            var separatorDetector = MockRepository.GenerateMock<ISeparatorDetector>();
            separatorDetector.Expect(p => p.IsSeparator(Arg<string>.Is.Equal("1")))
                .Return(false).Repeat.Twice();
            separatorDetector.Expect(p => p.IsSeparator(Arg<string>.Is.Equal("+")))
                .Return(true);
            separatorDetector.Expect(p => p.IsSeparator(Arg<string>.Is.Equal("2")))
                .Return(false).Repeat.Twice();

            var splitter = new Splitter(separatorDetector);

            var result = splitter.Split(s).ToList();

            CollectionAssert.AreEqual(new[] { "11", "+", "22" }, result);
            separatorDetector.VerifyAllExpectations();
        }

        [Test]
        public void TestSplitMultipleSeparators()
        {
            var s = "1++2";
            var separatorDetector = MockRepository.GenerateMock<ISeparatorDetector>();
            separatorDetector.Expect(p => p.IsSeparator(Arg<string>.Is.Equal("1")))
                .Return(false);
            separatorDetector.Expect(p => p.IsSeparator(Arg<string>.Is.Equal("+")))
                .Return(true).Repeat.Twice();
            separatorDetector.Expect(p => p.IsSeparator(Arg<string>.Is.Equal("2")))
                .Return(false);

            var splitter = new Splitter(separatorDetector);

            var result = splitter.Split(s).ToList();

            CollectionAssert.AreEqual(new[] { "1", "+", "+", "2"}, result);
            separatorDetector.VerifyAllExpectations();
        }

    }
}