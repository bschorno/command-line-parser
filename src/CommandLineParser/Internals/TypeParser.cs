namespace CommandLineParser.Internals
{
    internal static class TypeParser
    {
        private static readonly Dictionary<Type, ITypeParser> _typeParsers = new Dictionary<Type, Internals.ITypeParser>();

        static TypeParser()
        {
            _typeParsers.Add(typeof(string), new TypeParserString());
            _typeParsers.Add(typeof(bool), new TypeParserBoolean());
            _typeParsers.Add(typeof(bool?), new TypeParserNullable<bool>());
        }

        public static ITypeParser<T>? GetTypeParser<T>()
        {
            if (_typeParsers.TryGetValue(typeof(T), out var typeParser))
                return (ITypeParser<T>)typeParser;
            return default;
        }
    }
}
