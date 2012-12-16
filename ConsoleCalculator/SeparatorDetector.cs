namespace ConsoleCalculator
{
    class SeparatorDetector : ISeparatorDetector
    {
        private readonly IBracketSignDetector bracketSignDetector;
        private readonly IOperationSignDetector operationSignDetector;

        public SeparatorDetector(IBracketSignDetector bracketSignDetector, IOperationSignDetector operationSignDetector)
        {
            this.bracketSignDetector = bracketSignDetector;
            this.operationSignDetector = operationSignDetector;
        }

        public bool IsSeparator(string sign)
        {
            return bracketSignDetector.IsBracket(sign) || operationSignDetector.IsOperator(sign);
        }
    }
}