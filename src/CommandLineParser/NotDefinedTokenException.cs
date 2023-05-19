using CmdLineParser.Internals;

namespace CmdLineParser
{
    public class NotDefinedTokenException : ParserException
    {
        private readonly Token _token;

        internal Token Token => _token;

        public override string Message
        {
            get
            {
                return $"The argument {_token.Value} could not be assigned unambiguosusly or is not defined";
            }
        }

        internal NotDefinedTokenException(Token token)
            : base()
        {
            _token = token;
        }
    }
}
