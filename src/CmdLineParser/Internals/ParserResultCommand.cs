using System;

namespace CmdLineParser.Internals
{
    internal abstract class ParserResultCommand : IParserResultCommand
    {
        private readonly ParserCommand _parserCommand;
        private readonly List<IParserOption> _remainingOptions = new List<IParserOption>();
        private readonly List<IParserArgument> _remainingArguments = new List<IParserArgument>();

        public string Command => _parserCommand.CommandName;

        protected ParserResultCommand(ParserCommand parserCommand)
        {
            _parserCommand = parserCommand;
            _remainingOptions = _parserCommand.Options.ToList();
            _remainingArguments = _parserCommand.Arguments.ToList();
        }

        public bool HandleParserAttribute(IParserAttribute attribute, string? value)
        {
            if (attribute is IParserOption option)
            {
                if (!_remainingOptions.Contains(option))
                    return false;
                _remainingOptions.Remove(option);
            }
            else if (attribute is IParserArgument argument)
            {
                if (!_remainingArguments.Contains(argument))
                    return false;
                _remainingArguments.Remove(argument);
            }
            attribute.ParseValue(this, value);
            return true;
        }

        public void HandleRemainingAttributes()
        {
            foreach (var option in _remainingOptions)
            {
                option.ParseValue(this, null);
            }
            foreach (var argument in _remainingArguments)
            {
                if (argument.IsRequired)
                {
                    throw new ParserException($"The required argument {argument.Name} was not set");
                }
                argument.ParseValue(this, null);
            }
        }

        public abstract T? GetObject<T>() where T : new();
    }

    internal class ParserResultCommand<TCommand> : ParserResultCommand, IParserResultCommand<TCommand>
    {
        private TCommand _object;

        public TCommand Object => _object;

        public ParserResultCommand(ParserCommand parserCommand, TCommand @object)
            : base(parserCommand)
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
