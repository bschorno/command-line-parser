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

        public abstract ParserResultCommand CreateParserResultCommand(ParserResult parserResult);

        public abstract void InvokeCallback(ParserResult parserResult);
    }

    internal class ParserCommand<T1> : ParserCommand, IParserCommand<T1> where T1 : new()
    {
        protected Action<T1>? _callback;

        public ParserCommand(string commandName)
            : base(commandName)
        {

        }

        public IParserCommand<T1> Callback(Action<T1> callback)
        {
            _callback = callback;
            return this;
        }

        public IParserCommand<T1> AddCommand(string commandName)
        {
            var command = new ParserCommand<T1>(commandName);
            _commands.Add(command);
            return command;
        }

        public IParserCommand<T1> AddCommand(string commandName, Action<IParserCommand<T1>>? buildingAction)
        {
            var command = AddCommand(commandName);
            buildingAction?.Invoke(command);
            return command;
        }

        public IParserCommand<T1, K> AddCommand<K>(string commandName, Expression<Func<T1, K>> propertySelector) where K : new()
        {
            var command = new ParserCommand<T1, K>(commandName, propertySelector);
            _commands.Add(command);
            return command;
        }

        public IParserCommand<T1, K> AddCommand<K>(string commandName, Expression<Func<T1, K>> propertySelector, Action<IParserCommand<T1, K>>? buildingAction) where K : new()
        {
            var command = AddCommand(commandName, propertySelector);
            buildingAction?.Invoke(command);
            return command;
        }

        public IParserOption<K> AddOption<K>(char shortName, string longName, Expression<Func<T1, K>> propertySelector)
        {
            var option = new ParserOption<T1, K>(shortName, longName, propertySelector);
            _options.Add(option);
            return option;
        }

        public IParserOption<K> AddOption<K>(char shortName, string longName, Expression<Func<T1, K>> propertySelector, Action<IParserOption<K>>? buildingAction)
        {
            var option = AddOption(shortName, longName, propertySelector);
            buildingAction?.Invoke(option);
            return option;
        }

        public IParserArgument<K> AddArgument<K>(string name, Expression<Func<T1, K>> propertySelector)
        {
            var argument = new ParserArgument<T1, K>(name, propertySelector);
            _arguments.Add(argument);
            return argument;
        }

        public IParserArgument<K> AddArgument<K>(string name, Expression<Func<T1, K>> propertySelector, Action<IParserArgument<K>>? buildingAction)
        {
            var argument = AddArgument(name, propertySelector);
            buildingAction?.Invoke(argument);
            return argument;
        }

        public override ParserResultCommand CreateParserResultCommand(ParserResult parserResult)
        {
            var objectReference = new T1();

            return new ParserResultCommand<T1>(this, objectReference);
        }

        public override void InvokeCallback(ParserResult parserResult)
        {
            if (_callback == null)
                return;

            T1 parameter1 = parserResult.GetParserResultCommand(0).GetObject<T1>()!;
            _callback.Invoke(parameter1);
        }
    }

    internal class ParserCommand<T1, T2> : ParserCommand, IParserCommand<T1, T2> where T1 : new() where T2 : new() 
    {
        private readonly Expression<Func<T1, T2>> _propertySelector;
        protected Action<T1, T2>? _callback;

        public ParserCommand(string commandName, Expression<Func<T1, T2>> propertySelector)
            : base(commandName)
        {
            _propertySelector = propertySelector;
        }

        public IParserCommand<T1, T2> Callback(Action<T1, T2> callback)
        {
            _callback = callback;
            return this;
        }

        public IParserCommand<T1, T2> AddCommand(string commandName)
        {
            var command = new ParserCommand<T1, T2>(commandName, _propertySelector);
            _commands.Add(command);
            return command;
        }

        public IParserCommand<T1, T2> AddCommand(string commandName, Action<IParserCommand<T1, T2>>? buildingAction)
        {
            var command = AddCommand(commandName);
            buildingAction?.Invoke(command);
            return command;
        }

        public IParserCommand<T1, T2, K> AddCommand<K>(string commandName, Expression<Func<T2, K>> propertySelector) where K : new()
        {
            var command = new ParserCommand<T1, T2, K>(commandName, propertySelector);
            _commands.Add(command);
            return command;
        }

        public IParserCommand<T1, T2, K> AddCommand<K>(string commandName, Expression<Func<T2, K>> propertySelector, Action<IParserCommand<T1, T2, K>>? buildingAction) where K : new()
        {
            var command = AddCommand(commandName, propertySelector);
            buildingAction?.Invoke(command);
            return command;
        }

        public IParserOption<K> AddOption<K>(char shortName, string longName, Expression<Func<T2, K>> propertySelector)
        {
            var option = new ParserOption<T2, K>(shortName, longName, propertySelector);
            _options.Add(option);
            return option;
        }

        public IParserOption<K> AddOption<K>(char shortName, string longName, Expression<Func<T2, K>> propertySelector, Action<IParserOption<K>>? buildingAction)
        {
            var option = AddOption(shortName, longName, propertySelector);
            buildingAction?.Invoke(option);
            return option;
        }

        public IParserArgument<K> AddArgument<K>(string name, Expression<Func<T2, K>> propertySelector)
        {
            var argument = new ParserArgument<T2, K>(name, propertySelector);
            _arguments.Add(argument);
            return argument;
        }

        public IParserArgument<K> AddArgument<K>(string name, Expression<Func<T2, K>> propertySelector, Action<IParserArgument<K>>? buildingAction)
        {
            var argument = AddArgument(name, propertySelector);
            buildingAction?.Invoke(argument);
            return argument;
        }

        public override ParserResultCommand CreateParserResultCommand(ParserResult parserResult)
        {
            var propertyInfo = (PropertyInfo)((MemberExpression)_propertySelector.Body).Member;
            var objectReference = new T2();
            var parentReference = parserResult.GetParserResultCommand(0).GetObject<T1>()!;
            propertyInfo.SetValue(parentReference, objectReference, null);

            return new ParserResultCommand<T2>(this, objectReference);
        }

        public override void InvokeCallback(ParserResult parserResult)
        {
            if (_callback == null)
                return;

            T1 parameter1 = parserResult.GetParserResultCommand(0).GetObject<T1>()!;
            T2 parameter2 = parserResult.GetParserResultCommand(1).GetObject<T2>()!;
            _callback.Invoke(parameter1, parameter2);
        }
    }

    internal class ParserCommand<T1, T2, T3> : ParserCommand, IParserCommand<T1, T2, T3> where T1 : new() where T2 : new() where T3 : new()
    {
        private readonly Expression<Func<T2, T3>> _propertySelector;
        protected Action<T1, T2, T3>? _callback;

        public ParserCommand(string commandName, Expression<Func<T2, T3>> propertySelector)
            : base(commandName)
        {
            _propertySelector = propertySelector;
        }

        public IParserCommand<T1, T2, T3> Callback(Action<T1, T2, T3> callback)
        {
            _callback = callback;
            return this;
        }

        public IParserCommand<T1, T2, T3> AddCommand(string commandName)
        {
            var command = new ParserCommand<T1, T2, T3>(commandName, _propertySelector);
            _commands.Add(command);
            return command;
        }

        public IParserCommand<T1, T2, T3> AddCommand(string commandName, Action<IParserCommand<T1, T2, T3>>? buildingAction)
        {
            var command = AddCommand(commandName);
            buildingAction?.Invoke(command);
            return command;
        }

        public IParserCommand<T1, T2, T3, K> AddCommand<K>(string commandName, Expression<Func<T3, K>> propertySelector) where K : new()
        {
            var command = new ParserCommand<T1, T2, T3, K>(commandName, propertySelector);
            _commands.Add(command);
            return command;
        }

        public IParserCommand<T1, T2, T3, K> AddCommand<K>(string commandName, Expression<Func<T3, K>> propertySelector, Action<IParserCommand<T1, T2, T3, K>>? buildingAction) where K : new()
        {
            var command = AddCommand(commandName, propertySelector);
            buildingAction?.Invoke(command);
            return command;
        }

        public IParserOption<K> AddOption<K>(char shortName, string longName, Expression<Func<T3, K>> propertySelector)
        {
            var option = new ParserOption<T3, K>(shortName, longName, propertySelector);
            _options.Add(option);
            return option;
        }

        public IParserOption<K> AddOption<K>(char shortName, string longName, Expression<Func<T3, K>> propertySelector, Action<IParserOption<K>>? buildingAction)
        {
            var option = AddOption(shortName, longName, propertySelector);
            buildingAction?.Invoke(option);
            return option;
        }

        public IParserArgument<K> AddArgument<K>(string name, Expression<Func<T3, K>> propertySelector)
        {
            var argument = new ParserArgument<T3, K>(name, propertySelector);
            _arguments.Add(argument);
            return argument;
        }

        public IParserArgument<K> AddArgument<K>(string name, Expression<Func<T3, K>> propertySelector, Action<IParserArgument<K>>? buildingAction)
        {
            var argument = AddArgument(name, propertySelector);
            buildingAction?.Invoke(argument);
            return argument;
        }

        public override ParserResultCommand CreateParserResultCommand(ParserResult parserResult)
        {
            var propertyInfo = (PropertyInfo)((MemberExpression)_propertySelector.Body).Member;
            var objectReference = new T3();
            var parentReference = parserResult.GetParserResultCommand(1).GetObject<T2>()!;
            propertyInfo.SetValue(parentReference, objectReference, null);

            return new ParserResultCommand<T3>(this, objectReference);
        }

        public override void InvokeCallback(ParserResult parserResult)
        {
            if (_callback == null)
                return;

            T1 parameter1 = parserResult.GetParserResultCommand(0).GetObject<T1>()!;
            T2 parameter2 = parserResult.GetParserResultCommand(1).GetObject<T2>()!;
            T3 parameter3 = parserResult.GetParserResultCommand(2).GetObject<T3>()!;
            _callback.Invoke(parameter1, parameter2, parameter3);
        }
    }

    internal class ParserCommand<T1, T2, T3, T4> : ParserCommand, IParserCommand<T1, T2, T3, T4> where T1 : new() where T2 : new() where T3 : new() where T4 : new()
    {
        private readonly Expression<Func<T3, T4>> _propertySelector;
        protected Action<T1, T2, T3, T4>? _callback;

        public ParserCommand(string commandName, Expression<Func<T3, T4>> propertySelector)
            : base(commandName)
        {
            _propertySelector = propertySelector;
        }

        public IParserCommand<T1, T2, T3, T4> Callback(Action<T1, T2, T3, T4> callback)
        {
            _callback = callback;
            return this;
        }

         public IParserOption<K> AddOption<K>(char shortName, string longName, Expression<Func<T4, K>> propertySelector) 
        {
            var option = new ParserOption<T4, K>(shortName, longName, propertySelector);
            _options.Add(option);
            return option;
        }

        public IParserOption<K> AddOption<K>(char shortName, string longName, Expression<Func<T4, K>> propertySelector, Action<IParserOption<K>>? buildingAction)
        {
            var option = AddOption(shortName, longName, propertySelector);
            buildingAction?.Invoke(option);
            return option;
        }

        public IParserArgument<K> AddArgument<K>(string name, Expression<Func<T4, K>> propertySelector)
        {
            var argument = new ParserArgument<T4, K>(name, propertySelector);
            _arguments.Add(argument);
            return argument;
        }

        public IParserArgument<K> AddArgument<K>(string name, Expression<Func<T4, K>> propertySelector, Action<IParserArgument<K>>? buildingAction)
        {
            var argument = AddArgument(name, propertySelector);
            buildingAction?.Invoke(argument);
            return argument;
        }

        public override ParserResultCommand CreateParserResultCommand(ParserResult parserResult)
        {
            var propertyInfo = (PropertyInfo)((MemberExpression)_propertySelector.Body).Member;
            var objectReference = new T4();
            var parentReference = parserResult.GetParserResultCommand(2).GetObject<T3>()!;
            propertyInfo.SetValue(parentReference, objectReference, null);

            return new ParserResultCommand<T4>(this, objectReference);
        }

        public override void InvokeCallback(ParserResult parserResult)
        {
            if (_callback == null)
                return;

            T1 parameter1 = parserResult.GetParserResultCommand(0).GetObject<T1>()!;
            T2 parameter2 = parserResult.GetParserResultCommand(1).GetObject<T2>()!;
            T3 parameter3 = parserResult.GetParserResultCommand(2).GetObject<T3>()!;
            T4 parameter4 = parserResult.GetParserResultCommand(3).GetObject<T4>()!;
            _callback.Invoke(parameter1, parameter2, parameter3, parameter4);
        }
    }
}
