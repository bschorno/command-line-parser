namespace CommandLineParser.Internals.TypeParsers
{
    internal class TypeParserShort : ITypeParser<short>
    {
        public bool CanParse(string value)
        {
            return short.TryParse(value, out _);
        }

        public short Parse(string value)
        {
            return short.Parse(value);
        }
    }
}
