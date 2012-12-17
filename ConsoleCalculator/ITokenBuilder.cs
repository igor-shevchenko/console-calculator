namespace ConsoleCalculator
{
    public interface ITokenBuilder
    {
        Token Build(string token, TokenType type);
    }
}