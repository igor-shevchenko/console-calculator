namespace ConsoleCalculator
{
    class BracketSignProvider : IBracketSignProvider
    {
        public bool IsOpeningBracket(string sign)
        {
            return sign == "(";
        }

        public bool IsClosingBracket(string sign)
        {
            return sign == ")";
        }

        public bool IsBracket(string sign)
        {
            return IsClosingBracket(sign) || IsOpeningBracket(sign);
        }
    }
}