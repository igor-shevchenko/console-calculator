using System.Linq;
using ConsoleCalculator.Operations;

namespace ConsoleCalculator.Detectors
{
    public class OperationSignDetector : IOperationSignDetector
    {
        private readonly IOperatorListFactory factory;

        public OperationSignDetector(IOperatorListFactory factory)
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