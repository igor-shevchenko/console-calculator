namespace ConsoleCalculator.Operations
{
    public class MultiplicationOperator : IBinaryOperator
    {
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