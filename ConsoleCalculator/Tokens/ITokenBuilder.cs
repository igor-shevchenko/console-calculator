namespace ConsoleCalculator.Tokens
{
    public interface ITokenBuilder
    {
        Token Build(string token, TokenType type);
    }
}