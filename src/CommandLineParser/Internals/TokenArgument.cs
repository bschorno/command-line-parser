namespace CommandLineParser.Internals
{
    internal sealed class TokenArgument : Token
    {
        private IParserArgument _argument;

        public IParserArgument Argument => _argument;

        public TokenArgument(IParserArgument argument, string value)
            : base(TokenType.Argument, value)
        {
            _argument = argument;
        }
    }
}
