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

        public IList<string> GetBinaryOperatorSigns()
        {
            return factory.GetBinaryOperators().Select(o => o.Sign).ToList();
        }

        public IList<string> GetUnaryOperatorSigns()
        {
            return factory.GetUnaryOperators().Select(o => o.Sign).ToList();
        }

        public IList<string> GetOperatorSigns()
        {
            return GetBinaryOperatorSigns().Concat(GetUnaryOperatorSigns()).Distinct().ToList();
        }

        public bool IsBinaryOperator(string sign)
        {
            return GetBinaryOperatorSigns().Contains(sign);
        }

        public bool IsUnaryOperator(string sign)
        {
            return GetUnaryOperatorSigns().Contains(sign);
        }

        public bool IsOperator(string sign)
        {
            return GetOperatorSigns().Contains(sign);
        }
    }
}