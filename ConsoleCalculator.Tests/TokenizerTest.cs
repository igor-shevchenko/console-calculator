using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleCalculator.Operations;
using NUnit.Framework;
using Rhino.Mocks;

namespace ConsoleCalculator.Tests
{
    [TestFixture]
    class TokenizerTest
    {
        [Test]
        public void TestTokenize()
        {
            var tokens = new string[] {"1", "+", "(", "-", "2", ")"};
            // TODO
        }
    }
}