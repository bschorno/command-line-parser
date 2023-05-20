namespace CmdLineParser.Internals
{
    internal abstract class ParserResultCommand : IParserResultCommand
    {
        private readonly string _command;

        public string Command => _command;

        protected ParserResultCommand(string command)
        {
            _command = command;
        }

        public abstract T? GetObject<T>() where T : new();
    }

    internal class ParserResultCommand<TCommand> : ParserResultCommand, IParserResultCommand<TCommand>
    {
        private TCommand _object;

        public TCommand Object => _object;

        public ParserResultCommand(string command, TCommand @object)
            : base(command)
        {
            _object = @object;
        }

        public override T GetObject<T>()
        {
            if (_object is T castedObject)
                return castedObject;
            return new T();
        }
    }
}
