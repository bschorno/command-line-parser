namespace CommandLineParser.Internals
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

        internal abstract void InvokeCallback();
    }

    internal class ParserResultCommand<TCommand> : ParserResultCommand, IParserResultCommand<TCommand>
    {
        
        private Action<TCommand>? _callback;
        private TCommand _object;

        public TCommand Object => _object;

        internal Action<TCommand>? Callback => _callback;

        public ParserResultCommand(string command, TCommand @object, Action<TCommand>? callback)
            : base(command)
        {
            _object = @object;
            _callback = callback;
        }

        public override T GetObject<T>()
        {
            if (_object is T castedObject)
                return castedObject;
            return new T();
        }

        internal override void InvokeCallback()
        {
            if (_callback == null)
                return;

            _callback.Invoke(_object);
        }
    }
}
