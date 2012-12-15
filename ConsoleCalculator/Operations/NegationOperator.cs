namespace ConsoleCalculator.Operations
{
    public class NegationOperator : IUnaryOperator
    {
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