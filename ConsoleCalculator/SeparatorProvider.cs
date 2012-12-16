namespace ConsoleCalculator
{
    class SeparatorProvider : ISeparatorProvider
    {
        private readonly IBracketSignProvider bracketSignProvider;
        private readonly IOperationSignProvider operationSignProvider;

        public SeparatorProvider(IBracketSignProvider bracketSignProvider, IOperationSignProvider operationSignProvider)
        {
            this.bracketSignProvider = bracketSignProvider;
            this.operationSignProvider = operationSignProvider;
        }

        public bool IsSeparator(string sign)
        {
            return bracketSignProvider.IsBracket(sign) || operationSignProvider.IsOperator(sign);
        }
    }
}