namespace CmdLineParser.Internals
{
    internal sealed class TokenValue : Token
    {
        public TokenValue(string value)
            : base(TokenType.Value, value)
        {

        }
    }
}
