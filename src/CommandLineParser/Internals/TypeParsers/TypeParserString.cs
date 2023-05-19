namespace CmdLineParser.Internals.TypeParsers
{
    internal class TypeParserString : ITypeParser<string>
    {
        public bool CanParse(string value)
        {
            return true;
        }

        public string Parse(string value)
        {
            return value;
        }
    }
}
