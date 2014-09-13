using ConsoleCalculator.OperatorContracts;

namespace ConsoleCalculator.MyOperators
{
    public class MultiplicationOperator : IBinaryOperator
    {
        public string Sign
        {
            get { return "*"; }
        }

        public int Precedence
        {
            get { return 2; }
        }

        public double Apply(double arg1, double arg2)
        {
            return arg1 * arg2;
        }
    }
}