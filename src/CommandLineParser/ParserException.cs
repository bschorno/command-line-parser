namespace CmdLineParser
{
    [Serializable]
    public class ParserException : Exception
    {
        internal ParserException()
            : base()
        {

        }

        internal ParserException(string message)
            : base(message)
        {

        }

        internal ParserException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
