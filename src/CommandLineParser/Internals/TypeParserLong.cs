namespace CommandLineParser.Internals
{
    internal class TypeParserLong : ITypeParser<long>
    {
        public bool CanParse(string value)
        {
            return long.TryParse(value, out _);
        }

        public long Parse(string value)
        {
            return long.Parse(value);
        }
    }
}
