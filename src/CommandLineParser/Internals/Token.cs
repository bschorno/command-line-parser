namespace CmdLineParser.Internals
{
    internal class Token
    {
        private TokenType _type;
        private string? _value;

        public TokenType Type => _type;

        public string? Value => _value;

        public Token(TokenType type)
        {
            _type = type;
        }

        public Token(TokenType type, string value)
        {
            _type = type;
            _value = value;
        }
    }
}
