namespace ConsoleCalculator
{
    public interface IBracketSignProvider
    {
        bool IsOpeningBracket(string sign);
        bool IsClosingBracket(string sign);
        bool IsBracket(string sign);
    }
}