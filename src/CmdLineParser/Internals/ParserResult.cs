using CmdLineParser;

namespace CmdLineParser.Internals
{
    internal class ParserResult : IParserResult
    {
        private readonly List<ParserResultCommand> _commands = new List<ParserResultCommand>();

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
            
            parentResultCommand.HandleParserAttribute(attribute, value);
        }

        public IParserResultCommand GetParserResultCommand(int level)
        {
            return _commands[level];
        }

        public void FinalizeCommands()
        {
            foreach (var command in _commands) 
            {
                command.HandleRemainingAttributes();
            }
        }
    }
}
