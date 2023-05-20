using CmdLineParser;

namespace CmdLineParser.Internals
{
    internal class ParserResult : IParserResult
    {
        private readonly List<IParserResultCommand> _commands = new List<IParserResultCommand>();

        public IEnumerable<IParserResultCommand> Commands => _commands;

        public ParserResult()
        {

        }

        public void AddCommand(IParserCommand parserCommand)
        {
            _commands.Add(parserCommand.CreateParserResultCommand(this));
        }

        public void AddAttribute(IParserAttribute attribute, string? value)
        {
            var parentResultCommand = _commands.Last();
            if (parentResultCommand == null)
                return;
            attribute.ParseValue(parentResultCommand, value);
        }

        internal IParserResultCommand GetParserResultCommand(int level)
        {
            return _commands[level];
        }
    }
}
