namespace CmdLineParser
{
    public class ParserBuilderException : Exception
    {
        public ParserBuilderException(string message)
            : base(message)
        {

        }

        public ParserBuilderException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
