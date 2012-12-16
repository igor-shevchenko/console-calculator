using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleCalculator.Operations;

namespace ConsoleCalculator
{
    public class OperatorFactory : IOperatorFactory
    {
        private readonly IOperatorListFactory factory;

        public OperatorFactory(IOperatorListFactory factory)
        {
            this.factory = factory;
        }

        public IBinaryOperator GetBinaryOperator(string sign)
        {
            return factory.GetBinaryOperators().Single(o => o.Sign == sign);
        }
        
        public IUnaryOperator GetUnaryOperator(string sign)
        {
            return factory.GetUnaryOperators().Single(o => o.Sign == sign);
        }
    }

}