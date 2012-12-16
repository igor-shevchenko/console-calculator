namespace ConsoleCalculator
{
    public interface IBracketSignDetector
    {
        bool IsOpeningBracket(string sign);
        bool IsClosingBracket(string sign);
        bool IsBracket(string sign);
    }
}