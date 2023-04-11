namespace CommandLineParser.Internals
{
    internal static class TypeParser
    {
        private static readonly Dictionary<Type, ITypeParser> _typeParsers = new Dictionary<Type, Internals.ITypeParser>();

        static TypeParser()
        {

        }
    }
}
