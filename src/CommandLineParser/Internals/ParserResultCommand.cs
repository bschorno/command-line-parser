namespace CommandLineParser.Internals
{
    internal class ParserResultCommand<TCommand> : IParserResultCommand<TCommand>
    {
        private readonly string _command;
        private TCommand _object;

        public TCommand Object => _object;

        public string Command => _command;

        public ParserResultCommand(string command, TCommand @object)
        {
            _command = command;
            _object = @object;
        }

        public T? GetObject<T>()
        {
            if (_object is T castedObject)
                return castedObject;
            return default;
        }
    }
}
