namespace CmdLineParser.Internals
{
    internal sealed class TokenCommand : Token
    {
        private IParserCommand _command;

        public IParserCommand Command => _command;

        public TokenCommand(IParserCommand command, string value)
            : base(TokenType.Command, value)
        {
            _command = command;
        }
    }
}
