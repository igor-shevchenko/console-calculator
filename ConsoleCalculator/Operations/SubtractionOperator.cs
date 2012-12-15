namespace ConsoleCalculator.Operations
{
    public class SubtractionOperator : IBinaryOperator
    {
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