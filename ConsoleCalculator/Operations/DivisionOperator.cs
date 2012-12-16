namespace ConsoleCalculator.Operations
{
    public class DivisionOperator : IBinaryOperator
    {
        public string Sign
        {
            get { return "/"; }
        }

        public int Precedence
        {
            get { return 2; }
        }

        public double Apply(double arg1, double arg2)
        {
            return arg1 / arg2;
        }
    }
}