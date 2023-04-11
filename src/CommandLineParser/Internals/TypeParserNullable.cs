namespace CommandLineParser.Internals
{
    internal class TypeParserNullable<T> : ITypeParser<T?>
    {
        private ITypeParser<T>? _typeParser;

        public bool CanParse(string value)
        {
            return true;
        }

        public T? Parse(string value)
        {
            _typeParser ??= TypeParser.GetTypeParser<T>();
            if (_typeParser == null)
                return default;

            if (!_typeParser.CanParse(value))
                return default;

            return _typeParser.Parse(value);
        }
    }
}
