namespace CmdLineParser.Internals
{
    internal sealed class TokenOption : Token
    {
        private IParserOption _option;

        public IParserOption Option => _option;

        public TokenOption(IParserOption option, char value)
            : base(TokenType.ShortOption, value.ToString())
        {
            _option = option;
        }

        public TokenOption(IParserOption option, string value)
            : base(TokenType.LongOption, value)
        {
            _option = option;
        }
    }
}
