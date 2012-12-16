using System.Collections.Generic;
using System.Linq;
using ConsoleCalculator.Operations;

namespace ConsoleCalculator
{
    public class OperationSignProvider : IOperationSignProvider
    {
        private readonly IOperatorListFactory factory;

        public OperationSignProvider(IOperatorListFactory factory)
        {
            this.factory = factory;
        }

        public bool IsBinaryOperator(string sign)
        {
            return factory.GetBinaryOperators()
                            .Select(o => o.Sign)
                            .Contains(sign);
        }

        public bool IsUnaryOperator(string sign)
        {
            return factory.GetUnaryOperators()
                            .Select(o => o.Sign)
                            .Contains(sign);
        }

        public bool IsOperator(string sign)
        {
            return IsBinaryOperator(sign) || IsUnaryOperator(sign);
        }
    }
}