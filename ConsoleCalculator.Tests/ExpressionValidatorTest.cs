using System.Collections.Generic;
using NUnit.Framework;

namespace ConsoleCalculator.Tests
{
    [TestFixture]
    class ExpressionValidatorTest
    {
        [Test]
        public void ValidateValidTokens()
        {
            var validTokens = new List<string> {"1", "+", "2"};
            var validator = new ExpressionValidator();
            
            var result = validator.Validate(validTokens);
            
            Assert.AreEqual(true, result);
        }

        [Test]
        public void NotValidateInvalidTokens()
        {
            var invalidTokens = new List<string> { "aas", "ads"};
            var validator = new ExpressionValidator();

            var result = validator.Validate(invalidTokens);

            Assert.AreEqual(false, result);
        }

        [Test]
        public void NotValidateUnbalancedLeftBracket()
        {
            var invalidTokens = new List<string> { "1", "+", "(", "2" };
            var validator = new ExpressionValidator();

            var result = validator.Validate(invalidTokens);

            Assert.AreEqual(false, result);
        }

        [Test]
        public void NotValidateUnbalancedRightBracket()
        {
            var invalidTokens = new List<string> { "1", "+", "2", ")" };
            var validator = new ExpressionValidator();

            var result = validator.Validate(invalidTokens);

            Assert.AreEqual(false, result);
        }

        [Test]
        public void NotValidateEmptyBrackets()
        {
            var invalidTokens = new List<string> { "1", "+", "(", ")" };
            var validator = new ExpressionValidator();

            var result = validator.Validate(invalidTokens);

            Assert.AreEqual(false, result);
        }

        [Test]
        public void ValidateBalancedBrackets()
        {
            var validTokens = new List<string> { "1", "+", "(", "(", "(", "2", "*", "3", ")", "/", "4", ")", "-", "5", ")" };
            var validator = new ExpressionValidator();

            var result = validator.Validate(validTokens);

            Assert.AreEqual(true, result);
        }

        [Test]
        public void NotValidateWrongPrefixOperation()
        {
            var invalidTokens = new List<string> {"+", "1", "+", "2",};
            var validator = new ExpressionValidator();

            var result = validator.Validate(invalidTokens);

            Assert.AreEqual(false, result);
        }

        [Test]
        public void NotValidateWrongPrefixOperationInBrackets()
        {
            var invalidTokens = new List<string> { "1", "+", "(", "*", "2", ")", };
            var validator = new ExpressionValidator();

            var result = validator.Validate(invalidTokens);

            Assert.AreEqual(false, result);
        }

        [Test]
        public void ValidateCorrectPrefixOperations()
        {
            var validTokens = new List<string> { "-", "1", "+", "(", "-", "2", ")", };
            var validator = new ExpressionValidator();

            var result = validator.Validate(validTokens);

            Assert.AreEqual(true, result);
        }

        [Test]
        public void ValidatePrefixOperationBeforeBrackets()
        {
            var validTokens = new List<string> { "-", "(", "1", "-", "2", ")", };
            var validator = new ExpressionValidator();

            var result = validator.Validate(validTokens);

            Assert.AreEqual(true, result); 
        }

        [Test]
        public void NotValidateTwoOperators()
        {
            var invalidTokens = new List<string> { "1", "+", "-", "2" };
            var validator = new ExpressionValidator();

            var result = validator.Validate(invalidTokens);

            Assert.AreEqual(false, result);
        }

        [Test]
        public void NotValidateBracketsWithOperatorOnly()
        {
            var invalidTokens = new List<string> { "1", "+", "(", "-", ")" };
            var validator = new ExpressionValidator();

            var result = validator.Validate(invalidTokens);

            Assert.AreEqual(false, result);
        }
    }
}