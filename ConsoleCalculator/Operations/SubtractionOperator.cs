using ConsoleCalculator.OperatorContracts;

namespace ConsoleCalculator.Operations
{
    public class SubtractionOperator : IBinaryOperator
    {
        public string Sign
        {
            get { return "-"; }
        }

        public int Precedence
        {
            get { return 1; }
        }

        public double Apply(double arg1, double arg2)
        {
            return arg1 - arg2;
        }
    }
}