using ConsoleCalculator.OperatorContracts;

namespace ConsoleCalculator.Operations
{
    public class NegationOperator : IUnaryOperator
    {
        public string Sign
        {
            get { return "-"; }
        }

        public int Precedence
        {
            get { return 10; }
        }

        public double Apply(double arg)
        {
            return -arg;
        }
    }
}