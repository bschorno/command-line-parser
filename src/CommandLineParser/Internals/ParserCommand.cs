using System.Linq.Expressions;
using System.Reflection;

namespace CmdLineParser.Internals
{
    internal abstract class ParserCommand : IParserCommand
    {
        protected readonly string _commandName;
        protected readonly List<IParserCommand> _commands = new List<IParserCommand>();
        protected readonly List<IParserOption> _options = new List<IParserOption>();
        protected readonly List<IParserArgument> _arguments = new List<IParserArgument>();

        public string CommandName => _commandName;

        public IReadOnlyList<IParserCommand> Commands => _commands;

        public IReadOnlyList<IParserArgument> Arguments => _arguments;

        public IReadOnlyList<IParserOption> Options => _options;

        public ParserCommand(string commandName)
        {
            _commandName = commandName;
        }

        public abstract IParserResultCommand CreateParserResultCommand(IParserResultCommand? parentResultCommand);
    }

    internal class ParserCommand<TCommand> : ParserCommand, IParserCommand<TCommand> where TCommand : new()
    {
        protected Action<TCommand>? _callback;

        public ParserCommand(string commandName)
            : base(commandName)
        {

        }

        public IParserCommand<TCommand> Callback(Action<TCommand> callback)
        {
            _callback = callback;
            return this;
        }

        public IParserCommand<TCommand> AddCommand(string commandName)
        {
            var command = new ParserCommand<TCommand>(commandName);
            _commands.Add(command);
            return command;
        }

        public IParserCommand<TCommand> AddCommand(string commandName, Action<IParserCommand<TCommand>>? buildingAction)
        {
            var command = AddCommand(commandName);
            buildingAction?.Invoke(command);
            return command;
        }

        public IParserCommand<TSubCommand> AddCommand<TSubCommand>(string commandName, Expression<Func<TCommand, TSubCommand>> propertySelector) where TSubCommand : new()
        {
            var command = new ParserCommand<TCommand, TSubCommand>(commandName, propertySelector);
            _commands.Add(command);
            return command;
        }

        public IParserCommand<TSubCommand> AddCommand<TSubCommand>(string commandName, Expression<Func<TCommand, TSubCommand>> propertySelector, Action<IParserCommand<TSubCommand>>? buildingAction) where TSubCommand : new()
        {
            var command = AddCommand(commandName, propertySelector);
            buildingAction?.Invoke(command);
            return command;
        }

        public IParserOption<K> AddOption<K>(char shortName, string longName, Expression<Func<TCommand, K>> propertySelector)
        {
            var option = new ParserOption<TCommand, K>(shortName, longName, propertySelector);
            _options.Add(option);
            return option;
        }

        public IParserOption<K> AddOption<K>(char shortName, string longName, Expression<Func<TCommand, K>> propertySelector, Action<IParserOption<K>>? buildingAction)
        {
            var option = AddOption(shortName, longName, propertySelector);
            buildingAction?.Invoke(option);
            return option;
        }

        public IParserArgument<K> AddArgument<K>(Expression<Func<TCommand, K>> propertySelector)
        {
            var argument = new ParserArgument<TCommand, K>(propertySelector);
            _arguments.Add(argument);
            return argument;
        }

        public IParserArgument<K> AddArgument<K>(Expression<Func<TCommand, K>> propertySelector, Action<IParserArgument<K>>? buildingAction)
        {
            var argument = AddArgument(propertySelector);
            buildingAction?.Invoke(argument);
            return argument;
        }

        public override IParserResultCommand CreateParserResultCommand(IParserResultCommand? parentResultCommand)
        {
            return new ParserResultCommand<TCommand>(_commandName, new TCommand(), _callback);
        }
    }

    internal class ParserCommand<TParentCommand, TCommand> : ParserCommand<TCommand> where TCommand : new() where TParentCommand : new()
    {
        private readonly Expression<Func<TParentCommand, TCommand>> _propertySelector;

        public ParserCommand(string commandName, Expression<Func<TParentCommand, TCommand>> propertySelector)
            : base(commandName)
        {
            _propertySelector = propertySelector;
        }

        public override IParserResultCommand CreateParserResultCommand(IParserResultCommand? parentResultCommand)
        {
            if (parentResultCommand == null)
                return base.CreateParserResultCommand(parentResultCommand);

            var propertyInfo = (PropertyInfo)((MemberExpression)_propertySelector.Body).Member;
            var objectReference = new TCommand();
            propertyInfo.SetValue(parentResultCommand.GetObject<TParentCommand>(), objectReference, null);

            return new ParserResultCommand<TCommand>(_commandName, objectReference, _callback);
        }
    }
}
