namespace CmdLineParser.Internals.TypeParsers
{
    internal class TypeParserBoolean : ITypeParser<bool>
    {
        public bool CanParse(string value)
        {
            return bool.TryParse(value, out _);
        }

        public bool Parse(string value)
        {
            return bool.Parse(value);
        }
    }
}
