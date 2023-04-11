using System.Linq.Expressions;

namespace CommandLineParser.Internals
{
    internal class ParserBuilder : IParserBuilder
    {
        private readonly List<IParserCommand> _commands = new List<IParserCommand>();

        internal IReadOnlyList<IParserCommand> Commands => _commands;

        public IParserCommand<K> AddCommand<K>(string commandName) where K : new()
        {
            var command = new ParserCommand<K>(commandName);
            _commands.Add(command);
            return command;
        }

        public IParserCommand<K> AddCommand<K>(string commandName, Action<IParserCommand<K>>? buildingAction) where K : new()
        {
            var command = AddCommand<K>(commandName);
            buildingAction?.Invoke(command);
            return command;
        }

        public IParser Build()
        {
            return new Parser(this);
        }
    }
}
