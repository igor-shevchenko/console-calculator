using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleCalculator.OperatorContracts;

namespace ConsoleCalculator.Operations
{
    public class OperatorListFactory : IOperatorListFactory
    {
        private IList<IBinaryOperator> cachedBinaryOperators;
        private IList<IUnaryOperator> cachedUnaryOperators;

        public IList<IBinaryOperator> GetBinaryOperators()
        {
            if (cachedBinaryOperators != null)
                return cachedBinaryOperators;

            cachedBinaryOperators = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => typeof (IBinaryOperator).IsAssignableFrom(t) && !t.IsInterface)
                .Select(t => (IBinaryOperator) Activator.CreateInstance(t))
                .ToList();
            return cachedBinaryOperators;
        }

        public IList<IUnaryOperator> GetUnaryOperators()
        {
            if (cachedUnaryOperators != null)
                return cachedUnaryOperators;

            cachedUnaryOperators = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => typeof(IUnaryOperator).IsAssignableFrom(t) && !t.IsInterface)
                .Select(t => (IUnaryOperator)Activator.CreateInstance(t))
                .ToList();
            return cachedUnaryOperators;
        } 
    }
}