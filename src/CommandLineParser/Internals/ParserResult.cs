
namespace CommandLineParser.Internals
{
    internal class ParserResult : IParserResult
    {
        private readonly List<IParserResultCommand> _commands = new List<IParserResultCommand>();

        public bool HasErrors { get; internal set; }

        public IEnumerable<IParserResultCommand> Commands => _commands;

        public ParserResult()
        {

        }

        public void AddCommand(IParserCommand parserCommand)
        {
            _commands.Add(parserCommand.CreateParserResultCommand(GetTopParserResultCommand()));
        }

        public void AddAttribute(IParserAttribute attribute, string? value)
        {
            var parentResultCommand = GetTopParserResultCommand();
            if (parentResultCommand == null)
                return;
            attribute.ParseValue(parentResultCommand, value);
        }

        private IParserResultCommand? GetTopParserResultCommand()
        {
            return _commands.Count > 0 ? _commands[_commands.Count - 1] : null;
        }
    }
}
