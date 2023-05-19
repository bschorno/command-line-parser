using CmdLineParser.Internals.TypeParsers;

namespace CmdLineParser.Internals
{
    internal static class TypeParser
    {
        private static readonly Dictionary<Type, ITypeParser> _typeParsers = new Dictionary<Type, ITypeParser>();

        static TypeParser()
        {
            _typeParsers.Add(typeof(string), new TypeParserString());
            _typeParsers.Add(typeof(bool), new TypeParserBoolean());
            _typeParsers.Add(typeof(bool?), new TypeParserNullable<bool>());
            _typeParsers.Add(typeof(short), new TypeParserInt());
            _typeParsers.Add(typeof(short?), new TypeParserNullable<short>());
            _typeParsers.Add(typeof(int), new TypeParserInt());
            _typeParsers.Add(typeof(int?), new TypeParserNullable<int>());
            _typeParsers.Add(typeof(long), new TypeParserInt());
            _typeParsers.Add(typeof(long?), new TypeParserNullable<long>());
        }

        public static ITypeParser<T>? GetTypeParser<T>()
        {
            if (_typeParsers.TryGetValue(typeof(T), out var typeParser))
                return (ITypeParser<T>)typeParser;
            return default;
        }
    }
}
